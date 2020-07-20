using SALEERP.Models;
using SalesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.Repository.Interface
{
   public interface ICashSaleRepository
    {
        public Task<List<StockDetailVM>> GetStock(string terms);
        public Task<CashSaleVM> Init();
        public Task<bool> AddCashSale(CashSaleVM _user, int userid);

        public Task<bool> DeleteCashSale(int orderid,int userid);

        public Task<CashSaleVM> GetSales(long mirrorid);
    }
}
