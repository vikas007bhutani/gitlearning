using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class AgentUser
    {
        public AgentUser()
        {
            AgentContact = new HashSet<AgentContact>();
            BankDetails = new HashSet<BankDetails>();
            PayDetails = new HashSet<PayDetails>();
            VehicleDetails = new HashSet<VehicleDetails>();
        }

        public int AgentId { get; set; }
        public int? AgentTypeId { get; set; }
        public string AgentCode { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string status { get; set; }
        public DateTime? Contractstartdate { get; set; }
        public string Panno { get; set; }
        public int? Parcheeid { get; set; }
        public string Website { get; set; }
        public string Contractformalities { get; set; }
        public DateTime? Createddatetime { get; set; }
        public int? Createdby { get; set; }
        public bool? IsActive { get; set; }
        public DateTime? Updateddatetime { get; set; }

        public virtual AgentMaster AgentType { get; set; }
        public virtual UserLogin CreatedbyNavigation { get; set; }
        public virtual ICollection<AgentContact> AgentContact { get; set; }
        public virtual ICollection<BankDetails> BankDetails { get; set; }
        public virtual ICollection<PayDetails> PayDetails { get; set; }
        public virtual ICollection<VehicleDetails> VehicleDetails { get; set; }
    }
}
