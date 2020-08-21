using System.Collections.Generic;
using System.Data;

using Microsoft.AspNetCore.Mvc;
using EpidemicManager.Models;
using System.Diagnostics;
using System.Threading;

namespace EpidemicManager.Controllers
{
    public class QuestionnaireController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        public IActionResult Modify()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Read()
        {
            /*var ques_Id = Sql.Read("SELLECT q_num FROM questionnaire");
            var ques_Conten = Sql.Read("SELECET q_name FROM questionnaire");
            var ques_Type = Sql.Read("SELECET quesType FROM questionnaire");
            var op_Id = Sql.Read("SELECT optionID FROM question_option");
            var op_Conten=Sql.Read("SELECT optionID FROM question_option");
            var op_ques= Sql.Read("SELECT q_num FROM question_option");
            */
            var ques = Sql.Read("SELECT * FROM questionnaire");//查找所有问题
            var list = new List<Question>();
            var model = new QuestionnaireModel();//问卷模型
            foreach (DataRow question in ques)
            {
                var now = new Question();
                if(question[3].ToString()=="选择")//获取题目类型
                {
                    now.is_Bridge = true;
                    var opt = Sql.Read("SELECT optionID,optionContent FROM question_option WHERE q_num=@0", question[0]);
                    var xuanhao=new List<string>();
                    var xuanxiang = new List<string>();
                    foreach (DataRow op in opt)
                    {
                        xuanhao.Add(op[0].ToString());
                        xuanxiang.Add(op[1].ToString());
                    }
                    now.option_Content = xuanxiang;
                    now.option_ID = xuanhao;

                }
                now.question_ID = question[0].ToString();
                now.question_Content = question[1].ToString();
                now.manager_ID = question[2].ToString();
                list.Add(now);
            }
            model.questionnaire = list;
            return View(model);
        }
        [HttpGet]
        public IActionResult Fill(string id)
        {
            var ques = Sql.Read("SELECT * FROM questionnaire");//查找所有问题
            var list = new List<Question>();
            var model = new QuestionnaireModel();//问卷模型
            foreach (DataRow question in ques)
            {
                var now = new Question();
                if (question[3].ToString() == "选择")//获取题目类型
                {
                    now.is_Bridge = true;
                    var opt = Sql.Read("SELECT optionID,optionContent FROM question_option WHERE q_num=@0", question[0]);
                    var xuanhao = new List<string>();
                    var xuanxiang = new List<string>();
                    foreach (DataRow op in opt)
                    {
                        xuanhao.Add(op[0].ToString());
                        xuanxiang.Add(op[1].ToString());
                    }
                    now.option_Content = xuanxiang;
                    now.option_ID = xuanhao;

                }
                now.question_ID = question[0].ToString();
                now.question_Content = question[1].ToString();
                now.manager_ID = question[2].ToString();
                list.Add(now);
            }
            model.questionnaire = list;
            return View(model);
        }
    }
}
