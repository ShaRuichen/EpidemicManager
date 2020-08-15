using System;
using System.Data;

using Microsoft.AspNetCore.Mvc;

namespace EpidemicManager.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string id, string password, string kind)
        {
            var person = kind switch
            {
                "people" => Sql.Read("SELECT password FROM people WHERE id = @0", id),
                "doctor" => Sql.Read("SELECT password FROM doctor WHERE id = @0", id),
                "manager" => Sql.Read("SELECT password FROM manager WHERE id = @0", id),
                _ => throw new InvalidOperationException()
            };
            if (person.Count == 0) return Json(new
            {
                isSucceeded = false,
                message = "用户名不存在。",
            });
            var correctPassword = person[0][0].ToString();
            if (password != correctPassword) return Json(new
            {
                isSucceeded = false,
                message = "密码错误。",
            });
            return Json(new
            {
                isSucceeded = true,
            });
        }
    }
}
