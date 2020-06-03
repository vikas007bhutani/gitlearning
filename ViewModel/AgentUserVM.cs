using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SALEERP.Models;

namespace SALEERP.ViewModel
{
    public class AgentUserVM
    {
        public int AgentId { get; set; } = 0;
        public int? AgentTypeId { get; set; } = 0;
        public string AgentCode { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Address { get; set; } = string.Empty;
        public string City { get; set; } = string.Empty;
        public string Country { get; set; } = string.Empty;
        public DateTime? Contractstartdate { get; set; } = DateTime.Now;
        [DisplayName("PAN")]
        public string Panno { get; set; } = string.Empty;
        public int? Parcheeid { get; set; } = 0;
        public string Website { get; set; }= string.Empty;
        public string Contractformalities { get; set; } = string.Empty;
        public DateTime? Createddatetime { get; set; } = DateTime.Now;
        public int? Createdby { get; set; } = 0;
        public bool? IsActive { get; set; } = false;    
        public List<AgentContact> AgentContact { get; set; }
        public List<SelectListItem> vdetails { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> bdetails { get; set; } = new List<SelectListItem>();
        public List<BankDetails> BankDetails { get; set; } 
        public List<VehicleDetails> VehicleDetails { get; set; }
        public int? VehicleId { get; set; }
        public int? BankId { get; set; }

        public string searchstring { get; set; } = string.Empty;

        public static implicit operator AgentUserVM(List<string> v)
        {
            throw new NotImplementedException();
        }
    }
}
