﻿using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class VehicleDetails
    {
        public int Id { get; set; }
        public int? AgentId { get; set; }
        public int? VehicleId { get; set; }
        public string Number { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? CreatedBy { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual AgentUser Agent { get; set; }
        public virtual VehicleMaster Vehicle { get; set; }
    }
}
