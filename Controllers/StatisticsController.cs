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
        /*
        public IActionResult Index()
        {
            var session = HttpContext.Session;
            var userKind = session.GetString("userKind");
            var userId = session.GetString("userId");
            if(userId==null)
            {
                return RedirectPermanent("/login?path=statistics");
            }

            return View();
        }

        public IActionResult Read()
        {
            if(id!=null)
            {
                Sql.Execute("INSERT INTO people VALUES(@0, 'name', 'address', 'tel', 'sex', 'password')", id);
            }
            var ID = Sql.Read("SELECT ID FROM travel_info");
            var Date = Sql.Read("SELECT date FROM travel_info");
            var Time = Sql.Read("SELECT time FROM travel_info");
            var Site = Sql.Read("SELECT site FROM travel_info");

            foreach(DataRow travel in ID)
            {
                IDlist.Add(travel[0].ToString);
            }
            foreach (DataRow travel in date)
            {
                datelist.Add(travel[0].ToString);
            }
            foreach (DataRow travel in time)
            {
                timelist.Add(travel[0].ToString);
            }
            foreach (DataRow travel in site)
            {
                sitelist.Add(travel[0].ToString);
            }

            var model = new StatisticsModel
            {
                ID = IDlist,
                Date = datelist,
                Time = timelist,
                Site = sitelist
            };

            return View(model);
        }

        public IActionResult PeopleAdd()
        {
            var session = HttpContext.Session;
            var userKind = session.GetString("userKind");
            var userId = session.GetString("userId");
            var model = new PepQRcodeModel
            {
                PepID = userId
            };

            return View(model);
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
            var model = new PepQRcodeModel
            {
                PepID = userId
            };

            return View(model);
        }
        */
    }
}
