using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class PayDetails
    {
        public long PayId { get; set; }
        public DateTime? PayDate { get; set; }
        public int? AgentId { get; set; }
        public long? MirrorId { get; set; }
        public decimal? Amount { get; set; }
        public long? CommId { get; set; }
        public DateTime? Createddatetime { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public bool? IsActive { get; set; }

        public virtual AgentUser Agent { get; set; }
        public virtual CommissionDetails Comm { get; set; }
        public virtual MirrorDetails Mirror { get; set; }
    }
}
