using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class CommissionDetails
    {
        public CommissionDetails()
        {
            PayDetails = new HashSet<PayDetails>();
        }

        public long CommId { get; set; }
        public DateTime? CommDate { get; set; }
        public long? MirrorId { get; set; }
        public int? AgentId { get; set; }
        public long? SaleId { get; set; }
        public int? CommPecentage { get; set; }
        public decimal? CommAmount { get; set; }
        public DateTime? Createddatetime { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<PayDetails> PayDetails { get; set; }
    }
}
