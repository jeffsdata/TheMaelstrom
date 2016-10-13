using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMaelstrom.Models
{
    public class Achievement
    {
        public string id { get; set; }
        public string title { get; set; }
        public string points { get; set; }
        public IList<GameItem> rewardItems { get; set; }
        public string icon { get; set; }
        public string accountWide { get; set; }
        public int factionId { get; set; }
    }
}
