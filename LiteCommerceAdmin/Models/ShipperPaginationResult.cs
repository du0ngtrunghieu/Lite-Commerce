﻿using LiteCommerce.DomainModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LiteCommerceAdmin.Models
{
    public class ShipperPaginationResult:PaginationResult
    {
        public List<Shipper> Data { get; set; }
    }
}