using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using SALEERP.ViewModel;

namespace SALEERP.Repository.Interface
{
    public interface ICommonRepository
    {
        UserLoginVM getroles();
        List<SelectListItem> getvehicles();
        List<SelectListItem> getBanks();
        List<SelectListItem> getunits();
        List<SelectListItem> getcountry();
    }
}
