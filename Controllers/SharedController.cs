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
            var cookies = Response.Cookies;
            cookies.Delete("id");
            cookies.Delete("password");
            cookies.Delete("kind");
            return RedirectToAction("Index", "Home");
        }
    }
}
