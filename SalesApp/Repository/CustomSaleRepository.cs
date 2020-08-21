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
    public class CustomSaleRepository : ICustomSaleRepository
    {
        private ExportErpDbContext _ERPDB;
        private Sales_ERPContext _SALESDBE;

        public CustomSaleRepository(Sales_ERPContext salesdbcontext, ExportErpDbContext exporterpdbcontext)
        {

            this._ERPDB = exporterpdbcontext;
            this._SALESDBE = salesdbcontext;

        }
        public async Task<bool> AddCashSale(CustomSaleVM _sale, int userid)
        {
            bool result = false, innerresult = false;
            try
            {
                using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    await this._SALESDBE.OrderMaster.AddAsync(new OrderMaster()
                    {
                        MirrorId = 2,
                        SaleDate = DateTime.Now,
                        DelieveryType = 0,
                        SaleType = 3,
                        PortType = 0,
                        Unit = 1,
                        Description = "CustomSale",
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
                        //result = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                       // long oid = await this._SALESDBE.OrderItemDetails.MaxAsync(p => p.Id).ConfigureAwait(false);
                        //foreach (var item in _sale.specialadditions)
                        //{
                        //    if (item.Selected)
                        //    {
                        //        if (item != null)
                        //        {
                        //            await this._SALESDBE.SpecialAdditionDetails.AddAsync(new SpecialAdditionDetails() { OrderItemId = oid, SpecialAdditionDesc = item.Text, SpecialAdditionId = Convert.ToInt32(item.Value), CreatedBy = userid, CreatedDatetime = DateTime.Now, IsActive = true }).ConfigureAwait(false);
                        //        }
                        //    }
                        //}
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
                        innerresult = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                        if (innerresult)
                        {
                            await dbusertrans.CommitAsync().ConfigureAwait(false);

                        }
                        else
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

        public async Task<bool> FinishCashSale(int orderid, int userid)
        {

            bool result = false, innerresult = false;
            try
            {


                using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync().ConfigureAwait(false))
                {


                    var entity = await _SALESDBE.OrderMaster.FirstOrDefaultAsync(item => item.Id == orderid).ConfigureAwait(false);

                    if (entity != null)
                    {

                        entity.CreatedDatetime = DateTime.Now;
                        entity.salestatus = 1;
                        // entity.UpdatedBy=
                        this._SALESDBE.OrderMaster.Update(entity);
                        innerresult = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                        if (innerresult)
                        {
                            await dbusertrans.CommitAsync().ConfigureAwait(false);

                        }
                        else
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

        public async Task<CustomSaleVM> GetSales(long _orderid)
        {
            CustomSaleVM _cashsaledetails = new CustomSaleVM();
            CommonRepository _comm = new CommonRepository(_SALESDBE);
            _cashsaledetails.cashsaledetails = await(from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                                     join od in this._SALESDBE.OrderItemDetails.Where(c => c.IsActive == true)
                                                    on m.Id equals od.OrderId

                                                     where m.Id == _orderid
                                                     select new customsaledetails
                                                     {
                                                         itemorderid = od.Id,
                                                         stockid = od.StockId,
                                                         //  itemdesc = od.category.Concat(",").Concat(st.itemname).Concat(",").Concat(st.marble).Concat(",").Concat(st.size).Concat(",").Concat(st.marblestone).ToString(),
                                                         itemdesc = od.ItemDesc,
                                                         ordertype = od.OrderTypePrefix,
                                                         salevalue = od.Price,
                                                         salevalueinr = od.PriceInr

                                                     }).ToListAsync();


            //_cashsaledetails.currencydetails = await _comm.GetCurrency();
            //_cashsaledetails.specialadditions = await _comm.GetSpecialAddition();
            _cashsaledetails.grandtotal = _cashsaledetails.cashsaledetails.Sum(s => s.salevalueinr);
            return _cashsaledetails;

        }

        public async Task<CustomSaleVM> Init()
        {
            CustomSaleVM _cashsaledetails = new CustomSaleVM();
            CommonRepository _comm = new CommonRepository(_SALESDBE);
            _cashsaledetails.currencydetails = await _comm.GetCurrency();
            _cashsaledetails.shapesdetails = await _comm.GetShapes();
            _cashsaledetails.categorydetails = await _comm.GetCategory();
            _cashsaledetails.marblecolordetails = await _comm.GetMarbleColor();
            _cashsaledetails.nationalitydetails = await _comm.GetNationality();
            _cashsaledetails.countrydetails = await _comm.GetCountries();
            return _cashsaledetails;
        }

        public async Task<bool> AddCustomerinfo(CustomSaleVM _sale, int userid)
        {
            bool result = false, innerresult = false;
            long uid = 0;
            try
            {
                using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync().ConfigureAwait(false))
                {

                    var entityorder = await this._SALESDBE.OrderMaster.FirstOrDefaultAsync(i => i.Id == _sale.orderid && i.MirrorId == 2).ConfigureAwait(false);
                    if (entityorder != null && entityorder.Id > 0)
                    {
                        uid = entityorder.Id;
                    }
                    else
                    {
                        await this._SALESDBE.OrderMaster.AddAsync(new OrderMaster()
                        {
                            MirrorId = 2,
                            SaleDate = DateTime.Now,
                            DelieveryType = 0,
                            SaleType = 3,
                            PortType = 0,
                            Unit = 1,
                            Description = "CustomSale",
                            TransactionId = Common.GetUnique(),
                            CreatedDatetime = DateTime.Now,
                            IsActive = true,
                            salestatus = 1

                        }).ConfigureAwait(false);

                        result = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                        uid = await this._SALESDBE.OrderMaster.MaxAsync(p => p.Id).ConfigureAwait(false);
                    }
                    if (uid>0)
                    {
                       

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
                        //result = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                        // long oid = await this._SALESDBE.OrderItemDetails.MaxAsync(p => p.Id).ConfigureAwait(false);
                        //foreach (var item in _sale.specialadditions)
                        //{
                        //    if (item.Selected)
                        //    {
                        //        if (item != null)
                        //        {
                        //            await this._SALESDBE.SpecialAdditionDetails.AddAsync(new SpecialAdditionDetails() { OrderItemId = oid, SpecialAdditionDesc = item.Text, SpecialAdditionId = Convert.ToInt32(item.Value), CreatedBy = userid, CreatedDatetime = DateTime.Now, IsActive = true }).ConfigureAwait(false);
                        //        }
                        //    }
                        //}
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


    }
}
