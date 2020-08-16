using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Data;
using System;
using EpidemicManager.Models;

public struct Dongroupmon
{
    public string id;
    public string date;
    public string time;
    public string number;
};

public struct Dongroupmat
{
    public string id;
    public string date;
    public string time;
    public string type;
    public string amount;
};

public struct Dongroupdocmon
{
    public string id;
    public string datet;
    public string timet;
    public string dated;
    public string timed;
    public string number;
};

public struct Dongroupdocmat
{
    public string id;
    public string datet;
    public string timet;
    public string dated;
    public string timed;
    public string type;
    public string amount;
};

namespace EpidemicManager.Controllers
{
    public class DistributionController : Controller
    {

        public IActionResult Index()
        {
            var model = new DistributionModel
            {

            };
            return View(model);
        }

        public IActionResult Doctor()
        {
            var docid = 654321;
            var donsmonid = Sql.Read("SELECT donate_id,date,time FROM distribute WHERE hospital_name=(SELECT hospital_name FROM doctor WHERE ID=@0)",docid);
            var list = new List<Dongroupdocmon>();
            foreach (DataRow don in donsmonid)
            {
                var dons1 = Sql.Read("SELECT donate_id,date,time,number FROM donate_money WHERE donate_id=@0", don[0]);

                foreach (DataRow don01 in dons1)
                {
                    Dongroupdocmon temp;
                    temp.id = don01[0].ToString();
                    temp.dated = Convert.ToDateTime(don[1]).ToString("yyyy-MM-dd");
                    temp.timed = don[2].ToString();
                    temp.datet = Convert.ToDateTime(don01[1]).ToString("yyyy-MM-dd");
                    temp.timet = don01[2].ToString();
                    temp.number = don01[3].ToString();
                    list.Add(temp);
                }

            }


            var list02 = new List<Dongroupdocmat>();
            foreach (DataRow don in donsmonid)
            {
                var dons1 = Sql.Read("SELECT donate_id,date,time,type,amount FROM donate_material WHERE donate_id=@0", don[0]);

                foreach (DataRow don01 in dons1)
                {
                    Dongroupdocmat temp;
                    temp.id = don01[0].ToString();
                    temp.dated = Convert.ToDateTime(don[1]).ToString("yyyy-MM-dd");
                    temp.timed = don[2].ToString();
                    temp.datet = Convert.ToDateTime(don01[1]).ToString("yyyy-MM-dd");
                    temp.timet = don01[2].ToString();
                    temp.type = don01[3].ToString();
                    temp.amount = don01[4].ToString();
                    list02.Add(temp);
                }

            }



            var model = new DistributionModel
            {
                Idsdocmon = list,
                Idsdocmat = list02,
            };
            return View(model);
        }

        public IActionResult Manager()
        {
            //for(int i = 300; i < 310; i++)
            //{
            //    sql.Execute("INSERT INTO donate_money VALUES(@0, @0, @0, '0')",i);
            //}

            //for (int m = 400; m < 420; m++)
            //{
            //    sql.Execute("INSERT INTO donate_material VALUES(@0, @0, @0, @0,'0')", m);
            //}


            var dons1 = Sql.Read("SELECT donate_id,date,time,number FROM donate_money WHERE is_destributed=0");
            var list = new List<Dongroupmon>();
            foreach (DataRow don1 in dons1)
            {
                Dongroupmon temp;
                temp.id = don1[0].ToString();
                temp.date = Convert.ToDateTime(don1[1]).ToString("yyyy-MM-dd");
                temp.time = don1[2].ToString();
                temp.number = don1[3].ToString();
                list.Add(temp);
            }


            var dons2 = Sql.Read("SELECT donate_id,date,time,type,amount FROM donate_material WHERE is_distributed=0");
            var list02 = new List<Dongroupmat>();
            foreach (DataRow don1 in dons2)
            {
                Dongroupmat temp;
                temp.id = don1[0].ToString();
                temp.date = Convert.ToDateTime(don1[1]).ToString("yyyy-MM-dd");
                temp.time = don1[2].ToString();
                temp.type = don1[3].ToString();
                temp.amount = don1[4].ToString();
                list02.Add(temp);
            }



            //var dons2 = sql.Read("SELECT time FROM donate_money WHERE is_destributed=0");
            //foreach (DataRow don2 in dons2)
            //{
            //    list01.Add(don2[0].ToString());
            //}

            //var dons3 = sql.Read("SELECT number FROM donate_money WHERE is_destributed=0");

            //foreach (DataRow don3 in dons3)
            //{
            //    list02.Add(don3[0].ToString());
            //}



            var hospital = Sql.Read("SELECT hospital_name FROM hospital");
            var list2 = new List<string>();
            foreach (DataRow hospital1 in hospital)
            {
                list2.Add(hospital1[0].ToString());
            }

            var model = new DistributionModel
            {
                Idsmon = list,
                Idsmat = list02,
                Hops = list2,
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
        [HttpPost]
        public JsonResult ClickToDistributeMoney(string hospname, string donid)
        {
            if (donid != null && hospname != null)
            {
                Sql.Execute("INSERT INTO distribute VALUES(@0,@1,@2,@3,'123456')", donid, hospname, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToLongTimeString().ToString());
                Sql.Execute("UPDATE donate_money SET is_destributed=1 WHERE donate_id=@0", donid);
            }
            return Json(new
            {
                hospname,
            });
        }


        [HttpPost]
        public JsonResult ClickToDistributeMaterial(string hospname, string donid)
        {
            if (donid != null && hospname != null)
            {
                Sql.Execute("INSERT INTO distribute VALUES(@0,@1,@2,@3,'123456')", donid, hospname, DateTime.Now.ToString("yyyy-MM-dd"), DateTime.Now.ToLongTimeString().ToString());
                Sql.Execute("UPDATE donate_material SET is_distributed=1 WHERE donate_id=@0", donid);
            }
            return Json(new
            {
                hospname,
            });
        }
    }


}



