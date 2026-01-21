using Microsoft.AspNetCore.Mvc;

namespace FoundersDesk.Controllers
{
    public class HomeController : Controller
    {
        // GET: / (Landing Page)
        public IActionResult Index()
        {
            return View();
        }

        // GET: /Home/Error
        public IActionResult Error()
        {
            return View();
        }
    }
}
