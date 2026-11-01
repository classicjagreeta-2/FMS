using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using WebAppStartUp.Models;
using WebAppStartUp.Common;
using static WebAppStartUp.Models.Model_Common;

namespace WebAppStartUp.Controllers
{

    public class ProjectStartUp_CompanySelection999Controller : Controller
    {
        // GET: ProjectStartUp_CompanySelection
        Model_CompanySelection model_CompanySelection = new Model_CompanySelection();
        string Comid, UnitDest, Fy;
        LogDefault logDefault = new LogDefault();
        GetCookies getCookies = new GetCookies();
        public ActionResult ProjectStartUp_CompanySelection()
        {
            ServiceRepository serviceObj = new ServiceRepository();
            Model_CompanySelection jsonDataR = new Model_CompanySelection();
            //HttpCookie RtDefault = Request.Cookies["DefaultModel"];
            Model_SessionOtherProject model_SessionOtherProject = (Model_SessionOtherProject)TempData.Peek("SessionCookies");
            Model_CompanStartUp model_CompanStartUp = getCookies.GetStoredCookies("Model_Company");

          // var DefaultData = logDefault.IniReadValue(model_SessionOtherProject.UserId + '_' + model_SessionOtherProject.ModuleCode, "DefaultLog", Server.MapPath("~/Log/Jaguar.ini"));
            Comid = string.Empty;



            var LoginDetails = Utility.GetLoggedInUserDetails();
            if (LoginDetails.LastLoginData != null )
            {
                if (LoginDetails.LastLoginData != null && LoginDetails.LastLoginData !="")
                {
                    ViewBag.LastLogin = Convert.ToDateTime(LoginDetails.LastLoginData).ToString("dd/MM/yyyy hh:mm:ss");
                }
                else
                {
                    ViewBag.LastLogin = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy hh:mm:ss");
                }
                
            }
            else
            {
                ViewBag.LastLogin = Convert.ToDateTime(DateTime.Now).ToString("dd/MM/yyyy hh:mm:ss");
            }
               
            ViewBag.UserName = LoginDetails.User_Name;

            if (model_CompanStartUp != null)
            {
                try
                {
                    //jsonDataR = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_CompanStartUp>(model_CompanStartUp);


                    Comid = model_CompanStartUp.DdlCompId; //"JAY SHREE TEA & INDUSTRIES LTD. (2023)";
                    UnitDest = model_CompanStartUp.DdlCountry; //"HEAD_OFFICE";
                    Fy = model_CompanStartUp.DdlFYear; //"FIN_JSTIL2023";

                    //Get Default Values 
                    model_CompanySelection.Comid = Comid;
                    model_CompanySelection.Loca = UnitDest;
                    model_CompanySelection.ProjectDb = Fy;
                    model_CompanySelection.Ischeked = true;



                }
                catch (Exception ex)
                {

                }



            }

            //HttpCookie Rt = Request.Cookies["Model_Session"];
            //Model_Session jsonDataR = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_Session>(Rt.Value);

            //var LoginDetails = Utility.GetLoggedInUserDetails();

            //Model_SessionOtherProject model_SessionOtherProject = (Model_SessionOtherProject)TempData["SessionCookies"];
            //TempData.Keep();
            string[] strData = new string[] { model_SessionOtherProject.UserId, model_SessionOtherProject.ModuleNAME, model_SessionOtherProject.ModuleCode };


            var LstReturn = serviceObj.SubmitPost<Model_Master_CONTROLDetails>("api/ProjectSelectionController/GetCompany", strData);

            ViewBag.ddlCompnay = LstReturn.Lst.OrderBy(x => x.NAME).ToList();
            ViewBag.ddlLoaction = "";
            if (Comid != null && Comid != "")
                ViewBag.ddlLoaction = LoadLocation(Comid);

            ViewBag.ddlfincialyr = "";
            if (Comid != null && UnitDest != null && Comid != "")
                ViewBag.ddlfincialyr = LoadFincialYear(Comid, UnitDest);

            return View(model_CompanySelection);
        }


        [HttpPost]
        public JsonResult GetLoction(string CId)
        {
            List<ModelMaster> Lst = LoadLocation(CId).OrderBy(x => x.Text).ToList();

            return Json(Lst, JsonRequestBehavior.AllowGet);
        }

        List<ModelMaster> LoadLocation(string CId)
        {
            var LoginDetails = Utility.GetLoggedInUserDetails();

            Model_SessionOtherProject model_SessionOtherProject = (Model_SessionOtherProject)TempData["SessionCookies"];


            string[] strData = new string[] { LoginDetails.UserId, model_SessionOtherProject.ModuleNAME, model_SessionOtherProject.ModuleCode, CId };

            ServiceRepository serviceObj = new ServiceRepository();
            var LstReturn = serviceObj.SubmitPost<Model_Master_CONTROLDetails>("api/ProjectSelectionController/GetLocation", strData);
            string strLOCA = string.Empty;
            if (LstReturn.Lst.Count > 0)
            {
                strLOCA = LstReturn.Lst.Select(x => x.LOCA).FirstOrDefault().ToString();
                 
            }
            Session["Loca"] = strLOCA;
            model_SessionOtherProject.Loca = strLOCA;
            ViewBag.ProjectLoca = strLOCA;
            TempData["SessionCookies"] = model_SessionOtherProject;

            //Model_SessionOtherProject model_SessionOtherProject = (Model_SessionOtherProject)TempData["SessionCookies"];
            //TempData.Keep();

            List<ModelMaster> Lst = LstReturn.Lst.Select(x => new ModelMaster
            {
                Text = x.UNITDEST,
                Value = x.UNITDEST
            }).ToList<ModelMaster>();
            return Lst;
        }

