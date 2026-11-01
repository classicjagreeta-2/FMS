using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using WebAppStartUp.Enum;

namespace WEB_PRO17
{
    public class CommonServices
    {


        public static string ShowAlert(Alerts obj, string message)
        {
            string alertDiv = null;
            switch (obj)
            {
                case Alerts.Success:
                    alertDiv = "<div class='alert alert-success alert-dismissable' id='alert'><button type='button' class='close' data-dismiss='alert'>×</button><strong> Success!</ strong > " + message + "</a>.</div>";
                    break;
                case Alerts.Danger:
                    alertDiv = "<div class='alert alert-danger alert-dismissible' id='alert'><button type='button' class='close' data-dismiss='alert'>×</button><strong> Error!</ strong > " + message + "</a>.</div>";
                    break;
                case Alerts.Info:
                    alertDiv = "<div class='alert alert-info alert-dismissable' id='alert'><button type='button' class='close' data-dismiss='alert'>×</button><strong> Info!</ strong > " + message + "</a>.</div>";
                    break;
                case Alerts.Warning:
                    alertDiv = "<div class='alert alert-warning alert-dismissable' id='alert'><button type='button' class='close' data-dismiss='alert'>×</button><strong> Warning!</strong> " + message + "</a>.</div>";
                    break;
            }
            return alertDiv;
        }

        public class JsonContent<T>
        {
            public int StatusCode { get; set; }
            public string StatusText { get; set; }
            public string ExtraText { get; set; }
            public string Message { get; set; }
            public string Stringadded { get; set; }
            public T Content { get; set; }
            public T TempContent { get; set; }
            public T ErrorTable { get; set; }
            public Exception exception { get; set; }
            public bool boolMsg { get; set; }

        }
        public class ApiResponse<TData>
        {
            public int StatusCode { get; set; }
            public string Message { get; set; }
            public string strData { get; set; }
            public TData Data { get; set; }
            public bool BoolMsg { get; set; }
            public List<TData> Lst { get; set; }
            public List<TData> Lst2 { get; set; }
            public int Id { get; set; }
            public string UserId { get; set; }
            public int TotalCount { get; set; }
            public List<dynamic> LtDynamics { get; set; }

        }

        public class SearchResponse<TData> : ApiResponse<TData>
        {
            public int TotalCount { get; set; }
        }
        public class CommanMutipleDataTable
        {
            public DataTable DtTable { get; set; }
            public DataTable DtTable2 { get; set; }

        }

    }
}