using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerceAdmin.Models
{
    public class CategoryPaginationResult:PaginationResult
    {
        public List<Category> Data { get; set; }
    }
}