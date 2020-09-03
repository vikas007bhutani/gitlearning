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
    public class CustomSaleRepository : ICustomSaleRepository
    {
        private ExportErpDbContext _ERPDB;
        private Sales_ERPContext _SALESDBE;

        public CustomSaleRepository(Sales_ERPContext salesdbcontext, ExportErpDbContext exporterpdbcontext)
        {

            this._ERPDB = exporterpdbcontext;
            this._SALESDBE = salesdbcontext;

        }
        public async Task<Int64> AddCashSale([Bind("orderid")] CustomSaleVM _sale, int userid)
        {
            bool result = false, innerresult = false;
            long uid = 0;
            try
            {
                using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync())
                {
                    var entityorder = await this._SALESDBE.OrderMaster.FirstOrDefaultAsync(i => i.Id == _sale.orderid && i.MirrorId == 2);
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
                            

                        });

                        result = await this._SALESDBE.SaveChangesAsync()> 0;
                        uid = await this._SALESDBE.OrderMaster.MaxAsync(p => p.Id);
                    }
                    if (uid>0)
                    {
                    
                        await this._SALESDBE.OrderItemDetails.AddAsync(new OrderItemDetails()
                        {
                            OrderId = uid,
                            ItemDesc = _sale.categoryid+"/"+ _sale.size+"/"+ _sale.shapeid+"/"+ _sale.marblecolor,
                            StockId = _sale.stockno,
                            OrderType = _sale.saletypevalue == "OrderForm" ? (int?)SaleType.OF : (int?)SaleType.CM,
                            OrderTypePrefix = _sale.saletypevalue == "OrderForm" ?"OF":"CM",
                            ItemType = 1,
                            Price = _sale.totalvalue,
                            PriceInr =Math.Round(_sale.totalvalue*_sale.conversionrate,MidpointRounding.AwayFromZero),
                            ConversionRate = _sale.conversionrate,
                            Size=_sale.size,
                            Category=_sale.categoryid,
                            Shape=_sale.shapeid,
                            Color=_sale.marblecolor,
                            Unit = 1,
                            CreatedDatetime = DateTime.Now,
                            IsActive = true,
                            CurrencyType=_sale.currencyid
                           

                        });
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
                        innerresult = await this._SALESDBE.SaveChangesAsync() > 0;
                        if (innerresult)
                        {
                            await dbusertrans.CommitAsync();

                        }
                        else { await dbusertrans.RollbackAsync(); }


                    }
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return uid;
        }
        public async Task<Int64?> DeleteCashSale(int orderid, int userid)
        {
            bool result = false, innerresult = false;
            Int64? _orderid = 0;
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
                            _orderid =entity.OrderId;

                        }
                        else
                        { await dbusertrans.RollbackAsync().ConfigureAwait(false); }

                    }
                }
                return _orderid;
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

        public async Task<CustomSaleVM> GetSales(long _orderid,CustomSaleVM _cashsaledetails)
        {
            if (_cashsaledetails == null)
            {
                _cashsaledetails = new CustomSaleVM();
            }
            CommonRepository _comm = new CommonRepository(_SALESDBE);
            _cashsaledetails.cashsaledetails = await(from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                                     join od in this._SALESDBE.OrderItemDetails.Where(c => c.IsActive == true)
                                                    on m.Id equals od.OrderId

                                                     where m.Id == _orderid && od.ItemType == 1
                                                     select new customsaledetails
                                                     {
                                                         itemorderid = od.Id,
                                                         stockid = od.StockId,
                                                         //  itemdesc = od.category.Concat(",").Concat(st.itemname).Concat(",").Concat(st.marble).Concat(",").Concat(st.size).Concat(",").Concat(st.marblestone).ToString(),
                                                         itemdesc = od.ItemDesc,
                                                         ordertype = od.OrderTypePrefix,
                                                         salevalue = od.Price,
                                                         salevalueinr = od.PriceInr,
                                                         shape=od.Shape,
                                                         color=od.Color,
                                                         category=od.Category,
                                                         size=od.Size,
                                                         conversionrate=od.ConversionRate

                                                     }).ToListAsync();
            _cashsaledetails.standsaledetails = await (from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                                      join od in this._SALESDBE.OrderItemDetails.Where(c => c.IsActive == true)
                                                     on m.Id equals od.OrderId

                                                      where m.Id == _orderid && od.ItemType==2
                                                      select new standdetails
                                                      {
                                                          //itemorderid = od.Id,
                                                           color=od.Color,
                                                           standdesc=od.ItemDesc,
                                                           ordertype = od.OrderTypePrefix,
                                                           height=od.height

                                                      }).ToListAsync();

            _cashsaledetails.cinfo = await (from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                            join od in this._SALESDBE.OrderItemDetails.Where(c => c.IsActive == true)
                                           on m.Id equals od.OrderId
                                           join c in this._SALESDBE.CustomerDetails.Where(c => c.Isactive == true)
                                            on od.OrderId equals c.OrderId
                                            where m.Id == _orderid 
                                            select new customerinfo
                                            {
                                                Name=c.Name,
                                                countryid=c.countryid,
                                                Country=c.Country,
                                                Title=c.Title,
                                                City=c.City,
                                                Zipcode=c.Zipcode,
                                                State=c.State,
                                                TeleCountryCode=c.TeleCountryCode,
                                                Telephone=c.Telephone,
                                                MobCountryCode=c.MobCountryCode,
                                                Mobile=c.Mobile,
                                                 Address=c.Address,
                                                 Email=c.Email

                                            }).FirstOrDefaultAsync();

            _cashsaledetails.dinfo = await (from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                            where m.Id == _orderid 
                                            select new deliveryinfo
                                            {
                                               PortName=m.PortName,
                                               Passport=m.PassportNo,
                                               DeliveryFrom=m.DeliveryFrom,
                                               DeliveryTo=m.DeliveryTo

                                            }).FirstOrDefaultAsync();

            _cashsaledetails.paymentdetails = await (from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                                       join od in this._SALESDBE.OrderPayment.Where(c => c.IsActive == true)
                                                     on m.Id equals od.OrderId
                                                     join d  in this._SALESDBE.CurrencyMaster
                                                     on od.CurrencyType equals d.Id into currencydetails
                                                     from curr in currencydetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                                                     join e in this._SALESDBE.PayMethodMaster
                                                     on od.PayMode equals e.Id into paymethoddetails
                                                     from pm in paymethoddetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                                                     join f in this._SALESDBE.CardTypeMaster
                                                   on od.CardType equals f.Id into cardtypedetails
                                                     from card in cardtypedetails.Where(c => c.IsActive == true).DefaultIfEmpty()

                                                     where m.Id == _orderid 
                                                       select new paydetails
                                                       {
                                                           //itemorderid = od.Id,
                                                           payid=od.Id,
                                                           paymode= pm.PayName,
                                                           payamount=od.Amount,
                                                           paytype = (string.IsNullOrEmpty(card.CardName) ? curr.Type  : card.CardName) ,
                                                          
                                                       }).ToListAsync();

            _cashsaledetails.grandtotal = _cashsaledetails.cashsaledetails.Sum(s => s.salevalueinr);
            _cashsaledetails.grandtotalinr =_cashsaledetails.cashsaledetails.Sum(s => s.salevalueinr);
            _cashsaledetails.grandtotalcurrency= _cashsaledetails.cashsaledetails.Sum(s => s.salevalue);
            _cashsaledetails.balcurrency = _cashsaledetails.grandtotalcurrency  - Math.Round(((decimal) _cashsaledetails.cashsaledetails[0].conversionrate * (decimal)_cashsaledetails.paymentdetails.Sum(p => p.payamount)) / 100);
            _cashsaledetails.balinr = _cashsaledetails.grandtotalinr - Math.Round(((decimal)_cashsaledetails.cashsaledetails[0].conversionrate * (decimal)_cashsaledetails.paymentdetails.Sum(p => p.payamount)));



            _cashsaledetails.currencydetails = await _comm.GetCurrency();
            _cashsaledetails.shapesdetails = await _comm.GetShapes();
            _cashsaledetails.categorydetails = await _comm.GetCategory();
            _cashsaledetails.marblecolordetails = await _comm.GetMarbleColor();
            _cashsaledetails.standcolordetails = await _comm.GetStandColor();
            _cashsaledetails.nationalitydetails = await _comm.GetNationality();
            _cashsaledetails.countrydetails = await _comm.GetCountries();
            _cashsaledetails.sizeinwidth = await _comm.GetWidth();
            _cashsaledetails.sizeinheight = await _comm.GetLenght();
            _cashsaledetails.standcategory = await _comm.GetItemName();
            _cashsaledetails.cardtypedetails = await _comm.GetCardType();
            _cashsaledetails.paylaterdetails = await _comm.GetPayLaterType();
            _cashsaledetails.orderid = _orderid;
            _cashsaledetails.cardid = 0;
            _cashsaledetails.cardiddebit = 0;
            _cashsaledetails.paylaterid = 0;
            _cashsaledetails.currencyid = 0;
            _cashsaledetails.CashAmount = 0;
            _cashsaledetails.DebitAmount = 0;
            _cashsaledetails.CreditAmount = 0;
            _cashsaledetails.PayLaterAmount = 0;
            _cashsaledetails.PaytmAmount = 0;
            _cashsaledetails.paymethodvalue = "0";
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
            _cashsaledetails.standcolordetails = await _comm.GetStandColor();
            _cashsaledetails.nationalitydetails = await _comm.GetNationality();
            _cashsaledetails.cardtypedetails = await _comm.GetCardType();
            _cashsaledetails.paylaterdetails = await _comm.GetPayLaterType();
            _cashsaledetails.countrydetails = await _comm.GetCountries();
            _cashsaledetails.sizeinwidth = await _comm.GetWidth();
            _cashsaledetails.sizeinheight = await _comm.GetLenght();
            _cashsaledetails.standcategory = await _comm.GetItemName();
            return _cashsaledetails;
        }

        public async Task<bool> AddCustomerinfo([Bind("orderid")] CustomSaleVM _sale, int userid)
        {
            bool result = false, innerresult = false;
            long uid = 0;
            try
            {
                using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync().ConfigureAwait(false))
                {

                    var entitycust = await this._SALESDBE.CustomerDetails.FirstOrDefaultAsync(i => i.Id == _sale.orderid).ConfigureAwait(false);
                    if (entitycust != null && entitycust.Id > 0)
                    {
                        entitycust.Name = _sale.cinfo.Name;
                        entitycust.MobCountryCode = _sale.cinfo.MobCountryCode;
                        entitycust.Mobile = _sale.cinfo.Mobile;
                        entitycust.countryid = _sale.countryid;
                        entitycust.nationality = _sale.nationalityid.ToString();
                        entitycust.Email = _sale.cinfo.Email;
                        entitycust.Title = _sale.title_type.ToString();
                        entitycust.TeleCountryCode = _sale.cinfo.TeleCountryCode;
                        entitycust.Telephone = _sale.cinfo.Telephone;
                        entitycust.Zipcode = _sale.cinfo.Zipcode;
                        entitycust.Address = _sale.cinfo.Address;
                        entitycust.UpdatedDatetime = DateTime.Now;
                        entitycust.UpdatedBy = userid;
                        entitycust.Isactive = true;
                        entitycust.City = _sale.cinfo.City;
                        entitycust.State = _sale.cinfo.State;
                       this._SALESDBE.CustomerDetails.Update(entitycust);
                        result = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;

                    }
                    else
                    {
                        await this._SALESDBE.CustomerDetails.AddAsync(new CustomerDetails()
                        {
                            Name = _sale.cinfo.Name,
                            MobCountryCode= _sale.cinfo.MobCountryCode,
                            Mobile=_sale.cinfo.Mobile,
                            TeleCountryCode = _sale.cinfo.TeleCountryCode,
                            Telephone=_sale.cinfo.Telephone,
                            Email=_sale.cinfo.Email,
                             Address=_sale.cinfo.Address,
                              City=_sale.cinfo.City,
                              countryid=_sale.countryid,
                              Zipcode=_sale.cinfo.Zipcode,
                              nationality=_sale.cinfo.nationality,
                              State=_sale.cinfo.State,
                              Title=_sale.titletypevalue,
                            CreatedDatetime = DateTime.Now,
                            CreatedBy=userid,
                            Isactive = true,
                            OrderId=_sale.orderid
                           

                        }).ConfigureAwait(false);

                        result = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                       
                    }
                    if (result)
                    {
                        await dbusertrans.CommitAsync().ConfigureAwait(false);

                    }
                    else { await dbusertrans.RollbackAsync().ConfigureAwait(false); }

                }
            }
            catch (Exception ex)
            {

                result = false;
            }
            return result;
        }
        public async Task<bool> AddStandSale([Bind("orderid")] CustomSaleVM _sale, int userid)
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
                        var entityitem = await this._SALESDBE.CarpetNumber.Where(i => i.Prefix == _sale.standcode).OrderByDescending(b=>b.TStockNo).FirstOrDefaultAsync().ConfigureAwait(false);
                        if (entityitem != null && entityitem.TStockNo != string.Empty)
                        {
                            await this._SALESDBE.OrderItemDetails.AddAsync(new OrderItemDetails()
                            {
                                OrderId = _sale.orderid,
                                ItemDesc = entityitem.Prefix,
                                StockId = entityitem.TStockNo,

                                OrderType = _sale.saletypevalue == "OrderForm" ? (int?)SaleType.OF : (int?)SaleType.CM,
                                OrderTypePrefix = _sale.saletypevalue == "OrderForm" ? "OF" : "CM",
                                ItemType = 2,
                                width = _sale.width,
                                height = _sale.height,
                                CreatedDatetime = DateTime.Now,
                                IsActive = true,
                                Color = _sale.color

                            }).ConfigureAwait(false);
                            innerresult = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                        }
                        else
                        { innerresult = false;  }

                       
                        if (innerresult)
                        {
                            await dbusertrans.CommitAsync().ConfigureAwait(false);

                        }
                        else { await dbusertrans.RollbackAsync().ConfigureAwait(false); }
                        //uid = await this._SALESDBE.OrderMaster.MaxAsync(p => p.Id).ConfigureAwait(false);
                    }
                    //if (uid > 0)
                    //{

                    //    await this._SALESDBE.OrderItemDetails.AddAsync(new OrderItemDetails()
                    //    {
                    //        OrderId = uid,
                    //        ItemDesc = _sale.item_desc,
                    //        StockId = _sale.stockno,
                    //        OrderType = (int?)SaleType.OrderForm,
                    //        OrderTypePrefix = _sale.saletypevalue,
                    //        ItemType = 2,
                    //        Price = _sale.totalvalue,
                    //        PriceInr = _sale.totalvalue,
                    //        ConversionRate = _sale.conversionrate,
                    //        Unit = 1,
                    //        CreatedDatetime = DateTime.Now,
                    //        IsActive = true,

                    //    }).ConfigureAwait(false);
                        
                    //    innerresult = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                    //    if (innerresult)
                    //    {
                    //        await dbusertrans.CommitAsync().ConfigureAwait(false);

                    //    }
                    //    else { await dbusertrans.RollbackAsync().ConfigureAwait(false); }


                    //}
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;

        }

        public async Task<bool> AddDeliveryDetails([Bind("orderid")] CustomSaleVM _sale, int userid)
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
                        entityorder.PortType = (int)(PortType)Enum.Parse(typeof(PortType), _sale.porttypevalue) ;
                        entityorder.DelieveryType = (int)(DeliveryType)Enum.Parse(typeof(DeliveryType), _sale.deliverytypevalue);
                        entityorder.PassportNo =_sale.dinfo.Passport;
                        entityorder.PortName = _sale.dinfo.PortName;
                        entityorder.DeliveryFrom = _sale.dinfo.DeliveryFrom;
                        entityorder.DeliveryTo = _sale.dinfo.DeliveryTo;

                        innerresult = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;

                    
                        //uid = await this._SALESDBE.OrderMaster.MaxAsync(p => p.Id).ConfigureAwait(false);
                    }
                    if (innerresult)
                    {
                        await dbusertrans.CommitAsync().ConfigureAwait(false);

                    }
                    else { await dbusertrans.RollbackAsync().ConfigureAwait(false); }
                    //if (uid > 0)
                    //{

                    //    await this._SALESDBE.OrderItemDetails.AddAsync(new OrderItemDetails()
                    //    {
                    //        OrderId = uid,
                    //        ItemDesc = _sale.item_desc,
                    //        StockId = _sale.stockno,
                    //        OrderType = (int?)SaleType.OrderForm,
                    //        OrderTypePrefix = _sale.saletypevalue,
                    //        ItemType = 2,
                    //        Price = _sale.totalvalue,
                    //        PriceInr = _sale.totalvalue,
                    //        ConversionRate = _sale.conversionrate,
                    //        Unit = 1,
                    //        CreatedDatetime = DateTime.Now,
                    //        IsActive = true,

                    //    }).ConfigureAwait(false);

                    //    innerresult = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                    //    if (innerresult)
                    //    {
                    //        await dbusertrans.CommitAsync().ConfigureAwait(false);

                    //    }
                    //    else { await dbusertrans.RollbackAsync().ConfigureAwait(false); }


                    //}
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        public async Task<long?> DeleteStandSale(int standid, int userid)
        {

            bool result = false, innerresult = false;
            Int64? _orderid = 0;
            try
            {


                using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync().ConfigureAwait(false))
                {


                    var entity = await _SALESDBE.OrderItemDetails.FirstOrDefaultAsync(item => item.Id == standid).ConfigureAwait(false);

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
                            _orderid = entity.OrderId;

                        }
                        else
                        { await dbusertrans.RollbackAsync().ConfigureAwait(false); }

                    }
                }
                return _orderid;
            }
            catch (Exception)
            {

                throw;
            }

        }

        public async Task<bool> AddSalePayment(CustomSaleVM _sale, int userid)
        {
            bool result = false, innerresult = false;
            long uid = 0;
            int paymodeval = 0;
            try
            {
                using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync().ConfigureAwait(false))
                {

                    var entityorder =    await (from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                           join od in this._SALESDBE.OrderPayment.Where(c => c.IsActive == true)
                           on m.Id equals od.OrderId into paydetails
                           from pay in paydetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                           join e in this._SALESDBE.OrderItemDetails

                           on m.Id equals e.OrderId into orderitemdetails
                           from item in orderitemdetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                          

                           where m.Id == _sale.orderid select m                                               



                           ).ToListAsync();
                  



                    //var entityorder = await this._SALESDBE.OrderMaster.FirstOrDefaultAsync(i => i.Id == _sale.orderid && i.MirrorId == 2).ConfigureAwait(false);

                    //var entitypayment = await this._SALESDBE.OrderPayment.Where(i => i.Id == _sale.orderid).ToListAsync().ConfigureAwait(false);
                    //var entityoderdetails = await this._SALESDBE.OrderItemDetails.Where(i => i.Id == _sale.orderid).ToListAsync().ConfigureAwait(false);
                    decimal? currencyamount= entityorder.Sum(s => s.OrderPayment.Sum(a=>a.Amount));
                   decimal? currencyamountinr = entityorder.Sum(s => s.OrderPayment.Sum(a => a.AmoutHd));
                    if(_sale.paymethodvalue.ToUpper().Contains("CASH") && _sale.currencyid !=6)
                    {


                    }

                    if (entityorder != null && entityorder.Count > 0)
                    {
                     
                            await this._SALESDBE.OrderPayment.AddAsync(new OrderPayment()
                            {
                               
                               Amount=_sale.PayLaterAmount+ _sale.CashAmount+ _sale.CreditAmount+ _sale.DebitAmount+ _sale.PaytmAmount,
                                PayMode= (int)(paymethod)Enum.Parse(typeof(paymethod), _sale.paymethodvalue),
                                CardType= (int)(paymethod)Enum.Parse(typeof(paymethod), _sale.paymethodvalue) == 4?_sale.paylaterid : _sale.cardid+_sale.cardiddebit,
                                CreatedDatetime = DateTime.Now,
                                IsActive = true,
                                OrderId=_sale.orderid,
                                PayDate=DateTime.Now,
                                CurrencyType=_sale.currencyid

                            }).ConfigureAwait(false);
                            innerresult = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                      


                        if (innerresult)
                        {
                            await dbusertrans.CommitAsync().ConfigureAwait(false);
                           
                           

                        }
                        else { await dbusertrans.RollbackAsync().ConfigureAwait(false); }
                        //uid = await this._SALESDBE.OrderMaster.MaxAsync(p => p.Id).ConfigureAwait(false);
                    }
                   
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return result;
        }

        public async Task<long?> DeletePayment(int payid, int userid)
        {
            bool result = false, innerresult = false;
            Int64? _orderid = 0;
            try
            {


                using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync().ConfigureAwait(false))
                {


                    var entity = await _SALESDBE.OrderPayment.FirstOrDefaultAsync(item => item.Id == payid).ConfigureAwait(false);

                    if (entity != null)
                    {

                        entity.CreatedDatetime = DateTime.Now;
                        entity.IsActive = false;
                        // entity.UpdatedBy=
                        this._SALESDBE.OrderPayment.Update(entity);
                        innerresult = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                        if (innerresult)
                        {
                            await dbusertrans.CommitAsync().ConfigureAwait(false);
                            _orderid = entity.OrderId;

                        }
                        else
                        { await dbusertrans.RollbackAsync().ConfigureAwait(false); }

                    }
                }
                return _orderid;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
