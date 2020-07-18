using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging.Abstractions;
using SALEERP.Data;
using SALEERP.Models;
using SalesApp.Repository.Interface;
using SalesApp.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SalesApp.Repository
{
    public class CashSaleRepository : ICashSaleRepository
    {
        private ExportErpDbContext _ERPDB;
        private Sales_ERPContext _SALESDBE;

        public CashSaleRepository(Sales_ERPContext salesdbcontext,ExportErpDbContext exporterpdbcontext)
        {

            this._ERPDB = exporterpdbcontext;
            this._SALESDBE = salesdbcontext;

        }
        public async Task<List<StockDetailVM>> GetStock(string stockid)
        {
            List<StockDetailVM> _list ;
            try
            {
                _list = await  (from c in this._ERPDB.CarpetNumber
                                 join vf in this._ERPDB.V_FinishedItemDetail
                                 on c.item_finished_id equals vf.ITEM_FINISHED_ID
                                 where c.TStockNo == stockid
                                 select new StockDetailVM
                                 {
                                     stockid = c.TStockNo,
                                     category = vf.CATEGORY_NAME,
                                     itemname = vf.ITEM_NAME,
                                     marblecolor = vf.MARBLECOLOR,
                                     price = c.Price,
                                     size = vf.SizeInch,
                                     marblestone=vf.MARBLETYPE


                                 }).ToListAsync();
            }
            catch (Exception ex)
            {

                throw ex;
            }
            //StockDetailVM _sdetails = new StockDetailVM();
          
            return _list;


        }

        public async Task<CashSaleVM> Init()
        {
            CashSaleVM _cashsaledetails = new CashSaleVM();
            CommonRepository _comm = new CommonRepository(_SALESDBE);
            _cashsaledetails.currencydetails = await _comm.GetCurrency().ConfigureAwait(false);
            _cashsaledetails.specialadditions = await _comm.GetSpecialAddition().ConfigureAwait(false);
            return _cashsaledetails;
           
        }
    }
}
