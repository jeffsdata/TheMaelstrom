using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMaelstrom.Models
{
    public class Acheivement
    {
        public string AchId { get; set; }
        public string AchTitle { get; set; }
        public string AchPoints { get; set; }
        public GameItem AchReward { get; set; }
        public string AchRewardItems { get; set; }
    }
}
