using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebAppStartUp.Models
{

    public class Model_CompanySelection
    {
        [Display(Name = "Select Company")]
        public string Comid { get; set; }
        [Display(Name = "Location")]
        public string Loca { get; set; }
        [Display(Name = "Financial Year")]
        public string ProjectDb { get; set; }
        public string Company_Name { get; set; }
        public string LocationName { get; set; }
        public string FinancialYearName { get; set; }
        public bool Ischeked { get; set; }

        public DateTime? YRFR { get; set; }
        public DateTime? YRTO { get; set; }
        public string YR { get; set; }

    }
}