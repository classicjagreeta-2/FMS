using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebAppStartUp.Models;

namespace WEB_PRO17
{
    [Authorize]
    public class BaseCommonController : Controller
    {
        ServiceRepository serviceObj = new ServiceRepository();
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var LoginDetails = Utility.GetLoggedInUserDetails();

            //var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(LoginDetails);
            //var authTicket = new FormsAuthenticationTicket(1, LoginDetails.UserId, DateTime.Now, DateTime.Now.AddMinutes(5), false, jsonData.ToString());
            //string EncryptTicket = FormsAuthentication.Encrypt(authTicket);

            //var authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, EncryptTicket);
            //HttpContext.Response.Cookies.Add(authCookies);
            //var Id = HttpContext.User.Identity;

            Session["UserName"] = LoginDetails.User_Name;
            //Menu Created
            HttpCookie Rt = Request.Cookies["Model_Session"];
            var JstReturn = (dynamic)null;
            if (Rt != null)
            {

                var str = new HtmlString(Convert.ToString(Session["MenuList"]));
                if (Rt.Value != "")
                {
                    if (string.IsNullOrEmpty(str.ToHtmlString()))
                    {


                        Model_SessionOtherProject jsonDataR = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_SessionOtherProject>(Rt.Value);
                        string[] strData = new string[] { LoginDetails.UserId, Rt.Value };
                        JstReturn = serviceObj.SubmitPost2<Model_Layout>("api/ProjectSelectionController/GetMenu", strData);

                        Session["User_Privileges"] = JstReturn.LtDynamics;
                        Session["MenuList"] = JstReturn.Data.ManuDetails;                       
                        Session["CompanyName"] = jsonDataR.Company_Name + ", " + jsonDataR.UnitDesnitaion +
                            " [ Financial Year : " + jsonDataR.FinancialYearName + " ]";
                    }
                }

            }




            RedirectToRouteResult routeData = null;
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
    }
}