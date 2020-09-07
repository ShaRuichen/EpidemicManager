using System;
using Microsoft.VisualBasic.CompilerServices;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;


namespace EpidemicManager.Models
{
    public class ExamineModel//一张报告
    {
        public string ID_doctor { get; set; }
        public string ID_patient { get; set; }
        public string ID_report { get; set; }
        public string time { get; set; }
        public string date { get; set; }
        public string detail { get; set; }
        public string name_doctor { get; set; }
        public string name_patient { get; set; }
        public string title { get; set; }
    }
    public class ExamineIndexModel//一个病人所有报告
    {
        public List<string> ID_patient { get; set; }
        public List<string> name_patient { get; set; }
        public List<string> report { get; set; }
        public List<string> title { get; set; }
        public List<string> date { get; set; }
        public int n = 0;
    }


}