using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.Repository.Interface
{
    public interface ICommonRepository
    {
        Task<List<SelectListItem>> GetCurrency();
        Task<List<SelectListItem>> GetSpecialAddition();
    }
}
