using System;
using TSI_App.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSI_App.Data
{
    public class SearchService
    {
        public SearchResponse ExecuteSearch(DateTime? date, string twitch)
        {
            SearchResponse search = new SearchResponse();
            DateTime dt = new DateTime();
            if (date != null)
                dt = (DateTime)date;

            if (date == null && !String.IsNullOrWhiteSpace(twitch)) // Just twitch has a value
            {
                search.Streamer.Add(twitch);
                search.Message = "Single User";
                int id;
                using (var db = new TSI_DbContext())
                {
                    var idQuery = from b in db.UserInfo
                                where b.TwitchUserLogin == twitch
                                select b;
                    var idResponse = idQuery.ToList();
                    if (idResponse.Count > 0)
                        id = idResponse[0].ID;
                    else
                        return new SearchResponse("Invalid twitch username");

                    var query = from b in db.Streams
                                where b.UserInfoID == id
                                orderby b.DateCreated
                                select b;
                    var response = query.ToList();
                    search.Streams = response;

                    return search;
                }
            } 
            else if (date != null && String.IsNullOrWhiteSpace(twitch)) //Just date has a value
            {
                search.Message = "Multiple User";
                using (var db = new TSI_DbContext())
                {
                    var query = from b in db.Streams
                                where b.DateCreated.Date == dt.Date
                                select b;
                    var response = query.ToList();
                    search.Streams = response;


                    for (int ii = 0; ii < search.Streams.Count; ii++)
                    {
                        var userQuery = from b in db.UserInfo
                                        where b.ID == search.Streams[ii].UserInfoID
                                        select b;
                        var userResponse = userQuery.ToList();
                        search.Streamer.Add(userResponse[0].TwitchUserLogin);
                    }
                }

                return search;
            }
            else //Both twitch and date have values
            {
                int id;
                using (var db = new TSI_DbContext())
                {
                    var idQuery = from b in db.UserInfo
                                  where b.TwitchUserLogin == twitch
                                  select b;
                    var idResponse = idQuery.ToList();
                    if (idResponse.Count > 0)
                        id = idResponse[0].ID;
                    else
                        return new SearchResponse("Invalid twitch username");

                    var query = from b in db.Streams
                                where (b.DateCreated.Date == dt.Date && b.UserInfoID == id)
                                select b;
                    var response = query.ToList();
                    search.Streams = response;
                    search.Streamer.Add(twitch);

                    return search;
                }
            }
        }
    }
}
