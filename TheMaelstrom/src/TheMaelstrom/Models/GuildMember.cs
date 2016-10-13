using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TheMaelstrom.Models
{
    public class GuildMember
    {
        public Character character { get; set; }
        public string customImage { get; set; }
        public string rank { get; set; }
        public string rankName { get; set; }
    }
}
