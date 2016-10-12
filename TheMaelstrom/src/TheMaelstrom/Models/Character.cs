using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMaelstrom.Models
{
    public class Character
    {
        public string name { get; set; }
        [JsonProperty("class")]
        public string characterClass { get; set; }
        public string realm { get; set; }
        public string race { get; set; }
        public string gender { get; set; }
        //public CharSpec spec { get; set; }
        public int level { get; set; }
        //public int AvgItemLevel { get; set; }
        public int achievementPoints { get; set; }
        public string thumbnail { get; set; }
    }
}
