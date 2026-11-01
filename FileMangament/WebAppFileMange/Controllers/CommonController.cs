using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_PRO17;
using WebAppFileMange.Models;
using WebAppFileMange.Models.Master;
using WebAppStartUp.Common;
using WebAppStartUp.Models;

namespace WebAppFileMange.Controllers
{
    public class CommonController : Controller
    {
        GetCookies getCookies = new GetCookies();
        ServiceRepository serviceObj = new ServiceRepository();
        // GET: Common
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadReportCheckList(string AutoId, string FmType, string ViewId,string FileName)
        {
            Model_Report model_Report = new Model_Report();
            Model_SessionOtherProject Cookies_Stored = getCookies.GetStoredCookies();
            var LoginDetails = Utility.GetLoggedInUserDetails();
            FMS_Model_Permission fMS_Model_Permission = new FMS_Model_Permission();
            fMS_Model_Permission.UserName = LoginDetails.UserId;
           // fMS_Model_Permission.UserName = "CC";
            var Lst = serviceObj.SubmitPost<Mast_Permissions>("api/FMS/Document/GetPermissionDetails", fMS_Model_Permission);
            model_Report.LstReport = Lst.Lst;

            return View("_ReportCheckListShare", model_Report);
        }

        public ActionResult SendEmail(string ToEmail, string AutoId, string FmType,string FileName,string Sub)
        {
            string[] CheckList = null;
            string FilePath = FmType.Replace("^", "\\");
            string RtFMode = FmType;
            string RtsubmitType = TempData["submitType"] as string;
            int EmailSentstatus = 0;
            string Emailmsg = string.Empty; ;
            string ErrorMsg = string.Empty;
            // string RtFMode = "S";
            int PI_Autoid = Convert.ToInt32(AutoId);
            TD_Model_ReportFilter tD_Model_ReportFilter = new TD_Model_ReportFilter();



            string[] str = SendEmailSo(ToEmail, "", "", FilePath, CheckList, 1,FileName,Sub);

            EmailSentstatus = Convert.ToInt32(str[0]);
            if (EmailSentstatus <= 0)
            {
                EmailSentstatus = 0;
                Emailmsg = "Email Sending Failed";
                ErrorMsg = (str[1]);

            }
            else
            {
                TempData["EmailSent"] = "EMAIL SENT";
                Emailmsg = "Email Sent";
            }
            TempData.Keep();
            return Json(new
            {
                Id = EmailSentstatus,
                success = true,
                message = Emailmsg + "<br>" + ErrorMsg.ToString(),
                fmode = RtFMode,
                submitType = RtsubmitType,
                details = ErrorMsg.ToString(),
                JsonRequestBehavior.AllowGet
            });

            //  return Json(new { Id = EmailSentstatus }, JsonRequestBehavior.AllowGet);
        }

        //public string[] SendEmailSo(string ToEmail, string CCEmail, string FileName, string FilePath, string[] CheckList, int AttchFile,
        //    string sFileName,string Subj)
        //{





        //    SendMail sendMailClass = new SendMail();
        //    HttpCookie Rt = Request.Cookies["Model_Session"];
        //    Model_SessionOtherProject jsonDataR = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_SessionOtherProject>(Rt.Value);
        //    string[] MailSending = null;



        //    MailSending = sendMailClass.SendMailAttchment(null, null, ToEmail, jsonDataR.Company_Name, AttchFile, "587", "smtp.gmail.com",
        //"rajeshraj.classic@gmail.com", "bpjklgvzohrgexad", "rajeshraj.classic@gmail.com", Subj, "HH", "", "",
        //"Please find the attachment along with this mail", FilePath,sFileName);




        //    return MailSending;

        //}

        public string[] SendEmailSo(string ToEmail, string CCEmail, string FileName, string FilePath, string[] CheckList, int AttchFile,
    string sFileName, string Subj)
        {
            SendMail sendMailClass = new SendMail();
            HttpCookie Rt = Request.Cookies["Model_Session"];
            Model_SessionOtherProject jsonDataR = Newtonsoft.Json.JsonConvert.DeserializeObject<Model_SessionOtherProject>(Rt.Value);
            string[] MailSending = null;

            // ✅ First, try SSL on Port 465
            MailSending = sendMailClass.SendMailAttchment(null, null, ToEmail, jsonDataR.Company_Name, AttchFile,
                "465",  // ✅ Change to port 465 for SSL
                "smtp.gmail.com",
                "rajeshraj.classic@gmail.com",
                "bpjklgvzohrgexad",  // ✅ Use an App Password (Not your Gmail password!)
                "rajeshraj.classic@gmail.com",
                Subj, "HH", "", "",
                "Please find the attachment along with this mail",
                FilePath, sFileName);

            return MailSending;
        }


    }
}