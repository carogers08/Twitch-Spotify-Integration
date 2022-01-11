using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSI_App.JSON;

namespace TSI_App.Data
{
    public class ViewerMoment
    {
        public ViewerMoment() { }
        public ViewerMoment(int viewers, SongResponse song)
        {
            this.viewerCount = viewers;
            this.song = song;
        }

        public int viewerCount { get; set; }
        public SongResponse song { get; set; } = null;
    }
}
