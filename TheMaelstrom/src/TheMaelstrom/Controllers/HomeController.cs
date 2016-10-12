using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.Net.Http;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNet.Mvc;
using Newtonsoft.Json;
using TheMaelstrom.Models;
using System.Net.Http.Headers;

namespace TheMaelstrom.Controllers
{

    public class HomeController : Controller
    {

        public async Task<Guild> GetJson(bool n, bool m, bool a)
        {
                // ... Target page.
                string page = "https://us.api.battle.net/wow/guild/Nathrezim/The%20Maelstrom?fields=achievements%2Cchallenge%2C+members%2C+news&locale=en_US&apikey=bbtvqfdjvfk342xznb7yvgvddz5vt6r7";

                // ... Use HttpClient.
                using (HttpClient client = new HttpClient())
                using (HttpResponseMessage response = await client.GetAsync(page))
                using (HttpContent content = response.Content)
                {
                    // ... Read the string.
                    string result = await content.ReadAsStringAsync();

                    // ... Display the result.
                    if (result != null)
                    {
                        Guild guild = JsonConvert.DeserializeObject<Guild>(result);
                        foreach (GuildMember mem in guild.members)
                        {
                            mem.character.race = GetRace(mem.character.race);
                            mem.character.characterClass = GetClass(mem.character.characterClass);
                            mem.rank = GetRank(mem.rank);
                        }
                        return guild;
                    }
                    else
                    {
                        return null;
                    }
                }
        }

        public string GetRace(string s)
        {
            switch (s)
            {
                case "1":
                    return "Human";
                case "2":
                    return "Orc";
                case "3":
                    return "Dwarf";
                case "4":
                    return "Night Elf";
                case "5":
                    return "Undead";
                case "6":
                    return "Tauren";
                case "7":
                    return "Gnome";
                case "8":
                    return "Troll";
                case "9":
                    return "Goblin";
                case "10":
                    return "Blood Elf";
                case "11":
                    return "Draenei";
                case "22":
                    return "Worgen";
                case "24":
                    return "Pandaren";
                case "25":
                    return "Pandaren";
                case "26":
                    return "Pandaren";
                default:
                    return null;
            }
        }

        public string GetClass(string s)
        {
            switch (s)
            {
                case "1":
                    return "Warrior";
                case "2":
                    return "Paladin";
                case "3":
                    return "Hunter";
                case "4":
                    return "Rogue";
                case "5":
                    return "Priest";
                case "6":
                    return "Death Knight";
                case "7":
                    return "Shaman";
                case "8":
                    return "Mage";
                case "9":
                    return "Warlock";
                case "10":
                    return "Monk";
                case "11":
                    return "Druid";
                case "12":
                    return "Demon Hunter";
                default:
                    return null;
            }
        }

        public string GetRank(string s)
        {
            switch (s)
            {
                case "0":
                    return "Guild Master";
                case "1":
                    return "Co-GM";
                case "2":
                    return "Officer";
                case "3":
                    return "Member";
                case "4":
                    return "Meep?";
                case "5":
                    return "Member";
                case "6":
                    return "Inactive";
                default:
                    return null;
            }
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public async Task<IActionResult> Members()
        {
            ViewData["Message"] = "The members page.";
            Guild guild = await GetJson(true, true, true);

            return View(guild);
        }

        public IActionResult History()
        {
            ViewData["Message"] = "The history page.";

            return View();
        }

        public IActionResult Error()
        {
            return View();
        }
    }
}
