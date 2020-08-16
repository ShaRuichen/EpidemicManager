using System.Collections.Generic;
using System.Data;

using Microsoft.AspNetCore.Mvc;
using EpidemicManager.Models;

namespace EpidemicManager.Controllers
{
    public class TravelController : Controller
    {

        public IActionResult Index()
        {
            var people = Sql.Read("SELECT id FROM people");
            var list = new List<string>();
            foreach (DataRow person in people)
            {
                list.Add(person[0].ToString());
            }
            var model = new Travelmodel
            {
                Ids = list,
            };

            return View(model);
        }
        public IActionResult AddtravelInfo()
        {
            Models.Trmodel travelinfo = new Trmodel();

            travelinfo.ID = Request.Form["ID"];
            travelinfo.date = Request.Form["date"];
            travelinfo.time = Request.Form["time"];
            travelinfo.site = Request.Form["site"];
            Sql.Execute("INSERT INTO travel_info VALUES(@0, @1, @2, @3)", travelinfo.ID, travelinfo.date, travelinfo.time, travelinfo.site);
            return View(travelinfo);
            
        }
        public IActionResult Show(string id)
        {
            if (id != null)
            {
                Sql.Execute("INSERT INTO people VALUES(@0, 'name', 'address', 'tel', 'sex', 'password')", id);
            }
            var all = Sql.Read("SELECT * FROM travel_info");
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
                datelist.Add(travel[0].ToString());
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
            model.Id = num;
            var fine =new List<Travelmodel>();
            ViewBag.Huhaha = "旅行ID 旅行日期 旅行时间 旅行地点";
            return View(model);
        }
        [HttpPost]
        public JsonResult Click(string name, int number)
        {
            string str = Request.Form["iaa"].ToString();
            Sql.Execute("INSERT INTO people VALUES(@0, 'name', 'address', 'tel', 'sex', 'password')", str);
            return Json(new
            {
                name,
                num = number + 1,
            });
        }
    }
}
