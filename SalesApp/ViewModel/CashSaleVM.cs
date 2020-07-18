using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.ViewModel
{
    public class CashSaleVM
    {

        public long mirrorid { get; set; }
        public DateTime mirrordate { get; set; }
        public int specialaddition { get; set; }
        public string customspecialaddition { get; set; }
        public double conversionrate { get; set; }
        public int currencyid { get; set; }
        public double totalvalue { get; set; }
        public int userid { get; set; }
        public List<cashsaledetails> cashsaledetails { get; set; }
        public List<SelectListItem> currencydetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> specialadditions { get; set; } = new List<SelectListItem>();


    }
    public class cashsaledetails
    {

        public int ordertype { get; set; }
        public int currencytypeid { get; set; }
        public double conversionrate { get; set; }

        public double salevalue { get; set; }

        public double salevalueinr { get; set; }

    }
}
