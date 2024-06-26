﻿using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class AgentContact
    {
        public int ContactId { get; set; }
        public int? AgentId { get; set; }
        public string Mobile { get; set; }
        public string Telephone { get; set; }
        public string Email { get; set; }
        public DateTime? Createddatetime { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public bool? IsActive { get; set; }

        public virtual AgentUser Agent { get; set; }
    }
}
