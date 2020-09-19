using System;
using System.Collections.Generic;
using System.Data;
using EpidemicManager;
using Microsoft.AspNetCore.Mvc;
using EpidemicManager.Models;
using System.Diagnostics;
using System.Threading;
using Microsoft.AspNetCore.Http;

namespace EpidemicManager.Controllers
{
    public class QuestionnaireController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Create()
        {
            var ques = Sql.Read("SELECT * FROM questionnaire WHERE is_deleted=0");//查找所有问题
            var list = new List<Question>();
            var model = new QuestionnaireModel();//问卷模型
            foreach (DataRow question in ques)
            {
                if (Convert.ToInt32(question[4]) == 0)
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
                    now.question_ID = Convert.ToInt32(question[0]);
                    now.question_Content = question[1].ToString();
                    now.manager_ID = question[2].ToString();
                    list.Add(now);
                }
            }
            model.questionnaire = list;
            return View(model);
        }

        [HttpGet]
        public IActionResult Read()
        {
            var ques = Sql.Read("SELECT * FROM questionnaire WHERE is_deleted=0");//查找所有问题
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
                now.question_ID = Convert.ToInt32(question[0]);
                now.question_Content = question[1].ToString();
                now.manager_ID = question[2].ToString();
                list.Add(now);

            }
            model.questionnaire = list;
            return View(model);
        }
        [HttpGet]
        public IActionResult Fill()
        {
            var session = HttpContext.Session;
            var filler_id = HttpContext.Session.GetString("userId");
            var date = DateTime.Now.ToString("yyyy--MM-dd");
            var fill = 0;
            var filled = Sql.Read("SELECT date From answer WHERE people_id=@0 AND date=@1", filler_id, date);
            foreach (DataRow day in filled)
            {
                fill++;
            }
            if (fill != 0)
            {
                return RedirectToAction("reject");
            }
            //判断用户当日是否已经填写过问卷
            var ques = Sql.Read("SELECT * FROM questionnaire WHERE is_deleted=0");//查找所有问题
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

                now.question_ID = Convert.ToInt32(question[0]);
                now.question_Content = question[1].ToString();
                now.manager_ID = question[2].ToString();
                list.Add(now);

            }
            model.questionnaire = list;
            return View(model);
        }

        [HttpPost]
        public IActionResult Fill(QuestionnaireModel m)
        {
            //var filler_id = "defaultId";
            var session = HttpContext.Session;
            var filler_id = HttpContext.Session.GetString("userId");
            var date = DateTime.Now.ToString("yyyy--MM-dd");
            var ques = Sql.Read("SELECT * FROM questionnaire WHERE is_deleted=0");
            int count = 0;
            var type = new List<bool>();
            foreach (DataRow question in ques)
            {
                if (question[3].ToString() == "选择")
                {
                    type.Add(true);
                }
                else
                {
                    type.Add(false);
                }
                count++;
            }
            var effectiveques = Sql.Read("SELECT q_num FROM questionnaire where is_deleted=0");
            List<int> trueQuesNum = new List<int>();
            foreach (DataRow num in effectiveques)
            {
                int number = Convert.ToInt32(num[0]);
                trueQuesNum.Add(number);
            }
            for (int i = 0; i < count; i++)
            {
                if (!type[i])
                {
                    var fillContent = Request.Form["answers[" + i + "].fillContent"];
                    Sql.Execute("INSERT INTO answer(q_num,people_id,date,q_answer)  VALUES(@0,@1,@2,@3)", trueQuesNum[i], filler_id, date, fillContent);
                }
                else
                {
                    var choice = Request.Form["answers[" + i + "].choice"];
                    Sql.Execute("INSERT INTO answer(q_num,people_id,date,q_answer)  VALUES(@0,@1,@2,@3)", trueQuesNum[i], filler_id, date, choice);
                }
            }
            return RedirectToAction("Jump");
        }

        public IActionResult Jump()
        {
            return View();
        }

        public IActionResult Reject()
        {
            return View();
        }

        public IActionResult Result()
        {
            var ques = Sql.Read("SELECT * FROM questionnaire WHERE is_deleted=0");//查找所有问题
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
                    var tongji = new List<int>();
                    foreach (DataRow op in opt)
                    {
                        xuanhao.Add(op[0].ToString());
                        xuanxiang.Add(op[1].ToString());
                        tongji.Add(0);
                    }
                    now.option_Content = xuanxiang;
                    now.option_ID = xuanhao;
                    now.statics = tongji;
                }
                now.fills = new List<string>();
                now.question_ID = Convert.ToInt32(question[0]);
                now.question_Content = question[1].ToString();
                now.manager_ID = question[2].ToString();
                list.Add(now);

            }
            var date = DateTime.Now.ToString("yyyy--MM-dd");
            var results = Sql.Read("SELECT q_num,q_answer FROM answer WHERE date=@0", date);
            var effectiveques = Sql.Read("SELECT q_num FROM questionnaire where is_deleted=0");
            List<int> trueQuesNum = new List<int>();
            foreach (DataRow num in effectiveques)
            {
                int number = Convert.ToInt32(num[0]);
                trueQuesNum.Add(number);
            }
            foreach (DataRow ans in results)
            {
                int q_num = Convert.ToInt32(ans[0]);
                if (!list[findPosition(q_num, trueQuesNum)].is_Bridge)
                {
                    string content = ans[1].ToString();
                    (list[findPosition(q_num, trueQuesNum)].fills).Add(content);
                }
                else
                {
                    if (ans[1].ToString() != "")
                    {
                        int choice = Convert.ToInt32(ans[1]);
                        (list[findPosition(q_num, trueQuesNum)].statics[choice - 1])++;
                    }
                }
            }
            model.questionnaire = list;
            return View(model);
        }

        [HttpPost]
        public bool AddRadios(string content, string op1, string op2, string op3, string op4)
        {
            var ids = Sql.Read("SELECT MAX(q_num) FROM questionnaire");
            int count = 0;
            int id = 0;
            foreach (DataRow num in ids)
            {
                count++;
            }
            //string managerId = "123456";
            if (content == null)
            {
                return false;
            }
            var session = HttpContext.Session;
            var managerId = HttpContext.Session.GetString("userId");
            if (count == 0)
            {
                id = 1;
            }
            else
            {
                foreach (DataRow at in ids)
                {
                    id = Convert.ToInt32(at[0]) + 1;
                }
            }
            Sql.Execute("INSERT INTO questionnaire VALUES(@0, @1, @2, @3, @4)", id, content, managerId, "选择", 0);
            if (op1 != null)
            {
                Sql.Execute("INSERT INTO question_option VALUES(@0, @1, @2)", 1, op1, id);
            }
            else return false;
            if (op2 != null)
            {
                Sql.Execute("INSERT INTO question_option VALUES(@0, @1, @2)", 2, op2, id);
            }
            if (op3 != null)
            {
                Sql.Execute("INSERT INTO question_option VALUES(@0, @1, @2)", 3, op3, id);
            }
            if (op4 != null)
            {
                Sql.Execute("INSERT INTO question_option VALUES(@0, @1, @2)", 4, op4, id);
            }
            return true;

        }
        [HttpPost]
        public bool AddFill(string q_content)
        {
            var ids = Sql.Read("SELECT MAX(q_num) FROM questionnaire");
            int count = 0;
            int id = 0;
            foreach (DataRow num in ids)
            {
                count++;
            }
            //string managerId = "123456";
            if (q_content == null)
            {
                return false;
            }
            var session = HttpContext.Session;
            var managerId = HttpContext.Session.GetString("userId");
            if (count == 0)
            {
                id = 1;
            }
            else
            {
                foreach (DataRow at in ids)
                {
                    id = Convert.ToInt32(at[0]) + 1;
                }
            }
            Sql.Execute("INSERT INTO questionnaire VALUES(@0, @1, @2, @3, @4)", id, q_content, managerId, "填空", 0);
            return true;
        }

        [HttpPost]
        public bool DeleteQuestion(string delete_id)
        {
            var effectiveques = Sql.Read("SELECT q_num FROM questionnaire where is_deleted=0");
            List<int> trueQuesNum = new List<int>();
            foreach (DataRow num in effectiveques)
            {
                int number = Convert.ToInt32(num[0]);
                trueQuesNum.Add(number);
            }
            int range = trueQuesNum.Count;
            int del_ID = Convert.ToInt32(delete_id);
            if (del_ID < 1 || del_ID > range)
                return false;
            else
            {
                int id = trueQuesNum[del_ID - 1];
                Sql.Execute("UPDATE questionnaire SET is_deleted = 1 WHERE q_num = @0", id);
                return true;
            }

        }

        public int findPosition(int key, List<int> list)
        {
            int pos = 0;
            for (int i = 0; i < list.Count; i++)
            {
                if (key != list[i])
                    pos++;
                else
                    return pos;
            }
            return -1;
        }
    }
}
