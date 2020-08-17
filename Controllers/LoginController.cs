using System;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EpidemicManager.Controllers
{
    public class LoginController : Controller
    {
        public IActionResult Index(string path)
        {
            path ??= string.Empty;
            HttpContext.Session.SetString("sourcePath", path);
            return View();
        }

        public IActionResult Register()
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
            HttpContext.Session.SetString("userKind", kind);
            HttpContext.Session.SetString("userId", id);
            return Json(new
            {
                isSucceeded = true,
                path = HttpContext.Session.GetString("sourcePath"),
            });
        }

        [HttpPost]
        public bool RegisterPeople(string id, string password, string name, string sex,
            string tel, string address)
        {
            var person = Sql.Read("SELECT id FROM people WHERE id = @0", id);
            if (person.Count > 0) return false;
            Sql.Execute("INSERT INTO people VALUES(@0, @1, @2, @3, @4, @5)", id, name, address,
                tel, sex, password);
            return true;
        }

        [HttpPost]
        public string RegisterDoctor(string id, string name, string password, string hospital)
        {
            var person = Sql.Read("SELECT id FROM doctor WHERE id = @0", id);
            if (person.Count > 0) return bool.FalseString;
            var hospitals = Sql.Read("SELECT hospital_name FROM hospital WHERE hospital_name = @0", hospital);
            if (hospitals.Count == 0) return "医院不存在";
            Sql.Execute("INSERT INTO doctor VALUES(@0, @1, @2, @3)", id, name, hospital, password);
            return bool.TrueString;
        }

        [HttpPost]
        public bool RegisterManager(string id, string password, string name, string sex,
            string tel, string address, string unit)
        {
            var person = Sql.Read("SELECT id FROM manager WHERE id = @0", id);
            if (person.Count > 0) return false;
            Sql.Execute("INSERT INTO manager VALUES(@0, @1, @2, @3, @4, @5, @6)", id, name,
                sex, unit, tel, address, password);
            return true;
        }
    }
}
