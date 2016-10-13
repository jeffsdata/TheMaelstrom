using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMaelstrom.Models
{
    public class Guild
    {   
        public string name { get; set; }
        public GuildEmblem emblem { get; set; }
        public string realm { get; set; }
        public string battlegroup { get; set; }
        public string side { get; set; }
        public string level { get; set; }
        public string achievementPoints { get; set; }
        public IList<GuildMember> members { get; set; }
        public int memberCount { get; set; }
        public List<string> imageFiles { get; set; }
        public IList<News> news { get; set; }
    }

    public class GuildEmblem
    {
        public string icon { get; set; }
        public string iconColor { get; set; }
        public string iconColorId { get; set; }
        public string border { get; set; }
        public string borderColor { get; set; }
        public string backgroundColor { get; set; }
        public string backgroundColorId { get; set; }
    }

    public class News
    {
        public string type { get; set; }
        public string newsAction { get; set; }
        public string character { get; set; }
        public long timestamp { get; set; }
        public string timeString { get; set; }
        public int itemId { get; set; }
        //public string context { get; set; }
        public Achievement achievement { get; set; }
        public IList<int> bonusLists { get; set; }
    }
}
