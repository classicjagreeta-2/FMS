using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WEB_PRO17;
using WebAppFileMange.Models;
using WebAppFileMange.Models.Master;
using WebAppFileMange.Models.Table;
using WebAppStartUp.Common;
using WebAppStartUp.Models;
using static WebAppStartUp.Models.Model_Common;
using static WEB_PRO17.CommonServices;
using WebAppFileMange.Common;
using Newtonsoft.Json;
using System.Data;
using System.Configuration;
using System.Diagnostics;
using System.IO.Compression;
using Ionic.Zip;
using ZipFile = Ionic.Zip.ZipFile;


namespace WebAppFileMange.Controllers
{

    public class HomeController : BasePermissionController//BasePermissionController
    {
        string DefaultFolderType = "UploadFirst";
        string DefaultTransferFolder = "FMS_Files";
        ServiceRepository serviceObj = new ServiceRepository();
        //int[] Autoid = {44 };
        //int[] Autoid0 = { 45 };

        int[] Autoid = { Convert.ToInt32(ConfigurationManager.AppSettings["Hierarchy"].ToString()) };
        int[] Autoid0 = { Convert.ToInt32(ConfigurationManager.AppSettings["Hierarchy0"].ToString()) };
        // GET: Default
        public ActionResult Index()
        {

            return View();
        }

        public ActionResult ScannerON()
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
            return RedirectToAction("Scanner");
        }
        public ActionResult ScannerOFF()
        {
            var processes = Process.GetProcesses();

            foreach (Process pr in processes)
            {

                if (pr.ProcessName == "NewScan")
                {
                    pr.Kill();
                    //process = Process.Start(@"C:\Program Files (x86)\MdGRabbany\Scan App\NewScan.exe");
                }

            }



            return RedirectToAction("Scanner");
        }
        public ActionResult Scanner()
        {

            ViewBag.DropdownListMasterFileHierarchy = GetMasterFileHierarchy(Autoid);
            ViewBag.DropdownListMasterFileHierarchy0 = GetMasterFileHierarchy(Autoid0);

            ViewBag.DropdownListMasterNull = "";
            ViewBag.HLevel = Convert.ToInt32(ConfigurationManager.AppSettings["Hierarchy"].ToString());
            ViewBag.HLevel2 = Convert.ToInt32(ConfigurationManager.AppSettings["Hierarchy0"].ToString());
            ViewBag.FromSession = DateTime.Now.ToString("ddMMyyHHmmss"); //Create Temp DIR

            ViewBag.FmType = 0; //For Upload Form
            ViewBag.DropdownListMasterYear = GetYear();


            //Process process = new Process();

            //var str = Process.GetProcessesByName("NewScan").Length;
            //if (Process.GetProcessesByName("NewScan").Length <= 0)
            //{
            //    try
            //    {
            //        process = Process.Start(@"C:\Program Files (x86)\MdGRabbany\Scan App\NewScan.exe");
            //    }
            //    catch (Exception ex)
            //    {

            //        // throw;
            //    }

            //}
            //else
            //{
            //    var processes = Process.GetProcesses();

            //    foreach (Process pr in processes)
            //    {

            //        if (pr.ProcessName == "NewScan")
            //        {
            //            pr.Kill();
            //            process = Process.Start(@"C:\Program Files (x86)\MdGRabbany\Scan App\NewScan.exe");
            //        }

            //    }

            //}
            return View();
        }
        public ActionResult FileUpload()
        {


            ViewBag.DropdownListMasterFileHierarchy = GetMasterFileHierarchy(Autoid);
            ViewBag.DropdownListMasterFileHierarchy0 = GetMasterFileHierarchy(Autoid0);

            ViewBag.DropdownListMasterNull = "";
            ViewBag.HLevel = Convert.ToInt32(ConfigurationManager.AppSettings["Hierarchy"].ToString());
            ViewBag.HLevel2 = Convert.ToInt32(ConfigurationManager.AppSettings["Hierarchy0"].ToString());
            ViewBag.FromSession = DateTime.Now.ToString("ddMMyyHHmmss"); //Create Temp DIR

            ViewBag.FmType = 0; //For Upload Form
            ViewBag.DropdownListMasterYear = GetYear();
            return View();
        }
        //  [OutputCache(Duration = 300)] //Cache for 5 minutes.
        public ActionResult LoadHeadData(string Hic, string Hic2, int? Id)
        {
            List<ModelMaster> MasterParent1 = GetMasterFileHierarchy(Autoid).ToList();
            ViewBag.DropdownListMasterFileHierarchy = MasterParent1;
            var str = String.Join(",", MasterParent1.Select(x => x.Value));


            File_Hierarchy file_Hierarchy = new File_Hierarchy();
            new Head();
            ViewBag.DropdownListMasterFileHierarchy0 = GetMasterFileHierarchy(Autoid0);
            ViewBag.DropdownListMasterNull = "";
            ViewBag.DropdownListMasterYear = GetYear();
            ViewBag.FmType = 1; //for filter for > 0
                                //return View(FileHier);

            ViewBag.HLevel = Convert.ToInt32(ConfigurationManager.AppSettings["Hierarchy"].ToString());
            ViewBag.HLevel2 = Convert.ToInt32(ConfigurationManager.AppSettings["Hierarchy0"].ToString());

            Head hHead = serviceObj.SubmitGet<Head>("api/FMS/Document/EditHeadData?Id=" + Id.ToString());

            ViewBag.Hierarchy = Hic;
            ViewBag.Hierarchy2 = Hic2;
            file_Hierarchy.Head = hHead;
            return View(file_Hierarchy);
        }

