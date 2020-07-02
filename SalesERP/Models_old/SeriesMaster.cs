using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class SeriesMaster
    {
        public SeriesMaster()
        {
            MirrorDetails = new HashSet<MirrorDetails>();
        }

        public int SeriesId { get; set; }
        public string SeriesName { get; set; }
        public string SeriesCode { get; set; }
        public DateTime? Createddatetime { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<MirrorDetails> MirrorDetails { get; set; }
    }
}
