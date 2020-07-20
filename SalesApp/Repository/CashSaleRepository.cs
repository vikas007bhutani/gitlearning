
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
using SalesApp.Utility;

namespace SalesApp.Repository
{
    public class CashSaleRepository : ICashSaleRepository
    {
        private ExportErpDbContext _ERPDB;
        private Sales_ERPContext _SALESDBE;

        public CashSaleRepository(Sales_ERPContext salesdbcontext, ExportErpDbContext exporterpdbcontext)
        {

            this._ERPDB = exporterpdbcontext;
            this._SALESDBE = salesdbcontext;

        }

        public async Task<bool> AddCashSale(CashSaleVM _sale, int userid)
        {
            bool result = false, innerresult = false;
            try
            {
                using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    await this._SALESDBE.OrderMaster.AddAsync(new OrderMaster()
                    {
                        MirrorId = 10009,
                        SaleDate = DateTime.Now,
                        DelieveryType = 0,
                        SaleType = 2,
                        PortType = 0,
                        Unit = 1,
                        Description = "SaleType",
                        TransactionId = Common.GetUnique(),
                        CreatedDatetime = DateTime.Now,
                        IsActive = true,
                        salestatus = 1

                    }).ConfigureAwait(false);

                    result = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                    if (result)
                    {
                        long uid = await this._SALESDBE.OrderMaster.MaxAsync(p => p.Id).ConfigureAwait(false);

                        await this._SALESDBE.OrderItemDetails.AddAsync(new OrderItemDetails()
                        {
                            OrderId = uid,
                            ItemDesc = _sale.item_desc,
                            StockId = _sale.stockno,
                            OrderType = (int?)SaleType.OrderForm,
                            OrderTypePrefix = "O/F",
                            ItemType = 1,
                            Price = _sale.totalvalue,
                            PriceInr = _sale.totalvalue,
                            ConversionRate = _sale.conversionrate,
                            Unit = 1,
                            CreatedDatetime = DateTime.Now,
                            IsActive = true,

                        }).ConfigureAwait(false);
                        result = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                        long oid = await this._SALESDBE.OrderItemDetails.MaxAsync(p => p.Id).ConfigureAwait(false);
                        foreach (var item in _sale.specialadditions)
                        {
                            if (item.Selected)
                            {
                                if (item != null)
                                {
                                    await this._SALESDBE.SpecialAdditionDetails.AddAsync(new SpecialAdditionDetails() { OrderItemId = oid, SpecialAdditionDesc = item.Text, SpecialAdditionId = Convert.ToInt32(item.Value), CreatedBy = userid, CreatedDatetime = DateTime.Now, IsActive = true }).ConfigureAwait(false);
                                }
                            }
                        }
                        innerresult = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                        if (innerresult)
                        {
                            await dbusertrans.CommitAsync().ConfigureAwait(false);

                        }
                        else { await dbusertrans.RollbackAsync().ConfigureAwait(false); }


                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;

        }

        public async Task<bool> DeleteCashSale(int orderid, int userid)
        {
            bool result = false, innerresult = false;
            try
            {

           
            using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync().ConfigureAwait(false))
            {

               
                var entity = await _SALESDBE.OrderItemDetails.FirstOrDefaultAsync(item => item.Id == orderid).ConfigureAwait(false);

                if (entity != null)
                {

                    entity.CreatedDatetime = DateTime.Now;
                    entity.IsActive = false;
                    // entity.UpdatedBy=
                     this._SALESDBE.OrderItemDetails.Update(entity);
                    innerresult = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false)>0;
                    if(innerresult)
                    {
                        await dbusertrans.CommitAsync().ConfigureAwait(false);

                    }else
                    { await dbusertrans.RollbackAsync().ConfigureAwait(false); }

                }
            }
                return innerresult;
            }
            catch (Exception)
            {

                throw;
            }
        }

        public async Task<CashSaleVM> GetSales(long _mirrorid)
        {
            CashSaleVM _cashsaledetails = new CashSaleVM();
            CommonRepository _comm = new CommonRepository(_SALESDBE);
            _cashsaledetails.cashsaledetails = await (from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                                      join od in this._SALESDBE.OrderItemDetails.Where(c => c.IsActive == true)
                                                     on m.Id equals od.OrderId

                                                      where m.MirrorId == _mirrorid
                                                      select new cashsaledetails
                                                      {
                                                          itemorderid = od.Id,
                                                          stockid = od.StockId,
                                                          //  itemdesc = od.category.Concat(",").Concat(st.itemname).Concat(",").Concat(st.marble).Concat(",").Concat(st.size).Concat(",").Concat(st.marblestone).ToString(),
                                                          itemdesc = od.ItemDesc,
                                                          ordertype = od.OrderTypePrefix,
                                                          salevalue = od.Price,
                                                          salevalueinr = od.PriceInr

                                                      }).ToListAsync();


            _cashsaledetails.currencydetails = await _comm.GetCurrency();
            _cashsaledetails.specialadditions = await _comm.GetSpecialAddition();
            return _cashsaledetails;

        }

        public async Task<List<StockDetailVM>> GetStock(string stockid)
        {
            List<StockDetailVM> _list;
            try
            {
                _list = await (from c in this._ERPDB.CarpetNumber
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
                                   marblestone = vf.MARBLETYPE


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
            _cashsaledetails.currencydetails = await _comm.GetCurrency();
            _cashsaledetails.specialadditions = await _comm.GetSpecialAddition();
            return _cashsaledetails;

        }
    }
}
