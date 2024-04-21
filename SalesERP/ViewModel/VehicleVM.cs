using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALEERP.ViewModel
{
    public class VehicleVM
    {
        public int VehicleId { get; set; }
        public string VehicleType { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
        public string searchstring { get; set; }
    }
}
