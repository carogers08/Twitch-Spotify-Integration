using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSI_App.JSON
{
    public class TwitchTokenResponse
    {
        public string access_token { get; set; }
        public string refresh_token { get; set; }
        public int expires_in { get; set; }
        public string[] scope { get; set; }
        public string token_type { get; set; }
    }
}
