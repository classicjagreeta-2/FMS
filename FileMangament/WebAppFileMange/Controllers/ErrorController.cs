using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using static WebAppStartUp.Models.Model_Common;


namespace WebAppFileMange.Controllers
{
    public class ErrorController : Controller
    {
       // Logger logger = LogManager.GetCurrentClassLogger();

        // GET: Error
        public ViewResult Index(Exception exception)
        {

            Response.StatusCode = 500;

            Exception ex = Server.GetLastError();
            ViewBag.Error = ex;
            // logger.ErrorException("", ex);
            ModelMaster ModelMaster = new ModelMaster
            {
                Text = ex.ToString()
            };
            return View("~/Views/Shared/_Error.cshtml", ModelMaster);
        }
        public ViewResult Index2(string ExMsg)
        {
            ViewBag.Error = ExMsg;
            return View("~/Views/Shared/_Error.cshtml");
        }

        public ViewResult NoPrivilage(string StrUrl)
        {

            ViewBag.Url = StrUrl;
            return View("~/Views/Shared/_NoPrivilage.cshtml");
        }
        public ViewResult NoEdit(string StrUrl)
        {

            ViewBag.Url = StrUrl;
            return View("~/Views/Shared/_NoEdit.cshtml");
        }
        public ActionResult NotFound()
        {
            Response.StatusCode = 404;

            //return new FilePathResult("~/libraries/extra-pages/04/2/Error404.htm", "text/html");
            return View("_NotFound");
        }

        public JsonResult KeepSessionAlive()
        {
            return new JsonResult
            {
                Data = "Beat Generated"
            };
        }

    }
}