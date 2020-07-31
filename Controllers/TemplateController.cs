using System.Collections.Generic;

using Microsoft.AspNetCore.Mvc;

using EpidemicManager.Models;
using MySql.Data.MySqlClient;
using Microsoft.Extensions.Logging;

namespace EpidemicManager.Controllers
{
    public class TemplateController : Controller
    {
        public IActionResult Index()
        {
            using var conn = new MySqlConnection(Program.ConnString);
            conn.Open();
            using var cmd = new MySqlCommand("SELECT * FROM people", conn);
            using var reader = cmd.ExecuteReader();
            var list = new List<string>();
            while (reader.Read())
            {
                list.Add(reader.GetString(0));
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
                using var conn = new MySqlConnection(Program.ConnString);
                conn.Open();
                using var cmd = new MySqlCommand
                {
                    Connection = conn,
                    CommandText = $"INSERT INTO people VALUES(@id, 'name', 'address', 'tel', 'sex', 'password')",
                };
                cmd.Parameters.Add(new MySqlParameter("@id", id));
                cmd.ExecuteNonQuery();
            }
            else
            {
                using var conn = new MySqlConnection(Program.ConnString);
                conn.Open();
                using var cmd = new MySqlCommand("DELETE FROM people", conn);
                cmd.ExecuteNonQuery();
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
    }
}
