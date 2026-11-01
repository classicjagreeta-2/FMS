using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using WEB_PRO17;
using WebAppFileMange.Controllers;

namespace WebAppFileMange
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
        }
        protected void Application_Error()
        {

            Exception ex = Server.GetLastError();
            LogDefault logDefault = new LogDefault();
            Logger logger = LogManager.GetCurrentClassLogger();

            //  logDefault.IniWriteValue("Error_" + DateTime.Now.ToString("dd/MM/yyhhmmss") + "", "DefaultLog", ex.Message, Server.MapPath("~/Log/JaguarError.ini"));
            //if (!ex.Message.Contains("A potentially dangerous"))
            //    logger.ErrorException("", ex);
            //  Not Found(When user digit unexisting url)
            if (ex is HttpException && ((HttpException)ex).GetHttpCode() == 404)
            {
                HttpContextWrapper contextWrapper = new HttpContextWrapper(this.Context);

                RouteData routeData = new RouteData();
                routeData.Values.Add("controller", "Error");
                routeData.Values.Add("action", "NotFound");

                IController controller = new ErrorController();
                RequestContext requestContext = new RequestContext(contextWrapper, routeData);
                controller.Execute(requestContext);
                Response.End();
            }
            else //Unhandled Errors from aplication
            {
                if (ex.Message.Contains("A potentially dangerous Request."))
                { }
                else
                {
                    logger.ErrorException("", ex);
                   // ErrorLogService.LogError(ex);
                    HttpContextWrapper contextWrapper = new HttpContextWrapper(this.Context);

                    RouteData routeData = new RouteData();
                    routeData.Values.Add("controller", "Error");
                    routeData.Values.Add("action", "Index");
                    routeData.Values.Add("exception", ex);

                    IController controller = new ErrorController();
                    RequestContext requestContext = new RequestContext(contextWrapper, routeData);
                    controller.Execute(requestContext);
                    Response.End();
                    contextWrapper.Server.ClearError();
                }

            }
        }
    }
}
