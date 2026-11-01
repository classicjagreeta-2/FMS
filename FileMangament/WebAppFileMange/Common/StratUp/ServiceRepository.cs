using System;
using System.Net.Http;
using System.Configuration;
using System.Web.Script.Serialization;
using static WEB_PRO17.CommonServices;
using System.Text;
using System.Net.Http.Headers;
using WEB_PRO17.Common;
using System.Collections.Generic;
using System.Web;

 

namespace WEB_PRO17
{

    public class ServiceRepository
    {
        public HttpClient Client { get; set; }
        private static string UserId = null;
        private static string Password = null;
        private static string DbName = null;
        ExecptionErrorMsg execptionErrorMsg = new ExecptionErrorMsg();
        public ServiceRepository()
        {
            Client = new HttpClient();

            Client.BaseAddress = new Uri(ConfigurationManager.AppSettings["ServiceUrl"].ToString());
            if (UserId != null)
            {
                Client.DefaultRequestHeaders.Accept.Clear();
                Client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var byteArray = Encoding.ASCII.GetBytes(GetUserId + ":" + GetPassword + ":" + Convert.ToString(HttpContext.Current.Session["DbName"]));//DbName);

                Client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Basic", Convert.ToBase64String(byteArray));
            }
        }
        public ServiceRepository(string UserId, string Password)
        {

            GetUserId = UserId;
            GetPassword = Password;

        }
        public void SetDbNameClass(string dbName)
        {

            GetDbName = dbName;
        }
        public string GetUserId
        {
            get { return UserId; }
            set { UserId = value; }
        }
        public string GetPassword
        {
            get { return Password; }
            set { Password = value; }
        }
        public string GetDbName
        {
            get { return DbName; }
            set { DbName = value; }
        }
        public HttpResponseMessage PostResponse(string url, object model)
        {
            return Client.PostAsJsonAsync(url, model).Result;
        }
        public HttpResponseMessage GetResponse(string url)
        {
            return Client.GetAsync(url).Result;
        }
        public HttpResponseMessage DeleteResponse(string url)
        {
            return Client.DeleteAsync(url).Result;
        }

        public ApiResponse<IDictionary<string, object>> SubmitPost5<TData>(string url, object model)
        {

            var rest = Client.PostAsJsonAsync(url, model).Result;

            if (!rest.IsSuccessStatusCode)
            {
                string responseBody = rest.Content.ReadAsStringAsync().Result;
                throw new HttpException((int)rest.StatusCode, responseBody);
            }

            ApiResponse<IDictionary<string, object>> Return = new JavaScriptSerializer().Deserialize<ApiResponse<IDictionary<string, object>>>(rest.Content.ReadAsStringAsync()
                                              .Result);
            return Return;
        }

        public ApiResponse<TData> SubmitPost2<TData>(string url, object model)
        {

            var rest = Client.PostAsJsonAsync(url, model).Result;

            if (!rest.IsSuccessStatusCode)
            {
                string responseBody = rest.Content.ReadAsStringAsync().Result;
                throw new HttpException((int)rest.StatusCode, responseBody);
            }

            ApiResponse<TData> Return = new JavaScriptSerializer().Deserialize<ApiResponse<TData>>(rest.Content.ReadAsStringAsync()
                                              .Result);
            return Return;
        }

        public ApiResponse<TData> SubmitPost1stLogin<TData>(string url, object model)
        {

            var rest = Client.PostAsJsonAsync(url, model).Result;

            if (!rest.IsSuccessStatusCode)
            {
                string responseBody = rest.Content.ReadAsStringAsync().Result;
                throw new HttpException((int)rest.StatusCode, responseBody);
            }

            ApiResponse<TData> Return = new JavaScriptSerializer().Deserialize<ApiResponse<TData>>(rest.Content.ReadAsStringAsync()
                                              .Result);
            return Return;
        }
        public ApiResponse<TData> SubmitPost<TData>(string url, object model)
        {
            var rest = Client.PostAsJsonAsync(url, model).Result;

            if (!rest.IsSuccessStatusCode)
            {
                string responseBody = rest.Content.ReadAsStringAsync().Result;
                throw new HttpException((int)rest.StatusCode, responseBody);
            }

            ApiResponse<TData> Return = Newtonsoft.Json.JsonConvert.DeserializeObject<ApiResponse<TData>>(rest.Content.ReadAsStringAsync().Result);
            return Return;
        }

        public TData SubmitGet<TData>(string url)
        {

            JavaScriptSerializer jsJson = new JavaScriptSerializer();
            jsJson.MaxJsonLength = 2147483644;
            HttpResponseMessage rest = Client.GetAsync(url).Result;
            string StrMsg = rest.ReasonPhrase;
            var StatuCode = rest.StatusCode;
           
            if (!rest.IsSuccessStatusCode)
            {
                string responseBody = rest.Content.ReadAsStringAsync().Result;
                throw new HttpException((int)rest.StatusCode, responseBody);
            }

            var RTest = rest.Content.ReadAsStringAsync().Result;
            var Return = jsJson.Deserialize<CommonServices.ApiResponse<TData>>(rest.Content.ReadAsStringAsync()
                                               .Result);
            //jsonContent = apiResponse.Content.ReadAsStringAsync().Result;
            return Return.Data;
        }
        public List<IDictionary<string, object>> SubmitGet3(string url)
        {
            JavaScriptSerializer jsJson = new JavaScriptSerializer();
            jsJson.MaxJsonLength = 2147483644;
            var rest = Client.GetAsync(url).Result;

            if (!rest.IsSuccessStatusCode)
            {
                string responseBody = rest.Content.ReadAsStringAsync().Result;
                throw new HttpException((int)rest.StatusCode, responseBody);
            }

            var RTest = rest.Content.ReadAsStringAsync().Result;
            var Return = jsJson.Deserialize<List<IDictionary<string, object>>>(rest.Content.ReadAsStringAsync()
                                               .Result);
            //jsonContent = apiResponse.Content.ReadAsStringAsync().Result;
            return Return;
        }

        public List<IDictionary<string, object>> SubmitGet4(string url, object model)
        {
            var rest = Client.PostAsJsonAsync(url, model).Result;

            if (!rest.IsSuccessStatusCode)
            {
                string responseBody = rest.Content.ReadAsStringAsync().Result;
                throw new HttpException((int)rest.StatusCode, responseBody);
            }

            var Return = Newtonsoft.Json.JsonConvert.DeserializeObject<List<IDictionary<string, object>>>(rest.Content.ReadAsStringAsync().Result);
            return Return;
        }
        public List<T> SubmitGetList<T>(string url, object model)
        {
            var rest = Client.PostAsJsonAsync(url, model).Result;

            if (!rest.IsSuccessStatusCode)
            {
                string responseBody = rest.Content.ReadAsStringAsync().Result;
                throw new HttpException((int)rest.StatusCode, responseBody);
            }

            var Return = Newtonsoft.Json.JsonConvert.DeserializeObject<List<T>>(rest.Content.ReadAsStringAsync().Result);
            return Return;
        }
        //public TData SubmitGet<TData>(string url)
        //{
        //    var rest = Client.GetAsync(url).Result;
        //    var Return = new JavaScriptSerializer().Deserialize<ApiResponse<TData>>(rest.Content.ReadAsStringAsync()
        //                                       .Result);
        //    //jsonContent = apiResponse.Content.ReadAsStringAsync().Result;
        //    return Return.Data;
        //}



    }


}