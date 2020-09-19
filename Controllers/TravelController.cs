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

        
        public IActionResult PeopleAdd()
        {
            var session = HttpContext.Session;
            var userKind = session.GetString("userKind");
            var userId = session.GetString("userId");
            if (userId == null)
            {
                return RedirectPermanent("/login?path=travel/PeopleAdd");
            }
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
            if (userId == null)
            {
                return RedirectPermanent("/login?path=travel/ShowQRcode");
            }
            var model = new Mamodel
            {
                maID = userId,
                site = realpresite
            };
            return View(model);
        }
        public IActionResult Addpresite()
        {
            var session = HttpContext.Session;
            var userKind = session.GetString("userKind");
            var userId = session.GetString("userId");
            if (userId == null)
            {
                return RedirectPermanent("/login?path=travel/Addpresite");
            }
            if (userKind != "manager")
            {
                return View("notmanager");
            }
            var model = new Mamodel
            {
                maID = userId,
                site = userKind
            };
            return View(model);
        }
        public IActionResult AddtravelInfo()
        {
            Models.Trmodel travelinfo = new Trmodel();
            var session = HttpContext.Session;
            var userId = session.GetString("userId");
            travelinfo.site = Request.Form["site1"];
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("T");
            Sql.Execute("INSERT INTO travel_info VALUES(@0, @1, @2, @3)", userId, date, time, travelinfo.site);
            return RedirectToAction("Index", "Home");

        }
        public IActionResult Setpresite()
        {
            var session = HttpContext.Session;
            string realpresite = Request.Form["realpresite"];
            HttpContext.Session.SetString("realpresite", realpresite);
            return RedirectToAction("Index", "Home");
        }
        public IActionResult ManagerAdd_info(string id)
        {
            var session = HttpContext.Session;
            var site = session.GetString("realpresite");
            if (site == null)
            {
                return RedirectPermanent("/Travel/Addpresite");
            }
            Models.Trmodel Mtravelinfo = new Trmodel();
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("T");
            Sql.Execute("INSERT INTO travel_info VALUES(@0, @1, @2, @3)", id, date, time, site);
            return RedirectToAction("Index", "Home");

        }
        public IActionResult Show()
        {
            var session = HttpContext.Session;
            var userKind = session.GetString("userKind");
            var userId = session.GetString("userId");
            if (userId == null)
            {
                return RedirectPermanent("/login?path=travel/Show");
            }
            var ID = Sql.Read("SELECT ID FROM travel_info WHERE ID=@0",userId);
            var date = Sql.Read("SELECT date FROM travel_info WHERE ID=@0", userId); 
            var time = Sql.Read("SELECT time FROM travel_info WHERE ID=@0", userId); 
            var site = Sql.Read("SELECT site FROM travel_info WHERE ID=@0", userId);
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
            return View(model);
        }
    }
}
