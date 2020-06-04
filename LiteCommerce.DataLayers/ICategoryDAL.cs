using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LiteCommerce.DomainModels;
namespace LiteCommerce.DataLayers
{
    public interface ICategoryDAL
    {
        List<Category> List(int page, int pagesize, string searchvalue);
        Category Get(int categoryID);
        int Add(Category category);
        int Count(string searchvalue);
        bool Update(Category data);
        int Delete(int[] CategoryIDs);
    }
}
