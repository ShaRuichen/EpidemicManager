
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
        public IActionResult NormCheck()
        {
            var session = HttpContext.Session;
            var pat_id = session.GetString("userId");
            var list1 = new List<string>();
            if (session.GetString("isPatient") == "True")
            {
                var plan = Sql.Read("SELECT * FROM treat_plan WHERE patient_id ='" + pat_id + "'");
                foreach (DataRow person in plan)
                {
                    for (int i = 0; i < 7; i++)
                    {
                        list1.Add(person[i].ToString());
                    }
                }
            }


            var model = new TreatmentModel
            {
                IDs = list1,
            };

            return View(model);
        }

        [HttpPost]
        public int Click2( string pat_id, string med, string detail)
        {
            int aa;
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
                    aa = 1;
                }
                else
                {
                    aa = 2;//违反
                }
               
            }
            else
            {
                aa = 0;
            }
            return aa;
                
        }


        public IActionResult Check(string patient)
        {
            var session = HttpContext.Session;
            var userkind = session.GetString("userKind");
            if (userkind == "doctor")
            {

                var list2 = new List<string>();
                if (patient == "null")
                {
                    
                    var plan = Sql.Read("SELECT * FROM treat_plan");
                    foreach (DataRow person in plan)
                    {
                        for (int i = 0; i < 7; i++)
                        {
                            list2.Add(person[i].ToString());
                        }
                    }
                }
                else
                {
                    var plan = Sql.Read("SELECT * FROM treat_plan WHERE patient_id='"+patient+"' ");
                    if (plan.Count != 0)
                    {
                        foreach (DataRow person in plan)
                        {
                            for (int i = 0; i < 7; i++)
                            {
                                list2.Add(person[i].ToString());
                            }
                        }
                    }
                }
                for (int i = 0; i < list2.Count; i++)
                {
                    if ((i + 4) % 7 == 0)
                    {
                        list2[i] = CutString(list2[i], 11, false);
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
            for(int i = 0; i < list2.Count; i++)
            {
                if ((i + 4) % 7 == 0)
                {
                    list2[i] = CutString(list2[i], 11, false);
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
            var plan = Sql.Read("SELECT plan_id FROM treat_plan WHERE patient_id ='" + plan_id2 + "'");
            
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
        public IActionResult Findout2(string plan_id2)
        {
            var plan = Sql.Read("SELECT * FROM treat_plan WHERE plan_id ='" + plan_id2 + "'");
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
        public static string CutString(string str, int len, bool flag)
        {
            string _outString = "";
            int _len = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (Char.ConvertToUtf32(str, i) >= Convert.ToInt32("4e00", 16) && Char.ConvertToUtf32(str, i) <= Convert.ToInt32("9fff", 16))
                {
                    _len += 2;
                    if (_len > len)//截取的长度若是最后一个占两个字节，则不截取
                    {
                        break;
                    }
                }
                else
                {
                    _len++;
                }


                try
                {
                    _outString += str.Substring(i, 1);
                }
                catch
                {
                    break;
                }
                if (_len >= len)
                {
                    break;
                }
            }
            if (str != _outString && flag == true)//判断是否添加省略号
            {
                _outString += "...";
            }
            return _outString;
        }
    }

}

