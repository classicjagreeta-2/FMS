using System;

using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WEB_PRO17;
using WebAppStartUp.Models;

namespace WebAppFileMange.Controllers.Auth
{

    public class Login22Controller : Controller
    {
        // GET: Login

      
        public ActionResult Login()
        {
            //HttpCookie RtDefault = Request.Cookies["DefaultModel"];
            //HttpContext.Response.Cookies.Set(new HttpCookie("Model_Session") { Value = string.Empty });


            //if (RtDefault != null)
            //{

            //    HttpCookie httpCookie = new HttpCookie("DefaultModel", RtDefault.Value);
            //    HttpContext.Response.Cookies.Add(httpCookie);
            //    httpCookie.Expires.AddMonths(1);
            //}



            ViewBag.ErrorMsg = string.Empty;


            Request.Cookies.Clear();
            string[] myCookies = Request.Cookies.AllKeys;
            foreach (string key in Request.Cookies.AllKeys)
            {
                HttpCookie c = Request.Cookies[key];
                c.Expires = DateTime.Now.AddSeconds(1);
                Response.AppendCookie(c);
            }

            Session.Clear();
            Session.Abandon();



            return View();
        }
        public ActionResult Auth(Model_User model_User)
        
        {
            ViewBag.ErrorMsg = string.Empty;

            ServiceRepository serviceObj = new ServiceRepository();

            // var ReturnObj = serviceObj.SubmitPost<ApiResponse<Model_User>>("api/Login/CheckUsercredential", model_User);
            var Return = serviceObj.SubmitPost1stLogin<Model_User>("api/Login/CheckUsercredential", model_User);
            // var model_Userx = (Model_User)ReturnObj.DataObj;
            if (Return.StatusCode == 0)
            {
                Model_User userDetails = Return.Data;

                ServiceRepository serviceObj1 = new ServiceRepository(model_User.UserName, model_User.Password); //Storing UserId And Password

                var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(userDetails);

                FormsAuthentication.SetAuthCookie(userDetails.UserId, false);
                var authTicket = new FormsAuthenticationTicket(1, userDetails.UserId, DateTime.Now, DateTime.Now.AddDays(1), false, jsonData.ToString());
                string EncryptTicket = FormsAuthentication.Encrypt(authTicket);

                var authCookies = new HttpCookie(FormsAuthentication.FormsCookieName, EncryptTicket);
                HttpContext.Response.Cookies.Add(authCookies);

                return Json(new { redirectToUrl = Url.Action("ProjectStartUp_ModuleSelect", "ProjectStartUp_ModuleSelect"), statusCode = 9999 });
                //  return RedirectToAction("ProjectStartUp_ModuleSelect", "ProjectStartUp_ModuleSelect");

            }
            else
            {
                ViewBag.ErrorMsg = Return.Message;
                return Json(new { redirectToUrl = Url.Action("Login", "Login22"), ErrMsg = Return.Message, statusCode = 876 });
                //Return.Message = string.Empty;
                // return View("Login");
            }



        }
    }
}