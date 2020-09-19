using Microsoft.AspNetCore.Mvc;
using EpidemicManager.Models;
using System.Collections.Generic;
using System.Data;
using System;
using Microsoft.AspNetCore.Http;
using System.Web;


namespace EpidemicManager.Controllers
{
    public class CommunicationController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]

        public IActionResult user()
        {
            return View();
        }
        [HttpPost]
        public IActionResult user_(UserCommunitionMobel user)
        {
            UserCommunitionMobel user_info = new UserCommunitionMobel();
            //user_info.user_name = Request.Form["user_name"];
            user_info.user_id = Request.Form["user_id"];
            string time = DateTime.Now.ToString("yyyy-MM-dd");
            user_info.question_id = Convert.ToInt32(Request.Form["question_id"]);
            user_info.question_content = Request.Form["question_content"];
            Sql.Execute("INSERT INTO question(user_id,time,question_id,detail)  VALUES(@0,@1,@2,@3)", user_info.user_id, time, user_info.question_id, user_info.question_content);
            return RedirectToAction("Index");
        }
        [HttpGet]

        public IActionResult doctor()
        {
            return View();
        }
        [HttpPost]
        public IActionResult doctor_(DocCommunicationMobel doctor)
        {
            DocCommunicationMobel doc_info = new DocCommunicationMobel();
            doc_info.question_type = Request.Form["question_type"];
            doc_info.doc_name = Request.Form["doc_name"];
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            doc_info.question_id = Convert.ToInt32(Request.Form["question_id"]);
            doc_info.question_content = Request.Form["question_content"];
            doc_info.answer = Request.Form["answer"];
            Sql.Execute("INSERT INTO doc_communication(question_type,doc_name,date,question_id,question_content,answer  VALUES(@0,@1,@2,@3,@4,@5)", doc_info.question_type, doc_info.doc_name, date, doc_info.question_id, doc_info.question_content, doc_info.answer);
            return RedirectToAction("Index");
        }
        [HttpGet]
        public IActionResult check()
        {
            return View();
        }
        [HttpPost]
        public IActionResult check_(CheckMobel check)
        {
            CheckMobel check_info = new CheckMobel();
            check_info.user_name = Request.Form["user_name"];
            check_info.user_idNumber = Request.Form["user_idNumber"];
            string date = DateTime.Now.ToString("yyyy-MM-dd");
            check_info.question_id = Convert.ToInt32(Request.Form["question_id"]);
            check_info.question_type = Request.Form["question_type"];
            check_info.answer = Request.Form["answer"];
            check_info.doc_name = Request.Form["doc_name"];
            Sql.Execute("INSERT INTO Check(user_name,user_idNumber,date,question_id,question_type,answer,doc_name  VALUES(@0,@1,@2,@3,@4,@5,@6)", check_info.user_name, check_info.user_idNumber, date, check_info.question_id, check_info.question_type, check_info.answer, check_info.doc_name);
            return RedirectToAction("Index");
        }

    }
}