using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IEmployeeDAL
    {
        List<Employee> List(int page, int pagesize, string searchvalue);
        Employee Get(int employeeID);
        int Add(Employee employee);
        int Count(string searchvalue);
        bool Update(Employee data);
        int Delete(int[] EmployeeIDs);
        bool ChangePassword(int id, string password, string nPassword, string aPassword);
    }
}
