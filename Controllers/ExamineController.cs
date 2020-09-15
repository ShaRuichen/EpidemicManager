﻿using EpidemicManager;
using EpidemicManager.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Data;

public class ExamineController : Controller
{


    public IActionResult Index_doctor()
    {

        if (HttpContext.Session.GetString("userKind") != "doctor")
        {
            return RedirectToAction("Index", "Login", new { path = "/Examine/Index_doctor" });
        }
        return View();
    }
    [HttpGet]
    public IActionResult Create()
    {
        if (HttpContext.Session.GetString("userKind") != "doctor")
        {
            return RedirectToAction("Index", "Login", new { path = "Examine /Create" });
        }
        return View();
    }
    [HttpPost]
    public string Create_()
    {
        ExamineModel m = new ExamineModel();
        m.ID_doctor = HttpContext.Session.GetString("userId");
        m.ID_patient = Request.Form["patient_id"];
        m.detail = Request.Form["detail"];
        m.title = Request.Form["title"];
        m.date = DateTime.Now.ToString("hh:mm:ss");
        m.time = DateTime.Now.ToString("T");
        var i = 0;

        var p = Sql.Read("SELECT name FROM patient where ID=@0", m.ID_patient);
        foreach(DataRow n in p)
        {
            i++;
        }
        if (i == 0)
        {
            
            return "病人ID输入有误";
            
        }
        Sql.Execute("INSERT INTO examine_repo(date,time,patient_id,doctor_id,title,detail)  VALUES(@0,@1,@2,@3,@4,@5)", m.date, m.time, m.ID_patient, m.ID_doctor, m.title, m.detail);
        return "创建成功";

    }

    public IActionResult Read_index()
    {
        if (HttpContext.Session.GetString("userKind") != "doctor")
        {
            return RedirectToAction("Index", "Login", new { path = "Exmaine/Read_index" });
        }
        var report = Sql.Read("SELECT report_id,patient_id,title,date FROM examine_repo ");
        ExamineIndexModel m = new ExamineIndexModel();
        var list_r = new List<string>();
        var list_p = new List<string>();
        var list_t = new List<string>();
        var list_n = new List<string>();
        var list_d = new List<string>();
        var i = 0;
        foreach (DataRow r in report)
        {
            list_r.Add(r[0].ToString());
            list_p.Add(r[1].ToString());
            list_t.Add(r[2].ToString());
            list_d.Add(r[3].ToString());
            var name = Sql.Read("SELECT name FROM patient where ID=@0", r[1]);
            foreach (DataRow n in name)
            {
                list_n.Add(n[0].ToString());
            }
            i++;
        }
        m.title = list_t;
        m.report = list_r;
        m.name_patient = list_n;
        m.ID_patient = list_p;
        m.date = list_d;
        m.n = i;

        return View(m);
    }
    public IActionResult Write_index()
    {
        if (HttpContext.Session.GetString("userKind") != "doctor")
        {
            return RedirectToAction("Index", "Login", new { path = "Examine/Write_index" });
        }
        var report = Sql.Read("SELECT report_id,patient_id,title,date FROM examine_repo ");
        ExamineIndexModel m = new ExamineIndexModel();
        var list_r = new List<string>();
        var list_p = new List<string>();
        var list_t = new List<string>();
        var list_n = new List<string>();
        var list_d = new List<string>();
        var i = 0;
        foreach (DataRow r in report)
        {
            list_r.Add(r[0].ToString());
            list_p.Add(r[1].ToString());
            list_t.Add(r[2].ToString());
            list_d.Add(r[3].ToString());
            var name = Sql.Read("SELECT name FROM patient where ID=@0", r[1]);
            foreach (DataRow n in name)
            {
                list_n.Add(n[0].ToString());
                i++;
            }
        }
        m.title = list_t;
        m.report = list_r;
        m.name_patient = list_n;
        m.ID_patient = list_p;
        m.date = list_d;
        m.n = i;

        return View(m);
    }

    public IActionResult Index_patient()//病人Id
    {
        if (HttpContext.Session.GetString("userKind") != "patient")
        {
            return RedirectToAction("Index", "Login", new { path = "Examine/Index_patient" });
        }
        var id_p = HttpContext.Session.GetString("userId");
        var report = Sql.Read("SELECT report_id,title ,date FROM examine_repo WHERE patient_id=@0", id_p);
        var list_r = new List<string>();
        var list_t = new List<string>();
        var list_d = new List<string>();
        var i = 0;
        foreach (DataRow r in report)
        {
            list_r.Add(r[0].ToString());
            list_t.Add(r[1].ToString());
            list_d.Add(r[2].ToString());
            i++;
        }

        var model = new ExamineIndexModel
        {
            report = list_r,
            title = list_t,
            date=list_d,
            n = i,
        };

        return View(model);
    }
    [HttpPost]
    public IActionResult Read(ExamineModel M)
    {
        var id_r = Convert.ToInt32(Request.Form["report_id"]);
        //var id_r = request("report_id");
        var report = Sql.Read("SELECT patient_id,doctor_id,detail,time,date,title FROM examine_repo WHERE report_id=@0", id_r);
        var id_p = "M";
        var id_d = "M";
        var d = "M";
        var t = "M";
        var date = "M";
        var title = "M";
        foreach (DataRow r in report)
        {
            id_p = r[0].ToString();
            id_d = r[1].ToString();
            d = r[2].ToString();
            t = r[3].ToString();
            date = r[4].ToString();
            title = r[5].ToString();
        }

        var namep = Sql.Read("SELECT name FROM patient where ID=@0", id_p);
        var n_p = "M";
        foreach (DataRow r in namep)
        {
            n_p = r[0].ToString();
        }
        var named = Sql.Read("SELECT name FROM doctor where ID=@0", id_d);
        var n_d = "M";
        foreach (DataRow r in named)
        {
             n_d = r[0].ToString();
        }

        var model = new ExamineModel
        {

            ID_patient = id_p,
            name_patient = n_p,
            ID_doctor = id_d,
            name_doctor = n_d,
            detail = d,
            time = t,
            ID_report = id_r.ToString(),
            date = date,
            title = title,
        };

        return View(model);

    }




