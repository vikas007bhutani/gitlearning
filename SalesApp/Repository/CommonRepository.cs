using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SALEERP.Data;
using SALEERP.Models;
using SalesApp.Repository.Interface;
using SalesApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private Sales_ERPContext _DBERP;
        public CommonRepository(Sales_ERPContext dbcontext)
        {

            this._DBERP = dbcontext;

        }
        public CommonRepository()
        { }
        public async Task<List<SelectListItem>> GetCurrency()
        {
            List<SelectListItem> _getcurrency = new List<SelectListItem>();
            List<CurrencyMaster> _currencymaster = await this._DBERP.CurrencyMaster.Where(i => i.IsActive == true).ToListAsync();
            _getcurrency = BindingListUtillity.GenerateSelectList(_currencymaster);
            return _getcurrency;
        }

        public async Task<List<SelectListItem>> GetSpecialAddition()
        {
            List<SelectListItem> _getspecialaddition = new List<SelectListItem>();
            List<SpecialEdition> _samaster = await this._DBERP.SpecialEdition.Where(i => i.IsActive == true).ToListAsync();
            _getspecialaddition = BindingListUtillity.GenerateSelectList(_samaster);
            return _getspecialaddition;
        }
    }
}
