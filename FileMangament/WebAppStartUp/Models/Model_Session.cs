using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebAppStartUp.Models
{

    public class Model_Session : Model_CompanySelection //Model_CompanySelection add to make single cookies
    {
        public string ModuleCode { get; set; }
        public string ModuleControlSchema { get; set; }
        public string ModuleMenuSchema { get; set; }
        public string ModuleMenuSortBy { get; set; }
        public string ModuleNAME { get; set; }
        public string UnitDesnitaion { get; set; }
        public string Unit { get; set; }
        public string Os_user { get; set; }
        public string Terminal_id { get; set; }
        public string MenuId { get; set; }

        public string Logo_path { get; set; }



    }

    public class Model_SessionOtherProject : Model_Session
    {
        public string UserId { get; set; } //User For Pass session for Other Project
        [DefaultValue(false)]
        public bool Previlage_Add { get; set; }
        [DefaultValue(false)]
        public bool Previlage_Edit { get; set; }
        [DefaultValue(false)]
        public bool Previlage_View { get; set; }
        [DefaultValue(false)]
        public bool Previlage_Delete { get; set; }


        public int Previlage_EDAY { get; set; }
        public int Previlage_ADAY { get; set; }
        public int Previlage_DDAY { get; set; }

        public DateTime? Auth_ExpiryTime { get; set; }
        public DateTime? Cookies_ExpiryTime { get; set; }

        public string MAIN_UNIT { get; set; }

    }

    public class Model_CompanStartUp 
    {
        public string DdlCompId { get; set; }
        public string DdlCountry { get; set; }
        public string DdlFYear { get; set; }
    }
}