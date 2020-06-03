using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class LoginRoles
    {
        public LoginRoles()
        {
            Roleclaim = new HashSet<Roleclaim>();
        }

        public int RoleId { get; set; }
        public string RoleName { get; set; }
        public string RoleDescripton { get; set; }
        public DateTime? CreatedDatetime { get; set; }
        public DateTime? UpdatedDatetime { get; set; }
        public int? UpdatedBy { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<Roleclaim> Roleclaim { get; set; }
    }
}
