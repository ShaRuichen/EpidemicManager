using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpidemicManager.Models
{
    public class StatisticsModel//民众行程数据
    {
        public string ID { get; set; }
        public string Date { get; set; }
        public string Time { get; set; }
        public string Site { get; set; }
    }

    public class PepQRcodeModel//二维码
    {
        public string PepID { get; set; }
    }
}
