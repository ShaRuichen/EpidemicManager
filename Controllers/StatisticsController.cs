using System.Collections.Generic;
using System.Data;

using Microsoft.AspNetCore.Mvc;
using EpidemicManager.Models;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNetCore.Http;
using System;

namespace EpidemicManager.Controllers
{
    public class StatisticsController : Controller
    {

        public IActionResult Index()
        {
            var session = HttpContext.Session;
            var userKind = session.GetString("userKind");
            var userId = session.GetString("userId");
            if (userId == null)
            {
                return RedirectPermanent("/login?path=statistics");
            }

            return View();
        }

        public IActionResult Read(string id)
        {
            //if (id != null)
            //{
            //    Sql.Execute("INSERT INTO people VALUES(@0, 'name', 'address', 'tel', 'sex', 'password')", id);
            //}
            var ID = Sql.Read("SELECT ID FROM travel_info");
            var Date = Sql.Read("SELECT date FROM travel_info");
            var Time = Sql.Read("SELECT time FROM travel_info");
            var Site = Sql.Read("SELECT site FROM travel_info");
            var IDlist = new List<string>();
            var datelist = new List<string>();
            var timelist = new List<string>();
            var sitelist = new List<string>();

            foreach (DataRow statistics in ID)
            {
                IDlist.Add(statistics[0].ToString());
            }
            foreach (DataRow statistics in Date)
            {
                datelist.Add(Convert.ToDateTime(statistics[0]).ToString("yyyy-MM-dd"));
            }
            foreach (DataRow statistics in Time)
            {
                timelist.Add(statistics[0].ToString());
            }
            foreach (DataRow statistics in Site)
            {
                sitelist.Add(statistics[0].ToString());
            }

            var model = new StatisticsModel
            {
                ID = IDlist,
                Date = datelist,
                Time = timelist,
                Site = sitelist
            };
            int num = 0;
            foreach (var nums in model.ID)
            {
                num++;
            }
            model.Info_num = num;
            return View(model);
        }

        public IActionResult PeopleAdd(string site)
        {
            var session = HttpContext.Session;
            var userKind = session.GetString("userKind");
            var userId = session.GetString("userId");
            var now = DateTime.Now;
            var time = now.ToLongTimeString();
            var date = now.ToString("yyyy-MM-dd");
            if (userId != null && userKind == "people")
            {
                Sql.Execute("INSERT INTO travel_info VALUES(@0, @1, @2, @3)", userId, date, time, site);
            }
            else
            {
                return Redirect($"/login?path=/statistics/peopleadd?site={site}");
            }
            ViewBag.Site = site;

            return View();
        }

        public IActionResult PeopleAdd_info()
        {
            var session = HttpContext.Session;
            var userKind = session.GetString("userKind");
            var userId = session.GetString("userId");

            Models.PepModel PeopleInfo = new PepModel();
            PeopleInfo.ID = Request.Form["MID"];
            PeopleInfo.Site = Request.Form["Msite"];
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            string time = DateTime.Now.ToString("T");
            Sql.Execute("INSERT INTO travel_info VALUES(@0, @1, @2, @3)", PeopleInfo.ID, date, time, PeopleInfo.Site);

            return View(PeopleInfo);
        }

        public IActionResult ShowQRcode()
        {
            var session = HttpContext.Session;
            var userKind = session.GetString("userKind");
            var userId = session.GetString("userId");
            if (userKind != "manager")
            {
                return View("Warning");
            }
            var model = new QRcodeModel
            {
                ID = userId
            };

            return View(model);
        }

    }
}
