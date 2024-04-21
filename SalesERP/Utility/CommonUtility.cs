using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SALEERP.Utility
{
    public class CommonUtility
    {
       
    }
    public class unitmaster
    {
        public int unitid { get; set; }
        public string unitname { get; set; }


    }
   
    public enum agentStatus
    {
        dr,
        pi,
        te,
        ec,
        ex,
        gu
    }
    public enum paymethod
    {
        Cash = 1,
        Credit = 2,
        Debit = 3,
        Later = 4,
        Paytm = 5
    }
    public enum SaleType
    {

        CM = 1,
        OF = 2
    }
}
