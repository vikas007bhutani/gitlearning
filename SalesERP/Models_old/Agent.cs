﻿using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class Agent
    {
        public int AgentId { get; set; }
        public string AgentType { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
