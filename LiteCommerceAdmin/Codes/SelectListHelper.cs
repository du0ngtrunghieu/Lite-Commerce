using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LiteCommerceAdmin
{
    public static class SelectListHelper
    {
        public static List<SelectListItem> Countries()
        {
            List<SelectListItem> list = new List<SelectListItem>();
            list.Add(new SelectListItem() { Value = "USA", Text = "United States" });
            list.Add(new SelectListItem() { Value = "LAOS", Text = "Lào" });
            list.Add(new SelectListItem() { Value = "CAMPODIA", Text = "Campuchia" });
            list.Add(new SelectListItem() { Value = "Spain", Text = "Tây Ban Nha" });
            list.Add(new SelectListItem() { Value = "France", Text = "Pháp" });
            list.Add(new SelectListItem() { Value = "Canada", Text = "Canada" });
            list.Add(new SelectListItem() { Value = "Italy", Text = "Ý" });

            return list;
        }
    }
}