        [OutputCache(Duration = 300)] //Cache for 5 minutes.
        public ActionResult FilterData()
        {
            //Head hHead = TempData["Head"] as Head;
            //TempData.Keep();

            //File_Hierarchy FileHier = new File_Hierarchy();


            //FileHier.Head = hHead;


            List<ModelMaster> MasterParent1 = GetMasterFileHierarchy(Autoid).ToList();
            ViewBag.DropdownListMasterFileHierarchy = MasterParent1;
            var str = String.Join(",", MasterParent1.Select(x => x.Value));



            ViewBag.DropdownListMasterFileHierarchy0 = GetMasterFileHierarchy(Autoid0);
            ViewBag.DropdownListMasterNull = "";
            ViewBag.DropdownListMasterYear = GetYear();
            ViewBag.FmType = 1; //for filter for > 0
            //return View(FileHier);
            return View();
        }
        //   [OutputCache(Duration = 300)] //Cache for 5 minutes.
        [HttpPost]
        public JsonResult Loaddll(string ddlCode)
        {
            var Lst = (object)null;
            if (ddlCode != "")
            {
                int[] DCode = Array.ConvertAll(ddlCode.Split(','), s => int.Parse(s));
                Lst = GetMasterFileHierarchy(DCode);
            }

            return Json(Lst, JsonRequestBehavior.AllowGet);
        }

        List<ModelMaster> GetYear()
        {

            var LstReturn = serviceObj.SubmitGet<List<MAST_YEAR>>("api/FMS/Master/GetMAST_YEAR");
            var Lstddl = LstReturn.Select(x => new ModelMaster
            {
                Text = x.Yyear.ToString(),
                Value = x.Yyear.ToString()
            }).ToList<ModelMaster>();
            return Lstddl;
        }
        [OutputCache(Duration = 300)] //Cache for 5 minutes.
        List<ModelMaster> GetMasterFileHierarchy(int[] ddlCode)
        {


            Parameter pParameter = new Parameter();
            pParameter.MyIntArrayData = ddlCode;
            var LstReturn = serviceObj.SubmitGetList<Mast_File_Hierarchy>("api/FMS/Master/GetMaster_File_Hierarchy", pParameter);
            var Lstddl = LstReturn.Select(x => new ModelMaster
            {
                Text = x.HIERARCHY_NAME,
                Value = x.AUTOID.ToString()
            }).ToList<ModelMaster>();
            return Lstddl;
        }

