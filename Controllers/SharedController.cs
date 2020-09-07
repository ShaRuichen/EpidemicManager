using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EpidemicManager.Controllers
{
    public class SharedController : Controller
    {
        [HttpGet]
        public IActionResult Logout()
        {
            var session = HttpContext.Session;
            session.Remove("userId");
            session.Remove("userKind");
            return RedirectToAction("Index", "Home");
        }
    }
}
