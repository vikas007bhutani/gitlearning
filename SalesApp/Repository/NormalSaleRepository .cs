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
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;

namespace SalesApp.Repository
{
    public class NormalSaleRepository : INormalSaleRepository
    {
        private ExportErpDbContext _ERPDB;
        private Sales_ERPContext _SALESDBE;

        public NormalSaleRepository(Sales_ERPContext salesdbcontext, ExportErpDbContext exporterpdbcontext)
        {

            this._ERPDB = exporterpdbcontext;
            this._SALESDBE = salesdbcontext;

        }
        public async Task<Int64> AddCashSale([Bind("orderid")] NormalSaleVM _sale, int userid)
        {
            bool result = false, innerresult = false;
            long uid = 0;
            decimal finalINR = 0, INRvalue = 0;

            try
            {
                using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    var entitystock = await this._SALESDBE.OrderItemDetails.FirstOrDefaultAsync(i => i.StockId == _sale.stockno && i.IsActive == true).ConfigureAwait(false);
                    if (entitystock == null)
                    {
                        var entityorder = await this._SALESDBE.OrderMaster.FirstOrDefaultAsync(i => i.Id == _sale.orderid).ConfigureAwait(false);
                        INRvalue = Math.Round((decimal)(_sale.totalvalue * _sale.conversionrate), MidpointRounding.AwayFromZero);
                        if (INRvalue > 10)
                        {
                            decimal resultd = Math.Abs(INRvalue % 10);
                            if (resultd > 0)
                            {
                                finalINR = INRvalue + (10 - resultd);
                            }
                            else
                            {
                                finalINR = INRvalue;
                            }
                        }
                        else
                        {
                            finalINR = INRvalue;
                        }
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
                                Description = "NormalSale",
                                TransactionId = Common.GetUnique(),
                                CreatedDatetime = DateTime.Now,
                                CreatedBy = userid,
                                IsActive = true,
                                // salestatus = 1

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
                                OrderType = _sale.saletypevalue == "OF" ? (int?)SaleType.OF : (int?)SaleType.CM,
                                OrderTypePrefix = _sale.saletypevalue,
                                ItemType = 1,
                                Price = _sale.totalvalue,
                                PriceInr = finalINR,
                                ConversionRate = _sale.conversionrate,
                                Unit = 1,
                                CreatedDatetime = DateTime.Now,
                                CreatedBy = userid,
                                IsActive = true,
                                CurrencyType = _sale.currencyid,
                                SaleType = 1

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
                    else { return -1; }

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
                        entity.UpdatedBy = userid;
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
            NormalSaleVM _cashsaledetails = new NormalSaleVM();
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
                            _cashsaledetails.cashsaledetails = await (from  od in this._SALESDBE.OrderItemDetails.Where(c => c.IsActive == true)
                                                                     

                                                                      where od.OrderId == orderid && od.ItemType == 1
                                                                      select new Ncashsaledetails
                                                                      {
                                                                          saletype =od.SaleType
                                                                          
                                                                          //  salevalueinr = Math.Round((decimal)(od.PriceInr * od.ConversionRate), MidpointRounding.AwayFromZero),
                                                                        


                                                                      }).ToListAsync();
                            _cashsaledetails.dinfo = await (from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                                            where m.Id == orderid
                                                            select new Ndeliveryinfo
                                                            {
                                                                PortName = m.PortName,
                                                                Passport = m.PassportNo,
                                                                DeliveryFrom = m.DeliveryFrom,
                                                                DeliveryTo = m.DeliveryTo,
                                                                DelieveryType = m.DelieveryType,
                                                                PortType = m.PortType

                                                            }).FirstOrDefaultAsync();
                           
                                _cashsaledetails.paymentdetails = await (from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                                                         join od in this._SALESDBE.OrderPayment.Where(c => c.IsActive == true)
                                                                       on m.Id equals od.OrderId
                                                                         join d in this._SALESDBE.CurrencyMaster
                                                                         on od.CurrencyType equals d.Id into currencydetails
                                                                         from curr in currencydetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                                                                         join e in this._SALESDBE.PayMethodMaster
                                                                         on od.PayMode equals e.Id into paymethoddetails
                                                                         from pm in paymethoddetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                                                                         join f in this._SALESDBE.CardTypeMaster
                                                                       on od.CardType equals f.Id into cardtypedetails
                                                                         from card in cardtypedetails.Where(c => c.IsActive == true).DefaultIfEmpty()

                                                                         where m.Id == orderid
                                                                         select new Npaydetails
                                                                         {
                                                                             //itemorderid = od.Id,
                                                                             payid = od.Id,
                                                                             paymode = pm.PayName,
                                                                             payamount = od.Amount,
                                                                             payamountinr = od.AmoutHd,
                                                                             paytype = (string.IsNullOrEmpty(card.CardName) ? curr.Type : card.CardName),
                                                                             symbol = curr.Symbol != null ? curr.Symbol : "$",
                                                                             currencyid = curr.Id


                                                                         }).ToListAsync();

                                _cashsaledetails.grandtotal = _cashsaledetails.cashsaledetails.Sum(s => s.salevalueinr);
                                _cashsaledetails.grandtotalinr = _cashsaledetails.cashsaledetails.Sum(s => s.salevalueinr);
                                _cashsaledetails.grandtotalcurrency = _cashsaledetails.cashsaledetails.Sum(s => s.salevalue);

                                if (_cashsaledetails.paymentdetails.Count > 0)
                                {
                                    _cashsaledetails.balcurrency = _cashsaledetails.grandtotalcurrency - ((decimal)_cashsaledetails.paymentdetails.Sum(p => p.payamount));
                                    _cashsaledetails.balinr = _cashsaledetails.grandtotalinr - Math.Round(((decimal)_cashsaledetails.paymentdetails.Sum(p => p.payamountinr)));
                                    if (_cashsaledetails.paymentdetails[0].currencyid != 6)
                                    {
                                        _cashsaledetails.currsymbol = _cashsaledetails.paymentdetails[0].symbol;
                                    }
                                    else
                                    { _cashsaledetails.currsymbol = "$"; }

                                }
                                else
                                {
                                    _cashsaledetails.balcurrency = _cashsaledetails.grandtotalcurrency;
                                    _cashsaledetails.balinr = _cashsaledetails.grandtotalinr;
                                    _cashsaledetails.currsymbol = "$";
                                }
                                if (_cashsaledetails.balcurrency < 0 || _cashsaledetails.balinr < 0)
                                {
                                    _cashsaledetails.balcurrency = 0;
                                    _cashsaledetails.balinr = 0;

                                }
                            if (_cashsaledetails.balinr == 0)
                            {
                                if ((_cashsaledetails.dinfo.DeliveryFrom != null && _cashsaledetails.cashsaledetails[0].saletype == 2) || _cashsaledetails.cashsaledetails[0].saletype == 1)
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
                                            entitycarpet.PackingDetailId = (Int32)item.Id;
                                            entitycarpet.PackingId = (Int32)item.OrderId;
                                            this._SALESDBE.OrderMaster.Update(entity);

                                            await this._SALESDBE.Directstockpack.AddAsync(new Directstockpack()
                                            {
                                                Stockno = entitycarpet.StockNo,
                                                Remark = "SALES",
                                                Dateadded = DateTime.Now,
                                            }).ConfigureAwait(false);
                                            result = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }



                                    }
                                    if (result)
                                    {
                                        await dbusertrans.CommitAsync().ConfigureAwait(false);

                                    }
                                    else
                                    { await dbusertrans.RollbackAsync().ConfigureAwait(false); }
                                }



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

        public async Task<NormalSaleVM> GetSales(Int64? _orderid,NormalSaleVM _cashsaledetails)
        {
            if (_cashsaledetails == null)
            {
                _cashsaledetails = new NormalSaleVM();
            }
            CommonRepository _comm = new CommonRepository(_SALESDBE);
            _cashsaledetails.cashsaledetails = await (from mr in this._SALESDBE.MirrorDetails.Where(c => c.IsActive == true)
                                                      join m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                                       on mr.Id equals m.MirrorId
                                                      join od in this._SALESDBE.OrderItemDetails.Where(c => c.IsActive == true)
                                                     on m.Id equals od.OrderId into itemdetails
                                                      from item in itemdetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                                                      join c in this._SALESDBE.CurrencyMaster.Where(c => c.IsActive == true)
                                                       on item.CurrencyType equals c.Id into currdetails
                                                      from curr in currdetails.Where(c => c.IsActive == true).DefaultIfEmpty()

                                                      where m.Id == _orderid && item.ItemType == 1
                                                      select new Ncashsaledetails
                                                      {
                                                          itemorderid = item.Id,
                                                          stockid = item.StockId,
                                                          //  itemdesc = od.category.Concat(",").Concat(st.itemname).Concat(",").Concat(st.marble).Concat(",").Concat(st.size).Concat(",").Concat(st.marblestone).ToString(),
                                                          itemdesc = item.ItemDesc,
                                                          ordertype = item.OrderTypePrefix,
                                                          salevalue = item.Price,
                                                          salevalueinr = item.PriceInr,
                                                        //  salevalueinr = Math.Round((decimal)(od.PriceInr * od.ConversionRate), MidpointRounding.AwayFromZero),
                                                          mirrorid = mr.Id,
                                                          mirrordate = mr.Date,
                                                          conversionrate = item.ConversionRate,
                                                          symbol = curr.Symbol,
                                                          currencyid=(int)item.CurrencyType,
                                                          currency=curr.Type,
                                                          saletype=item.SaleType
                                                          

                                                      }).ToListAsync();
            _cashsaledetails.standsaledetails = await (from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                                      join od in this._SALESDBE.OrderItemDetails.Where(c => c.IsActive == true)
                                                     on m.Id equals od.OrderId

                                                      where m.Id == _orderid && od.ItemType==2
                                                      select new Nstanddetails
                                                      {
                                                          itemorderid = od.Id,
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
                                            join cn in this._SALESDBE.CountriesMaster
                                            on c.countryid equals cn.Id into countrydetails
                                            from country in countrydetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                                            where m.Id == _orderid 
                                            select new Ncustomerinfo
                                            {
                                                Name=c.Name,
                                                countryid=c.countryid,
                                                Country=country.Name,
                                                Title=c.Title,
                                                City=c.City,
                                                Zipcode=c.Zipcode,
                                                State=c.State,
                                                TeleCountryCode=c.TeleCountryCode,
                                                Telephone=c.Telephone,
                                                MobCountryCode=c.MobCountryCode,
                                                Mobile=c.Mobile,
                                                 Address=c.Address,
                                                 Email=c.Email,
                                                nationality = country.Name

                                            }).FirstOrDefaultAsync();

            _cashsaledetails.dinfo = await (from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                            where m.Id == _orderid 
                                            select new Ndeliveryinfo
                                            {
                                               PortName=m.PortName,
                                               Passport=m.PassportNo,
                                               DeliveryFrom=m.DeliveryFrom,
                                               DeliveryTo=m.DeliveryTo,
                                               DelieveryType=m.DelieveryType,
                                               PortType=m.PortType

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
                                                       select new Npaydetails
                                                       {
                                                           //itemorderid = od.Id,
                                                           payid=od.Id,
                                                           paymode= pm.PayName,
                                                           payamount=od.Amount,
                                                           payamountinr = od.AmoutHd,
                                                           paytype = (string.IsNullOrEmpty(card.CardName) ? curr.Type  : card.CardName) ,
                                                           symbol=od.PayMode ==1 ?  curr.Symbol !=null ?curr.Symbol: "$": "₹",
                                                           mainsymbol = curr.Symbol != null ? curr.Symbol : "$",
                                                           currencyid =curr.Id


                                                       }).ToListAsync();

            _cashsaledetails.grandtotal = _cashsaledetails.cashsaledetails.Sum(s => s.salevalueinr);
            _cashsaledetails.grandtotalinr =_cashsaledetails.cashsaledetails.Sum(s => s.salevalueinr);
            _cashsaledetails.grandtotalcurrency= _cashsaledetails.cashsaledetails.Sum(s => s.salevalue);

            if (_cashsaledetails.paymentdetails.Count > 0 && _cashsaledetails.cashsaledetails.Count>0)
            {
                _cashsaledetails.balcurrency = _cashsaledetails.grandtotalcurrency - ((decimal)_cashsaledetails.paymentdetails.Sum(p => p.payamount));
                _cashsaledetails.balinr = _cashsaledetails.grandtotalinr - Math.Round(((decimal)_cashsaledetails.paymentdetails.Sum(p => p.payamountinr)));
                if (_cashsaledetails.paymethodvalue.ToUpper().Contains("CASH")  && _cashsaledetails.paymentdetails[0].currencyid != 6)
                {
                    _cashsaledetails.currsymbol = _cashsaledetails.cashsaledetails[0].symbol;

                }
                else
                { _cashsaledetails.currsymbol = _cashsaledetails.cashsaledetails[0].symbol; }

            }
            else
            {
                _cashsaledetails.balcurrency = _cashsaledetails.grandtotalcurrency;
                _cashsaledetails.balinr = _cashsaledetails.grandtotalinr;
                _cashsaledetails.currsymbol = _cashsaledetails.cashsaledetails[0].symbol;
            }
            if(_cashsaledetails.balcurrency<0 || _cashsaledetails.balinr<0)
            {
                _cashsaledetails.balcurrency =0;
                _cashsaledetails.balinr =0;

            }
            //if (_cashsaledetails.paymentdetails.Count > 0)
            //{
            //    _cashsaledetails.balcurrency = _cashsaledetails.grandtotalcurrency - Math.Round(((decimal)_cashsaledetails.cashsaledetails[0].conversionrate * (decimal)_cashsaledetails.paymentdetails.Sum(p => p.payamount)) / 100);
            //    _cashsaledetails.balinr = _cashsaledetails.grandtotalinr - Math.Round(((decimal)_cashsaledetails.cashsaledetails[0].conversionrate * (decimal)_cashsaledetails.paymentdetails.Sum(p => p.payamount)));

            //}
            //else
            //{
            //    _cashsaledetails.balcurrency = 0;
            //    _cashsaledetails.balinr = 0;
            //}
            _cashsaledetails.countrydetails = await _comm.GetCountries();
            _cashsaledetails.currencydetails = await _comm.GetCurrency();
            _cashsaledetails.shapesdetails = await _comm.GetShapes();
            _cashsaledetails.categorydetails = await _comm.GetCategory();
            _cashsaledetails.marblecolordetails = await _comm.GetMarbleColor();
            _cashsaledetails.standcolordetails = await _comm.GetStandColor();
            _cashsaledetails.nationalitydetails = await _comm.GetNationality();
           
            _cashsaledetails.sizeinwidth = await _comm.GetWidth();
            _cashsaledetails.sizeinheight = await _comm.GetLenght();
            _cashsaledetails.standcategory = await _comm.GetItemName();
            _cashsaledetails.cardtypedetails = await _comm.GetCardType();
            _cashsaledetails.paylaterdetails = await _comm.GetPayLaterType();
            _cashsaledetails.itemcount = _cashsaledetails.cashsaledetails.Count();
            
            _cashsaledetails.orderid = _orderid;
            _cashsaledetails.cardid = 0;
            _cashsaledetails.cardiddebit = 0;
            _cashsaledetails.paylaterid = 0;
            _cashsaledetails.currencyid = 0;
            _cashsaledetails.CashAmount = null;
            _cashsaledetails.DebitAmount = null;
            _cashsaledetails.CreditAmount = null;
            _cashsaledetails.PayLaterAmount = null;
            _cashsaledetails.PaytmAmount = null;
            _cashsaledetails.paymethodvalue = "0";
            _cashsaledetails.stockno = string.Empty;
            _cashsaledetails.standcode = string.Empty;
            _cashsaledetails.color = string.Empty;
            _cashsaledetails.height = string.Empty;
            _cashsaledetails._saletype = 0;
           // _cashsaledetails._saletype = 0;
            _cashsaledetails.customspecialaddition = string.Empty;
            _cashsaledetails.specialadditions = await _comm.GetSpecialAddition();
            //  _cashsaledetails.specialaddition = 0;
            _cashsaledetails.totalvalue = null;
            _cashsaledetails.quantity = 1;
            if (_cashsaledetails.cashsaledetails.Count > 0)
            {
                _cashsaledetails.conversionrate = (decimal)_cashsaledetails.cashsaledetails[0].conversionrate;
                _cashsaledetails.currencyid = _cashsaledetails.cashsaledetails[0].currencyid;
                _cashsaledetails.mirrorid = _cashsaledetails.cashsaledetails[0].mirrorid;
                _cashsaledetails.mirrordate = _cashsaledetails.cashsaledetails[0].mirrordate;
                if (_cashsaledetails.cashsaledetails[0].itemorderid > 0)
                {
                    _cashsaledetails.saletype = _cashsaledetails.cashsaledetails[0].saletype;
                }
                    List<SelectListItem> newSelectList = new List<SelectListItem>();
                foreach (var item in _cashsaledetails.cashsaledetails)
                {
                    newSelectList.Add(new SelectListItem
                    {
                        Value = item.currencyid.ToString(),
                        Text = item.currency
                    });
                }
                
                newSelectList.Add(new SelectListItem("INR", "6"));
                _cashsaledetails.currencydetails = newSelectList.GroupBy(a => a.Value).Select(b => b.First()).ToList();
            }
            else
            {
                _cashsaledetails.currencydetails = await _comm.GetCurrency();

            }
            if (_cashsaledetails.dinfo != null)

            {
                _cashsaledetails.deliverytypevalue = _cashsaledetails.dinfo.DelieveryType.ToString();
                _cashsaledetails.porttypevalue = _cashsaledetails.dinfo.PortType.ToString();

            }
            if (_cashsaledetails.cashsaledetails.Count > 0)
            {

                var saletype = _cashsaledetails.cashsaledetails.FirstOrDefault(a => a.ordertype == "OF");
                if (saletype != null)
                {
                    _cashsaledetails.standsaletype = "OF";
                }
                else { _cashsaledetails.standsaletype = "CM"; }

            }

                return _cashsaledetails;

        }

        public async Task<NormalSaleVM> Init(long mirrorId)
        {
            NormalSaleVM _cashsaledetails = new NormalSaleVM();
            CommonRepository _comm = new CommonRepository(_SALESDBE);
            var entityorder = await this._SALESDBE.MirrorDetails.FirstOrDefaultAsync(i => i.Id == mirrorId).ConfigureAwait(false);
            if (entityorder != null && entityorder.Id > 0)
            {
                _cashsaledetails.mirrordate = entityorder.Date;
            }
            _cashsaledetails.mirrorid = mirrorId;
            _cashsaledetails.quantity = 1;
            _cashsaledetails.currencydetails = await _comm.GetCurrency();
            _cashsaledetails.specialadditions = await _comm.GetSpecialAddition();
            _cashsaledetails.countrydetails = await _comm.GetCountries();
         //   _cashsaledetails.currencydetails = await _comm.GetCurrency();
            _cashsaledetails.shapesdetails = await _comm.GetShapes();
            _cashsaledetails.categorydetails = await _comm.GetCategory();
            _cashsaledetails.marblecolordetails = await _comm.GetMarbleColor();
            _cashsaledetails.standcolordetails = await _comm.GetStandColor();
            _cashsaledetails.nationalitydetails = await _comm.GetNationality();
            _cashsaledetails.conversionrate = null;
            _cashsaledetails.CreditAmount = null;
            _cashsaledetails.DebitAmount = null;
            _cashsaledetails.PayLaterAmount = null;
            _cashsaledetails.CashAmount = null;
            _cashsaledetails.PaytmAmount = null;



            //_cashsaledetails.sizeinwidth = await _comm.GetWidth();
            //_cashsaledetails.sizeinheight = await _comm.GetLenght();
            //_cashsaledetails.standcategory = await _comm.GetItemName();
            //_cashsaledetails.cardtypedetails = await _comm.GetCardType();
            //_cashsaledetails.paylaterdetails = await _comm.GetPayLaterType();

            return _cashsaledetails;
        }

        public async Task<bool> AddCustomerinfo([Bind("orderid")] NormalSaleVM _sale, int userid)
        {
            bool result = false, innerresult = false;
            long uid = 0;
            try
            {
                using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync().ConfigureAwait(false))
                {

                    var entitycust = await this._SALESDBE.CustomerDetails.FirstOrDefaultAsync(i => i.OrderId == _sale.orderid).ConfigureAwait(false);
                    if (entitycust != null && entitycust.Id > 0)
                    {
                        entitycust.Name = _sale.cinfo.Name;
                        entitycust.MobCountryCode = _sale.cinfo.MobCountryCode;
                        entitycust.Mobile = _sale.cinfo.Mobile;
                        entitycust.countryid = _sale.countryid;
                        entitycust.nationality = _sale.nationalityid.ToString();
                        entitycust.Email = _sale.cinfo.Email;
                        entitycust.Title = _sale.titletypevalue;
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
        public async Task<bool> AddStandSale([Bind("orderid")] NormalSaleVM _sale, int userid)
        {

            bool result = false, innerresult = false;
            long uid = 0;
            try
            {
                using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    var entityorder = await this._SALESDBE.OrderMaster.FirstOrDefaultAsync(i => i.Id == _sale.orderid).ConfigureAwait(false);
                   
                    if (entityorder != null && entityorder.Id > 0)
                    {
                        var entityitem = await (from m in this._SALESDBE.CarpetNumber
                                                join it in this._SALESDBE.ItemMaster
                                               on m.Prefix equals it.ItemCode

                                                where m.Prefix == _sale.standcode
                                                select new Nstanddetails
                                                {
                                                    standcode = m.TStockNo,
                                                    standdesc = it.ItemName

                                                }).OrderByDescending(m => m.standcode).FirstOrDefaultAsync().ConfigureAwait(false);

                      //  await this._SALESDBE.CarpetNumber.Where(i => i.Prefix == _sale.standcode).OrderByDescending(b=>b.TStockNo).FirstOrDefaultAsync().ConfigureAwait(false);
                        if (entityitem != null && entityitem.standcode != string.Empty)
                        {
                            await this._SALESDBE.OrderItemDetails.AddAsync(new OrderItemDetails()
                            {
                                OrderId = _sale.orderid,
                                ItemDesc = entityitem.standdesc,
                                StockId = entityitem.standcode,

                                OrderType = _sale.saletypevalue == "OF" ? (int?)SaleType.OF : (int?)SaleType.CM,
                                OrderTypePrefix = _sale.saletypevalue,
                                ItemType = 2,
                                width = _sale.width,
                                height = _sale.height,
                                CreatedDatetime = DateTime.Now,
                                IsActive = true,
                                Color = _sale.color,
                                Unit=1,
                                CreatedBy=userid

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
                   
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return innerresult;

        }

        public async Task<bool> AddDeliveryDetails([Bind("orderid")] NormalSaleVM _sale, int userid)
        {
            bool result = false, innerresult = false;
            long uid = 0;
            try
            {
                using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync().ConfigureAwait(false))
                {
                    var entityorder = await this._SALESDBE.OrderMaster.FirstOrDefaultAsync(i => i.Id == _sale.orderid).ConfigureAwait(false);

                    if (entityorder != null && entityorder.Id > 0)
                    {
                        //if (_sale.cashsaledetails[0].ordertype == "OF")
                        //{
                            entityorder.PortType = (int)(PortType)Enum.Parse(typeof(PortType), _sale.porttypevalue);
                            entityorder.DelieveryType = (int)(DeliveryType)Enum.Parse(typeof(DeliveryType), _sale.deliverytypevalue);
                            entityorder.PassportNo = _sale.dinfo.Passport;
                            entityorder.PortName = _sale.dinfo.PortName;
                            entityorder.DeliveryFrom = _sale.dinfo.DeliveryFrom;
                            entityorder.DeliveryTo = _sale.dinfo.DeliveryTo;
                            entityorder.CreatedBy = userid;
                            innerresult = await this._SALESDBE.SaveChangesAsync().ConfigureAwait(false) > 0;
                      //  }

                    }
                    if (innerresult)
                    {
                        await dbusertrans.CommitAsync().ConfigureAwait(false);

                    }
                    else { await dbusertrans.RollbackAsync().ConfigureAwait(false); }
                   
                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return innerresult;
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
                        entity.UpdatedBy = userid;
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

        public async Task<bool> AddSalePayment(NormalSaleVM _sale, int userid)
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

                           where m.Id == _sale.orderid
                                                select new Ncalucation
                                                {
                                                    mirrorid = m.MirrorId,
                                                    itemorderid = item.Id,
                                                    orderid = m.Id,
                                                    TotalAmount = Convert.ToDecimal(pay.Amount),
                                                    TotalAmountINR = Convert.ToDecimal(pay.AmoutHd),
                                                    Amount = Convert.ToDecimal(item.Price),
                                                    AmountINR = Convert.ToDecimal(item.PriceInr),
                                                    conversionrate = item.ConversionRate,

                                                }



                           ).ToListAsync();




                    //var entityorder = await this._SALESDBE.OrderMaster.FirstOrDefaultAsync(i => i.Id == _sale.orderid && i.MirrorId == 2).ConfigureAwait(false);

                    //var entitypayment = await this._SALESDBE.OrderPayment.Where(i => i.Id == _sale.orderid).ToListAsync().ConfigureAwait(false);
                    //var entityoderdetails = await this._SALESDBE.OrderItemDetails.Where(i => i.Id == _sale.orderid).ToListAsync().ConfigureAwait(false);
                    decimal currencyamount = entityorder.Select(s => s.TotalAmount).FirstOrDefault();
                    decimal currencyamountinr = entityorder.Select(s => s.TotalAmountINR).FirstOrDefault();
                    decimal oldamount = entityorder.Sum(s => s.Amount);
                    decimal oldamountinr = entityorder.Sum(s => s.AmountINR);
                    decimal convrate = (decimal)entityorder.Where(s => s.conversionrate > 0).FirstOrDefault().conversionrate;
                    decimal? amount = 0;
                    decimal? amounthd = 0;
                    decimal? finalamountinr = 0;
                    decimal finalINR = 0, INRvalue = 0;

                    if (_sale.paymethodvalue.ToUpper().Contains("CASH") && _sale.currencyid != 6)
                    {


                        amount = Convert.ToDecimal(_sale.PayLaterAmount) + Convert.ToDecimal(_sale.CashAmount) + Convert.ToDecimal(_sale.CreditAmount) + Convert.ToDecimal(_sale.DebitAmount) + Convert.ToDecimal(_sale.PaytmAmount);

                        INRvalue = Math.Round((decimal)(amount * convrate), MidpointRounding.AwayFromZero);
                        decimal resultd = Math.Abs(INRvalue % 10);
                        if (resultd > 0)
                        {
                            finalINR = INRvalue + (10 - resultd);
                        }
                        else
                        {
                            finalINR = INRvalue;
                        }

                        finalamountinr = oldamountinr - (currencyamountinr + finalINR);
                        if(finalamountinr<0)
                        {
                            finalamountinr = finalINR;

                        }
                        amounthd = finalINR;

                    }
                    else
                    {
                        amounthd =Convert.ToDecimal( _sale.PayLaterAmount) + Convert.ToDecimal(_sale.CashAmount) + Convert.ToDecimal(_sale.CreditAmount) + Convert.ToDecimal(_sale.DebitAmount) + Convert.ToDecimal(_sale.PaytmAmount);

                        finalamountinr = oldamountinr - (currencyamountinr + amounthd);
                        amount = (Convert.ToDecimal(_sale.PayLaterAmount) + Convert.ToDecimal(_sale.CashAmount) + Convert.ToDecimal(_sale.CreditAmount) + Convert.ToDecimal(_sale.DebitAmount) + Convert.ToDecimal(_sale.PaytmAmount)) / convrate;
                        //  amount=

                    }
                    if (finalamountinr >= 0)
                    {

                        if (entityorder != null && entityorder.Count > 0)
                    {
                     
                            await this._SALESDBE.OrderPayment.AddAsync(new OrderPayment()
                            {
                               
                                Amount=amount,
                                PayMode= (int)(paymethod)Enum.Parse(typeof(paymethod), _sale.paymethodvalue),
                                CardType= (int)(paymethod)Enum.Parse(typeof(paymethod), _sale.paymethodvalue) == 4?_sale.paylaterid : _sale.cardid+_sale.cardiddebit,
                                CreatedDatetime = DateTime.Now,
                                IsActive = true,
                                OrderId=_sale.orderid,
                                PayDate=DateTime.Now,
                                CurrencyType=_sale.currencyid,
                                AmoutHd = amounthd,
                                CreatedBy=userid

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
                    else
                    { innerresult = false; }

                }
            }
            catch (Exception ex)
            {

                throw;
            }
            return innerresult;
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
                        entity.UpdatedBy = userid;
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
        public async Task<List<StockDetailVM>> GetStock(string stockid)
        {
            List<StockDetailVM> _list;
            try
            {
                _list = await (from c in this._ERPDB.CarpetNumber
                               join vf in this._ERPDB.V_FinishedItemDetail
                               on c.item_finished_id equals vf.ITEM_FINISHED_ID
                               where c.TStockNo == stockid && c.Pack == 0
                               select new StockDetailVM
                               {
                                   stockid = c.TStockNo,
                                   category = vf.CATEGORY_NAME,
                                   itemname = vf.ITEM_NAME,
                                   marblecolor = vf.MARBLECOLOR,
                                   price = c.Price,
                                   size = vf.SizeInch + "x" + Convert.ToString(vf.HeightInch),
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
        public async Task<string> GetTeleCode(int countryid)
        {
            string resultcode = string.Empty;
            try
            {
               var CTelecode= await this._SALESDBE.CountriesMaster.Where(a=>a.Id==countryid).FirstOrDefaultAsync();
                resultcode = CTelecode.Code;
            }
            catch (Exception ex)
            {

                throw ex;
            }
            //StockDetailVM _sdetails = new StockDetailVM();

            return resultcode;
        }

        public async Task<Int64> AddCustomSale([Bind("orderid")] NormalSaleVM _sale, int userid)
        {
            bool result = false, innerresult = false;
            long uid = 0;
            decimal finalINR = 0, INRvalue = 0;
            try
            {
                using (var dbusertrans = await this._SALESDBE.Database.BeginTransactionAsync())
                {
                    var entityorder = await this._SALESDBE.OrderMaster.FirstOrDefaultAsync(i => i.Id == _sale.orderid);
                    INRvalue = Math.Round((decimal)(_sale.totalvalue * _sale.conversionrate), MidpointRounding.AwayFromZero);
                    decimal resultd = Math.Abs(INRvalue % 10);
                    if (resultd > 0)
                    {
                        finalINR = INRvalue + (10 - resultd);
                    }
                    else
                    {
                        finalINR = INRvalue;
                    }
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
                            Description = "CustomSale",
                            TransactionId = Common.GetUnique(),
                            CreatedDatetime = DateTime.Now,
                            IsActive = true,
                            //  salestatus = 1


                        });

                        result = await this._SALESDBE.SaveChangesAsync() > 0;
                        uid = await this._SALESDBE.OrderMaster.MaxAsync(p => p.Id);
                    }
                    if (uid > 0)
                    {

                        await this._SALESDBE.OrderItemDetails.AddAsync(new OrderItemDetails()
                        {
                            OrderId = uid,
                            ItemDesc = _sale.categoryid + "/" + _sale.size + "/" + _sale.shapeid + "/" + _sale.marblecolor,
                            StockId = _sale.stockno,
                            //OrderType = _sale.saletypevalue == "OF" ? (int?)SaleType.OF : (int?)SaleType.CM,
                            OrderType = 2,
                            // OrderTypePrefix = _sale.saletypevalue ,
                            OrderTypePrefix = "OF",
                            ItemType = 1,
                            Price = _sale.totalvalue,
                            PriceInr = finalINR,
                            ConversionRate = _sale.conversionrate,
                            Size = _sale.size,
                            Category = _sale.categoryid,
                            Shape = _sale.shapeid,
                            Color = _sale.marblecolor,
                            Unit = 1,
                            CreatedDatetime = DateTime.Now,
                            IsActive = true,
                            CurrencyType = _sale.currencyid,
                            SaleType = 3,
                            CreatedBy = userid




                        }) ;
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

        public async Task<NormalSaleVM> Invoice(long orderid)
        {

            NormalSaleVM _cashsaledetails = new NormalSaleVM();
        
        CommonRepository _comm = new CommonRepository(_SALESDBE);
        _cashsaledetails.cashsaledetails = await(from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                                      join od in this._SALESDBE.OrderItemDetails.Where(c => c.IsActive == true)
                                                     on m.Id equals od.OrderId
                                                      join c in this._SALESDBE.CurrencyMaster.Where(c => c.IsActive == true)
                                                       on od.CurrencyType equals c.Id into currdetails
                                                      from curr in currdetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                                                      join u in this._SALESDBE.UserLogin.Where(c => c.IsActive == true)
                                                    on od.CreatedBy equals u.Id into userdetails
                                                 from user in userdetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                                                 where m.Id == orderid && od.ItemType == 1 
                                                 select new Ncashsaledetails
                                                      {
                                                     
                                                          itemorderid = od.Id,
                                                          stockid = od.StockId,
                                                          orderid=(long)od.OrderId,
                                                          //  itemdesc = od.category.Concat(",").Concat(st.itemname).Concat(",").Concat(st.marble).Concat(",").Concat(st.size).Concat(",").Concat(st.marblestone).ToString(),
                                                          itemdesc = od.ItemDesc,
                                                          ordertype = od.OrderTypePrefix,
                                                          salevalue = od.Price,
                                                          salevalueinr =od.PriceInr,
                                                          conversionrate = od.ConversionRate,
                                                          symbol = curr.Symbol,
                                                          currencyid=(int) od.CurrencyType,
                                                          currency= curr.Type,
                                                          unitid=(int)od.Unit,
                                                          username=user.Name,
                                                          InvoiceID =m.Id


                                                      }).ToListAsync();

            _cashsaledetails.cashsaledetailsCM = _cashsaledetails.cashsaledetails.Where(a => a.ordertype == "CM").ToList();
            _cashsaledetails.cashsaledetailsOF = _cashsaledetails.cashsaledetails.Where(a => a.ordertype == "OF").ToList();
            //_cashsaledetails.cashsaledetailsOF = await (from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
            //                                          join od in this._SALESDBE.OrderItemDetails.Where(c => c.IsActive == true)
            //                                         on m.Id equals od.OrderId
            //                                          join c in this._SALESDBE.CurrencyMaster.Where(c => c.IsActive == true)
            //                                           on od.CurrencyType equals c.Id into currdetails
            //                                          from curr in currdetails.Where(c => c.IsActive == true).DefaultIfEmpty()
            //                                          join u in this._SALESDBE.UserLogin.Where(c => c.IsActive == true)
            //                                        on od.CreatedBy equals u.Id into userdetails
            //                                          from user in userdetails.Where(c => c.IsActive == true).DefaultIfEmpty()
            //                                          where m.Id == orderid && od.ItemType == 1 && od.OrderType!=1 
            //                                          select new Ncashsaledetails
            //                                          {

            //                                              itemorderid = od.Id,
            //                                              stockid = od.StockId,
            //                                              orderid = (long)od.OrderId,
            //                                              //  itemdesc = od.category.Concat(",").Concat(st.itemname).Concat(",").Concat(st.marble).Concat(",").Concat(st.size).Concat(",").Concat(st.marblestone).ToString(),
            //                                              itemdesc = od.ItemDesc,
            //                                              ordertype = od.OrderTypePrefix,
            //                                              salevalue = od.Price,
            //                                              salevalueinr = od.PriceInr,
            //                                              conversionrate = od.ConversionRate,
            //                                              symbol = curr.Symbol,
            //                                              currencyid = (int)od.CurrencyType,
            //                                              currency = curr.Type,
            //                                              unitid = (int)od.Unit,
            //                                              username = user.Name


            //                                          }).ToListAsync();

            //_cashsaledetails.cashsaledetailsOF = await (from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
            //                                            join od in this._SALESDBE.OrderItemDetails.Where(c => c.IsActive == true)
            //                                           on m.Id equals od.OrderId
            //                                            join c in this._SALESDBE.CurrencyMaster.Where(c => c.IsActive == true)
            //                                             on od.CurrencyType equals c.Id into currdetails
            //                                            from curr in currdetails.Where(c => c.IsActive == true).DefaultIfEmpty()
            //                                            join u in this._SALESDBE.UserLogin.Where(c => c.IsActive == true)
            //                                          on od.CreatedBy equals u.Id into userdetails
            //                                            from user in userdetails.Where(c => c.IsActive == true).DefaultIfEmpty()
            //                                            where m.Id == orderid && od.ItemType == 1 && od.OrderType != 1
            //                                            select new Ncashsaledetails
            //                                            {

            //                                                itemorderid = od.Id,
            //                                                stockid = od.StockId,
            //                                                orderid = (long)od.OrderId,
            //                                                //  itemdesc = od.category.Concat(",").Concat(st.itemname).Concat(",").Concat(st.marble).Concat(",").Concat(st.size).Concat(",").Concat(st.marblestone).ToString(),
            //                                                itemdesc = od.ItemDesc,
            //                                                ordertype = od.OrderTypePrefix,
            //                                                salevalue = od.Price,
            //                                                salevalueinr = od.PriceInr,
            //                                                conversionrate = od.ConversionRate,
            //                                                symbol = curr.Symbol,
            //                                                currencyid = (int)od.CurrencyType,
            //                                                currency = curr.Type,
            //                                                unitid = (int)od.Unit,
            //                                                username = user.Name


            //                                            }).ToListAsync();
            _cashsaledetails.standsaledetails = await (from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                                       join od in this._SALESDBE.OrderItemDetails.Where(c => c.IsActive == true)
                                                      on m.Id equals od.OrderId

                                                       where m.Id == orderid && od.ItemType == 2
                                                       select new Nstanddetails
                                                       {
                                                           itemorderid = od.Id,
                                                           color = od.Color,
                                                           standdesc = od.ItemDesc,
                                                           ordertype = od.OrderTypePrefix,
                                                           height = od.height

                                                       }).ToListAsync();

          //  _cashsaledetails.standsaledetailsCM= _cashsaledetails.standsaledetails

                _cashsaledetails.standsaledetailsCM = _cashsaledetails.standsaledetails.Where(a => a.ordertype == "CM").ToList();
            _cashsaledetails.standsaledetailsOF = _cashsaledetails.standsaledetails.Where(a => a.ordertype == "OF").ToList();

            _cashsaledetails.cinfo = await (from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                            join od in this._SALESDBE.OrderItemDetails.Where(c => c.IsActive == true)
                                           on m.Id equals od.OrderId
                                            join c in this._SALESDBE.CustomerDetails.Where(c => c.Isactive == true)
                                             on od.OrderId equals c.OrderId
                                            join cn in this._SALESDBE.CountriesMaster
                                            on c.countryid equals cn.Id into countrydetails
                                            from country in countrydetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                                            where m.Id == orderid
                                            select new Ncustomerinfo
                                            {
                                                Name = c.Name,
                                                countryid = c.countryid,
                                                Country = country.Name,
                                                Title = c.Title,
                                                City = c.City,
                                                Zipcode = c.Zipcode,
                                                State = c.State,
                                                TeleCountryCode = c.TeleCountryCode,
                                                Telephone = c.Telephone,
                                                MobCountryCode = c.MobCountryCode,
                                                Mobile = c.Mobile,
                                                Address = c.Address,
                                                Email = c.Email,
                                                PassportNo=m.PassportNo,
                                                nationality=country.Name

                                            }).FirstOrDefaultAsync();

            _cashsaledetails.paymentdetails = await (from m in this._SALESDBE.OrderMaster.Where(c => c.IsActive == true)
                                                     join od in this._SALESDBE.OrderPayment.Where(c => c.IsActive == true)
                                                   on m.Id equals od.OrderId
                                                     join d in this._SALESDBE.CurrencyMaster
                                                     on od.CurrencyType equals d.Id into currencydetails
                                                     from curr in currencydetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                                                     join e in this._SALESDBE.PayMethodMaster
                                                     on od.PayMode equals e.Id into paymethoddetails
                                                     from pm in paymethoddetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                                                     join f in this._SALESDBE.CardTypeMaster
                                                   on od.CardType equals f.Id into cardtypedetails
                                                     from card in cardtypedetails.Where(c => c.IsActive == true).DefaultIfEmpty()

                                                     where m.Id == orderid
                                                     select new Npaydetails
                                                     {
                                                         //itemorderid = od.Id,
                                                         payid = od.Id,
                                                         paymode = pm.PayName,
                                                         payamount = od.Amount,
                                                         payamountinr = od.AmoutHd,
                                                         paytype = (string.IsNullOrEmpty(card.CardName) ? curr.Type : card.CardName),
                                                         symbol = curr.Symbol != null ? curr.Symbol : "$",
                                                         currencyid = curr.Id


                                                     }).ToListAsync();

           // _cashsaledetails.grandtotal = _cashsaledetails.cashsaledetails.Sum(s => s.salevalueinr);
            _cashsaledetails.grandtotalinrCM = _cashsaledetails.cashsaledetailsCM.Sum(s => s.salevalueinr);
            _cashsaledetails.grandtotalinrOF = _cashsaledetails.cashsaledetailsOF.Sum(s => s.salevalueinr);

            // _cashsaledetails.grandtotalcurrency = _cashsaledetails.cashsaledetails.Sum(s => s.salevalue);

            if (_cashsaledetails.paymentdetails.Count > 0)
            {
               // _cashsaledetails.balcurrency = _cashsaledetails.grandtotalcurrency - ((decimal)_cashsaledetails.paymentdetails.Sum(p => p.payamount));
                _cashsaledetails.balinr = _cashsaledetails.grandtotalinr - Math.Round(((decimal)_cashsaledetails.paymentdetails.Sum(p => p.payamountinr)));
                _cashsaledetails.PaytmAmount = (decimal)_cashsaledetails.paymentdetails.Sum(s => s.payamountinr);
                ////if (_cashsaledetails.paymentdetails[0].currencyid != 6)
                ////{
                ////    _cashsaledetails.currsymbol = _cashsaledetails.paymentdetails[0].symbol;
                ////}
                ////else
                ////{ _cashsaledetails.currsymbol = "$"; }

            }
            else
            {
              //  _cashsaledetails.balcurrency = _cashsaledetails.grandtotalcurrency;
                _cashsaledetails.balinr = _cashsaledetails.grandtotalinr;
              //  _cashsaledetails.currsymbol = "$";
            }
            if (_cashsaledetails.balcurrency < 0 || _cashsaledetails.balinr < 0)
            {
               // _cashsaledetails.balcurrency = 0;
                _cashsaledetails.balinr = 0;

            }


            return _cashsaledetails;
        }

        public async Task<string> IsExist(string stockno)
        {

            string result = string.Empty;
                bool innerresult = false;
            try
            {


                var entity = await _SALESDBE.CarpetNumber.FirstOrDefaultAsync(item => item.TStockNo == stockno).ConfigureAwait(false);

                if (entity != null)
                {

                    result = entity.TStockNo;
                   

                }
                

                return result;
            }
            catch (Exception)
            {

                throw;
            }

        }

    }
    
}
