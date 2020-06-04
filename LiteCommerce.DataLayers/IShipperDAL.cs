using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LiteCommerce.DataLayers
{
    public interface IShipperDAL
    {
        List<Shipper> List(int page, int pagesize, string searchvalue);
        Shipper Get(int shipperID);
        int Add(Shipper shipper);
        int Count(string searchvalue);
        bool Update(Shipper data);
        int Delete(int[] ShipperIDs);
    }
}
