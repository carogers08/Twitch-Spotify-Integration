using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TSI_App.JSON;

namespace TSI_App.Data
{
    public class Streamer
    {
        public Streamer (Stream newStream, int ID, int streamID)
        {
            this.stream = newStream;
            this.ID = ID;
            this.StreamID = streamID;
            this.viewers = new List<ViewerMoment>();
        }
        public Stream stream { get; set; }
        public List<ViewerMoment> viewers { get; set; }
        public int ID { get; set; }
        public int StreamID { get; set; } = -1;
    }
}
