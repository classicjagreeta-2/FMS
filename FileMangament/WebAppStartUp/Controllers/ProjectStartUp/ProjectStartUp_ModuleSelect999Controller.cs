using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebAppStartUp;
using WebAppStartUp.Common;
using WebAppStartUp.Models;

namespace WebAppStartUp.Controllers
{


    public class ProjectStartUp_ModuleSelect999Controller : Controller
    {
        Model_SessionOtherProject moduleSelector = new Model_SessionOtherProject();
        //private readonly ICacheService _ICacheService;

        //// GET: ProjectStartUp_ModuleSelect
        //public ProjectStartUp_ModuleSelectController(ICacheService cacheService)
        //{
        //    _ICacheService = cacheService;
        //}

        public ActionResult ProjectStartUp_ModuleSelect()
        {
            ServiceRepository serviceObj = new ServiceRepository();
            var LoginDetails = Utility.GetLoggedInUserDetails();
            List<Model_ModulSelection> Return = new List<Model_ModulSelection>();
            if (LoginDetails.UserId != null)
            {
                 Return = serviceObj.SubmitGet<List<Model_ModulSelection>>("api/ProjectSelectionController/GetModule");

               
            }
            return View("ProjectStartUp_ModuleSelect", Return);
        }

        public ActionResult ModuleSelector(string code, string controlschema, string menuschema, string sortby, string mname)
        {
            var LoginDetails = Utility.GetLoggedInUserDetails();


            moduleSelector.ModuleCode = code;
            moduleSelector.ModuleControlSchema = controlschema;
            moduleSelector.ModuleMenuSchema = menuschema;
            moduleSelector.ModuleMenuSortBy = sortby;
            moduleSelector.ModuleNAME = mname;


            Model_SessionOtherProject model_SessionOtherProject = moduleSelector;
            model_SessionOtherProject.UserId = LoginDetails.UserId;

            TempData["SessionCookies"] = model_SessionOtherProject;

            return RedirectToAction("ProjectStartUp_CompanySelection", "ProjectStartUp_CompanySelection999");
        }
    }
}