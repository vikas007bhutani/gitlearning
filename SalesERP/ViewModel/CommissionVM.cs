using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SALEERP.Models;

namespace SALEERP.ViewModel
{
    public class CommissionVM
    {
        public long MirrorId { get; set; }

        public DateTime? MirrorDate { get; set; }
        public long OrderId { get; set; }

        public decimal? cashamount { get; set; }

        public decimal? cardamount { get; set; }

        public decimal? Instoreamount { get; set; }

        public decimal? paylateramount { get; set; }
        public decimal? HDamount { get; set; }
        public decimal? CardCharges { get; set; }
        public decimal? GST   { get; set; }
        public decimal? NetSaleAmount { get; set; }
        public  int unitid { get; set; }
        public string ipadress { get; set; }
        public List<SelectListItem> unitdetails { get; set; } = new List<SelectListItem>();
        public List<MirrorDetailVM> Mirrors { get; set; } = new List<MirrorDetailVM>();

        [DataType(DataType.Date)]
        public DateTime fromdate { get; set; }
        [DataType(DataType.Date)]
        public DateTime Todate { get; set; }

        public List<AddCommissionDetails> agent_List { get; set; }

    }
    public class AddCommissionDetails
    {
        public long? MirrorId { get; set; }
        public int? AgentId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string agentcode { get; set; } = string.Empty;
        public decimal? commission_percentage { get; set; }
        public decimal? commission_amount { get; set; }
        public List<AddCommissionDetails> driver { get; set; }
        public List<AddCommissionDetails> excursion { get; set; }
        public List<AddCommissionDetails> principle { get; set; }
        public List<AddCommissionDetails> guide { get; set; }
        public List<AddCommissionDetails> teamlead { get; set; }
        public List<AddCommissionDetails> teamescort { get; set; }
        public List<AddCommissionDetails> demo { get; set; }


    }

}
