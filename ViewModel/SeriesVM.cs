using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALEERP.ViewModel
{
    public class SeriesVM
    {
        public int SeriesId { get; set; }
        public string SeriesName { get; set; }
        public string SeriesCode { get; set; }
        public DateTime? Createddatetime { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public bool? IsActive { get; set; }
        public string searchstring { get; set; }
    }
}