    [HttpPost]
    public IActionResult Write(ExamineModel M)
    {
        var id_r = Convert.ToInt32(Request.Form["report_id"]);
        //var id_r = request("report_id");
        var report = Sql.Read("SELECT patient_id,doctor_id,detail,time,date,title FROM examine_repo WHERE report_id=@0", id_r);
        var id_p = "M";
        var id_d = "M";
        var d = "M";
        var t = "M";
        var date = "M";
        var title = "M";
        foreach (DataRow r in report)
        {
            id_p = r[0].ToString();
            id_d = r[1].ToString();
            d = r[2].ToString();
            t = r[3].ToString();
            date = r[4].ToString();
            title = r[5].ToString();
        }

        var namep = Sql.Read("SELECT name FROM patient where ID=@0", id_p);
        var n_p = "M";
        foreach (DataRow r in namep)
        {
            n_p = r[0].ToString();
        }
        var named = Sql.Read("SELECT name FROM doctor where ID=@0", id_d);
        var n_d = "M";
        foreach (DataRow r in named)
        {
            n_d = r[0].ToString();
        }

        var model = new ExamineModel
        {

            ID_patient = id_p,
            name_patient = n_p,
            ID_doctor = id_d,
            name_doctor = n_d,
            detail = d,
            time = t,
            ID_report = id_r.ToString(),
            date = date,
            title = title,
        };
        return View(model);
    }

    [HttpPost]
    public string Write_(ExamineModel M)
    {
        ExamineModel m = new ExamineModel();
        m.ID_doctor = HttpContext.Session.GetString("userId");
        m.ID_patient = Request.Form["patient_id"];
        m.detail = Request.Form["detail"];
        m.title = Request.Form["title"];
        m.date = DateTime.Now.ToString("hh:mm:ss");
        m.time = DateTime.Now.ToString("T");

        m.ID_report = Request.Form["report_id"];
        var i = 0;

        var p = Sql.Read("SELECT name FROM patient where ID=@0", m.ID_patient);
        foreach (DataRow n in p)
        {
            i++;
        }
        if (i == 0)
        {

            return "病人ID输入有误";

        }
        Sql.Execute("INSERT INTO examine_repo(date,time,patient_id,doctor_id,title,detail)  VALUES(@0,@1,@2,@3,@4,@5)", m.date, m.time, m.ID_patient, m.ID_doctor, m.title, m.detail);
        return "修改成功";

    }
    

    [HttpPost]
    public IActionResult Read_index_()
    {
        var id_p = Request.Form["patient_id"];
        
        var report = Sql.Read("SELECT report_id,patient_id,title ,date FROM examine_repo WHERE patient_id=@0", id_p);
        ExamineIndexModel m = new ExamineIndexModel();
        var list_r = new List<string>();
        var list_p = new List<string>();
        var list_t = new List<string>();
        var list_n = new List<string>();
        var list_d = new List<string>();
        var i = 0;
        foreach (DataRow r in report)
        {
            list_r.Add(r[0].ToString());
            list_p.Add(r[1].ToString());
            list_t.Add(r[2].ToString());
            list_d.Add(r[3].ToString());
            var name = Sql.Read("SELECT name FROM patient where ID=@0", r[1]);
            foreach (DataRow n in name)
            {
                list_n.Add(n[0].ToString());
            }
            i++;
        }
        m.title = list_t;
        m.report = list_r;
        m.name_patient = list_n;
        m.ID_patient = list_p;
        m.date = list_d;
        m.n = i;
        return View(m);
    }
    public IActionResult Write_index_()
    {
        var id_p = Request.Form["patient_id"];
        
        var report = Sql.Read("SELECT report_id,patient_id,title,date  FROM examine_repo WHERE patient_id=@0", id_p);
        ExamineIndexModel m = new ExamineIndexModel();
        var list_r = new List<string>();
        var list_p = new List<string>();
        var list_t = new List<string>();
        var list_n = new List<string>();
        var list_d = new List<string>();
        var i = 0;
        foreach (DataRow r in report)
        {
            list_r.Add(r[0].ToString());
            list_p.Add(r[1].ToString());
            list_t.Add(r[2].ToString());
            list_d.Add(r[3].ToString());
            var name = Sql.Read("SELECT name FROM patient where ID=@0", r[1]);
            foreach (DataRow n in name)
            {
                list_n.Add(n[0].ToString());
            }
            i++;
        }
        m.title = list_t;
        m.report = list_r;
        m.name_patient = list_n;
        m.ID_patient = list_p;
        m.date = list_d;
        m.n = i;
        return View(m);
    }
}
