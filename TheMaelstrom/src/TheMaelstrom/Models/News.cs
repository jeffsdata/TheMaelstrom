using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace TheMaelstrom.Models
{
    public class News
    {
        public string type { get; set; }
        public string character { get; set; }
        public DateTime timestamp { get; set; }
        public int itemId { get; set; }
        public string context { get; set; }
        public Acheivement acheivement { get; set; }
    }
}
