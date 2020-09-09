
using System.Collections.Generic;
using System.Data;

using Microsoft.AspNetCore.Mvc;
using EpidemicManager.Models;
using Microsoft.AspNetCore.Http;
using Org.BouncyCastle.Asn1.X509.SigI;
using System;

namespace EpidemicManager.Controllers
{
    public class TreatmentController : Controller
    {
       
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Insert()
        {
            return View();
        }


        [HttpPost]
        public int Click2( string pat_id, string med, string detail)
        {
            string date = DateTime.Now.ToShortDateString().ToString();
            string time = DateTime.Now.ToShortTimeString().ToString();
            var session = HttpContext.Session;
            var userkind = session.GetString("userKind");
            var doc_id = session.GetString("userId");
            if (userkind == "doctor")
            {
                var plan = Sql.Read("SELECT * FROM patient WHERE ID ='" + pat_id + "'");
                if (plan.Count != 0)
                {
                    Sql.Execute("INSERT INTO treat_plan(doctor_id, patient_id, date, time, medicine, details) VALUES( @0, @1, @2, @3, @4, @5)", doc_id, pat_id, date, time, med, detail);
                    return 1;
                }
                else
                {
                    return 2;//违反
                }
               
            }
            else
            {
                return 0;
            }
                
        }


        public IActionResult Check()
        {
            var session = HttpContext.Session;
            var userkind = session.GetString("userKind");
            if (userkind == "doctor")
            {
                var plan = Sql.Read("SELECT * FROM treat_plan");
                var list2 = new List<string>();
                foreach (DataRow person in plan)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        list2.Add(person[i].ToString());
                    }
                }
                

                var model = new TreatmentModel
                {
                    IDs = list2,
                };

                return View(model);
            }
            else
            {
                var model = new TreatmentModel
                {
                    IDs = new List<string>(),
                };
                return View(model);
            }
        }
        public IActionResult Findout(string plan_id2)
        {
            var plan=Sql.Read("SELECT * FROM treat_plan WHERE plan_id ='" + plan_id2 + "'");
            int ji = plan.Count;
            var list2 = new List<string>();
            foreach (DataRow person in plan)
            {
                for (int i = 0; i < 7; i++)
                {
                    list2.Add(person[i].ToString());
                }
            }


            var model = new TreatmentModel
            {
                IDs = list2,
            };

            return View(model);
    }
        public int Find(string plan_id2)
        {
            var t = 0;
            var plan = Sql.Read("SELECT plan_id FROM treat_plan WHERE plan_id ='" + plan_id2 + "'");
            
            foreach (DataRow person in plan)
            {
                t++;
            }
            if (t==0) { return 1; }
            else { return 2; }
        }

        [HttpPost]

        public List<string> Give(string plan_id2)
        {
            List<string> aa;
            aa = new List<string>();
            var plan = Sql.Read("SELECT * FROM treat_plan WHERE plan_id='" + plan_id2 + "'");
            foreach(DataRow person in plan)
            {
                aa.Add(person[3].ToString());
                aa.Add(person[4].ToString());
                aa.Add(person[5].ToString());
                aa.Add(person[6].ToString());
            }
            return aa;
        }
        public int Update(string plan_id, string med, string det)
        {
            string date = DateTime.Now.ToShortDateString().ToString();
            string time = DateTime.Now.ToShortTimeString().ToString();
            Sql.Execute("UPDATE treat_plan SET time=@0 WHERE plan_id=@1", time, plan_id);
            Sql.Execute("UPDATE treat_plan SET date=@0 WHERE plan_id=@1", date, plan_id);
            Sql.Execute("UPDATE treat_plan SET medicine=@0 WHERE plan_id=@1",med,plan_id);
            Sql.Execute("UPDATE treat_plan SET details=@0 WHERE plan_id=@1", det, plan_id);
            return 1;
        }
        public int CheckLogin()
        {
            var session = HttpContext.Session;
            var userkind = session.GetString("userKind");
            if (userkind == "null")
            {
                return 0;
            }
            else if (userkind != "doctor")
            {
                return 1;
            }
            else
            {
                return 2;
            }
        }
    }
}

