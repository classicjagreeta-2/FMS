
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppStartUp.Models;

namespace WEB_PRO17.Controllers.FIN
{
    public class ProjectStartUp_DashBoardController : BaseCommonController
    {
        // GET: ProjectStartUp_DashBoard
        public ActionResult ProjectStartUp_DashBoard()
        {


            HttpCookie Rt = Request.Cookies["Model_Session"];
            ServiceRepository serviceRepository = new ServiceRepository();

            Model_SessionOtherProject jsonDataR = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_SessionOtherProject>(Rt.Value);
            var LoginDetails = Utility.GetLoggedInUserDetails();

            serviceRepository.SetDbNameClass(jsonDataR.ProjectDb); //Second Db Set
            Response.AppendCookie(Rt); //Cookies for Pass in OLd Project

            string ProjectName = jsonDataR.ModuleNAME;
            switch (ProjectName)
            {
                case "":
                    return RedirectToAction("ProjectStartUp_DashBoard", "ProjectStartUp_DashBoard");
                default:
                    return RedirectToAction("CommonDashBoard", "CommonDashBoard");
            }

          
           // return View();

        }
        protected override void OnException(ExceptionContext filterContext)
        {
            //Logger logger = LogManager.GetCurrentClassLogger();
            //logger.ErrorException("ProjectStartUp_DashBoard", filterContext.Exception);
        }
    }
}