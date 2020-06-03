using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class BankDetails
    {
        public int Id { get; set; }
        public int? AgentId { get; set; }
        public int? BankId { get; set; }
        public string AccountNo { get; set; }
        public DateTime? Createddatetime { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public bool? IsActive { get; set; }

        public virtual AgentUser Agent { get; set; }
        public virtual BankMaster Bank { get; set; }
    }
}
