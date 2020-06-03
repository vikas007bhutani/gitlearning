using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class VehicleDetails
    {
        public int Id { get; set; }
        public int? AgentId { get; set; }
        public int? VehicleId { get; set; }
        public string VehicleNo { get; set; }
        public DateTime? Createddatetime { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public int? Createdby { get; set; }
        public bool? IsActive { get; set; }

        public virtual AgentUser Agent { get; set; }
        public virtual VehicleMaster Vehicle { get; set; }
    }
}
