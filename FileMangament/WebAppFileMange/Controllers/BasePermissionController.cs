using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_PRO17;
using WebAppStartUp.Models;

namespace WebAppFileMange.Controllers
{
    [Authorize]
    public class BasePermissionController : Controller
    {
        ServiceRepository serviceObj = new ServiceRepository();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {


            //GetActiveSessions();



            var LoginDetails = Utility.GetLoggedInUserDetails();

            //var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(LoginDetails);
            //var authTicket = new FormsAuthenticationTicket(1, LoginDetails.UserId, DateTime.Now, DateTime.Now.AddMinutes(1), false, jsonData.ToString());
            //string EncryptTicket = FormsAuthentication.Encrypt(authTicket);

            //var authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, EncryptTicket);
            //HttpContext.Response.Cookies.Add(authCookies);


            //var conf = System.Web.Configuration.WebConfigurationManager.OpenWebConfiguration(System.Web.Hosting.HostingEnvironment.ApplicationVirtualPath);
            //var section = (System.Web.Configuration.SessionStateSection)conf.GetSection("system.web/sessionState");
            //string timeout = section.Timeout.TotalMinutes.ToString();

            //var timeoutT = FormsAuthentication.Timeout.TotalMilliseconds;

            // var Id = HttpContext.User.Identity;

            //   var CurrentController = filterContext.Controller.ToString();//.Replace("KMD_CMS_2018.Controllers.", "");

            // string MenuId = filterContext.HttpContext.Request.QueryString["MId"];

            //Menu Created





            HttpCookie Rt = Request.Cookies["Model_Session"];


            var JstReturn = (dynamic)null;
            if (Rt != null)
            {
                if (Rt.Value != null)
                {
                    var str = new HtmlString(Convert.ToString(Session["MenuList"]));

                    if (Rt.Value != "")
                    {
                        Model_SessionOtherProject jsonDataR = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_SessionOtherProject>(Rt.Value);
                        Session["AuthExp"] = jsonDataR.Auth_ExpiryTime;
                        //Session["CookiesExp"] = jsonDataR.Cookies_ExpiryTime;

                        if (string.IsNullOrEmpty(str.ToHtmlString()))
                        {

                            string[] strData = new string[] { LoginDetails.UserId, Rt.Value };
                            JstReturn = serviceObj.SubmitPost2<Model_Layout>("api/ProjectSelectionController/GetMenu", strData);
                            Session["MenuList"] = JstReturn.Data.ManuDetails;
                            Session["User_Privileges"] = JstReturn.LtDynamics;
                        }

                    }

                }
            }

            //Cookies Time Extension
            //GetCookies getCookies = new GetCookies();
            //Model_SessionOtherProject model_SessionOtherProject = getCookies.GetStoredCookies();
            //if (MenuId != null)
            //    model_SessionOtherProject.MenuId = MenuId;
            //var jsonDataSessionCookies = Newtonsoft.Json.JsonConvert.SerializeObject(model_SessionOtherProject);
            //HttpCookie moduleSelectorcookies = new HttpCookie("Model_Session", jsonDataSessionCookies);
            //HttpContext.Response.Cookies.Add(moduleSelectorcookies);
            //moduleSelectorcookies.Expires = DateTime.Now.AddMinutes(30);

            RedirectToRouteResult routeData = null;
            /*
            string url = Request.Url.DnsSafeHost;           

            GetCookies getCookies = new GetCookies();
            Model_SessionOtherProject model_SessionOtherProject = getCookies.GetStoredCookies();
            if (new string[2] { "103.234.116.64", "localhost" }.Contains(url))
            {
                string FullUrl = ((System.Web.HttpRequestWrapper)Request).UrlReferrer.AbsoluteUri;
               
                
                string ReqMenuId = Request.Params["MId"];
                if (ReqMenuId == null)
                {
                    ReqMenuId = model_SessionOtherProject.MenuId;
                }
                else
                {
                    model_SessionOtherProject.MenuId = ReqMenuId;
                }



                List<dynamic> Lst = Session["User_Privileges"] as List<dynamic>;
                var json = JsonConvert.SerializeObject(Lst);
                var LstPrivileges = JsonConvert.DeserializeObject<List<Model_Menu>>(json).Where(x => x.AutoId == Convert.ToInt32(ReqMenuId)).SingleOrDefault();

                if (LstPrivileges.AEDV.Contains("M"))
                {
                    model_SessionOtherProject.Previlage_View = true;
                }
                else
                {
                    model_SessionOtherProject.Previlage_View = false;
                    routeData = new RedirectToRouteResult
                    (new System.Web.Routing.RouteValueDictionary {
                       { "action", "NoPrivilage" },
                        { "controller", "Error" },
                        { "StrUrl", FullUrl }
                    }
                    );
                }
            }

            getCookies.SetProjectCookies(model_SessionOtherProject);
            */
            if (LoginDetails.UserId == null)
            {
                routeData = new RedirectToRouteResult
                    (new System.Web.Routing.RouteValueDictionary
                    (new
                    {
                        controller = "Login22",
                        action = "Login",
                    }
                    ));
            }
            filterContext.Result = routeData;
        }


        //public static IEnumerable<SessionStateItemCollection> GetActiveSessions()
        //{
        //    object obj = typeof(HttpRuntime).GetProperty("CacheInternal", BindingFlags.NonPublic | BindingFlags.Static).GetValue(null, null);
        //    object[] obj2 = (object[])obj.GetType().GetField("_caches", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj);

        //    for (int i = 0; i < obj2.Length; i++)
        //    {
        //        Hashtable c2 = (Hashtable)obj2[i].GetType().GetField("_entries", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(obj2[i]);
        //        foreach (DictionaryEntry entry in c2)
        //        {
        //            object o1 = entry.Value.GetType().GetProperty("Value", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(entry.Value, null);
        //            if (o1.GetType().ToString() == "System.Web.SessionState.InProcSessionState")
        //            {
        //                SessionStateItemCollection sess = (SessionStateItemCollection)o1.GetType().GetField("_sessionItems", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(o1);
        //                if (sess != null)
        //                {
        //                    yield return sess;
        //                }
        //            }
        //        }
        //    }
        //}
    }
}