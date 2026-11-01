using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Mvc;
using Ionic.Zip;
using WebAppFileMange.Models;
using WebAppStartUp.Common;
using WebAppStartUp.Models;

namespace WebAppFileMange.Controllers.Report
{
    public class FMS_ReportController : Controller
    {
        // GET: FMS_Report
        public ActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public ActionResult DownLoadFile(string path, string FileName)
        {
            // path = "D:/RR/Project/FileMangament/WebAppFileMange/WebAppFileMange/UploadedFiles/FMS_Files/COMPANY 2/UNIT 3/DEPARTMENT 3/TYPE 5/COMPANY_2_UNIT_3_DEPARTMENT_3_TYPE_5_190623104320.pdf";

            return File(path, "application/pdf", FileName);
        }
        #region anish code
        //anish code

        //[HttpGet]
        //public ActionResult ShowFiles(string ReportURL)
        //{
        //    Model_User MODELP = new Model_User();
        //    string staticPassword = System.Configuration.ConfigurationManager.AppSettings["ZipPassword"];


        //    string ext = Path.GetExtension(ReportURL).Replace(".", "");

        //    if (ext == "zip")
        //    {
        //        if (!System.IO.File.Exists(ReportURL))
        //        {
        //            TempData["Error"] = "ZIP file not found.";
        //            return RedirectToAction("Index");
        //        }

        //        try
        //        {
        //            using (var zip = ZipFile.Read(ReportURL))
        //            {
        //                zip.Password = staticPassword;
        //                try
        //                {
        //                    zip.Entries.FirstOrDefault()?.Extract(new MemoryStream());
        //                }
        //                catch
        //                {
        //                    TempData["Error"] = "Invalid password for the ZIP file.";
        //                    return RedirectToAction("Index");
        //                }

        //                foreach (var entry in zip.Entries)
        //                {
        //                    if (entry.FileName.EndsWith(".pdf", StringComparison.OrdinalIgnoreCase))
        //                    {
        //                        using (var MemoryStream = new MemoryStream())
        //                        {
        //                            entry.Extract(MemoryStream);
        //                            MemoryStream.Position = 0;

        //                            Response.Headers["Content-Disposition"] = "inline";
        //                            return File(MemoryStream.ToArray(), "application/pdf");
        //                        }
        //                    }
        //                }
        //                TempData["Error"] = "No PDF file found in the ZIP.";
        //                return RedirectToAction("Index");
        //            }
        //        }
        //        catch (Exception ex)
        //        {
        //            TempData["Error"] = "Failed to open the ZIP file.";
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    else
        //    {
        //        // If it's a standalone PDF or image, serve it
        //        switch (ext)
        //        {
        //            case "pdf":
        //                Response.Headers["Content-Disposition"] = "inline"; // Open PDFs in browser
        //                return File(ReportURL, "application/pdf");

        //            default:
        //                return File(ReportURL, "image/jpeg");
        //        }
        //    }
        //    switch (ext)
        //    {
        //        case "pdf":
        //            return File(ReportURL, "application/" + ext);

        //        default:
        //            return base.File(ReportURL);

        //    }

        //}


