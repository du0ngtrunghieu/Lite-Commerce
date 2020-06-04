using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerceAdmin.Controllers
{
    public class ShipperController : Controller
    {
        // GET: Shipper
        public ActionResult Index(int page = 1 , string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            List<Shipper> listOfShippers = CatelogBLL.ListOfShippers(page, pageSize, searchValue, out rowCount);
            var model = new Models.ShipperPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = listOfShippers
            };
            return View(model);
        }
        public ActionResult Input(string id)
        {

            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Create new Shipper";
                Shipper newShipper = new Shipper()
                {
                    ShipperID = 0
                };
                return View(newShipper);
            }
            else
            {
                ViewBag.Title = "Edit a Shipper";
                Shipper editShipper = CatelogBLL.GetShipper(Convert.ToInt32(id));
                if (editShipper == null)
                    return RedirectToAction("Index");
                return View(editShipper);
            }
        }
        [HttpPost]
        public ActionResult Input(Shipper model)
        {
            try
            {
                //TODO :Kiểm tra tính hợp lệ của dữ liệu nhập vào
                if (string.IsNullOrEmpty(model.CompanyName))
                    ModelState.AddModelError("CompanyName", "CompanyName expected");
                if (string.IsNullOrEmpty(model.Phone))
                    model.Phone = "";
                //TODO :Lưu dữ liệu nhập vào
                if (model.ShipperID == 0)
                {

                    CatelogBLL.AddShipper(model);
                }
                else
                {
                    CatelogBLL.UpdateShipper(model);
                }
                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + ":" + ex.StackTrace);
                return View(model);
            }
        }
        /// <summary>
        /// Xóa danh sách shipper
        /// </summary>
        /// <param name="ShipperIDs"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int[] ShipperIDs)
        {
            if (ShipperIDs != null)
            {
                CatelogBLL.DeleteShippers(ShipperIDs);

            }
            return RedirectToAction("Index");

        }
    }
}