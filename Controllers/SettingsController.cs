using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System;
using EpidemicManager.Models;

public struct SettingsPerson
{
    public string people_id { get; set; }
    public string name { get; set; }
    public string kind { get; set; }
    public string address { get; set; }
    public string tel { get; set; }
    public string sex { get; set; }
}

namespace EpidemicManager.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            var model = new SettingsModel { };
            return View(model);
        }
        public JsonResult Settings(string id, string kind, SettingsPerson new_info)
        {
            string name = new_info.name;
            string address = new_info.address;
            string tel = new_info.tel;
            string sex = new_info.sex;
            if (kind == "people")
                Sql.Execute("UPDATE people set name=@1,address=@2,tel=@3,sex=@4 where id=@0", id, name, address, tel, sex);
            else if (kind == "doctor")
                Sql.Execute("UPDATE doctor set name=@1,address=@2,tel=@3,sex=@4 where id=@0", id, name, address, tel, sex);
            else if (kind == "manager")
                Sql.Execute("UPDATE manager set name=@1,address=@2,tel=@3,sex=@4 where id=@0", id, name, address, tel, sex);
            return Json(new
            {
                isSucceeded = true,
            });
        }
    }
}
