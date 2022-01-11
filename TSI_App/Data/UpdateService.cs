using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TSI_App.JSON;
using TSI_App.Database;
using System.Net.Http;
using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;

namespace TSI_App.Data
{
    public class UpdateService : IHostedService, IDisposable
    {
        private readonly ILogger _log;

        const string TWITCH_CLIENT_ID = "oto5dc63lgjgznlexsg3pwqamfus6o";
        const string TWITCH_CLIENT_SECRET = "1wozpbw034synjjdx64zu6nfzs8mnm";
        const string SPOTIFY_CLIENT_ID = "1a4dc6b858f844e386a120a023479599";
        const string SPOTIFY_CLIENT_SECRET = "75d28f9e70f445bb9555ccb3bffd45e6";


        private Timer _timer; //Calls the update method
        private Timer _refreshSpotifyTimer; //Refreshes Spotify tokens
        private Timer _refreshTwitchTimer; //Refreshes Twitch token
        private TwitchTokenResponse TwitchToken;
        private List<Streamer> LiveStreams = new List<Streamer>();
        private HashSet<string> NameContainer = new HashSet<string>(); //Contains the name of all active streams for quick searching
        private SongResponse _offline = new SongResponse(0, false, null);

        public UpdateService(ILogger<UpdateService> log)
        {
            _log = log;
        }

        public void Dispose()
        {
            _timer.Dispose();
        }

        public Task StartAsync(CancellationToken cancellationToken)
        {
            _log.LogInformation("UpdateService:: UpdateService is Starting");
            _timer = new Timer(UpdateAsync, null, TimeSpan.FromSeconds(10), TimeSpan.FromSeconds(10));
            _refreshSpotifyTimer = new Timer(RefreshSpotifyTokens, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(3500));
            _refreshTwitchTimer = new Timer(GetTwitchToken, null, TimeSpan.FromSeconds(0), TimeSpan.FromSeconds(3600));

            return Task.CompletedTask;
        }

        public Task StopAsync(CancellationToken cancellationToken)
        {
            _log.LogInformation("UpdateService:: UpdateService is Stopping");
            _timer?.Change(Timeout.Infinite, 0);
            return Task.CompletedTask;
        }

        //The main method which processes just about everything. Everything else is more or less a helper method
        private async void UpdateAsync(object state)
        {
            _log.LogInformation("UpdateService:: Entering ");

            //Query every user in the database and send them off in groups of 100 or less
            //to see if they are live
            using (var db = new TSI_DbContext())
            {
                var userQuery = from b in db.UserInfo
                                where b.ID > 0
                                select b;
                var userResponse = userQuery.ToList();

                /**
                 * requestUsers is a string I format to be passed to the twitch api of all the users I want to check if they are live or not
                 * count keeps track of how many users I have assigned so I know when to send it to QueryLiveUsersAsync
                 */
                string requestUsers = "";
                int count = 0;

                if (userResponse.Count > 0 )
                {
                    //The first part of the string does not contain an '&' so it is formatted seperately
                    requestUsers += "user_login=" + userResponse[0].TwitchUserLogin;
                    count++;

                    for (int ii = 1; ii < userResponse.Count; ii++)
                    {
                        //If we haven't assembled 100 users we append the string
                        if (count < 100)
                        {
                            requestUsers += "&user_login=" + userResponse[ii].TwitchUserLogin;
                            count++;
                        }
                        //Otherwise we need to send off the users and ids and reset the list to continue on
                        else
                        {
                            QueryLiveUsersAsync(requestUsers);
                            requestUsers = "user_login=" + userResponse[ii].TwitchUserLogin;
                            count = 1;
                        }
                    }
                    //Query the last batch which is less than 100
                    if (!String.IsNullOrWhiteSpace(requestUsers))
                        QueryLiveUsersAsync(requestUsers);
                }

                db.Dispose();
            }

            //Update which streams are live, then updates their song and viewers
             for (int ii = 0; ii < LiveStreams.Count; ii++)
             {
                Streamer sr = LiveStreams[ii];
                if (IsLive(sr.stream.user_name) != null)
                {
                    //Streamer is live and now we need to update their song
                    using (var db = new TSI_DbContext())
                    {
                        //Get the user info in order to access Spotify token
                        var userQuery = from b in db.UserInfo
                                        where b.ID == sr.ID
                                        select b;
                        var userResponse = userQuery.ToList();

                        SongResponse currentSong = null;
                        //Spotify API request
                        try
                        {
                            using (var httpClient = new HttpClient())
                            {
                                using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.spotify.com/v1/me/player/currently-playing"))
                                {
                                    request.Headers.TryAddWithoutValidation("Accept", "application/json");
                                    request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + userResponse[0].SpotifyToken);

                                    var response = await httpClient.SendAsync(request);
                                    string responseAsString = await response.Content.ReadAsStringAsync();
                                    
                                    currentSong = JsonConvert.DeserializeObject<SongResponse>(responseAsString);
                                }
                            }
                        }
                        catch (Exception e)
                        {
                            _log.LogError("Error requesting Spotify song: {0}", e.Message);
                            //If we don't get a response we can't log the song anymore
                        }

                        SaveSong(sr, currentSong);

                        db.Dispose();
                    }
                }
                //Streamer is not live, remove them from LiveStreams
                else
                {
                    LiveStreams.Remove(sr);
                    NameContainer.Remove(sr.stream.user_name);
                    SaveSong(sr, _offline);
                }
            }

