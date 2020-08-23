using System.Collections.Generic;

namespace EpidemicManager.Models
{
    public class Travelmodel
    {
        public List<string> Ids { get; set; }
        public List<string> Dates { get; set; }
        public List<string> Times { get; set; }
        public List<string> Sites { get; set; }
        public int info_num { get; set; }
    }
    public class Trmodel
    {
        public string ID { get; set; }
        public string site { get; set; }
       // public System.DateTime
    }
    public class Mamodel
    {
        public string ID { get; set; }
        public string site { get; set; }
        public string maID { get; set; }
    }
}
