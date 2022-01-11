using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace TSI_App.Database
{
    public class Songs
    {
        [Key]
        public int ID { get; set; }
        public int StreamID { get; set; }
        public string SpotifyID { get; set; }
        public string SongTitle { get; set; }
        public int AverageViewers { get; set; }

        public virtual Streams Stream { get; set; }
    }
}
