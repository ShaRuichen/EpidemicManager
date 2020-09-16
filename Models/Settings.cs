using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EpidemicManager.Models
{
    public class SettingsPeople
    {
        public string people_id { get; set; }
        public string name { get; set; }
        public string address { get; set; }
        public string tel { get; set; }
        public string sex { get; set; }
        public string password { get; set; }
        public bool password1 { get; set; }
    }
    public class Settingsdoctor
    {
        public string doc_id { get; set; }
        public string name { get; set; }
        public string hos_name { get; set; }
        public string password { get; set; }
        public string password1 { get; set; }
        public string password2 { get; set; }
    }
    public class Settingsmanager
    {
        public string man_id { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string work_unit { get; set; }
        public string tel { get; set; }
        public string address { get; set; }
        public string password { get; set; }
        public string password1 { get; set; }
        public string password2 { get; set; }

    }
    public class Settingspatient
    {
        public string pat_id { get; set; }
        public string name { get; set; }
        public string sex { get; set; }
        public string hos_name { get; set; }
        public string password { get; set; }
        public string password1 { get; set; }
        public string password2 { get; set; }
    }
    public class Settingsmessage
    {
        public string error { get; set; }
    }
}
