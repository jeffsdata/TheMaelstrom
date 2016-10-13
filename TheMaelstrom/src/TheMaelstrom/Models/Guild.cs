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
        public string side { get; set; }
        public string level { get; set; }
        public string acheivementPoints { get; set; }
        public IList<GuildMember> members { get; set; }
        public int memberCount { get; set; }
        public List<string> imageFiles { get; set; }
        //public IList<News> news { get; set; }
    }
}
