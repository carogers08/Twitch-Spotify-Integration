using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TSI_App.Database
{
    public class Streams
    {
        [Key]
        public int ID { get; set; }
        public int UserInfoID { get; set; }
        public DateTime DateCreated { get; set; }
        
        public virtual UserInfo User { get; set; }
        public virtual List<Songs> Songs { get; set; }
    }
}
