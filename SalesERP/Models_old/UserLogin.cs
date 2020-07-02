using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class UserLogin
    {
        public UserLogin()
        {
            AgentUser = new HashSet<AgentUser>();
            MirrorDetails = new HashSet<MirrorDetails>();
            Roleclaim = new HashSet<Roleclaim>();
        }

        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserPass { get; set; }
        public string UserPhone { get; set; }
        public string UserEmail { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<AgentUser> AgentUser { get; set; }
        public virtual ICollection<MirrorDetails> MirrorDetails { get; set; }
        public virtual ICollection<Roleclaim> Roleclaim { get; set; }
    }
}