        [HttpPost]
        public ActionResult UpdateForm(File_Hierarchy fMS_Model_Document)
        {
            GetCookies getCookies = new GetCookies();
            Model_SessionOtherProject model_SessionOtherProject = new Model_SessionOtherProject();
            model_SessionOtherProject = getCookies.GetStoredCookies();

            if (model_SessionOtherProject.UnitDesnitaion == null || model_SessionOtherProject.UnitDesnitaion == string.Empty ||
                model_SessionOtherProject.UnitDesnitaion == "")
                throw new HttpException((int)0, "Your Login Session Is Expired ,Please Re-Login and Try Again");

            var JsReturn = serviceObj.SubmitPost<ApiResponse<File_Hierarchy>>("api/FMS/Document/UpdateEntry", fMS_Model_Document);


            return Json(new { rReturn = JsReturn }, JsonRequestBehavior.AllowGet);

        }
        //[HttpPost]
        //public ActionResult SaveForm(File_Hierarchy fMS_Model_Document)
        //{
        //    string FilePath = TempData["folder"] as string;
        //    GetCookies getCookies = new GetCookies();
        //    Model_SessionOtherProject model_SessionOtherProject = new Model_SessionOtherProject();

        //    model_SessionOtherProject = getCookies.GetStoredCookies();

        //    if (model_SessionOtherProject.UnitDesnitaion == null || model_SessionOtherProject.UnitDesnitaion == string.Empty ||
        //        model_SessionOtherProject.UnitDesnitaion == "")
        //        throw new HttpException((int)0, "Your Login Session Is Expired ,Please Re-Login and Try Again");

        //    List<TRN_Document> Lstfiles = new List<TRN_Document>();

        //    string FolderServerLocation = "~/UploadedFiles/" + DefaultFolderType + "/" + fMS_Model_Document.Head.UploadSessionId;
        //    string dirPath = Server.MapPath(FolderServerLocation);
        //    string[] filePaths = Directory.GetFiles(dirPath);

        //    //Move the file to the Folder Location 

        //    string FolderLocation = "~/UploadedFiles/" + DefaultTransferFolder + "/" + fMS_Model_Document.Head.FilePath;
        //    string FolderPath = Server.MapPath(FolderLocation);
        //    if (!Directory.Exists(FolderPath))
        //    {
        //        Directory.CreateDirectory(FolderPath);
        //    }

        //    int? MaxId = serviceObj.SubmitPost2<TRN_Document>("api/FMS/Document/GetMaxId", model_SessionOtherProject.UnitDesnitaion).Id;
        //    int i = 1;
        //    foreach (string filePath in filePaths)
        //    {

        //        string NewfileName = fMS_Model_Document.Head.FileName + "_" + MaxId + "_" + i.ToString() + Path.GetExtension(filePath);
        //        string destFile = System.IO.Path.Combine(FolderPath, NewfileName); // File ReName
        //        if (!System.IO.File.Exists(FolderPath))
        //        {
        //            System.IO.File.Copy(filePath, destFile, true); //File Copy

        //            TRN_Document tRN_Document = new TRN_Document
        //            {

        //                FileName = NewfileName,
        //                FilePath = FolderLocation,
        //                FullPath = destFile
        //            };
        //            Lstfiles.Add(tRN_Document);

        //        }
        //        i++;


        //    }

        //    fMS_Model_Document.Head.Unit = model_SessionOtherProject.UnitDesnitaion;
        //    fMS_Model_Document.LstDocument = Lstfiles;
        //    fMS_Model_Document.Head.dtDate = DateTime.Now;

        //    var JsReturn = serviceObj.SubmitPost<ApiResponse<File_Hierarchy>>("api/FMS/Document/AddFileEntry", fMS_Model_Document);

        //    if (JsReturn.StatusCode > 0)
        //    {
        //        DirectoryInfo dir = new DirectoryInfo(dirPath);
        //        dir.GetFiles("*", SearchOption.AllDirectories).ToList().ForEach(file => file.Delete());
        //        // will delete all files in the folder and its sub folder
        //        //so you don't need to iterate each sub folder and files in it
        //        Directory.Delete(dirPath);
        //    }

