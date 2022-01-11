using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSI_App.JSON
{
    public class SongResponse
    {
        public SongResponse(int progress, bool playing, Item item)
        {
            prgress_ms = progress;
            is_playing = playing;
            this.item = item;
        }

        public int prgress_ms { get; set; }
        public bool is_playing { get; set; }
        public Item item { get; set; }
    }
}
