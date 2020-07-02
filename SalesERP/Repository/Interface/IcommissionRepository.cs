using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEERP.Models;
using SALEERP.ViewModel;

namespace SALEERP.Repository.Interface
{
   public interface IcommissionRepository
    {
        CommissionVM getAllMirorforCommission(CommissionVM cm);
        CommissionVM Init_commission();
        PayCommissionVM Init_paycommission();
        CommissionVM EditCommission(Int64 uid);
        bool AddCommission(CommissionVM _comm, int userid);
        PayCommissionVM getAllAgentCommission(PayCommissionVM pcm);
        PayCommissionVM PayCommission(Int64 uid);
        bool UpdateCommission(PayCommissionVM _comm, int userid);
    }
}
