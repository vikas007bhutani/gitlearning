using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SALEERP.Models;
using SALEERP.ViewModel;

namespace SALEERP.Utility
{
    public static class BindingListUtillity
    {
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
        public static List<SelectListItem> GenerateSelectList(List<AgentMaster> collection)
        {
            List<SelectListItem> newSelectList = new List<SelectListItem>();
            foreach (var item in collection)
            {
                newSelectList.Add(new SelectListItem
                {
                    Value = item.Code.ToString(),
                    Text = item.Type
                });
            }
            return newSelectList;
        }
        public static List<SelectListItem> GenerateSelectList(List<VehicleMaster> collection)
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
        public static List<SelectListItem> GenerateSelectList(List<BankMaster> collection)
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
        public static List<SelectListItem> GenerateSelectList(List<LanguagesMaster> collection)
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
        public static List<SelectListItem> GenerateSelectList(List<CountriesMaster> collection)
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
        public static List<SelectListItem> GenerateSelectList(List<SeriesMaster> collection)
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