            _log.LogInformation("UpdateService:: Exiting ");
        }

        private async void RefreshSpotifyTokens(object state)
        {
            using(var db = new TSI_DbContext())
            {
                var userQuery = from b in db.UserInfo
                            where b.ID > 0
                            select b;
                var userResponse = userQuery.ToList();

                foreach( UserInfo u in userResponse )
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://accounts.spotify.com/api/token"))
                        {
                            request.Headers.TryAddWithoutValidation("Authorization", "Basic " + StringToB64(SPOTIFY_CLIENT_ID + ":" + SPOTIFY_CLIENT_SECRET));

                            var contentList = new List<string>();
                            contentList.Add("grant_type=refresh_token");
                            contentList.Add("refresh_token=" + u.SpotifyRefreshToken);
                            request.Content = new StringContent(string.Join("&", contentList));
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                            var response = await httpClient.SendAsync(request);
                            string responseAsString = await response.Content.ReadAsStringAsync();
                            SpotifyTokenResponse tokens = JsonConvert.DeserializeObject<SpotifyTokenResponse>(responseAsString);

                            var saveQuery = from b in db.UserInfo
                                            where b.ID == u.ID
                                            select b;
                            var saveResponse = saveQuery.ToList();
                            u.SpotifyToken = tokens.access_token;
                            db.SaveChanges();
                        }
                    }
                }

