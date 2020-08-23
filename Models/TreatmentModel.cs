using System.Collections.Generic;

namespace EpidemicManager.Models
{
    public class TreatmentModel
    {
        public string Plan_id { get; set;  }
        public string Doctor_id { get; set; }
        public string Patient_id { get; set;  }
        public string Time { get; set;  }
        public string Medicine { get; set; }
        public string Details { get; set;  }
        public List<string> IDs { get; set;  }

    }
}
