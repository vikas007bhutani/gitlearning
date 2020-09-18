
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
using Microsoft.AspNetCore.Mvc;

namespace SalesApp.Repository
{
    public class CashSaleRepository : ICashSaleRepository
    {
        #region Properties and Variables
        private ExportErpDbContext _ERPDB;
        private Sales_ERPContext _SALESDBE;

        #endregion

        #region Constructor and InIt Region
        public CashSaleRepository(Sales_ERPContext salesdbcontext, ExportErpDbContext exporterpdbcontext)
        {

            this._ERPDB = exporterpdbcontext;
            this._SALESDBE = salesdbcontext;

        }
        public async Task<CashSaleVM> Init(long mirrorId)
        {
            CashSaleVM _cashsaledetails = new CashSaleVM();
            CommonRepository _comm = new CommonRepository(_SALESDBE);
            var entityorder = await this._SALESDBE.MirrorDetails.FirstOrDefaultAsync(i => i.Id == mirrorId).ConfigureAwait(false);
            if (entityorder != null && entityorder.Id > 0)
            {
                _cashsaledetails.mirrordate = entityorder.Date;
            }
                _cashsaledetails.mirrorid = mirrorId;
            _cashsaledetails.currencydetails = await _comm.GetCurrency();
            _cashsaledetails.specialadditions = await _comm.GetSpecialAddition();
            return _cashsaledetails;

        }
        #endregion

