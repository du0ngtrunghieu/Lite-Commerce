using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerceAdmin.Controllers
{
    public class SupplierController : Controller
    {
        // GET: Supplier
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            //int pageSize = 5;
            //int rowCount = 0;
            //List<Supplier> model = CatelogBLL.ListOfSuppliers(page, pageSize, searchValue, out rowCount);
            //ViewBag.RowCount = rowCount;
            //return View(model);
            int pageSize = 5;
            int rowCount = 0;
            List<Supplier> listOfSupplier = CatelogBLL.ListOfSuppliers(page, pageSize, searchValue, out rowCount);
            var model = new Models.SupplierPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = listOfSupplier
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.Title = "Create new Supplier";
                    Supplier newSupplier = new Supplier()
                    {
                        SupplierID = 0
                    };
                    return View(newSupplier);
                }
                else
                {
                    ViewBag.Title = "Edit a Supplier";
                    Supplier editSupplier = CatelogBLL.GetSupplier(Convert.ToInt32(id));
                    if (editSupplier == null)
                        return RedirectToAction("Index");
                    return View(editSupplier);
                }
            }
            catch(Exception ex)
            {
                return Content(ex.Message + ": " + ex.StackTrace);
            }
        }
        [HttpPost]
        public ActionResult Input(Supplier model)
        {
            try
            {
                //TODO: Kiểm tra tính hợp lệ của dữ liệu được nhập
                if (string.IsNullOrEmpty(model.CompanyName))
                    ModelState.AddModelError("CompanyName", "CompanyName expected");
                if (string.IsNullOrEmpty(model.ContactName))
                    ModelState.AddModelError("ContactName", "ContactName expected");
                if (string.IsNullOrEmpty(model.ContactTitle))
                    ModelState.AddModelError("ContactTitle", "ContactTitle expected");
                if (string.IsNullOrEmpty(model.Address))
                    model.Address = "";
                if (string.IsNullOrEmpty(model.City))
                    model.City = "";
                if (string.IsNullOrEmpty(model.Country))
                    model.Country = "";
                if (string.IsNullOrEmpty(model.Fax))
                    model.Fax = "";
                if (string.IsNullOrEmpty(model.HomePage))
                    model.HomePage = "";
                if (string.IsNullOrEmpty(model.Phone))
                    model.Phone = "";



                //TODO: Lưu dư liệu vào DB
                if (model.SupplierID == 0)
                {
                    CatelogBLL.AddSupplier(model);
                }else
                {
                    CatelogBLL.UpdateSupplier(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + ": " + ex.StackTrace);
                return View(model);
            }
        }
        [HttpPost]
        public ActionResult Delete(int[] supplierIDs = null)
        {
            if (supplierIDs != null)
                CatelogBLL.DeleteSuppliers(supplierIDs);
            return RedirectToAction("Index");
        }
    }
}