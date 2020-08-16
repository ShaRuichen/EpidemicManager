using System.Collections.Generic;
using System.Data;
using Microsoft.AspNetCore.Mvc;
using EpidemicManager.Models;
using System.Diagnostics;
using System.Threading;

namespace EpidemicManager.Controllers
{
    public class ExamineController : Controller
    {
        public IActionResult Index(string id_p)//病人Id
        {
            TestSql();

            var report = Sql.Read("SELECT id_report FROM examine_repo WHERE id_patient=@0",id_p);
            var list = new List<string>();
            foreach (DataRow r in report)
            {
                list.Add(r[0].ToString());
            }

            var name = Sql.Read("SELECT name FROM patient where ID=@0",id_p);
            var n;
            foreach (DataRow r in name)
            {
                n=r[0].ToString();
            }

            

            var model = new ExamineIndexModel
            {
                report = list,
                ID_patient=id_p,
                name_patient=n,
            };

            return View(model);
        
        }
        public IActionResult Read(string id_r)
        {
            TestSql();

            var report = Sql.Read("SELECT id_patient,id_doctor,detail,time FROM examine_repo WHERE id_report=@0",id_r);
            var id_p;
            var id_d;
            var d;
            var t;
            foreach (DataRow r in report)
            {
                id_p=r[0].ToString();
                id_d=r[1].ToString();
                d=r[2].ToString();
                t=r[3].ToString();
            }

            var namep = Sql.Read("SELECT name FROM patient where ID=@0",id_p);
            var n_p;
            foreach (DataRow r in namep)
            {
                n_p=r[0].ToString();
            }
            var named = Sql.Read("SELECT name FROM doctor where ID=@0",id_d);
            var n_d;
            foreach (DataRow r in named)
            {
                n_d=r[0].ToString();
            }
         
            var model = new ExamineModel
            {
                
                ID_patient=id_p,
                name_patient=n_p,
                ID_doctor=id_d,
                name_doctor=n_p,
                detail=d,
                time=t,
                ID_report=id_r,
            };

            return View(model);
        
        }

        public IActionResult Write(string d,string t, string id_r)
        {
            TestSql();
            if (d != null&&t!=null)
            {
                Sql.Execute("UPDATE  examine_repo set detail=@0,time=@1 WHERE report_id=@2", d,t,id_r);
            }
            else
            {
                //提示不合法
            }
            var model = new ExamineModel
            {
                ID_report = id_r ?? string.Empty,
            };
            return View(model);
            
        }
         public IActionResult Create(string d,string t,string id_p,string id_d)
        {
            TestSql();
            var id_r=0;
            var report = Sql.Read("SELECT * FROM examine_repo");
            foreach (DataRow r in report)
            {
                id_r++;
            }
            if (d != null &&t != null &&id_p != null &&id_d != null )
            {
                Sql.Execute("INSERT INTO examine_repo VALUES(@0,@1, @2,@3,@4)",id_r, t,d,id_d,id_p);
            }
            else
            {
                //提示不合法
            }

            var model = new ExamineModel
            {
                ID_report = id_r ?? string.Empty,
            };
            return View(model);
        }
        [HttpPost]
        public JsonResult Click(string reportname, int number)
        {
            return Json(new
            {
                reportname,
                num = number + 1,
            });
        }

        [Conditional("DebugSql")]
        private void TestSql()
        {
            for (var i = 0; i < 40; i++)
            {
                if (i == 20) Thread.Sleep(500);
                var thread = new Thread(() =>
                {
                    Sql.Read("SELECT id FROM people");
                });
                thread.Start();
            }
        }
    }
}
