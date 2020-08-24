using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System;
using EpidemicManager.Models;
using Microsoft.AspNetCore.Http;
using System.Xml.Schema;
using Org.BouncyCastle.Crypto.Tls;
using Microsoft.AspNetCore.Mvc.Rendering;

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
                return RedirectToAction("Index", "Login", new { path = "Settings" });
            }
            else if (HttpContext.Session.GetString("userKind") != null)
            {
                if (kind == "doctor") return RedirectToAction("Doctor");
                else if(kind=="patient") return RedirectToAction("Patient");
                else if (kind == "manager") return RedirectToAction("Manager");
                else
                {
                    var peo_info = Sql.Read("SELECT name,address,tel,sex,password from people where ID=@0", id);
                    var PEO_name = new List<string>();
                    var PEO_address = new List<string>();
                    var PEO_tel = new List<string>();
                    var PEO_sex = new List<string>();
                    var PEO_password = new List<string>();
                    foreach (DataRow peoi in peo_info)
                    {
                        PEO_name.Add(peoi[0].ToString());
                        PEO_address.Add(peoi[1].ToString());
                        PEO_tel.Add(peoi[2].ToString());
                        PEO_sex.Add(peoi[3].ToString());
                        PEO_password.Add(peoi[4].ToString());
                    }
                    var model = new SettingsPeople
                    {
                        people_id = id,
                        name = PEO_name[0],
                        address = PEO_address[0],
                        tel = PEO_tel[0],
                        sex = PEO_sex[0],
                        password = PEO_password[0],
                    };
                    return View(model);
                }
            }
            else return View();
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
            var newpassword1 = Request.Form["password1"];
            var newpassword2 = Request.Form["password2"];
            if (newpassword1 != newpassword2)
            {
                return RedirectToAction("Message", "Settings");
            }
            if(newpassword1=="") return RedirectToAction("Success");
            Sql.Execute("UPDATE people set name=@1,address=@2,tel=@3,sex=@4,password=@5 where id=@0", id, people_info.name, people_info.address, people_info.tel, people_info.sex, newpassword1);
            return RedirectToAction("Success");
        }

        [HttpGet]
        public IActionResult Doctor()
        {
            var id = HttpContext.Session.GetString("userId");
            if (HttpContext.Session.GetString("userId") == null)
            {
                return RedirectToAction("Index", "Login");
            }
            else
            {
                var doc_info = Sql.Read("SELECT name,hospital_name,password from doctor where id=@0", id);
                var DOC_hos_name = new List<string>();
                var DOC_name = new List<string>();
                var DOC_password = new List<string>();
                foreach (DataRow doci in doc_info)
                {
                    DOC_name.Add(doci[0].ToString());
                    DOC_hos_name.Add(doci[1].ToString());
                    DOC_password.Add(doci[2].ToString());
                }
                var model = new Settingsdoctor
                {
                    doc_id = id,
                    name = DOC_name[0],
                    hos_name = DOC_hos_name[0],
                    password = DOC_password[0],
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
            var newpassword1 = Request.Form["password1"];
            var newpassword2 = Request.Form["password2"];
            if (newpassword1 != newpassword2)
            {
                return RedirectToAction("Message", "Settings");
            }
            if (newpassword1 == "") return RedirectToAction("Success");
            Sql.Execute("UPDATE doctor set name=@1,hospital_name=@2,password=@3 where id=@0", id, doc_info.name, doc_info.hos_name, newpassword1);
            return RedirectToAction("Success");
        }
        [HttpGet]
        public IActionResult Manager()
        {
            var id = HttpContext.Session.GetString("userId");
            var man_info = Sql.Read("SELECT name,sex,tel,work_unit,password from manager where id=@0", id);
            var MAN_name = new List<string>();
            var MAN_sex = new List<string>();
            var MAN_tel = new List<string>();
            var MAN_work_unit = new List<string>();
            var MAN_password = new List<string>();
            foreach (DataRow man in man_info)
            {
                MAN_name.Add(man[0].ToString());
                MAN_sex.Add(man[1].ToString());
                MAN_tel.Add(man[0].ToString());
                MAN_work_unit.Add(man[0].ToString());
                MAN_password.Add(man[0].ToString());
            }
            var model = new Settingsmanager
            {
                man_id = id,
                name = MAN_name[0],
                sex = MAN_sex[0],
                tel = MAN_tel[0],
                work_unit = MAN_work_unit[0],
                password = MAN_work_unit[0],
            };
            return View(model);
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
            var newpassword1 = Request.Form["password1"];
            var newpassword2 = Request.Form["password2"];
            if(newpassword1!=newpassword2)
            {
                return RedirectToAction("Message", "Settings");
            }
            if (newpassword1 == "") return RedirectToAction("Success");
            Sql.Execute("UPDATE manager set name=@1,sex=@2,tel=@3,password=@4 where id=@0", id, man_info.name, man_info.sex, man_info.tel, newpassword1);
            return RedirectToAction("Success");
        }
        [HttpGet]
        public IActionResult Patient()
        {
            var id = HttpContext.Session.GetString("userId");
            var pat_info = Sql.Read("SELECT name,sex,hospital_name,password from patient where id=@0", id);
            var PAT_name= new List<string>();
            var PAT_sex= new List<string>();
            var PAT_hospital_name = new List<string>();
            var PAT_password = new List<string>();
            foreach (DataRow pat in pat_info)
            {
                PAT_name.Add(pat[0].ToString());
                PAT_hospital_name.Add(pat[2].ToString());
                PAT_sex.Add(pat[1].ToString());
                PAT_password.Add(pat[3].ToString());
            }
            var model = new Settingspatient
            {
                pat_id = id,
                name = PAT_name[0],
                sex=PAT_sex[0],
                hos_name=PAT_hospital_name[0],
                password=PAT_password[0],
            };
            return View(model);
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
            var newpassword1 = Request.Form["password1"];
            var newpassword2 = Request.Form["password2"];
            if (newpassword1 == "") return RedirectToAction("Success");
            if (newpassword1 != newpassword2)
            {
                return RedirectToAction("Message", "Settings");
            }
            Sql.Execute("UPDATE patient set name=@1,hos_name=@2,sex=@3,password=@4 where id=@0", id, pat_info.name, pat_info.hos_name, pat_info.sex, newpassword1);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Message()
        {
            ViewBag.hehe = "两次新密码不一致，请检查输入是否有效";
            return View();
        }
        [HttpGet]
        public IActionResult Success()
        {
            ViewBag.haha = "信息修改成功！";
            return View();
        }
        [HttpGet]
        public IActionResult Deletepeople()
        {
            var id = HttpContext.Session.GetString("userId");
            var kind = HttpContext.Session.GetString("userKind");
            if (HttpContext.Session.GetString("userId") == null)
            {
                return RedirectToAction("Index", "Login", new { path = "Settings" });
            }
            else
            {
                if (kind == "people")
                {
                    var peo_info = Sql.Read("SELECT name,address,tel,sex,password from people where ID=@0", id);
                    var PEO_name = new List<string>();
                    var PEO_address = new List<string>();
                    var PEO_tel = new List<string>();
                    var PEO_sex = new List<string>();
                    var PEO_password = new List<string>();
                    foreach (DataRow peoi in peo_info)
                    {
                        PEO_name.Add(peoi[0].ToString());
                        PEO_address.Add(peoi[1].ToString());
                        PEO_tel.Add(peoi[2].ToString());
                        PEO_sex.Add(peoi[3].ToString());
                        PEO_password.Add(peoi[4].ToString());
                    }
                    var model = new SettingsPeople
                    {
                        people_id = id,
                        name = PEO_name[0],
                        address = PEO_address[0],
                        tel = PEO_tel[0],
                        sex = PEO_sex[0],
                        password = PEO_password[0],
                    };
                    return View(model);
                }
                else if (kind == "manager")
                {
                    return RedirectToAction("Deletemanager");
                }
                else
                {
                    return RedirectToAction("Deletedoctor");
                }
            }
        }
        [HttpPost]
        public IActionResult Deletepeople(SettingsPeople settingsPeople)
        {
            var person_id = HttpContext.Session.GetString("userId");
            Sql.Execute("DELETE from people where ID = @0", person_id);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult Deletemanager()
        {
            var id = HttpContext.Session.GetString("userId");
            var man_info = Sql.Read("SELECT name,sex,tel,work_unit,password from manager where id=@0", id);
            var MAN_name = new List<string>();
            var MAN_sex = new List<string>();
            var MAN_tel = new List<string>();
            var MAN_work_unit = new List<string>();
            var MAN_password = new List<string>();
            foreach (DataRow man in man_info)
            {
                MAN_name.Add(man[0].ToString());
                MAN_sex.Add(man[1].ToString());
                MAN_tel.Add(man[0].ToString());
                MAN_work_unit.Add(man[0].ToString());
                MAN_password.Add(man[0].ToString());
            }
            var model = new Settingsmanager
            {
                man_id = id,
                name = MAN_name[0],
                sex = MAN_sex[0],
                tel = MAN_tel[0],
                work_unit = MAN_work_unit[0],
                password = MAN_work_unit[0],
            };
            return View(model);
        }
        [HttpPost]
        public IActionResult Deletemanager(Settingsmanager settingsmanager)
        {
            var person_id = HttpContext.Session.GetString("userId");
            Sql.Execute("DELETE from manager where ID = @0", person_id);
            return RedirectToAction("Index");
        }
        public IActionResult Deletedoctor()
        {
            var id = HttpContext.Session.GetString("userId");
            var doc_info = Sql.Read("SELECT name,hospital_name,password from doctor where id=@0", id);
            var DOC_hos_name = new List<string>();
            var DOC_name = new List<string>();
            var DOC_password = new List<string>();
            foreach (DataRow doci in doc_info)
            {
                DOC_name.Add(doci[0].ToString());
                DOC_hos_name.Add(doci[1].ToString());
                DOC_password.Add(doci[2].ToString());
            }
            var model = new Settingsdoctor
            {
                doc_id = id,
                name = DOC_name[0],
                hos_name = DOC_hos_name[0],
                password = DOC_password[0],
            };
            return View(model);
        }
        public IActionResult Deletedoctor(Settingsdoctor settingsdoctor)
        {
            var doc_id = HttpContext.Session.GetString("userId");
            Sql.Execute("DELETE from doctor where ID = @0", doc_id);
            return RedirectToAction("Index");
        }
    }
}
