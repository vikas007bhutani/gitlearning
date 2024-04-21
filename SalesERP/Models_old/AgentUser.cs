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
            MirrorDetailsDemoPerson = new HashSet<MirrorDetails>();
            MirrorDetailsDriver = new HashSet<MirrorDetails>();
            MirrorDetailsExcursionAgent = new HashSet<MirrorDetails>();
            MirrorDetailsPrincipleAgent = new HashSet<MirrorDetails>();
            MirrorDetailsTourEscort = new HashSet<MirrorDetails>();
            MirrorDetailsTourGuide = new HashSet<MirrorDetails>();
            MirrorDetailsTourLeader = new HashSet<MirrorDetails>();
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
        public DateTime? Contractstartdate { get; set; }
        public string Panno { get; set; }
        public int? Parcheeid { get; set; }
        public string Website { get; set; }
        public string Contractformalities { get; set; }
        public DateTime? Createddatetime { get; set; }
        public int? Createdby { get; set; }
        public bool? IsActive { get; set; }

        public virtual AgentMaster AgentType { get; set; }
        public virtual UserLogin CreatedbyNavigation { get; set; }
        public virtual ICollection<AgentContact> AgentContact { get; set; }
        public virtual ICollection<BankDetails> BankDetails { get; set; }
        public virtual ICollection<MirrorDetails> MirrorDetailsDemoPerson { get; set; }
        public virtual ICollection<MirrorDetails> MirrorDetailsDriver { get; set; }
        public virtual ICollection<MirrorDetails> MirrorDetailsExcursionAgent { get; set; }
        public virtual ICollection<MirrorDetails> MirrorDetailsPrincipleAgent { get; set; }
        public virtual ICollection<MirrorDetails> MirrorDetailsTourEscort { get; set; }
        public virtual ICollection<MirrorDetails> MirrorDetailsTourGuide { get; set; }
        public virtual ICollection<MirrorDetails> MirrorDetailsTourLeader { get; set; }
        public virtual ICollection<PayDetails> PayDetails { get; set; }
        public virtual ICollection<VehicleDetails> VehicleDetails { get; set; }
    }
}
