using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class PayDetails
    {
        public long Id { get; set; }
        public DateTime? Date { get; set; }
        public int? AgentId { get; set; }
        public long? MirrorId { get; set; }
        public decimal? Amount { get; set; }
        public long? CommId { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public bool? IsActive { get; set; }

        public virtual AgentUser Agent { get; set; }
        public virtual CommissionDetails Comm { get; set; }
        public virtual MirrorDetails Mirror { get; set; }
    }
}
