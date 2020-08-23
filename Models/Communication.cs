using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpidemicManager.Models
{
    public class DocCommunicationMobel
    {
        public int question_id { get; set; }
        public string question_type { get; set; }
        public string question_content { get; set; }
        public string doc_name { get; set; }
        public string date { get; set; }
        public string answer { get; set; }
    }
    public class UserCommunitionMobel
    {
        public string user_name { get; set; }
        public string user_idNumber { get; set; }
        public string date { get; set; }
        public int question_id { get; set; }
        public string question_content { get; set; }
    }

    public class CheckMobel
    {
        public string user_name { get; set; }
        public string user_idNumber { get; set; }
        public string question_type { get; set; }
        public int question_id { get; set; }
        public string doc_name { get; set; }
        public string date { get; set; }
        public string answer { get; set; }
    }

}