        #region Add Sale, Delete Sale, Finish Sale Region
        //#To DO:Remove Hard coded values and fetch from DB/Enums
        public async Task<Int64> AddCashSale([Bind("orderid")] CashSaleVM _sale, int userid)
        {
            bool result = false, innerresult = false;
            long uid = 0;
            decimal finalINR=0, INRvalue = 0;
           
            string digitvalue = string.Empty, lastdigit=string.Empty;  
            try
            {
                using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    //if (_sale.orderid > 0)
                    //{
                    var entityorder = await this._SALESDBE.OrderMaster.FirstOrDefaultAsync(i => i.Id == _sale.orderid).ConfigureAwait(false);
                    INRvalue = Math.Round((decimal)(_sale.totalvalue * _sale.conversionrate), MidpointRounding.AwayFromZero);
                    decimal resultd =  Math.Abs(INRvalue % 10);
                    if(resultd>0)
                    {
                        finalINR =INRvalue +(10 - resultd);
                    }
                    else
                    {
                        finalINR = INRvalue;
                    }
                    //if (INRvalue > 0)
                    //{
                    //    digitvalue = Convert.ToString(INRvalue);
                    //     lastdigit = digitvalue.Substring(digitvalue.Length - 1);
                    //}
                 
                  //  if(INRvalue>0)
                    if (entityorder != null && entityorder.Id > 0)
                    {
                        uid = entityorder.Id;
                    }
                    else
                    {
                        await this._SALESDBE.OrderMaster.AddAsync(new OrderMaster()
                        {
                            MirrorId = _sale.mirrorid,
                            SaleDate = DateTime.Now,
                            DelieveryType = 0,
                            PortType = 0,
                            Unit = 1,
                            Description = "CashSale",
                            TransactionId = new Guid().ToString(),//Common.GetUnique(),
                            CreatedDatetime = DateTime.Now,
                            IsActive = true,
                          //  salestatus = 1

                        }).ConfigureAwait(false);

                        result = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                        uid = await this._SALESDBE.OrderMaster.MaxAsync(p => p.Id).ConfigureAwait(false);
                    }
                    //  }
                    if (uid > 0)
                    {


                        await this._SALESDBE.OrderItemDetails.AddAsync(new OrderItemDetails()
                        {
                            OrderId = uid,
                            ItemDesc = _sale.item_desc,
                            StockId = _sale.stockno,
                            OrderType = (int?)SaleType.CM,
                            OrderTypePrefix = "CM",
                            ItemType = 1,
                            Price = _sale.totalvalue,
                            PriceInr = finalINR,
                            ConversionRate = _sale.conversionrate,
                            Unit = 1,
                            CreatedDatetime = DateTime.Now,
                            IsActive = true,
                            CurrencyType=_sale.currencyid,
                            SaleType=2




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
                                    innerresult = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                                }
                            }
                        }

                        if (oid > 0)
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
            return uid;

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

                            var entityorderitem = await _SALESDBE.OrderItemDetails.Where(item => item.OrderId == orderid).ToListAsync().ConfigureAwait(false);

                            foreach (var item in entityorderitem)
                            {
                                var entitycarpet = await _SALESDBE.CarpetNumber.FirstOrDefaultAsync(c => c.TStockNo == item.StockId).ConfigureAwait(false);
                                if (entitycarpet != null && entitycarpet.StockNo > 0)
                                {
                                    entitycarpet.PackDate = DateTime.Now;
                                    entitycarpet.Pack = 1;
                                    entitycarpet.PackSource = "SALE";
                                    entitycarpet.PackingDetailId =(Int32)item.Id;
                                    entitycarpet.PackingId = (Int32)item.OrderId;
                                    this._SALESDBE.OrderMaster.Update(entity);
                                }
                                await this._SALESDBE.Directstockpack.AddAsync(new Directstockpack()
                                { Stockno= entitycarpet.StockNo,
                                  Remark="SALES",
                                  Dateadded=DateTime.Now,
                                }).ConfigureAwait(false);
                                result=await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                                if(result)
                                {
                                    await dbusertrans.CommitAsync().ConfigureAwait(false);

                                }
                                else
                                { await dbusertrans.RollbackAsync().ConfigureAwait(false); }

                            }

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

        #endregion

        #region Common Methods
        public async Task<CashSaleVM> GetSales(long _orderid,CashSaleVM _saledetails)
        {
            CashSaleVM _cashsaledetails = new CashSaleVM();
            CommonRepository _comm = new CommonRepository(_SALESDBE);
            _cashsaledetails.cashsaledetails = await (from mr in this._SALESDBE.MirrorDetails.Where(c => c.IsActive == true)
                                                      join m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                                       on mr.Id equals m.MirrorId
                                                      join od in this._SALESDBE.OrderItemDetails.Where(c => c.IsActive == true)
                                                      on m.Id equals od.OrderId
                                                      join c in this._SALESDBE.CurrencyMaster.Where(c => c.IsActive == true)
                                                      on od.CurrencyType equals c.Id into currdetails
                                                      from curr in currdetails.Where(c => c.IsActive == true).DefaultIfEmpty()

                                                      where m.Id == _orderid
                                                      select new cashsaledetails
                                                      {
                                                          itemorderid = od.Id,
                                                          stockid = od.StockId,
                                                          //  itemdesc = od.category.Concat(",").Concat(st.itemname).Concat(",").Concat(st.marble).Concat(",").Concat(st.size).Concat(",").Concat(st.marblestone).ToString(),
                                                          itemdesc = od.ItemDesc,
                                                          ordertype = od.OrderTypePrefix,
                                                          salevalue = od.Price,
                                                          salevalueinr = od.PriceInr ,
                                                          mirrorid=mr.Id,
                                                          mirrordate=mr.Date,
                                                          symbol=curr.Symbol


                                                      }).ToListAsync();


            _cashsaledetails.currencydetails = await _comm.GetCurrency();
            _cashsaledetails.mirrorid = _cashsaledetails.cashsaledetails[0].mirrorid;
            _cashsaledetails.mirrordate = _cashsaledetails.cashsaledetails[0].mirrordate;
            _cashsaledetails.specialadditions = await _comm.GetSpecialAddition();
            _cashsaledetails.grandtotal = _cashsaledetails.cashsaledetails.Sum(s => s.salevalueinr);
            _cashsaledetails.itemcount = _cashsaledetails.cashsaledetails.Count();
            _cashsaledetails.stockno = _cashsaledetails.cashsaledetails.OrderByDescending(a=>a.itemorderid).FirstOrDefault().stockid;
            _cashsaledetails.item_desc = _cashsaledetails.cashsaledetails.OrderByDescending(a => a.itemorderid).FirstOrDefault().itemdesc;
            _cashsaledetails.orderid = _orderid;
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
                               where c.TStockNo == stockid && c.Pack==0
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

        public async Task<long> GetOderIdByOrderItemId(int orderItemId)
        {
            long orderId;

            try
            {
                var entity = await _SALESDBE.OrderItemDetails.FirstOrDefaultAsync(item => item.Id == orderItemId).ConfigureAwait(false);
                orderId = (long)entity.OrderId;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            return orderId;
        }

        //public Task<long> AddOrderPayment(OrderPaymentVM _payment, int userid)
        //{
        //    throw new NotImplementedException();
        //}



        #endregion

        #region Order Payment region
        //#To DO:Remove Hard coded values and fetch from DB/Enums
        public async Task<bool> AddOrderPayment(int orderItemId, int userid)
        {
            bool result = false, innerresult = false;
            long uid = 0;
            CashSaleVM _cashsaledetails = new CashSaleVM();
            try
            {
                using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    //if (_sale.orderid > 0)
                    //{
                    var entityorder = await this._SALESDBE.OrderPayment.FirstOrDefaultAsync(i => i.Id == orderItemId).ConfigureAwait(false);
                    _cashsaledetails.cashsaledetails = await (from  od in this._SALESDBE.OrderItemDetails.Where(c => c.IsActive == true)
                                                           

                                                              where od.OrderId == orderItemId
                                                              select new cashsaledetails
                                                              {
                                                                  itemorderid = od.Id,
                                                                  stockid = od.StockId,
                                                                  itemdesc = od.ItemDesc,
                                                                  ordertype = od.OrderTypePrefix,
                                                                  salevalue = od.Price,
                                                                  salevalueinr = od.PriceInr

                                                              }).ToListAsync();

                    decimal? totalprice= _cashsaledetails.cashsaledetails.Sum(s => s.salevalueinr);
                    if (entityorder != null && entityorder.Id > 0)
                    {
                        entityorder.OrderId = orderItemId;
                        entityorder.PayDate = DateTime.Now;
                        entityorder.PayMode = 1; //Cash
                        entityorder.Amount = totalprice;
                        entityorder.CardType = 0;
                        entityorder.AmoutHd = 0;
                        entityorder.CreatedBy = userid;
                        entityorder.CreatedDatetime = DateTime.Now;
                        entityorder.Gst = 0;
                        entityorder.Igst = 0;
                        entityorder.UpdatedBy = userid;
                        entityorder.UpdateDatetime = DateTime.Now;
                        entityorder.IsActive = true;
                        this._SALESDBE.OrderPayment.Update(entityorder);
                        result = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;

                        //   uid = await this._SALESDBE.OrderMaster.MaxAsync(p => p.Id).ConfigureAwait(false);
                    }
                    else
                    {
                        

                        await this._SALESDBE.OrderPayment.AddAsync(new OrderPayment()
                        {
                            OrderId = orderItemId,
                            PayDate = DateTime.Now,
                            PayMode = 1, //Cash
                            Amount = totalprice,
                            CardType = 0,
                            AmoutHd = 0,
                            CreatedBy = userid,
                            CreatedDatetime = DateTime.Now,
                            Gst = 0,
                            Igst = 0,
                            UpdatedBy = userid,
                            UpdateDatetime = DateTime.Now,
                            IsActive = true
                        }).ConfigureAwait(false);

                        result = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;

                    }
                    
                   

                    if (result)
                    {
                        
                        var query = _cashsaledetails.cashsaledetails;
                        foreach (var data in query)
                        {
                            var entitystock = await this._SALESDBE.CarpetNumber.FirstOrDefaultAsync(i => i.TStockNo == data.stockid).ConfigureAwait(false);
                            if (entitystock != null && entitystock.StockNo > 0)
                            {
                                entitystock.Pack = 1;
                                entitystock.PackDate = DateTime.Now;
                                entitystock.PackingDetailId = orderItemId;
                                entitystock.PackSource = "SALES";
                                entitystock.Userid = userid;
                                this._SALESDBE.CarpetNumber.Update(entitystock);
                             //   result = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;

                                //   uid = await this._SALESDBE.OrderMaster.MaxAsync(p => p.Id).ConfigureAwait(false);
                            }
                            await this._SALESDBE.Directstockpack.AddAsync(new Directstockpack()
                            {
                                Stockno = entitystock.StockNo,
                                Remark = "SALES",
                                Dateadded = DateTime.Now,
                            }).ConfigureAwait(false);
                            result = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;

                           

                        }

                        if (result)
                        {
                            await dbusertrans.CommitAsync().ConfigureAwait(false);
                        }
                        else { await dbusertrans.RollbackAsync().ConfigureAwait(false); }

                    }
                    else { await dbusertrans.RollbackAsync().ConfigureAwait(false); }


                    //}

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;

        }
        #endregion
    }


}
