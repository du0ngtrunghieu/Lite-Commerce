using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IProductDAL
    {
        List<Product> List(int page, int pagesize, string searchvalue);
        Product Get(int productID);
        int Add(Product product);
        int Count(string searchvalue);
        bool Update(Product data);
        int Delete(int[] ProductIDs);
    }
}
