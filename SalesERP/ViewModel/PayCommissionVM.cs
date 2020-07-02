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
    public class PayCommissionVM
    {
        public long MirrorId { get; set; }
        public string Name { get; set; } = string.Empty;
        public int AgentId { get; set; } = 0;
        public int modeId { get; set; } = 0;
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
        public List<SelectListItem> _agentuser { get; set; }
        public string AgentCode { get; set; } = string.Empty;
        public List<MirrorDetailVM> Mirrors { get; set; } = new List<MirrorDetailVM>();

        [DataType(DataType.Date)]
        public DateTime fromdate { get; set; }
        [DataType(DataType.Date)]
        public DateTime Todate { get; set; }
      
        public List<SelectListItem> paymodes { get; set; }

        public List<PayCommissionDetails> pay_list { get; set; } = new List<PayCommissionDetails>();

    }
    public class PayCommissionDetails
    {
        public long? MirrorId { get; set; }

        public DateTime? MirrorDate { get; set; }
        public int? AgentId { get; set; }
        public long? OrderId { get; set; }
        public string? AgentType { get; set; }
        public string Name { get; set; } = string.Empty;
        public string agentcode { get; set; } = string.Empty;
        public decimal? tds { get; set; }
        public decimal? commission_amount { get; set; }
        public decimal? balance { get; set; }

        public decimal? paid { get; set; }
        public decimal? amounttopay { get; set; }

        public string payby { get; set; } = string.Empty;


    }

}
