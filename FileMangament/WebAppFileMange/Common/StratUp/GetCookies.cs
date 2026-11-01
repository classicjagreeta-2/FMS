using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebAppStartUp.Models;

namespace WebAppStartUp.Common
{
    public class GetCookies
    {
        public Models.Model_Common.Model_StringParameters GetStoredCookies_QueryString()
        {
            HttpCookie RtCookie = HttpContext.Current.Request.Cookies["Model_QueryString"];
            Models.Model_Common.Model_StringParameters jsonDataR = new Models.Model_Common.Model_StringParameters();
            


            if (RtCookie != null)
            {
                if (RtCookie.Value != "")
                {
                    // Rt.Expires= DateTime.Now.AddMinutes(45); 
                    jsonDataR = Newtonsoft.Json.JsonConvert.DeserializeObject<Models.Model_Common.Model_StringParameters>(RtCookie.Value);
                     
                }
            }
            return jsonDataR;
        }

        public void SetCookies_QueryString(Models.Model_Common.Model_StringParameters model_StringParameters)
        {
            var jsonDataSessionCookies = Newtonsoft.Json.JsonConvert.SerializeObject(model_StringParameters);
            //Delete Existing Cookies
            HttpCookie OldCookie = HttpContext.Current.Request.Cookies["Model_QueryString"];
            if (OldCookie != null)
            {
                //Set the Expiry date to past date.
                OldCookie.Expires = DateTime.Now.AddMinutes(145);
                //Update the Cookie in Browser.
                OldCookie.Value = jsonDataSessionCookies;
                HttpContext.Current.Response.Cookies.Add(OldCookie);
            }
            else
            {
                HttpCookie moduleSelectorcookies = new HttpCookie("Model_QueryString", jsonDataSessionCookies);
                HttpContext.Current.Response.Cookies.Add(moduleSelectorcookies);
                moduleSelectorcookies.Expires = DateTime.Now.AddMinutes(145);
            }
           


           
        }


        public Model_SessionOtherProject GetStoredCookies()
        {
            HttpCookie RtCookie = HttpContext.Current.Request.Cookies["Model_Session"];
            Model_SessionOtherProject jsonDataR = new Model_SessionOtherProject();
            //HttpContext.Current.Session["CookiesExp"] = RtCookie.Expires;


            if (RtCookie != null)
            {
                if (RtCookie.Value != "")
                {

                    jsonDataR = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_SessionOtherProject>(RtCookie.Value);
                    jsonDataR.Loca = HttpContext.Current.Session["Loca"] as string;
                }
            }
            return jsonDataR;
        }

        public void SetProjectCookies(Model_SessionOtherProject model_SessionOtherProject)
        {
            var jsonDataSessionCookies = Newtonsoft.Json.JsonConvert.SerializeObject(model_SessionOtherProject);


            //  var AllCookies = HttpContext.Current.Request.Cookies.AllKeys;

            //Delete Existing Cookies
            var OldCookie =HttpContext.Current.Request.Cookies["Model_Session"];
            if (OldCookie != null)
            {
                //Set the Expiry date to past date.
                OldCookie.Expires = DateTime.Now.AddMinutes(145);
                //Update the Cookie in Browser.
                OldCookie.Value = jsonDataSessionCookies;
                HttpContext.Current.Response.Cookies.Add(OldCookie);

            }
            else
            {
                
                HttpCookie moduleSelectorcookies = new HttpCookie("Model_Session", jsonDataSessionCookies);
                HttpContext.Current.Response.Cookies.Add(moduleSelectorcookies);
                moduleSelectorcookies.Expires = DateTime.Now.AddMinutes(145);
            }


            // HttpCookie RtCookie = HttpContext.Current.Request.Cookies["Model_Session"];


        }
        public Model_CompanStartUp GetStoredCookies(string SessionId)
        {
            HttpCookie RtCookie = HttpContext.Current.Request.Cookies[""+ SessionId + ""];
            Model_CompanStartUp jsonDataR = new Model_CompanStartUp();
            //HttpContext.Current.Session["CookiesExp"] = RtCookie.Expires;


            if (RtCookie != null)
            {
                if (RtCookie.Value != "")
                {

                    jsonDataR = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_CompanStartUp>(RtCookie.Value);
                     
                }
            }
            return jsonDataR;
        }
        public void SetCompanyCookies(Model_CompanStartUp model_CompanStartUp)
        {
            var jsonDataSessionCookies = Newtonsoft.Json.JsonConvert.SerializeObject(model_CompanStartUp); 

            //Update Existing Cookies
            var OldCookie = HttpContext.Current.Request.Cookies["Model_Company"];
            if (OldCookie != null)
            {
                //Set the Expiry date to past date.
                OldCookie.Expires = DateTime.Now.AddDays(7);
                //Update the Cookie in Browser.
                OldCookie.Value = jsonDataSessionCookies;
                HttpContext.Current.Response.Cookies.Add(OldCookie);

            }
            else
            {

                HttpCookie moduleSelectorcookies = new HttpCookie("Model_Company", jsonDataSessionCookies);
                HttpContext.Current.Response.Cookies.Add(moduleSelectorcookies);
                moduleSelectorcookies.Expires = DateTime.Now.AddDays(7);
            } 

        }
    }
}

