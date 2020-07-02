using System;
using System.Collections.Generic;

namespace SALEERP.Models
{
    public partial class BankMaster
    {
        public BankMaster()
        {
            BankDetails = new HashSet<BankDetails>();
        }

        public int Bankid { get; set; }
        public string Bankname { get; set; }
        public string Bankaddress { get; set; }
        public string Bankcode { get; set; }
        public string Ifsc { get; set; }
        public DateTime? Createddatetime { get; set; }
        public DateTime? Updateddatetime { get; set; }
        public int? Createdby { get; set; }
        public bool? IsActive { get; set; }

        public virtual ICollection<BankDetails> BankDetails { get; set; }
    }
}
