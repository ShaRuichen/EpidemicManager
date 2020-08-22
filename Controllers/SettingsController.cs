using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System;
using EpidemicManager.Models;
using Microsoft.AspNetCore.Http;


namespace EpidemicManager.Controllers
{
    public class SettingsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Changepeople()
        {
            var id = HttpContext.Session.GetString("userId");
            var kind = HttpContext.Session.GetString("userKind");
            if (HttpContext.Session.GetString("userId") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else if(HttpContext.Session.GetString("userKind") != null)
            {
                if (kind == "doctor") return RedirectToAction("Doctor");
                else if (kind == "manager") return RedirectToAction("Manager");
                else if (kind == "patient") return RedirectToAction("Patient");
                else return View();
            }
            else
            {
                return View();
            }
        }
        [HttpPost]
        public IActionResult Changepeople(SettingsPeople settingsPeople)
        {
            var id = HttpContext.Session.GetString("userId");
            SettingsPeople people_info = new SettingsPeople();
            people_info.name = Request.Form["name"];
            people_info.address = Request.Form["address"];
            people_info.tel = Request.Form["tel"];
            people_info.sex = Request.Form["sex"];
            people_info.password = Request.Form["password"];
            Sql.Execute("UPDATE people set name=@1,address=@2,tel=@3,sex=@4 where id=@0", id, people_info.name, people_info.address, people_info.tel, people_info.sex);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Doctor()
        {
            var id = HttpContext.Session.GetString("userId");
            if (HttpContext.Session.GetString("userId") == null)
            {
                return RedirectToAction("Index","Login");
            }
            else
            {
                var model = new Settingsdoctor
                {
                    doc_id = id,
                };
                return View(model);
            }
        }
        [HttpPost]
        public IActionResult Doctor(Settingsdoctor settingsdoctor)
        {
            Settingsdoctor doc_info = new Settingsdoctor();
            var id = HttpContext.Session.GetString("userId");
            doc_info.hos_name = Request.Form["hos_name"];
            doc_info.name = Request.Form["name"];
            doc_info.password = Request.Form["password"];
            Sql.Execute("UPDATE doctor set name=@1,hos_name=@2,password=@3 where id=@0", id, doc_info.name, doc_info.hos_name, doc_info.password);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Manager()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Manager(Settingsmanager settingsmanager)
        {
            Settingsmanager man_info = new Settingsmanager();
            var id = HttpContext.Session.GetString("userId");
            man_info.name = Request.Form["name"];
            man_info.sex = Request.Form["sex"];
            man_info.tel = Request.Form["tel"];
            man_info.work_unit = Request.Form["work_unit"];
            man_info.password = Request.Form["password"];
            Sql.Execute("UPDATE manager set name=@1,sex=@2,tel=@3,password=@4 where id=@0", id, man_info.name, man_info.sex, man_info.tel, man_info.password);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Patient()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Patient(Settingspatient settingspatient)
        {
            Settingspatient pat_info = new Settingspatient();
            var id = HttpContext.Session.GetString("userId");
            pat_info.name = Request.Form["name"];
            pat_info.sex = Request.Form["sex"];
            pat_info.hos_name = Request.Form["hos_name"];
            pat_info.password = Request.Form["password"];
            Sql.Execute("UPDATE patient set name=@1,hos_name=@2,sex=@3,password=@4 where id=@0", id, pat_info.name, pat_info.hos_name, pat_info.sex, pat_info.password);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Delete()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Delete(Settingspatient settingspatient)
        {
            var person_id = HttpContext.Session.GetString("userId");
            var kind = HttpContext.Session.GetString("userKind");
            if (kind == "doctor") Sql.Execute("DELETE from doctor where ID = @0", person_id);
            else if (kind == "patient") Sql.Execute("DELETE from patient where ID = @0", person_id);
            else if (kind == "manager") Sql.Execute("DELETE from manager where ID = @0", person_id);
            else if (kind == "people") Sql.Execute("DELETE from people where ID = @0", person_id);
            return RedirectToAction("Index");
        }
    }
}
