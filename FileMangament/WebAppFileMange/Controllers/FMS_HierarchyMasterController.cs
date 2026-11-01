using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web.Mvc;
using WEB_PRO17;
using WebAppFileMange.Models.Master;
using WebAppStartUp.Common;
using WebAppStartUp.Models;
using static WEB_PRO17.CommonServices;
using static WebAppStartUp.Models.Model_Common;
using WebAppFileMange.Models;
using Newtonsoft.Json;

namespace WebAppFileMange.Controllers
{
    public class FMS_HierarchyMasterController : BaseCommonController
    {
        GetCookies getCookies = new GetCookies();
        ServiceRepository serviceObj = new ServiceRepository();
        // GET: Default
        #region  Hierarchy Master
        public ActionResult HierarchyMaster()
        {
            string Id = "0";
            if (TempData["11"] is null)
                Id = "0";
            else
                Id = TempData["11"].ToString();
            TempData.Keep();
            var lst = serviceObj.SubmitGet<List<Mast_ParentHierarchy>>("api/FMS/Master/GetParentHierarchy?ID=" + 0);
            ViewBag.DropdownListMasterHierarchy = lst;
            FMS_Model_HierarchyMaster model_HierarchyMaster = new FMS_Model_HierarchyMaster();
            model_HierarchyMaster = get_HierarchyMaster(Convert.ToInt32(Id));

            List<ModelTextId> model = lst.Select(x => new ModelTextId
            {
                id = x.AUTOID.ToString(),
                text = x.HIERARCHY_NAME
            }).ToList();

            model_HierarchyMaster.LstModel = model;
            return View(model_HierarchyMaster);

        }

