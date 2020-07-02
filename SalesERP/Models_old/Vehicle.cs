using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class Vehicle
    {
        public int VehicleId { get; set; }
        public string VehicleType { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
