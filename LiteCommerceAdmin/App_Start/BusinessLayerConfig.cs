using LiteCommerce.BusinessLayers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LiteCommerceAdmin
{
    public class BusinessLayerConfig
    {
        public static void init()
        {
            string connectionsString = ConfigurationManager.ConnectionStrings["LiteCommerce"].ConnectionString;

            //TODO : Khởi tạo các dll khi cần sử dụng 
            CatelogBLL.Initalize(connectionsString);
        }
    }
}