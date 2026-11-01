using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace WebAppStartUp.Models
{

  
    public class Model_Master_CONTROLDetails
    {
        
        public string USR_ID { get; set; }
        public string NAME { get; set; }
        public string ACTIVE_TAG { get; set; }

        public string UNITDEST { get; set; }
        public string ADD1 { get; set; }
        public string CITY { get; set; }
        public string PIN { get; set; }
        public string LOCA { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode =true,DataFormatString ="{0:yyyy-MM-dd}")]
        public DateTime? YRFR { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime? YRTO { get; set; }
        public string FY { get; set; }
        public string YR { get; set; }
        public string MAIN_UNIT { get; set; }

    }
}