using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using System.Text;
using System.Net.Http.Headers;
using Newtonsoft.Json;
using TSI_App.JSON;
using TSI_App.Database;

namespace TSI_App.Data
{
    public class SpotifyLoginService
    {
        const string SPOTIFY_CLIENT_ID = "1a4dc6b858f844e386a120a023479599";
        const string SPOTIFY_CLIENT_SECRET = "75d28f9e70f445bb9555ccb3bffd45e6";

        public async Task<SpotifyTokenResponse> GetTokenAsync(string url)
        {
            SpotifyTokenResponse spotifyToken = null;

            if (ParseURL(url) == null)
            {
                //CALEBX - Add logging
            }
            else
            {
                try
                {
                    using (var httpClient = new HttpClient())
                    {
                        using (var request = new HttpRequestMessage(new HttpMethod("POST"), "https://accounts.spotify.com/api/token"))
                        {
                            request.Headers.TryAddWithoutValidation("Authorization", "Basic " + StringToB64(SPOTIFY_CLIENT_ID + ":" + SPOTIFY_CLIENT_SECRET));

                            var contentList = new List<string>();
                            contentList.Add("grant_type=authorization_code");
                            contentList.Add("code=" + ParseURL(url));
                            contentList.Add("redirect_uri=https%3A%2F%2Flocalhost%3A44355%2Fcallback");
                            request.Content = new StringContent(string.Join("&", contentList));
                            request.Content.Headers.ContentType = MediaTypeHeaderValue.Parse("application/x-www-form-urlencoded");

                            var response = await httpClient.SendAsync(request);
                            string responseAsString = await response.Content.ReadAsStringAsync();

                            spotifyToken = JsonConvert.DeserializeObject<SpotifyTokenResponse>(responseAsString);
                        }
                    }
                }
                catch (Exception e)
                {
                    //CALEBX - add logging
                }
            }

            return spotifyToken;
        }

        public void SaveTokens(SpotifyTokenResponse tokens, int ID)
        {
            using (var db = new TSI_DbContext())
            {
                var userQuery = from b in db.UserInfo
                                where b.ID == ID
                                select b;
                var userResponse = userQuery.ToList();
                userResponse[0].SpotifyRefreshToken = tokens.refresh_token;
                userResponse[0].SpotifyToken = tokens.access_token;
                db.SaveChanges();

                db.Dispose();
            }
        }

        private string ParseURL(string url)
        {
            if (url.Contains("error"))
            {
                return null;
            }
            else
            {
                url = url.Substring(url.IndexOf("=") + 1);
                string state = url.Substring(url.IndexOf("=") + 1);
                url = url.Substring(0, url.IndexOf("&"));
            }

            return url;
        }

        private string StringToB64(string input)
        {
            var response = Encoding.UTF8.GetBytes(input);
            string temp = Convert.ToBase64String(response);
            return temp;
        }
    }
}
