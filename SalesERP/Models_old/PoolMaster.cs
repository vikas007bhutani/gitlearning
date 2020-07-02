using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class PoolMaster
    {
        public PoolMaster()
        {
            MirrorDetails = new HashSet<MirrorDetails>();
        }

        public long PoolId { get; set; }
        public DateTime? PoolStartDate { get; set; }
        public DateTime? PoolEndDate { get; set; }
        public string PoolName { get; set; }
        public string PoolDesc { get; set; }
        public DateTime? Createddatetime { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<MirrorDetails> MirrorDetails { get; set; }
    }
}
