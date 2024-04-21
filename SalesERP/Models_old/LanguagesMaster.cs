using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class LanguagesMaster
    {
        public int LanguageId { get; set; }
        public string LanguageName { get; set; }
        public string LanguageCode { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }
    }
}
