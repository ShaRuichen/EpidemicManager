using Microsoft.VisualBasic.CompilerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpidemicManager.Models
{

    public class DonateMoneyModel
    {
        public string people_id { get; set; }
        public int number { get; set; }
    }
    public class DonateMaterialModel
    {
        public string people_id { get; set; }
        public string type{ get; set; }
        public int amount { get; set; }
    }
    public class DistributeModel
    {
        public int donate_id { get; set; }
        public string hospital_name { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string manager_id { get; set; }
        public List<string> DIds { get; set; }
        public List<string> Names { get; set; }
        public List<string> Dates { get; set; }
        public List<string> Times { get; set; }
        public List<string> MIds { get; set; }
        public int num { get; set; }
    }
}
