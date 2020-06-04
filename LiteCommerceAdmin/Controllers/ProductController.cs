using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerceAdmin.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult Index(int page = 1 , string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            List<Product> listOfProduct = CatelogBLL.ListOfProduct(page, pageSize, searchValue, out rowCount);
            var model = new Models.ProductPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = listOfProduct
            };
            return View(model);
        }
        public ActionResult Input(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Create new Product";
            }
            else
            {
                ViewBag.Title = "Edit a Product";
            }
            return View();
        }
    }
}