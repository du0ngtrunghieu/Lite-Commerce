using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerceAdmin.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult Index(int page = 1, string searchValue ="")
        {
            int pageSize = 10;
            int rowCount = 0;
            List<Customer> listOfCustomer = CatelogBLL.ListOfCustomers(page, pageSize, searchValue, out rowCount);
            var model = new Models.CustomerPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = listOfCustomer
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Input(string id)
        {
            try
            {
                if (string.IsNullOrEmpty(id))
                {
                    ViewBag.Title = "Create new Customer";
                    Customer newCustomer = new Customer();
                    
                    ViewBag.Action = "_Create";
                    return View(newCustomer);
                }
                else
                {
                    ViewBag.Title = "Edit a Customer";
                    ViewBag.Action = "_Edit";
                    Customer editCustomer = CatelogBLL.getCustomer(id);
                    if (editCustomer == null)
                        return RedirectToAction("Index");
                    return View(editCustomer);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message + ": " + ex.StackTrace);
            }
        }
        [HttpPost]
        public ActionResult Input(Customer model,String Action)
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
                if (string.IsNullOrEmpty(model.Phone))
                    model.Phone = "";
                if(string.IsNullOrEmpty(model.CustomerID))
                    ModelState.AddModelError("CustomerID", "CustomerID expected");



                //TODO: Lưu dư liệu vào DB
                if (Action.Equals("_Create"))
                {
                    ViewBag.Action = "_Create";
                    CatelogBLL.AddCusstomer(model);
                }
                else if(Action.Equals("_Edit"))
                {
                    ViewBag.Action = "_Edit";
                    CatelogBLL.UpdateCustomer(model);
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
        public ActionResult Delete(string[] customerIDs)
        {
            if (customerIDs != null)
            {
                CatelogBLL.DeleteCustomer(customerIDs);

            }
            return RedirectToAction("Index");

        }
    }

}