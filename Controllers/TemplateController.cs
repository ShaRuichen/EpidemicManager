using System.Collections.Generic;
using System.Data;

using Microsoft.AspNetCore.Mvc;
using EpidemicManager.Models;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace EpidemicManager.Controllers
{
    public class TemplateController : Controller
    {
        public IActionResult Index()
        {
            TestSql();

            var session = HttpContext.Session;
            var userKind = session.GetString("userKind");
            var userId = session.GetString("userId");

            var people = Sql.Read("SELECT id FROM people");
            var list = new List<string>();
            foreach (DataRow person in people)
            {
                list.Add(person[0].ToString());
            }

            var model = new TemplateModel
            {
                Ids = list,
            };

            return View(model);
        }

        public IActionResult Modify(string id)
        {
            if (id != null)
            {
                Sql.Execute("INSERT INTO people VALUES(@0, 'name', 'address', 'tel', 'sex', 'password')", id);
            }
            else
            {
                Sql.Execute("DELETE FROM people");
            }

            var model = new TemplateModel
            {
                Id = id ?? string.Empty,
            };
            ViewBag.Huhaha = "666";
            return View(model);
        }

        [HttpPost]
        public JsonResult Click(string name, int number)
        {
            return Json(new
            {
                name,
                num = number + 1,
            });
        }

        [Conditional("DebugSql")]
        private void TestSql()
        {
            for (var i = 0; i < 40; i++)
            {
                if (i == 20) Thread.Sleep(500);
                var thread = new Thread(() =>
                {
                    Sql.Read("SELECT id FROM people");
                });
                thread.Start();
            }
        }
    }
}
