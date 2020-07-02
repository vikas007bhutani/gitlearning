using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class Series
    {
        public int SeriesId { get; set; }
        public string SeriesName { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
