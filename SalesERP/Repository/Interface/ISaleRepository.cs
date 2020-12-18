using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEERP.Models;
using SALEERP.ViewModel;

namespace SALEERP.Repository.Interface
{
   public interface ISaleRepository
    {
        SaleVM Init_commission();
    }
}
