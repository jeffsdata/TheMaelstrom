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
        public string characterClassName { get; set; }
        public string realm { get; set; }
        public string race { get; set; }
        public string raceName { get; set; }
        public string gender { get; set; }
        public string genderName { get; set; }
        public CharSpec spec { get; set; }
        public int level { get; set; }
        //public int AvgItemLevel { get; set; }
        public int achievementPoints { get; set; }
        public string thumbnail { get; set; }
    }

    public class CharSpec
    {
        public string name { get; set; }
        public string role { get; set; }
        public string backgroundImage { get; set; }
        public string icon { get; set; }
        public string description { get; set; }
        public string order { get; set; }
    }
}