        [HttpGet]
        public ActionResult ShowFile(string ReportURL)
        {
            // Read static password from Web.config
            string staticPassword = System.Configuration.ConfigurationManager.AppSettings["ZipPassWord"];

            // Get file extension
            string ext = Path.GetExtension(ReportURL).Replace(".", "").ToLower();

            // If the file is a ZIP, extract its contents
            if (ext == "zip")
            {
                if (!System.IO.File.Exists(ReportURL))
                {
                    TempData["Error"] = "ZIP file not found.";
                    return RedirectToAction("Index");
                }

                try
                {
                    using (var zip = ZipFile.Read(ReportURL))
                    {
                        zip.Password = staticPassword; // Set ZIP password

                        // Attempt to extract and validate password
                        try
                        {
                            zip.Entries.FirstOrDefault()?.Extract(new MemoryStream());
                        }
                        catch
                        {
                            TempData["Error"] = "Invalid password for the ZIP file.";
                            return RedirectToAction("Index");
                        }

                        // Extract files and return the first valid PDF or image
                        foreach (var entry in zip.Entries)
                        {
                            string fileExtension = Path.GetExtension(entry.FileName).ToLower();
                            if (fileExtension == ".pdf" || fileExtension == ".jpg" || fileExtension == ".jpeg" || fileExtension == ".png")
                            {
                                using (var memoryStream = new MemoryStream())
                                {
                                    entry.Extract(memoryStream);
                                    memoryStream.Position = 0;

                                    if (fileExtension == ".pdf")
                                    {
                                        Response.Headers["Content-Disposition"] = "inline";
                                        return File(memoryStream.ToArray(), "application/pdf");
                                    }
                                    else
                                    {
                                        return File(memoryStream.ToArray(), "image/jpeg");
                                    }
                                }
                            }
                        }

                        // If no valid file is found, show an error
                        TempData["Error"] = "No PDF or image file found in the ZIP.";
                        return RedirectToAction("Index");
                    }
                }
                catch
                {
                    TempData["Error"] = "Failed to open the ZIP file.";
                    return RedirectToAction("Index");
                }
            }
            else
            {
                // If it's a standalone PDF or image, serve it
                switch (ext)
                {
                    case "pdf":
                        Response.Headers["Content-Disposition"] = "inline";
                        return File(ReportURL, "application/pdf");

                    case "jpg":
                    case "jpeg":
                    case "png":
                        return File(ReportURL, "image/jpeg");

                    default:
                        TempData["Error"] = "Unsupported file format.";
                        return RedirectToAction("Index");
                }
            }
        }


        #endregion

        //anish code end 
        public ActionResult Report_DetailsAjax(string AutoId, string FmType, string[] CheckList)
        {


            //https://stackoverflow.com/questions/40224822/how-to-combine-several-crystal-reports-in-one-pdf
            //TD_Model_ReportFilter tD_Model_ReportFilter = new TD_Model_ReportFilter();

            //if (CheckList[0] != "")
            //{

            //    CheckList = CheckList[0].ToString().Split(',');
            //    foreach (var item in CheckList)
            //    {
            //        var PropertyInfo = typeof(TD_Model_ReportFilter).GetProperty(item);
            //        PropertyInfo.SetValue(tD_Model_ReportFilter, Convert.ChangeType(true, PropertyInfo.PropertyType), null);
            //    }
            //}

            //TempData["TData"] = tD_Model_ReportFilter;

            // byte[] buffer = GetReportData(AutoId,1,  tD_Model_ReportFilter);

            //  return Json(buffer, JsonRequestBehavior.AllowGet);
            // Pdf_DownLoadWithByte(buffer);

            string ReportURL = "D:/RR/Project/FileMangament/WebAppFileMange/WebAppFileMange/UploadedFiles/FMS_Files/COMPANY 2/UNIT 3/DEPARTMENT 3/TYPE 5/COMPANY_2_UNIT_3_DEPARTMENT_3_TYPE_5_190623104320.pdf";
            byte[] FileBytes = System.IO.File.ReadAllBytes(ReportURL);
            return File(FileBytes, "application/pdf");



        }
        //public byte[] GetReportData(string AutoId, int ReportId, TD_Model_ReportFilter tD_Model_ReportFilter)
        //{
        //    GetCookies getCookies = new GetCookies();
        //    var Cookies = getCookies.GetStoredCookies();


        //    return buffer;
        //}
        public void Pdf_DownLoadWithByte(byte[] buffer)
        {



            Response.Clear();
            Response.Buffer = true;
            Response.ContentType = "application/pdf";

            //merge the all reports & show the reports            
            Response.BinaryWrite(buffer);
            Response.End();
        }
    }
}