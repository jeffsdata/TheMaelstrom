using System.Linq;
using Microsoft.AspNet.Mvc;

namespace TheMaelstrom.Controllers
{
    public class GuildController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}