                db.Dispose();
            }
        }

        //Called On Start, gets a new TwitchToken
        //CALEBX - will need to get a new one eventually if this is perma running
        private async void GetTwitchToken(object state)
        {
            _log.LogInformation("GetTwitchToken:: Entering ");

            try
            {
                using (var httpClient = new HttpClient())
                {
                    using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://id.twitch.tv/oauth2/token?client_id=" + TWITCH_CLIENT_ID + "&client_secret=" + TWITCH_CLIENT_SECRET +"&grant_type=client_credentials"))
                    {
                        var response = await httpClient.SendAsync(request);
                        string responseAsString = await response.Content.ReadAsStringAsync();
                        TwitchToken = JsonConvert.DeserializeObject<TwitchTokenResponse>(responseAsString);
                    }
                }
            }
            catch (Exception e) //Add logging here not console write
            {
                Console.WriteLine("Failed to access token");
            }

            _log.LogInformation("GetTwitchToken:: Exiting");
        }

        //Queries Twitch API in batches of 100 or less to see if they are live
        //If they are live add them to the list of users that are currently live
        private async void QueryLiveUsersAsync(string userList)
        {
            _log.LogInformation("QueryLiveUsersAsync:: Entering");

            try
            {
                if (TwitchToken != null)
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.twitch.tv/helix/streams?" + userList))
                        {
                            request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + TwitchToken.access_token);
                            request.Headers.TryAddWithoutValidation("Client-Id", TWITCH_CLIENT_ID);

                            var response = await httpClient.SendAsync(request);
                            string responseAsString = await response.Content.ReadAsStringAsync();
                            StreamResponse temp = JsonConvert.DeserializeObject<StreamResponse>(responseAsString);
                            if (temp.data.Length > 0)
                            {
                                //Grab all the returned live streams and add them to the database and to LiveStreams
                                for (int ii = 0; ii < temp.data.Length; ii++)
                                {
                                    if (!NameContainer.Contains(temp.data[ii].user_name))
                                    {
                                        int id;
                                        int streamID = -1;
                                        using (var db = new TSI_DbContext())
                                        {
                                            //Get the UserID
                                            var idQuery = from b in db.UserInfo
                                                          where b.TwitchUserLogin == temp.data[ii].user_name
                                                          select b;
                                            var idResponse = idQuery.ToList();
                                            id = idResponse[0].ID;

                                            //Add the stream to the database
                                            DateTime dt = DateTime.Now;
                                            var newStream = new Streams { UserInfoID = id, DateCreated = dt };
                                            db.Streams.Add(newStream);
                                            db.SaveChanges();

                                            //Get the auto-generated ID (primary key) based on the UserID and DateCreated
                                            var streamQuery = from b in db.Streams
                                                            where b.UserInfoID == id && b.DateCreated.Date == dt.Date
                                                            select b;
                                            var streamResponse = streamQuery.ToList();

                                            //Set the StreamID
                                            for (int jj = 0; jj < streamResponse.Count; jj++)
                                            {
                                                if (dt.ToString("f") == streamResponse[jj].DateCreated.ToString("f"))
                                                    streamID = streamResponse[jj].ID;
                                            }
                                        }
                                        //Add the Streamer to LiveStreams and to NameContainer
                                        LiveStreams.Add(new Streamer(temp.data[ii], id, streamID));
                                        NameContainer.Add(temp.data[ii].user_name);
                                    }
                                }
                            }
                        }
                    }
                }
                else
                {
                    _log.LogError("TwitchToken was null ");
                    //This should not happen because TwitchToken is generated on start
                }
            }
            catch (Exception e)
            {
                _log.LogError("Failed to query twitch: " + e.Message);
            }

            _log.LogInformation("QueryLiveUsersAsync:: Exiting");
            _log.LogInformation("Current live streams: " + LiveStreams.Count.ToString()); //CALEBX - debug purposes, remove later
        }

        //Queries a specific twitch user to see if they are live and returns their stream
        //Returns null if user is not live or an exception occurs
        private async Task<Stream> IsLive(string input)
        {
            _log.LogInformation("IsLive:: Entering with user " + input);

            try
            {
                //TwitchToken should never be null as it is retrieved on startup
                if (TwitchToken != null)
                {
                    //Send request to Twitch API to get a user stream back or a null response if they are not live
                    using (var httpClient = new HttpClient())
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("GET"), "https://api.twitch.tv/helix/streams?user_login=" + input)) //Need to pull name from DB
                        {
                            request.Headers.TryAddWithoutValidation("Authorization", "Bearer " + TwitchToken.access_token);
                            request.Headers.TryAddWithoutValidation("Client-Id", TWITCH_CLIENT_ID);

                            var response = await httpClient.SendAsync(request);
                            string responseAsString = await response.Content.ReadAsStringAsync();
                            StreamResponse temp = JsonConvert.DeserializeObject<StreamResponse>(responseAsString);
                            //Return Stream if the user is live
                            if (temp.data.Length > 0)
                            {
                                _log.LogInformation("User was live\nIsLive:: Exiting with user: " + temp.data[0].user_name);
                                return temp.data[0];
                            }
                            //Return null if the user is not live
                            else
                            {
                                _log.LogInformation("User was not live\nIsLive:: Exiting with response: NULL");
                                return null;
                            }
                        }
                    }
                }
                else
                {
                    _log.LogError("TwitchToken is NULL ");
                    _log.LogInformation("IsLive:: Exiting with response: NULL");
                    return null;
                }
            }
            catch (Exception e)
            {
                _log.LogError("Error retrieving user: " + e.Message + "\nIsLive:: Exiting with response: NULL");
                return null;
            }
        }

        public void SaveSong(Streamer sr, SongResponse currentSong)
        {
            _log.LogInformation("SaveSong:: Entering");

            try
            {
                if (currentSong != null && currentSong.item != null)
                {
                    //Check if the streamer is offline
                    if (currentSong == _offline)
                    {
                        //Find the average viewers for the last song
                        int total = 0;
                        foreach (ViewerMoment vm in sr.viewers)
                            total += vm.viewerCount;
                        int average = total / sr.viewers.Count;

                        //Save the last song to a new record in the Songs table in the db
                        using (var db = new TSI_DbContext())
                        {
                            var song = new Songs { StreamID = sr.StreamID, SpotifyID = sr.viewers[0].song.item.id, SongTitle = sr.viewers[0].song.item.name, AverageViewers = average };
                            db.Songs.Add(song);
                            db.SaveChanges();

                            db.Dispose();
                        }
                    }
                    //Check if the current song has changed
                    else if (sr.viewers.Count > 0 && sr.viewers[sr.viewers.Count - 1].song.item.id != currentSong.item.id)
                    {
                        //Find the average viewers for the last song
                        int total = 0;
                        foreach (ViewerMoment vm in sr.viewers)
                            total += vm.viewerCount;
                        int average = total / sr.viewers.Count;

                        //Save the last song to a new record in the Songs table in the db
                        using (var db = new TSI_DbContext())
                        {
                            var song = new Songs { StreamID = sr.StreamID, SpotifyID = sr.viewers[0].song.item.id, SongTitle = sr.viewers[0].song.item.name, AverageViewers = average };
                            db.Songs.Add(song);
                            db.SaveChanges();

                            db.Dispose();
                        }

                        //Reset the ViewerMoment list in for the streamer to set up for next song, and add the first ViewerMoment
                        sr.viewers = new List<ViewerMoment>();
                        sr.viewers.Add(new ViewerMoment(sr.stream.viewer_count, currentSong));
                    }
                    else
                        sr.viewers.Add(new ViewerMoment(sr.stream.viewer_count, currentSong));
                }
            }
            catch (Exception e)
            {
                _log.LogError("Error adding the song: {0}", e.Message);
                //If there's an error here, there's not much to do besides log it and accept that either 
                //this moment, or the entire song won't be logged
            }

            _log.LogInformation("SaveSong:: Exiting");
        }
        private string StringToB64(string input)
        {
            var response = Encoding.UTF8.GetBytes(input);
            string temp = Convert.ToBase64String(response);
            return temp;
        }
    }
}
