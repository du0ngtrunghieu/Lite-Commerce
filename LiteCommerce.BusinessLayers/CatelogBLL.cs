using LiteCommerce.DataLayers;
using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.BusinessLayers
{
    
    public static class CatelogBLL
    {
        public static void Initalize(string connectionString)
        {
            SupplierDB = new DataLayers.SQLServer.SupplierDAL(connectionString);
            CustomerDB = new DataLayers.SQLServer.CustomerDAL(connectionString);
            CategoryDB = new DataLayers.SQLServer.CategoryDAL(connectionString);
            ShipperDB = new DataLayers.SQLServer.ShipperDAL(connectionString);
            EmployeeDB = new DataLayers.SQLServer.EmloyeeDAL(connectionString);
            ProductDB = new DataLayers.SQLServer.ProductDAL(connectionString);
        }
        private static ISupplierDAL SupplierDB { get; set; }
        private static ICustomerDAL CustomerDB { get; set; }
        private static ICategoryDAL CategoryDB { get; set; }
        private static IShipperDAL ShipperDB { get; set; }
        private static IEmployeeDAL EmployeeDB{ get; set; }
        private static IProductDAL ProductDB { get; set; }
        public static List<Supplier> ListOfSuppliers(int page, int pageSize, string searchValue, out int rowCount) {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;
            
            rowCount = SupplierDB.Count(searchValue);
            return SupplierDB.List(page, pageSize, searchValue);
        }
        public static List<Customer> ListOfCustomers(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;

            rowCount = CustomerDB.Count(searchValue);
            return CustomerDB.List(page, pageSize, searchValue);

        }
        public static List<Category> ListOfCategories(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;

            rowCount = CategoryDB.Count(searchValue);
            return CategoryDB.List(page, pageSize, searchValue);
        }
        public static List<Shipper> ListOfShippers(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;

            rowCount = ShipperDB.Count(searchValue);
            return ShipperDB.List(page, pageSize, searchValue);

        }
        public static List<Employee> ListOfEmployees(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;

            rowCount = EmployeeDB.Count(searchValue);
            return EmployeeDB.List(page, pageSize, searchValue);

        }
        public static List<Product> ListOfProduct(int page, int pageSize, string searchValue, out int rowCount)
        {
            if (page < 1)
                page = 1;
            if (pageSize < 0)
                pageSize = 20;

            rowCount = ProductDB.Count(searchValue);
            return ProductDB.List(page, pageSize, searchValue);

        }
        public static Supplier GetSupplier(int supplierID)
        {
            return SupplierDB.Get(supplierID);
        }
        /// <summary>
        /// Thêm 1 supplier
        /// </summary>
        /// <param name="data">1 supplier</param>
        /// <returns></returns>
        public static int AddSupplier(Supplier data)
        {
            return SupplierDB.Add(data);
        }
        /// <summary>
        /// Chỉnh sủa 1 supplier
        /// </summary>
        /// <param name="data"> 1 supplier</param>
        /// <returns></returns>
        public static bool UpdateSupplier(Supplier data)
        {
            return SupplierDB.Update(data);
        }
        public static int DeleteSuppliers(int[] supplierIDs)
        {
            return SupplierDB.Delete(supplierIDs);
        }
        public static Category GetCategory(int categoryID)
        {
            return CategoryDB.Get(categoryID);
        }
        /// <summary>
        /// Thêm 1 supplier
        /// </summary>
        /// <param name="data">1 supplier</param>
        /// <returns></returns>
        public static int AddCategory(Category data)
        {
            return CategoryDB.Add(data);
        }
        /// <summary>
        /// Chỉnh sủa 1 supplier
        /// </summary>
        /// <param name="data"> 1 supplier</param>
        /// <returns></returns>
        public static bool UpdateCategory(Category data)
        {
            return CategoryDB.Update(data);
        }
        public static int DeleteCategories(int[] categoryIDs)
        {
            return CategoryDB.Delete(categoryIDs);
        }

        /// <summary>
        /// GET ONE CUSTOMER
        /// </summary>
        /// <param name="customerID"></param>
        /// <returns></returns>
        public static Customer getCustomer(string customerID)
        {
            return CustomerDB.Get(customerID);
        }
        /// <summary>
        /// Thêm 1 Customer
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddCusstomer(Customer data)
        {
            return CustomerDB.Add(data);
        }
        public static bool UpdateCustomer(Customer data)
        {
            return CustomerDB.Update(data);
            
        }
        public static int DeleteCustomer(string[] customerIDs)
        {
            return CustomerDB.Delete(customerIDs);
        }




        /* SHIPPER */
        /// <summary>
        /// Lấy 1 Shipper 
        /// </summary>
        /// <param name="shipperID"></param>
        /// <returns></returns>
        public static Shipper GetShipper(int shipperID)
        {
            return ShipperDB.Get(shipperID);
        }
        /// <summary>
        /// thêm 1 Shipper
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddShipper(Shipper data)
        {
            return ShipperDB.Add(data);
        }
        /// <summary>
        /// xóa 1 danh sách Shipper
        /// </summary>
        /// <param name="ShipperIDs"></param>
        /// <returns></returns>
        public static int DeleteShippers(int[] ShipperIDs)
        {
            return ShipperDB.Delete(ShipperIDs);
        }
        /// <summary>
        /// update 1 Shipper
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateShipper(Shipper data)
        {
            return ShipperDB.Update(data);
        }



        /* EMPLOYEE*/
        /// <summary>
        /// Lấy 1 Employee 
        /// </summary>
        /// <param name="EmployeeID"></param>
        /// <returns></returns>
        public static Employee GetEmployee(int EmployeeID)
        {
            return EmployeeDB.Get(EmployeeID);
        }
        /// <summary>
        /// thêm 1 Employee
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static int AddEmployee(Employee data)
        {
            return EmployeeDB.Add(data);
        }
        /// <summary>
        /// xóa 1 danh sách Employee
        /// </summary>
        /// <param name="EmployeeIDs"></param>
        /// <returns></returns>
        public static int DeleteEmployees(int[] EmployeeIDs)
        {
            return EmployeeDB.Delete(EmployeeIDs);
        }
        /// <summary>
        /// update 1 Employee
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        public static bool UpdateEmployee(Employee data)
        {
            return EmployeeDB.Update(data);
        }

        public static bool ChangePassword(int id, string password, string nPassword, string aPassword)
        {
            return EmployeeDB.ChangePassword(id, password, nPassword, aPassword);
        }
    }
}
