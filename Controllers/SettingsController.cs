using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System;
using EpidemicManager.Models;

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
            return View();
        }
        [HttpPost]
        public IActionResult Changepeople(SettingsPeople settingsPeople)
        {
            SettingsPeople people_info = new SettingsPeople();
            people_info.people_id = Request.Form["settings_people_id"];
            people_info.name = Request.Form["name"];
            people_info.address = Request.Form["address"];
            people_info.tel = Request.Form["tel"];
            people_info.sex = Request.Form["sex"];
            people_info.password = Request.Form["password"];
            Sql.Execute("UPDATE people set name=@1,address=@2,tel=@3,sex=@4 where id=@0", people_info.people_id, people_info.name, people_info.address, people_info.tel, people_info.sex);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult Doctor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult Doctor(Settingsdoctor settingsdoctor)
        {
            Settingsdoctor doc_info = new Settingsdoctor();
            doc_info.doc_id = Request.Form["doc_id"];
            doc_info.hos_name = Request.Form["hos_name"];
            doc_info.name = Request.Form["name"];
            doc_info.password = Request.Form["password"];
            Sql.Execute("UPDATE doctor set name=@1,hos_name=@2,password=@3 where id=@0", doc_info.doc_id, doc_info.name, doc_info.hos_name, doc_info.password);
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
            man_info.man_id = Request.Form["man_id"];
            man_info.name = Request.Form["name"];
            man_info.sex = Request.Form["sex"];
            man_info.tel = Request.Form["tel"];
            man_info.work_unit = Request.Form["work_unit"];
            man_info.password = Request.Form["password"];
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
            pat_info.pat_id = Request.Form["pat_id"];
            pat_info.name = Request.Form["name"];
            pat_info.sex = Request.Form["sex"];
            pat_info.hos_name = Request.Form["hos_name"];
            pat_info.password = Request.Form["password"];
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
            string person_id = Request.Form["person_id"];
            string kind = Request.Form["kind"];
            if (kind == "doctor") Sql.Execute("DELETE from doctor where ID = @0", person_id);
            else if (kind == "patient") Sql.Execute("DELETE from patient where ID = @0", person_id);
            else if (kind == "manager") Sql.Execute("DELETE from manager where ID = @0", person_id);
            else if (kind == "people") Sql.Execute("DELETE from people where ID = @0", person_id);
            return RedirectToAction("Index");
        }
    }
}
