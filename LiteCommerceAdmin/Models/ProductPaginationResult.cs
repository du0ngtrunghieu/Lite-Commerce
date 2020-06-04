using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerceAdmin.Models
{
    public class ProductPaginationResult:PaginationResult
    {
        public List<Product> Data { get; set; }
    }
}