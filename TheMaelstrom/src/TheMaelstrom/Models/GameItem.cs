using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMaelstrom.Models
{
    public class GameItem
    {
        public string ItemId { get; set; }
        public string ItemName { get; set; }
        public string ItemDesc { get; set; }
        public string ItemLevel { get; set; }
        public int ItemQuality { get; set; }
        public bool ItemIsArmor { get; set; }
    }
}
