using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpidemicManager.Models
{
    public class StatisticsModel//民众行程数据
    {
        public List<string> ID { get; set; }
        public List<string> Date{ get; set; }
        public List<string> Time { get; set; }
        public List<string> Site { get; set; }
        public int Info_num { get; set; }
    }

    public class PepQRcodeModel//二维码
    {
        public string PepID { get; set; }
        public string PepSite { get; set; }
    }

    public class PepModel
    {
        public string ID { get; set; }
        public string Site { get; set; }
    }
}
