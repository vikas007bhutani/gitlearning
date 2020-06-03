using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class MirrorAgent
    {
        public long Id { get; set; }
        public long? Mirrorid { get; set; }
        public long? Agentid { get; set; }
        public decimal? parchiamount { get; set; }
        public bool? isparchi { get; set; }
        public DateTime? Createddatetime { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public bool? IsActive { get; set; }

        public virtual MirrorDetails Mirror { get; set; }
    }
}
