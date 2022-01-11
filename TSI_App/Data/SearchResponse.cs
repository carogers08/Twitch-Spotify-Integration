using System;
using TSI_App.Database;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TSI_App.Data
{
    public class SearchResponse
    {
        public SearchResponse() 
        {
            Streamer = new List<string>();
        }
        public SearchResponse(string message)
        {
            Message = message;
        }
        
        public List<string> Streamer { get; set; }
        public List<Streams> Streams { get; set; }
        public string Message { get; set; }
    }
}
