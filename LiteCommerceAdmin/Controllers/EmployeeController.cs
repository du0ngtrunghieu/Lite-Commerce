using LiteCommerce.BusinessLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using LiteCommerceAdmin.Models;
using System.IO;

namespace LiteCommerceAdmin.Controllers
{
    public class EmployeeController : Controller
    {
        // GET: Emloyee
        public ActionResult Index(int page = 1 , string searchValue = "")
        {
            int pageSize = 10;
            int rowCount = 0;
            List<Employee> listOfEmployees = CatelogBLL.ListOfEmployees(page, pageSize, searchValue, out rowCount);
            var model = new Models.EmployeePaginationResult()
            {
                Page = page,
                PageSize = pageSize,
                RowCount = rowCount,
                SearchValue = searchValue,
                Data = listOfEmployees
            };
            return View(model);
        }
        [HttpGet]
        public ActionResult Input(string id = "")
        {
            if (string.IsNullOrEmpty(id))
            {
                ViewBag.Title = "Create new Employee";
                Employee newEmployee = new Employee()
                {
                    EmployeeID = 0
                };
                return View(newEmployee);
            }
            else
            {
                ViewBag.Title = "Edit a Employee";
                Employee editEmployee = CatelogBLL.GetEmployee(Convert.ToInt32(id));
                if (editEmployee == null)
                    return RedirectToAction("Index");
                return View(editEmployee);
            }
        }
        [HttpPost]
        public ActionResult Input(Employee model, HttpPostedFileBase PhotoPath)
        {
            try
            {
                //TODO :Kiểm tra tính hợp lệ của dữ liệu nhập vào
                if (string.IsNullOrEmpty(model.FirstName))
                    ModelState.AddModelError("FirstName", "FirstName expected");
                if (string.IsNullOrEmpty(model.LastName))
                    ModelState.AddModelError("LastName", "LastName expected");
                if (string.IsNullOrEmpty(model.Title))
                    ModelState.AddModelError("Title", "Title expected");
                if (string.IsNullOrEmpty(model.Password))
                    ModelState.AddModelError("Password", "Password expected");
                if (model.BirthDate == DateTime.MinValue)
                    ModelState.AddModelError("BirthDate", "BirthDate expected");
                if (model.HireDate == DateTime.MinValue)
                    ModelState.AddModelError("HireDate", "HireDate expected");
                if (model.HireDate.CompareTo(model.BirthDate)<0)
                    ModelState.AddModelError("HideDate", "HireDate expected");
                if (string.IsNullOrEmpty(model.Email))
                    model.Email = "";
                if (string.IsNullOrEmpty(model.Address))
                    model.Address = "";
                if (string.IsNullOrEmpty(model.Country))
                    model.Country = "";
                if (string.IsNullOrEmpty(model.City))
                    model.City = "";
                if (string.IsNullOrEmpty(model.HomePhone))
                    model.HomePhone = "";
                if (string.IsNullOrEmpty(model.Notes))
                    model.Notes = "";
                if (string.IsNullOrEmpty(model.PhotoPath))
                    model.PhotoPath = "";
                //TODO :Lưu dữ liệu nhập vào
                if (model.EmployeeID == 0)
                {
                    if (PhotoPath != null && PhotoPath.ContentLength > 0)
                    {
                        var ResumeName = Path.GetFileName(PhotoPath.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/Uploads/"), ResumeName);
                        PhotoPath.SaveAs(path);
                        model.PhotoPath = "/Uploads/" + PhotoPath.FileName;
                    }
                    CatelogBLL.AddEmployee(model);
                }
                else
                {
                    if (PhotoPath != null && PhotoPath.ContentLength > 0)
                    {
                        var ResumeName = Path.GetFileName(PhotoPath.FileName);
                        string path = System.IO.Path.Combine(Server.MapPath("~/Uploads/"), ResumeName);
                        PhotoPath.SaveAs(path);
                        model.PhotoPath = "/Uploads/" + PhotoPath.FileName;
                    }
                    CatelogBLL.UpdateEmployee(model);
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
        /// Xóa danh sách employee
        /// </summary>
        /// <param name="employeeIDs"></param>
        /// <returns></returns>
        [HttpPost]
        public ActionResult Delete(int[] employeeIDs)
        {
            if (employeeIDs != null)
            {
                CatelogBLL.DeleteEmployees(employeeIDs);

            }
            return RedirectToAction("Index");

        }


        [HttpGet]
        public ActionResult ChangePassword(string Id = "")
        {
            EmployeePassword employeePassword = new EmployeePassword()
            {
                Id = Convert.ToInt32(Id)
            };
            ViewBag.Title = "ChangePassword";
            return View(employeePassword);
        }
        [HttpPost]
        public ActionResult ChangePassword(EmployeePassword model)
        {

            try
            {
                if (string.IsNullOrEmpty(model.password))
                    ModelState.AddModelError("password", "Old password expected");
                if (string.IsNullOrEmpty(model.nPassword))
                    ModelState.AddModelError("nPassword", "New password expected");
                if (string.IsNullOrEmpty(model.aPassword))
                    ModelState.AddModelError("aPassword", "Password expected");
                CatelogBLL.ChangePassword(model.Id, model.password, model.nPassword, model.aPassword);
                return RedirectToAction("Input/" + model.Id);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("", ex.Message + ":" + ex.StackTrace);
                return View(model);
            }
        }
    }

}