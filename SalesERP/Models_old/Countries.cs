using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class Countries
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public string CountryCode { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
