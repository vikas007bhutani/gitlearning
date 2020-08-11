using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEERP.Models;

namespace SalesApp.Utility
{
    public class BindingListUtillity
    {
        public static List<SelectListItem> GenerateSelectList(List<CurrencyMaster> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Type
                });
            }
            return newSelectList;
        }
        public static List<SelectListItem> GenerateSelectList(List<SpecialEdition> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name,
                     Selected=false
                });
            }
            return newSelectList;
        }
        public static List<SelectListItem> GenerateSelectList(List<LoginRoles> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                });
            }
            return newSelectList;
        }
    }
}
