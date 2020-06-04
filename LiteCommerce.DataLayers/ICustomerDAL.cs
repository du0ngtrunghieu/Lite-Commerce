using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;
namespace LiteCommerce.DataLayers
{
    public interface ICustomerDAL
    {
        List<Customer> List(int page, int pagesize, string searchvalue);
        int Count(string searchvalue);

        Customer Get(string customerID);
        /// <summary>
        /// Bổ sung 1 supplier hàn trả vè id của suplier được bổ sung ,
        /// Nếu lỗi , hàm trả về giá trị nhỏ hơn hoặc bằng 0
        /// </summary>
        /// <param name="Customer"></param>
        /// <returns></returns>
        int Add(Customer customer);

        bool Update(Customer data);
        int Delete(string[] CustomerIDs);
    }
}