        [HttpPost]
        public JsonResult GetFincialYear(string CId, string UnitDest)
        {
            List<ModelMaster> Lst = LoadFincialYear(CId, UnitDest);
            return Json(Lst, JsonRequestBehavior.AllowGet);
        }

        List<ModelMaster> LoadFincialYear(string CId, string UnitDest)
        {
            var LoginDetails = Utility.GetLoggedInUserDetails();

            Model_SessionOtherProject model_SessionOtherProject = (Model_SessionOtherProject)TempData["SessionCookies"];
            TempData.Keep();
            string[] strData = new string[] { CId.Trim(), UnitDest.Trim(), LoginDetails.UserId, model_SessionOtherProject.ModuleNAME };

            ServiceRepository serviceObj = new ServiceRepository();
            var LstReturn = serviceObj.SubmitPost<Model_Master_CONTROLDetails>("api/ProjectSelectionController/GetFincialYear", strData);
            List<ModelMaster> Lst = LstReturn.Lst.Select(x => new ModelMaster
            {
                //Text = x.YRFR.ToString() + "-" + x.YRTO.ToString(),
                Text = x.YRFR.Value.Date.Day.ToString().PadLeft(2, '0') + "/" + x.YRFR.Value.Date.Month.ToString().PadLeft(2, '0') + "/" + x.YRFR.Value.Date.Year.ToString().PadLeft(2, '0') +
                " - "
                + x.YRTO.Value.Date.Day.ToString().PadLeft(2, '0') + "/" + x.YRTO.Value.Date.Month.ToString().PadLeft(2, '0') + "/" + x.YRTO.Value.Date.Year.ToString().PadLeft(2, '0'),
                Value = x.USR_ID + "^" + x.YRFR.Value.Date.Day.ToString().PadLeft(2, '0') + "/" + x.YRFR.Value.Date.Month.ToString().PadLeft(2, '0') + "/" + x.YRFR.Value.Date.Year.ToString().PadLeft(2, '0') +
                " - "
                + x.YRTO.Value.Date.Day.ToString().PadLeft(2, '0') + "/" + x.YRTO.Value.Date.Month.ToString().PadLeft(2, '0') + "/" + x.YRTO.Value.Date.Year.ToString().PadLeft(2, '0')
                + "^" + x.YR +"^"+ x.MAIN_UNIT
            }).ToList<ModelMaster>();

            return Lst;
        }

        public ActionResult DashBoard(Model_CompanySelection model_CompanySelection)
        {
            GetCookies getCookies = new GetCookies();
            var jsonData = Newtonsoft.Json.JsonConvert.SerializeObject(model_CompanySelection);



            Model_CompanStartUp model_CompanStartUp = new Model_CompanStartUp();
            Model_SessionOtherProject model_SessionOtherProject = (Model_SessionOtherProject)TempData["SessionCookies"];
            string[] Fy = model_CompanySelection.ProjectDb.ToString().Split('^');

            Session["DbName"] = Fy[0];
            string Os = Environment.UserName;
            string TerminalId = Environment.MachineName;

            model_SessionOtherProject.ProjectDb = Fy[0];
            model_SessionOtherProject.FinancialYearName = Fy[1];
            model_SessionOtherProject.YR= Fy[2];
            model_SessionOtherProject.MAIN_UNIT = Fy[3];
            model_SessionOtherProject.Comid = model_CompanySelection.Comid;
            model_SessionOtherProject.Company_Name = model_CompanySelection.Comid;
            model_SessionOtherProject.UnitDesnitaion = model_CompanySelection.Loca;
            model_SessionOtherProject.Os_user = Os;
            model_SessionOtherProject.Terminal_id = TerminalId;

            model_CompanStartUp.DdlFYear = model_CompanySelection.ProjectDb;
            model_CompanStartUp.DdlCompId = model_CompanySelection.Comid;
            model_CompanStartUp.DdlCountry = model_CompanySelection.Loca;
             



            getCookies.SetProjectCookies(model_SessionOtherProject);
            
            //var jsonDataSessionCookies = Newtonsoft.Json.JsonConvert.SerializeObject(model_SessionOtherProject);
            //HttpCookie moduleSelectorcookies = new HttpCookie("Model_Session", jsonDataSessionCookies);
            //HttpContext.Response.Cookies.Add(moduleSelectorcookies);
            //moduleSelectorcookies.Expires = DateTime.Now.AddMinutes(45);


            if (model_CompanySelection.Ischeked == true)
            {
                getCookies.SetCompanyCookies(model_CompanStartUp);
                logDefault.IniWriteValue(model_SessionOtherProject.UserId + '_' + model_SessionOtherProject.ModuleCode, "DefaultLog", jsonData, Server.MapPath("~/Log/Jaguar.ini"));

            }
            //  return new RedirectResult(url: "/ProjectStartUp_DashBoard/ProjectStartUp_DashBoard", permanent: true);
            // return RedirectPermanent("/ProjectStartUp_DashBoard/ProjectStartUp_DashBoard");
            // return RedirectToAction("/LandPage/LandPage");
            // return RedirectToAction("LandPage", "LandPage", new { area = "Admin" });

            return  Redirect("/ProjectStartUp_DashBoard/ProjectStartUp_DashBoard");
        }



    }
}