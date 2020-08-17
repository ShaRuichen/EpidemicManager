using System.Collections.Generic;

namespace EpidemicManager.Models
{
    public class Travelmodel
    {
        public List<string> Ids { get; set; }
        public List<string> Dates { get; set; }
        public List<string> Times { get; set; }
        public List<string> Sites { get; set; }
        public int Id { get; set; }
    }
    public class Trmodel
    {
        public string ID { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string site { get; set; }
    }
}
