using Microsoft.AspNetCore.Mvc;

namespace EpidemicManager.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
