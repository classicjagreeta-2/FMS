using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppFileMange.Models;

namespace WebAppStartUp.Models
{
    public class Model_Common
    {
        public class DataTableAjaxPostModel
        {
            // properties are not capital due to json mapping
            public string draw { get; set; }
            public string start { get; set; }
            public string length { get; set; }
            public string search { get; set; }
        }

        public class SearchModel
        {
            public int? PageNumber { get; set; }
            public int PageSize { get; set; }
            public string SearchKey { get; set; }
            public string OrderBy { get; set; }
        }
        public class Model_MasterSpParamters
        {
            public string ItemCode { get; set; }
            public string AliasID { get; set; }

            public string PRAutoId { get; set; }
            public string pPOAutoId { get; set; }
            public string pSOAutoId { get; set; }
            public string pSIAutoId { get; set; }
            public string pSAAutoId { get; set; }
            public string FrmName { get; set; }

            public string AutoId { get; set; }
            public string CallType { get; set; }
            public int SpId { get; set; }
            public string Unit { get; set; }

            public string FrmDate { get; set; }
            public string ToDate { get; set; }

            public string pPRAutoId { get; set; }
            public string pInvAutoId { get; set; }
            public string TranType { get; set; }
            public string OrderType { get; set; }
            public string ShipAdvAutoid { get; set; }
            public string Code { get; set; }
            public DateTime? Ddate { get; set; }

            public string UsrName { get; set; }
            public string OrderCode { get; set; }

        }
        public class Model_MasterDynamicSp : DataTablePageing
        {

            public int SpId { get; set; }

            public string FrmType { get; set; }
            public string[,] paramsArray { get; set; }
            public Head head { get; set; }
        }

        public class DataTablePageing
        {
            public string search { get; set; }
            public string draw { get; set; }
            public string order { get; set; }
            public string orderColumnName { get; set; }
            public string orderDir { get; set; }
            public int startRec { get; set; }
            public int pageSize { get; set; }
            public bool IsEnablePageing { get; set; } = false;
        }

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