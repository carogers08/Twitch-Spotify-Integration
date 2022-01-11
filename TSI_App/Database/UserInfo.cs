using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TSI_App.Database
{
    public class UserInfo
    {
        [Key]
        public int ID { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string TwitchUserLogin { get; set; }
        public string SpotifyRefreshToken { get; set; }
        public string SpotifyToken { get; set; }

        public virtual ICollection<Streams> Streams { get; set; }
    }
}
