using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebAppFileMange.Controllers
{
    public class ScannerController : Controller
    {
        
        // GET: Scanner
        public ActionResult Scanner()
        {
            Process process = new Process();

            var str = Process.GetProcessesByName("NewScan").Length;
            if (Process.GetProcessesByName("NewScan").Length <= 0)
            {
                try
                {
                    process = Process.Start(@"C:\Program Files (x86)\MdGRabbany\Scan App\NewScan.exe");
                }
                catch (Exception ex)
                {

                    // throw;
                }

            }
            //else
            //{
            //    var processes = Process.GetProcesses();

            //    foreach (Process pr in processes)
            //    {

            //        if (pr.ProcessName == "NewScan")
            //        {
            //            pr.Kill();
                        
            //            //process = Process.Start(@"C:\Program Files (x86)\MdGRabbany\Scan App\NewScan.exe");
            //        }

            //    }
                
            //}
        

         
            return View();
        }
        [HttpPost]
        public ActionResult FileUpload(string base64)
        {
            string I = DateTime.Now.ToString("ddMMyyhhmmss");
            string Tsr = base64.Replace("data:application/octet-stream;base64,", string.Empty);
            byte[] imageBytes = Convert.FromBase64String(Tsr);

            //Save the Byte Array as Image File.
            string filePath = Server.MapPath("~/UploadedFiles/ScannedFiles/" + I + "_Img.jpg");

            string filePathPdf = Server.MapPath("~/UploadedFiles/ScannedFiles/" + I + "_Img.pdf");

            System.IO.File.WriteAllBytes(filePath, imageBytes);


              
            System.IO.File.WriteAllBytes(filePathPdf, imageBytes);
            return Json(new { UploadedFileCount = Request.Files.Count });
        }


        public string Pdf_DownLoadWithByte_ToSaveInFolder(byte[] buffer)
        {
            MemoryStream ms = new MemoryStream(buffer);
            string FileName = DateTime.Now.ToString("ddMMyyHHmmss") + ".pdf";
            string Path = Server.MapPath("~/UploadedFiles/ScannedFiles/" + FileName);

            //FileStream file = new FileStream(Path, FileMode.Create, FileAccess.Write);
            //ms.WriteTo(file);
            //file.Close();
            //ms.Close();


            //Response.Clear();
            //Response.AddHeader("Cache-Control", "no-cache, must-revalidate, post-check=0, pre-check=0");
            //Response.AddHeader("Pragma", "no-cache");
            //Response.AddHeader("Content-Description", "File Download");
            //Response.AddHeader("Content-Type", "application/pdf");
             
            //Response.AddHeader("content-disposition", "attachment;filename=" + FileName);
            
            //Response.BinaryWrite(buffer);
           
            //Response.End();


            return FileName;
        }
    }
}