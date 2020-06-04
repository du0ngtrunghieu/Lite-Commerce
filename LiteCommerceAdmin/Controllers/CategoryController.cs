using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerceAdmin.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {
        // GET: Category
        public ActionResult Index(int page = 1, string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            List<Category> listOfCategory = CatelogBLL.ListOfCategories(page, pageSize, searchValue, out rowCount);
            var model = new Models.CategoryPaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = listOfCategory
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
                    ViewBag.Title = "Create new Category";
                    Category newCategory = new Category()
                    {
                        CategoryID = 0
                    };
                    return View(newCategory);
                }
                else
                {
                    ViewBag.Title = "Edit a Category";
                    Category editCategory = CatelogBLL.GetCategory(Convert.ToInt32(id));
                    if (editCategory == null)
                        return RedirectToAction("Index");
                    return View(editCategory);
                }
            }
            catch (Exception ex)
            {
                return Content(ex.Message + ": " + ex.StackTrace);
            }
        }
        [HttpPost]
        public ActionResult Input(Category model)
        {
            try
            {
                //TODO: Kiểm tra tính hợp lệ của dữ liệu được nhập
                if (string.IsNullOrEmpty(model.CategoryName))
                    ModelState.AddModelError("CategoryName", "CategoryName expected");
                if (string.IsNullOrEmpty(model.Description))
                    ModelState.AddModelError("Description", "Description expected");
                



                //TODO: Lưu dư liệu vào DB
                if (model.CategoryID == 0)
                {
                    CatelogBLL.AddCategory(model);
                }
                else
                {
                    CatelogBLL.UpdateCategory(model);
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
        public ActionResult Delete(int[] categoryIDs = null)
        {
            if (categoryIDs != null)
                CatelogBLL.DeleteCategories(categoryIDs);
            return RedirectToAction("Index");
        }
    }
}