        public JsonResult GetUsersFiltredPaged(string match, int page = 1, int pageSize = 5)
        {
            List<Mast_ParentHierarchy> lst = serviceObj.SubmitGet<List<Mast_ParentHierarchy>>("api/FMS/Master/GetParentHierarchy?ID=" + 0).Take(10).ToList();
            //  var json = JsonConvert.SerializeObject(lst);

            List<ModelTextId> model = lst.Select(x => new ModelTextId
            {
                id = x.AUTOID.ToString(),
                text = x.HIERARCHY_NAME
            }).ToList();

            if (!string.IsNullOrWhiteSpace(match))
            {
                model = model.Where(m => m.text.Contains(match)).ToList();
            }

            ResultList<ModelTextId> results = new ResultList<ModelTextId>
            {
                items = model,
                total_count = 5,
            };
            return Json(results, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public JsonResult GetPartentData()
        {
            List<Mast_ParentHierarchy> lst = serviceObj.SubmitGet<List<Mast_ParentHierarchy>>("api/FMS/Master/GetParentHierarchy?ID=" + 0).Take(10).ToList();
            //  var json = JsonConvert.SerializeObject(lst);

            List<ModelTextId> LstMaster = lst.Select(x => new ModelTextId
            {
                id = x.AUTOID.ToString(),
                text = x.HIERARCHY_NAME
            }).ToList();

            return Json(new
            {
                data = LstMaster
            }, JsonRequestBehavior.AllowGet);
        }

        FMS_Model_HierarchyMaster get_HierarchyMaster(Int32 ID)
        {
            FMS_Model_HierarchyMaster model_HierarchyMaster = new FMS_Model_HierarchyMaster();

            var mAST_Hierarchy = serviceObj.SubmitGet<List<Mast_ParentHierarchy>>("api/FMS/Master/GetParentHierarchy?ID=" + ID);

            model_HierarchyMaster.HierarchyHead = mAST_Hierarchy.FirstOrDefault();
            if (ID == 0)
                ID = model_HierarchyMaster.HierarchyHead.PARENTID;

            var aST_HierarchyChild = serviceObj.SubmitGet<List<Mast_File_Hierarchy>>("api/FMS/Master/GetChildHierarchy?Id=" + ID);

            model_HierarchyMaster.HierarchyChlid = aST_HierarchyChild;

            return model_HierarchyMaster;
        }
        [HttpPost]
        public ActionResult HierarchyMaster1(string ID)
        {
            FMS_Model_HierarchyMaster model_HierarchyMaster = new FMS_Model_HierarchyMaster();
            ID = ID == "" ? "0" : ID;

            model_HierarchyMaster = get_HierarchyMaster(Convert.ToInt32(ID));
            //ViewBag.RowIndex = model_HierarchyMaster.HierarchyChlid;
            TempData["11"] = ID;
            TempData.Keep();
            return View("~/Views/FMS_HierarchyMaster/_HierarchyChild.cshtml", model_HierarchyMaster);
        }
        public ActionResult AddNewRow(int Id)
        {
            Mast_File_Hierarchy fMS_HierarchyMaster = new Mast_File_Hierarchy();
            fMS_HierarchyMaster = new Mast_File_Hierarchy
            {
                PARENTID = Id
            };
            return View("~/Views/FMS_HierarchyMaster/_HierarchyChildNew.cshtml", fMS_HierarchyMaster);
        }
        [HttpPost]
        public ActionResult GetData(DataTableAjaxPostModel requestModel)
        {
            ServiceRepository serviceObj = new ServiceRepository();

            var requestData = new SearchModel
            {
                PageNumber = requestModel.start != null && requestModel.start != "0" ? Convert.ToInt32(requestModel.start) : 0,
                PageSize = requestModel.length != null ? Convert.ToInt32(requestModel.length) : 0,
                SearchKey = Request.Form.GetValues("search[value]")[0]
            };

            if (requestData.PageNumber.HasValue)
            {
                requestData.PageNumber = (int)((requestData.PageNumber.Value / requestData.PageSize)) + 1;
            }
            var Lst = serviceObj.SubmitPost<List<Mast_File_Hierarchy>>("api/FMS/Master/GetParentHierarchy", requestData);

            return Json(new
            {
                draw = requestModel.draw,
                recordsFiltered = Lst.TotalCount,
                recordsTotal = Lst.TotalCount,
                data = Lst.Data.OrderBy(c => c.HIERARCHY_NAME)
            }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult HierarchyAddEditModel(string ParentId, string Id)
        {
            Mast_File_Hierarchy fMS_HierarchyMaster = new Mast_File_Hierarchy();
            var lst = serviceObj.SubmitGet<List<Mast_ParentHierarchy>>("api/FMS/Master/GetParentHierarchy?ID=" + ParentId);
            ViewBag.DropdownListMasterHierarchy = lst;
            if (Id.Length <= 1)
            {
                fMS_HierarchyMaster = new Mast_File_Hierarchy
                {
                    PARENTID = Convert.ToInt32(ParentId),
                    HIERARCHY_NAME = ""
                };
            }
            else
                fMS_HierarchyMaster = serviceObj.SubmitGet<Mast_File_Hierarchy>("api/FMS/Master/HierarchyEdit?Id=" + Id.ToString());


            return View("~/Views/FMS_HierarchyMaster/_HierarchyChildNew.cshtml", fMS_HierarchyMaster);
        }

        [HttpPost]
        public ActionResult Delete(string Id)
        {
            try
            {
                ServiceRepository serviceObj = new ServiceRepository();
                HttpResponseMessage response = serviceObj.DeleteResponse("api/FMS/Master/Delete?id=" + Id.ToString());
                response.EnsureSuccessStatusCode();
                return Json(new { success = true, message = "Deleted Successfully", JsonRequestBehavior.AllowGet });
            }
            catch (Exception ex)
            {

                return Json(new { success = false, message = "Failed to Delete", details = ex.Message, JsonRequestBehavior.AllowGet });
            }
        }

        [HttpPost]
        public ActionResult HierarchyStoreOrEdit(Mast_File_Hierarchy mAST_Hierarchy)
        {
            ServiceRepository serviceObj = new ServiceRepository();
            Mast_File_Hierarchy mST_Hierarchy = mAST_Hierarchy;
            var response = (dynamic)null;
            string msg = null;
            var LoginDetails = Utility.GetLoggedInUserDetails();
            Model_SessionOtherProject jsonDataR = getCookies.GetStoredCookies();
            DateTime dtime = DateTime.Now;
            mST_Hierarchy.UNIT = "";
            mST_Hierarchy.user_name = LoginDetails.UserId;
            mST_Hierarchy.LOCA = jsonDataR.Loca;
            mST_Hierarchy.user_entdt = dtime;
            mST_Hierarchy.os_user = jsonDataR.Os_user;
            mST_Hierarchy.terminal_id = jsonDataR.Terminal_id;
            if (mST_Hierarchy.AUTOID == 0)
            {
                response = serviceObj.SubmitPost<ApiResponse<Mast_File_Hierarchy>>("api/FMS/Master/AddHierarchy", mAST_Hierarchy);
                msg = "Saved Successfully";
            }
            else
            {
                response = serviceObj.SubmitPost<ApiResponse<Mast_File_Hierarchy>>("api/FMS/Master/UpdateHierarchy", mAST_Hierarchy);
                msg = "Updated Successfully";
            }
            //TempData["11"] = mAST_Hierarchy.PARENTID;
            if (response.StatusCode == 1)
                return Json(new { success = true, message = msg, JsonRequestBehavior.AllowGet });

            return Json(new { success = false, message = msg, details = response.Message, JsonRequestBehavior.AllowGet });

        }
        #endregion

    }
}