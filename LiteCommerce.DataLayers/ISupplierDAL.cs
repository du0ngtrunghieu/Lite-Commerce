using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    /// <summary>
    /// 
    /// </summary>
    public interface ISupplierDAL
    {
        /// <summary>
        /// Hiện thị danh sách supplier , phân trang và có thể tìm kiếm
        /// </summary>
        /// <param name="page"></param>
        /// <param name="pagesize"></param>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        List<Supplier> List(int page, int pagesize, string searchvalue);
        /// <summary>
        /// 
        /// </summary>
        /// <param name="searchvalue"></param>
        /// <returns></returns>
        int Count(string searchvalue);

        Supplier Get(int supplierId);
        /// <summary>
        /// Bổ sung 1 supplier hàn trả vè id của suplier được bổ sung ,
        /// Nếu lỗi , hàm trả về giá trị nhỏ hơn hoặc bằng 0
        /// </summary>
        /// <param name="supplier"></param>
        /// <returns></returns>
        int Add(Supplier supplier);

        bool Update(Supplier data);
        int Delete(int[] supplierIDs);
    }
}