        //    //var JsReturn = "";

        //    return Json(new { rReturn = JsReturn, Lst = Lstfiles }, JsonRequestBehavior.AllowGet);
        //}


        [HttpPost]
        public ActionResult SaveForm(File_Hierarchy fMS_Model_Document)
        {
            string FilePath = TempData["folder"] as string;
            GetCookies getCookies = new GetCookies();
            Model_SessionOtherProject model_SessionOtherProject = new Model_SessionOtherProject();

            model_SessionOtherProject = getCookies.GetStoredCookies();

            if (string.IsNullOrWhiteSpace(model_SessionOtherProject.UnitDesnitaion))
                throw new HttpException((int)0, "Your Login Session Is Expired, Please Re-Login and Try Again");

            List<TRN_Document> Lstfiles = new List<TRN_Document>();

            string FolderServerLocation = "~/UploadedFiles/" + DefaultFolderType + "/" + fMS_Model_Document.Head.UploadSessionId;
            string dirPath = Server.MapPath(FolderServerLocation);
            string[] filePaths = Directory.GetFiles(dirPath);

            // Move the file to the Folder Location
            string FolderLocation = "~/UploadedFiles/" + DefaultTransferFolder + "/" + fMS_Model_Document.Head.FilePath;
            string FolderPath = Server.MapPath(FolderLocation);

            if (!Directory.Exists(FolderPath))
            {
                Directory.CreateDirectory(FolderPath);
            }

            int? MaxId = serviceObj.SubmitPost2<TRN_Document>("api/FMS/Document/GetMaxId", model_SessionOtherProject.UnitDesnitaion).Id;
            int i = 1;

            foreach (string filePath in filePaths)
            {
                string NewfileName = fMS_Model_Document.Head.FileName + "_" + MaxId + "_" + i.ToString() + Path.GetExtension(filePath);
                string destFile = Path.Combine(FolderPath, NewfileName); // File ReName

                // ZIP File Name
                string zipFileName = Path.Combine(FolderPath, Path.GetFileNameWithoutExtension(NewfileName) + ".zip");

                // Create Password-Protected ZIP
                using (var zip = new ZipFile())
                {
                    zip.Password = System.Configuration.ConfigurationManager.AppSettings["ZipPassWord"]; // Static password as per request
                    

                    using (var fileStream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                    using (var memoryStream = new MemoryStream())
                    {
                        fileStream.CopyTo(memoryStream);
                        memoryStream.Position = 0; // Reset stream position

                        zip.AddEntry(NewfileName, memoryStream.ToArray()); // Use ToArray() to make it seekable
                    }

                    zip.Save(zipFileName);
                }



                TRN_Document tRN_Document = new TRN_Document
                {
                    FileName = Path.GetFileName(zipFileName),
                    FilePath = FolderLocation,
                    FullPath = zipFileName
                };
                Lstfiles.Add(tRN_Document);

                i++;
            }

            fMS_Model_Document.Head.Unit = model_SessionOtherProject.UnitDesnitaion;
            fMS_Model_Document.LstDocument = Lstfiles;
            fMS_Model_Document.Head.dtDate = DateTime.Now;

            var JsReturn = serviceObj.SubmitPost<ApiResponse<File_Hierarchy>>("api/FMS/Document/AddFileEntry", fMS_Model_Document);

            if (JsReturn.StatusCode > 0)
            {
                DirectoryInfo dir = new DirectoryInfo(dirPath);
                dir.GetFiles("*", SearchOption.AllDirectories).ToList().ForEach(file => file.Delete());
                Directory.Delete(dirPath);
            }

            return Json(new { rReturn = JsReturn, Lst = Lstfiles }, JsonRequestBehavior.AllowGet);
        }


        [HttpPost]
        public ActionResult SearchData(string Hierarchy, string Hierarchy2, string VoucherId,
            string Remarks, string Year, DateTime? FromDate, DateTime? ToDate)
        {
            Head hHead = new Head();
            hHead.Hierarchy = Hierarchy;
            hHead.Hierarchy2 = Hierarchy2;
            hHead.VoucherId = VoucherId;
            hHead.Remarks = Remarks;
            hHead.Yyear = Year;
            hHead.FromDate = FromDate;
            hHead.ToDate = ToDate;
            //var Lst = serviceObj.SubmitPost<dynamic>("api/FMS/Document/GetFilterReportData", hHead);

            TempData["Head"] = hHead;
            return Json("");
        }
        public ActionResult FileListx()
        {
            SpDefinedParamters spDefinedParamters = new SpDefinedParamters();
            Model_MasterDynamicSp model_MasterDynamicSp = new Model_MasterDynamicSp();

            Model_MasterSpParamters model_MasterSpParamters = new Model_MasterSpParamters();
            model_MasterSpParamters.SpId = 1;
            model_MasterDynamicSp = spDefinedParamters.GetFileList(model_MasterSpParamters);
            var RtDaTa = serviceObj.SubmitGetList<dynamic>("api/Td/Td_Report/GetReportData", model_MasterDynamicSp).
                   ToList();

            var jsonD = JsonConvert.SerializeObject(RtDaTa);
            DataTable dtR = (DataTable)JsonConvert.DeserializeObject(jsonD, (typeof(DataTable)));

            return View("~/Views/Home/_FileList.cshtml", dtR);

        }
        public ActionResult FileList2()
        {
            TempData.Keep();
            return View("~/Views/Home/_FileList.cshtml");
        }
        [HttpPost]
        public ActionResult GetFileList()
        {
            Head hHead = TempData["Head"] as Head;
            JsonResult result = new JsonResult();
            string search = Request.Form.GetValues("search[value]")[0];
            string draw = Request.Form.GetValues("draw")[0];
            string order = Request.Form.GetValues("order[0][column]")[0];
            string orderDir = Request.Form.GetValues("order[0][dir]")[0];
            int startRec = Convert.ToInt32(Request.Form.GetValues("start")[0]);
            int pageSize = Convert.ToInt32(Request.Form.GetValues("length")[0]);

            //SpDefinedParamters spDefinedParamters = new SpDefinedParamters();
            //Model_MasterDynamicSp model_MasterDynamicSp = new Model_MasterDynamicSp();

            //Model_MasterSpParamters model_MasterSpParamters = new Model_MasterSpParamters();
            //model_MasterSpParamters.SpId = 1;
            //model_MasterDynamicSp = spDefinedParamters.GetFileList(model_MasterSpParamters);

            hHead.search = search;
            hHead.draw = draw;
            hHead.order = order;
            hHead.orderDir = orderDir;
            hHead.startRec = startRec;
            hHead.pageSize = pageSize;
            hHead.IsEnablePageing = true;
            hHead.SpId = 1;

            //  var Lst = serviceObj.SubmitPost<dynamic>("api/Td/Td_Report/GetReportData", model_MasterDynamicSp);
            var Lst = serviceObj.SubmitPost<dynamic>("api/FMS/Document/GetFilterReportData", hHead);



            int totalRecords = Lst.TotalCount;
            int recFilter = Lst.TotalCount;
            // Apply pagination.
            // Lst = Lst.Skip(startRec).Take(pageSize).ToList();
            var jsonD = JsonConvert.SerializeObject(Lst.Data);
            TempData.Keep();
            return Json(new
            {
                draw = Convert.ToInt32(draw),
                recordsTotal = totalRecords,
                recordsFiltered = recFilter,
                Rdata = jsonD
            }, JsonRequestBehavior.AllowGet);


        }
        // [HttpPost]

       

        public ActionResult FileSubListData(string Id)
        {
            var Lst = serviceObj.SubmitGet<List<TRN_Document>>("api/FMS/Document/FMSSubList?Id=" + Convert.ToInt32(Id) + "");
            return View("~/Views/Home/_FileList_InnerTable.cshtml", Lst);
        }

        public JsonResult UploadFiles(string FileLoaction, string FileName,
           string SessionId, string FileCount, string Hierarchy, string Hierarchy2, string VoucherId, string Remarks, string Year)
        {
            try
            {

                if (SessionId != null && SessionId != "")
                {
                    string FolderFullPath = string.Empty;
                    string dateString = string.Empty;
                    string FolderServerLocation = string.Empty;
                    List<TRN_Document> LstFiles = new List<TRN_Document>();


                    //New 



                    FolderServerLocation = "~/UploadedFiles/" + DefaultFolderType + "/" + SessionId;
                    FolderFullPath = Server.MapPath(FolderServerLocation);

                    if (!Directory.Exists(FolderFullPath))
                    {
                        Directory.CreateDirectory(FolderFullPath);
                    }


                    for (int i = 0; i < Request.Files.Count; i++)
                    {
                        HttpPostedFileBase file = Request.Files[i] as HttpPostedFileBase;
                        //string fileName = file.FileName;

                        if (file.ContentLength == 0)
                            continue;
                        if (file.ContentLength > 0)
                        {
                            string strPathLc = FolderFullPath + "/" + Path.GetFileName(file.FileName);
                            file.SaveAs(strPathLc);
                        }
                    }
                    // TempData.Keep();
                    DirectoryInfo di = new DirectoryInfo(FolderFullPath);
                    int fileUploadedCount = Directory.GetFiles(FolderFullPath, "*.*", SearchOption.AllDirectories).Length;
                    return Json(new { FUploaded = fileUploadedCount }, JsonRequestBehavior.AllowGet);
                }
                else
                {
                    return Json("Connection Error Please Reset and try again");
                }
            }

            catch (Exception ex)
            {

                throw ex;
            }

        }

        [HttpPost]
        public ActionResult FileUpload(string base64, string SessionId)
        {
            if (SessionId != null && SessionId != "")
            {

                string FolderFullPath = string.Empty;
                string FolderServerLocation = string.Empty;

                FolderServerLocation = "~/UploadedFiles/" + DefaultFolderType + "/" + SessionId;
                FolderFullPath = Server.MapPath(FolderServerLocation);

                if (!Directory.Exists(FolderFullPath))
                {
                    Directory.CreateDirectory(FolderFullPath);
                }
                else
                {
                    System.IO.DirectoryInfo dii = new DirectoryInfo(FolderFullPath);
                    foreach (FileInfo file in dii.GetFiles())
                    {
                        file.Delete();
                    }
                }

                string FileName = DateTime.Now.ToString("ddMMyyhhmmss") + ".jpg";
                string Tsr = base64.Replace("data:application/octet-stream;base64,", string.Empty);
                byte[] imageBytes = Convert.FromBase64String(Tsr);



                System.IO.File.WriteAllBytes(FolderFullPath + "/" + FileName, imageBytes);

                DirectoryInfo di = new DirectoryInfo(FolderFullPath);
                int fileUploadedCount = Directory.GetFiles(FolderFullPath, "*.*", SearchOption.AllDirectories).Length;
                return Json(new { FUploaded = fileUploadedCount }, JsonRequestBehavior.AllowGet);

            }
            return Json("Connection Error Please Reset and try again");
        }


        public JsonResult UploadFiles_Old(string FileLoaction, string FileName,
            string SessionId, string FileCount, string Hierarchy, string Hierarchy2, string VoucherId, string Remarks, string Year)
        {



            string FromSessionId = TempData["SessionId"] as string;
            string FolderPath = string.Empty;
            string dateString = string.Empty;
            string FolderLocation = string.Empty;


            List<TRN_Document> LstFiles = new List<TRN_Document>();

            if (FromSessionId == null || SessionId != FromSessionId)
            {
                //New 
                TempData["SessionId"] = SessionId;
                string FolderType = "FMS_Files";

                //string ServerFilePath = "../UploadedFiles/" + FolderType;
                FolderLocation = "~/UploadedFiles/" + FolderType + "/" + FileLoaction;
                FolderPath = Server.MapPath(FolderLocation);

                if (!Directory.Exists(FolderPath))
                {
                    Directory.CreateDirectory(FolderPath);
                }
            }
            else
            {
                TempData["SessionId"] = FromSessionId;
                FolderPath = TempData["folder"] as string;
                LstFiles = TempData["TRN_Document"] as List<TRN_Document>;
                FolderLocation = TempData["Location"] as string;
            }



            //GetCookies getCookies = new GetCookies();;
            //Model_SessionOtherProject Cookies_Stored = getCookies.GetStoredCookies();


            foreach (string item in Request.Files)
            {
                HttpPostedFileBase file = Request.Files[item] as HttpPostedFileBase;

                if (file.ContentLength > 0)
                {
                    string ext = Path.GetExtension(file.FileName);
                    string OldFileName = file.FileName;

                    dateString = DateTime.Now.ToString("ddMMyyHHmmss");
                    string fileName = FileName + "_" + dateString + ext.ToLower();//rename
                    string strPathLc = FolderPath + "\\" + fileName;
                    if (System.IO.File.Exists(strPathLc))
                    {
                        dateString = DateTime.Now.ToString("ddMMyyHHmmss");
                        fileName = FileName + "_" + dateString + ext.ToLower();//rename
                        strPathLc = FolderPath + "\\" + fileName;
                    }

                    file.SaveAs(strPathLc);
                    TRN_Document Trn = new TRN_Document
                    {
                        FileName = fileName,
                        FilePath = FolderLocation,
                        FullPath = strPathLc
                    };
                    LstFiles.Add(Trn);
                }
            }
            TempData["folder"] = FolderPath;
            TempData["TRN_Document"] = LstFiles;
            TempData["Location"] = FolderLocation;
            if (Convert.ToInt32(FileCount) == LstFiles.Count)
            {
                try
                {

                    GetCookies getCookies = new GetCookies();
                    Model_SessionOtherProject model_SessionOtherProject = new Model_SessionOtherProject();
                    model_SessionOtherProject = getCookies.GetStoredCookies();
                    File_Hierarchy fMS_Model_Document = new File_Hierarchy();
                    Head head = new Head()
                    {
                        Hierarchy = Hierarchy,
                        Hierarchy2 = Hierarchy2,
                        VoucherId = VoucherId,
                        Remarks = Remarks,
                        Yyear = Year,
                        Unit = model_SessionOtherProject.UnitDesnitaion
                    };
                    fMS_Model_Document.Head = head;
                    fMS_Model_Document.LstDocument = LstFiles;



                    List<TRN_Document> LstExited = new List<TRN_Document>();
                    foreach (var item in LstFiles)
                    {
                        var Check_FileExit = item.FullPath;
                        if (System.IO.File.Exists(Check_FileExit))
                        {
                            TRN_Document ExitDoc = new TRN_Document
                            {
                                FileName = item.FileName
                            };
                            LstExited.Add(ExitDoc);
                        }
                    }

                    if (LstExited.Count == LstFiles.Count)
                    {

                        var JsReturn = serviceObj.SubmitPost<ApiResponse<File_Hierarchy>>("api/FMS/Document/AddFileEntry", fMS_Model_Document);

                        return Json(new { Code = JsReturn.StatusCode, Msg = JsReturn.Message, Lst = LstFiles }, JsonRequestBehavior.AllowGet);
                    }

                    return null;


                }
                catch (Exception ex)
                {
                    return null;
                    //  return Json(new { Code = "998", Msg = ex.Message}, JsonRequestBehavior.AllowGet);

                }

            }
            return Json(new { Code = 999, LCount = LstFiles.Count, FCount = Convert.ToInt32(FileCount) }, JsonRequestBehavior.AllowGet);
        }



        // return Json(new { StatusCode = 0, Message = "FAILED TO SAVE", strData = ErrorMsg, JsonRequestBehavior.AllowGet });


    }
}