using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesApp.Utility;
using SALEERP.Models;

namespace SalesApp.ViewModel
{
    public class CustomSaleVM
    {

        public long mirrorid { get; set; }
        public long orderid { get; set; }
        public DateTime mirrordate { get; set; }
        public int  specialaddition { get; set; }
        public string customspecialaddition { get; set; }
        public string stockno { get; set; }
        public string item_desc { get; set; }
        public decimal conversionrate { get; set; }
        public int currencyid { get; set; }
        public int countryid { get; set; }
        public int nationalityid { get; set; }
        public int quantity { get; set; }
        public int title_type { get; set; }
        public decimal totalvalue { get; set; }
        public decimal? grandtotal { get; set; }
        public int userid { get; set; }
        public int shapeid { get; set; }
        public int categoryid { get; set; }
        public int marblecolor { get; set; }
        public List<customsaledetails> cashsaledetails { get; set; }

        public CustomerDetails customerinfo { get; set; }
        public List<SelectListItem> nationalitydetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> currencydetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> shapesdetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> categorydetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> marblecolordetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> countrydetails { get; set; } = new List<SelectListItem>();
        //public List<SelectListItem> specialadditions { get; set; } = new List<SelectListItem>();
        public TitleType _titletype { get; set; }

        public string titletypevalue
        {
            get { return _titletype.ToString(); }
            set { _titletype = ((TitleType)Enum.Parse(typeof(TitleType), value.ToUpper().ToString())); }
        }


    }
    public class customsaledetails
    {
        public long itemorderid { get; set; }
        public string ordertype { get; set; }
        public string stockid { get; set; }
        public string itemdesc { get; set; }

        public decimal? salevalue { get; set; }

        public decimal? salevalueinr { get; set; }

    }
}
