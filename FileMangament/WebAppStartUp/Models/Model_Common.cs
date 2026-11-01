using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebAppStartUp.Models
{
    public class Model_Common
    {
        public class ModelMaster
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }
        public class ModelSelect : ModelMaster
        {
            public string PARTYNAME { get; set; }
            public Nullable<System.DateTime> DOCDATE { get; set; }

        }

        public class ModelFromFilter
        {
            public string Id_Report { get; set; }
            public string Unit { get; set; }
            public DateTime? FDate { get; set; }
            public DateTime? ToDate { get; set; }

        }
        public class ModelReportFilter
        {
            public bool BoolenFieldName { get; set; }

        }

        public class ModelConfrimation
        {
            public string FormType { get; set; }
            public string Code { get; set; }
            public string Autoid { get; set; }
            public string ViewId { get; set; }
            public string FMode { get; set; }
            public string Email { get; set; }
            public string UnitId { get; set; }
            public string MyProperty { get; set; }
            public string ControllerName { get; set; }
            public string ActionMethod { get; set; }

            public string RpControllerName { get; set; }
            public string RpActionMethod { get; set; }
        }
        public class Model_StringParameters
        {
            public string MenuId { get; set; }
            public string TranType { get; set; }
            public string FmType { get; set; }
            public string FormMode { get; set; }
            public string Code { get; set; }
            public string Id { get; set; }

        }
    }
}