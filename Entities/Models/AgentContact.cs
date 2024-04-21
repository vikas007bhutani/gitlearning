using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class AgentContact
    {
        public int Id { get; set; }
        public int? AgentId { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public bool? IsActive { get; set; }

        public virtual AgentUser Agent { get; set; }
    }
}
