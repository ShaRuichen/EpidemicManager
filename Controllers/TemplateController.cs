﻿using System.Collections.Generic;
using System.Data;

using Microsoft.AspNetCore.Mvc;
using EpidemicManager.Models;

namespace EpidemicManager.Controllers
{
    public class TemplateController : Controller
    {
        public IActionResult Index()
        {
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
    }
}
