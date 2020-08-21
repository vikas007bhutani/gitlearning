using SALEERP.Models;
using SalesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.Repository.Interface
{
  public interface ICustomSaleRepository
    {
        public Task<CustomSaleVM> Init();
        public Task<bool> AddCashSale(CustomSaleVM _sale, int userid);

        public Task<bool> DeleteCashSale(int orderid, int userid);

        public Task<bool> FinishCashSale(int orderid, int userid);

        public Task<CustomSaleVM> GetSales(long mirrorid);
        public Task<bool> AddCustomerinfo(CustomSaleVM _sale, int userid);
    }
}
