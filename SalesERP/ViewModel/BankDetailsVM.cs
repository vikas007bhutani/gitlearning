using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALEERP.ViewModel
{
    public class BankDetailsVM
    {
        public int Id { get; set; }
        public int? BankId { get; set; }
        public int? AgentId { get; set; }
        public string AccountNo { get; set; }
        public DateTime? Createddatetime { get; set; }
        public int? Createdby { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public bool? IsActive { get; set; }
    }
}
