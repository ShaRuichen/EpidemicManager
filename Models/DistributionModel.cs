using System.Collections.Generic;

namespace EpidemicManager.Models
{
    public class DistributionModel
    {
        public List<Dongroupmon> Idsmon { get; set; }
        public List<Dongroupmat> Idsmat { get; set; }
        public List<Dongroupdocmon> Idsdocmon { get; set; }
        public List<Dongroupdocmat> Idsdocmat { get; set; }
        public List<string> Hops { get; set; }
        public string Id { get; set; }
    }
}
