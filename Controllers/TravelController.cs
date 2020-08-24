using System.Collections.Generic;
using System.Data;

using Microsoft.AspNetCore.Mvc;
using EpidemicManager.Models;
using Microsoft.AspNetCore.Http;
using System.Diagnostics;
using System.Threading;
using System;

namespace EpidemicManager.Controllers
{
    public class TravelController : Controller
    {

        public IActionResult Index()
        {
            var session = HttpContext.Session;
            var userKind = session.GetString("userKind");
            var userId = session.GetString("userId");
            if(userId==null)
            {
                return RedirectPermanent("/login?path=travel");
            }
            return View();
        }
        public IActionResult PeopleAdd()
        {
            var session = HttpContext.Session;
            var userKind = session.GetString("userKind");
            var userId = session.GetString("userId");
            var model = new Mamodel
            {
                maID = userId
            };
            return View(model);
        }
        public IActionResult ShowQRcode()
        {
            var session = HttpContext.Session;
            var userKind = session.GetString("userKind");
            var userId = session.GetString("userId");
            var realpresite = session.GetString("realpresite");
            var model = new Mamodel
            {
                maID = userId,
                site = realpresite
            };
            if (realpresite == null)
            {
                return RedirectPermanent("/Travel/Addpresite");
            }
            else
            {
                return View(model);
            }
        }
        public IActionResult Addpresite()
        {
            var session = HttpContext.Session;
            var userKind = session.GetString("userKind");
            var userId = session.GetString("userId");
            var realpresite = session.GetString("realpresite");
            var model = new Mamodel
            {
                maID = userId,
                site = realpresite
            };
            return View(model);
        }
        public IActionResult AddtravelInfo()
        {
            Models.Trmodel travelinfo = new Trmodel();

            travelinfo.ID = Request.Form["ID"];
            travelinfo.site = Request.Form["site"];
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("T");
            Sql.Execute("INSERT INTO travel_info VALUES(@0, @1, @2, @3)", travelinfo.ID, date, time, travelinfo.site);
            return View(travelinfo);
            
        }
        public IActionResult ManagerAdd(string id)
        {
            var model = new Mamodel
            {
                maID = id
            };
            return View("ManagerAdd_info");
        }
        public IActionResult ManagerAdd_info(string id,string site)
        {
            var session = HttpContext.Session;
            string site2 = session.GetString("realpresite");
            if (site == null)
            {
                string realpresite = Request.Form["realpresite"];
                HttpContext.Session.SetString("realpresite", realpresite);
                return View("index");
            }
            Models.Trmodel Mtravelinfo = new Trmodel();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("T");
            Sql.Execute("INSERT INTO travel_info VALUES(@0, @1, @2, @3)", id, date, time, site);
            return View(Mtravelinfo);

        }
        public IActionResult Show(string id)
        {
            if (id != null)
            {
                Sql.Execute("INSERT INTO people VALUES(@0, 'name', 'address', 'tel', 'sex', 'password')", id);
            }
            var ID = Sql.Read("SELECT ID FROM travel_info");
            var date = Sql.Read("SELECT date FROM travel_info"); 
            var time = Sql.Read("SELECT time FROM travel_info"); 
            var site = Sql.Read("SELECT site FROM travel_info");
            var IDlist = new List<string>();
            var datelist = new List<string>();
            var timelist = new List<string>();
            var sitelist = new List<string>();
            foreach (DataRow travel in ID)
            {
                IDlist.Add(travel[0].ToString());
            }
            foreach (DataRow travel in date)
            {
                datelist.Add(Convert.ToDateTime(travel[0]).ToString("yyyy-MM-dd"));
            }
            foreach (DataRow travel in time)
            {
                timelist.Add(travel[0].ToString());
            }
            foreach (DataRow travel in site)
            {
                sitelist.Add(travel[0].ToString());
            }
            var model = new Travelmodel
            {
                Ids = IDlist,
                Dates=datelist,
                Times=timelist,
                Sites=sitelist
            };
            int num = 0;
            foreach(var nums in model.Ids)
            {
                num++;
            }
            model.info_num = num;
            var fine =new List<Travelmodel>();
            ViewBag.Huhaha = "旅行ID 旅行日期 旅行时间 旅行地点";
            return View(model);
        }
    }
}
