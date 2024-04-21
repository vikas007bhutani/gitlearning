using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SALEERP.ViewModel
{
    public class SaleVM
    {
        public long MirrorId { get; set; }

        public DateTime? MirrorDate { get; set; }
        public long OrderId { get; set; }
        public int unitid { get; set; }
        [DataType(DataType.Date)]
        public DateTime fromdate { get; set; }
        [DataType(DataType.Date)]
        public DateTime Todate { get; set; }
        public List<SelectListItem> unitdetails { get; set; } = new List<SelectListItem>();
    }
}
