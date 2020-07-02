using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class MirrorDetails
    {
        public MirrorDetails()
        {
            MirrorAgent = new HashSet<MirrorAgent>();
        }

        public long MirrorId { get; set; }
        public DateTime? MirrorDate { get; set; }
        public int? PrincipleAgentId { get; set; }
        public int? ExcursionAgentId { get; set; }
        public int? DemoPersonId { get; set; }
        public int? TourLeaderId { get; set; }
        public int? TourEscortId { get; set; }
        public int? TourGuideId { get; set; }
        public int? DriverId { get; set; }
        public int? Pax { get; set; }
        public int? Countryid { get; set; }
        public int? Languageid { get; set; }
        public long? PoolId { get; set; }
        public int? SeriesId { get; set; }
        public int? StatusId { get; set; }
        public string Remarks { get; set; }
        public DateTime? Createddatetime { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public int? Createdby { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<MirrorAgent> MirrorAgent { get; set; }
    }
}
