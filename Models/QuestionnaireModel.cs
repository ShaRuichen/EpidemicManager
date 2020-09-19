using System;
using System.Collections.Generic;

namespace EpidemicManager.Models
{
    public class QuestionnaireModel
    {
        public List<Question> questionnaire;
        public List<Answer> answers;
        public string filler_id { get; set; }
    }

    public class Question
    {
        public int question_ID { get; set; }
        public string question_Content { get; set; }
        public bool is_Bridge { get; set; }
        public string manager_ID { get; set; }
        public List<string> option_ID { get; set; }
        public List<string> option_Content { get; set; }
        public List<int> statics { get; set; }
        public List<string> fills { get; set; }
    }

    public class Answer
    {
        public int question_num { get; set; }
        public string choice { get; set; }
        public string fillContent { get; set; }
    }


    /*public class bridge
    {
        public List<string> option_ID { get; set; }
        public List<string> option_Content { get; set; }
    }*/
}
