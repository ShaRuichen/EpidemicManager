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
            var cookies = HttpContext.Request.Cookies;
            if (cookies.TryGetValue("id", out var id))
            {
                var password = cookies["password"];
                var kind = cookies["kind"];
                Login(id, password, kind, null);
            }
            return View();
        }

        public IActionResult PeopleRegister()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public JsonResult Login(string id, string password, string kind, string rememberMe)
        {
            var person = kind switch
            {
                "people" => Sql.Read("SELECT password FROM people WHERE id = @0", id),
                "patient" => Sql.Read("SELECT password FROM patient WHERE id = @0", id),
                "doctor" => Sql.Read("SELECT password FROM doctor WHERE id = @0", id),
                "manager" => Sql.Read("SELECT password FROM manager WHERE id = @0", id),
                _ => throw new InvalidOperationException()
            };
            if (person.Count == 0) return Json(new
            {
                isSucceeded = false,
                isIdValid = false,
            });
            var correctPassword = person[0][0].ToString();
            if (password != correctPassword) return Json(new
            {
                isSucceeded = false,
                isIdValid = true,
                isPasswordValid = false,
            });
            var session = HttpContext.Session;
            session.SetString("userKind", kind);
            session.SetString("userId", id);
            if (kind == "people")
            {
                var patients = Sql.Read("SELECT password FROM patient WHERE id = @0", id);
                if (patients.Count > 0) session.SetString("isPatient", bool.TrueString);
                else session.SetString("isPatient", bool.FalseString);
            }
            var originPath = session.GetString("sourcePath");
            var path = originPath.StartsWith("/") ? originPath : "/" + originPath;
            if (rememberMe == "checked")
            {
                var cookies = Response.Cookies;
                cookies.Append("id", id);
                cookies.Append("password", password);
                cookies.Append("kind", kind);
            }
            return Json(new
            {
                isSucceeded = true,
                path,
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
        public void Logout()
        {
            var cookies = Response.Cookies;
            cookies.Delete("id");
            cookies.Delete("password");
            cookies.Delete("kind");
        }

        [HttpPost]
        public string RegisterPatient(string id, string password, string name, string sex, string hospital)
        {
            var patient = Sql.Read("SELECT id FROM patient WHERE id = @0", id);
            if (patient.Count > 0) return bool.FalseString;
            var hospitals = Sql.Read("SELECT hospital_name FROM hospital WHERE hospital_name = @0", hospital);
            if (hospitals.Count == 0) return "医院不存在";
            Sql.Execute("INSERT INTO patient VALUES(@0, @1, @2, @3, @4)", id, name, sex, hospital, password);
            return bool.TrueString;
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
