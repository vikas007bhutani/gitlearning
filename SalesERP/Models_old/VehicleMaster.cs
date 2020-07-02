using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class VehicleMaster
    {
        public VehicleMaster()
        {
            VehicleDetails = new HashSet<VehicleDetails>();
        }

        public int VehicleId { get; set; }
        public string VehicleType { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<VehicleDetails> VehicleDetails { get; set; }
    }
}
