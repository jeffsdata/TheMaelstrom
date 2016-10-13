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
using Microsoft.ApplicationInsights.AspNet.Extensions;
using Microsoft.AspNet.Hosting;

namespace TheMaelstrom.Controllers
{

    public class HomeController : Controller
    {
        private readonly IHostingEnvironment _hostingEnvironment;
        public HomeController(IHostingEnvironment hostingEnvironment)
        {
            _hostingEnvironment = hostingEnvironment;
        }


        public async Task<Guild> GetJson(bool n, bool m, bool a, bool c)
        {
            string variable = "";
            if (n)
            {
                variable = variable + "news%2C";
            }
            if (m)
            {
                variable = variable + "members%2C";
            }
            if (a)
            {
                variable = variable + "achievements%2C";
            }
            if (c)
            {
                variable = variable + "challenge%2C";
            }
            variable = variable.TrimEnd('C').TrimEnd('2').TrimEnd('%');
            // ... Target page.
            string page =
                "https://us.api.battle.net/wow/guild/Nathrezim/The%20Maelstrom?fields=" + variable + "&locale=en_US&apikey=bbtvqfdjvfk342xznb7yvgvddz5vt6r7";

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
                    var memCt = 0;
                    Guild guild = JsonConvert.DeserializeObject<Guild>(result);
                    
                    string webRootPath = _hostingEnvironment.WebRootPath;
                    string file = webRootPath + "/images/members/";
                    List<string> imageFiles = new List<string>();
                    foreach (string s in Directory.EnumerateFiles(file,"*"))
                    {
                        imageFiles.Add(s);
                    }
                    guild.imageFiles = imageFiles;
                    if (m)
                    {
                        foreach (GuildMember mem in guild.members)
                        {
                            // Set races, classes, and ranks
                            mem.character.raceName = GetRace(mem.character.race);
                            mem.character.characterClassName = GetClass(mem.character.characterClass);
                            mem.rankName = GetRank(mem.rank);

                            var imageName = mem.character.name.ToLower() + ".jpg";
                            var absolutePath = "http://" + Request.GetUri().Host + ":" + Request.GetUri().Port +
                                               "/images/members/" +
                                               imageName;
                            foreach (string s in imageFiles)
                            {
                                if (s.Contains(imageName))
                                {
                                    mem.customImage = absolutePath;
                                }
                            }
                            memCt++;
                        }
                    }

                    // News- Specific stuff
                    if (n)
                    {
                        foreach (News news in guild.news)
                        {
                            news.newsAction = GetNewsAction(news.type);
                            var epoch = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
                            var timeSpan = news.timestamp/1000;
                            var localDateTime = epoch.AddSeconds(timeSpan).ToLocalTime();
                            news.timeString = localDateTime.ToString("MM/dd hh:mm tt");
                        }
                    }
                    guild.memberCount = memCt;
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
                    return "Co-GM";
                case "3":
                    return "Co-GM";
                case "4":
                    return "Officer";
                case "5":
                    return "Member";
                case "6":
                    return "Inactive";
                default:
                    return null;
            }
        }

        public string GetNewsAction(string s)
        {
            switch (s)
            {
                case "playerAchievement":
                    return "acheived";
                case "itemLoot":
                    return "got loot";
                case "itemCraft":
                    return "crafted loot";
                default:
                    return null;
            }
        }

        public async Task<IActionResult> Index()
        {
            Guild guild = await GetJson(true, true, false, false);
            return View(guild);
        }

        public async Task<IActionResult> Loots()
        {
            Guild guild = await GetJson(true, true, false, false);
            return View(guild);
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
            Guild guild = await GetJson(false, true, false, false);

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
