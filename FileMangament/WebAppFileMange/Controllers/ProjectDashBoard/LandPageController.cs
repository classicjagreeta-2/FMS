using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace WEB_PRO17.Controllers.FIN
{
    public class LandPageController : Controller
    {
        // GET: LandPage
        public ActionResult LandPage()
        {

            var Client = new HttpClient();

           // return Redirect("http://localhost:51000/ProjectStartUp_DashBoard/ProjectStartUp_DashBoard");
             return View();
        }
    }
}