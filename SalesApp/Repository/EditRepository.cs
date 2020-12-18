using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using SALEERP.Data;
using SALEERP.Models;
using SalesApp.Repository.Interface;
using SalesApp.ViewModel;

namespace SalesApp.Repository
{
    public class EditRepository : IEditRepository
    {
        private Sales_ERPContext _DBERP;
      
        private readonly ICommonRepository _comm;
        public EditRepository(Sales_ERPContext dbcontext, ICommonRepository commonRepository)
        {

            this._DBERP = dbcontext;
            this._comm = commonRepository;

        }
        public EditVM getAllOrders()
        {
            int userid = _comm.GetLoggedInUserId();
            EditVM _pdetails = new EditVM();

            _pdetails.Orders = (from master in this._DBERP.OrderMaster
                                join m in this._DBERP.OrderItemDetails
                                on master.Id equals m.OrderId
                                join c in this._DBERP.CustomerDetails
                                on m.OrderId equals c.OrderId into orderdetails
                                from order in orderdetails.Where(c => c.Isactive == true).DefaultIfEmpty()
                                join car in this._DBERP.CarpetNumber
                                on m.StockId equals car.TStockNo into carpet
                                from sc in carpet.DefaultIfEmpty()
                                join view in this._DBERP.V_FinishedItemDetail
                                on sc.item_finished_id equals view.ITEM_FINISHED_ID into view
                                from finish in view.DefaultIfEmpty()

                                where m.IsActive == true && m.CreatedBy == userid && m.ItemType == 1 && m.BillId>0
                                select new Edititemprintdeatils
                                {
                                    orderid = m.OrderId,
                                    customername = order.Name,
                                    stockdesc = finish.CATEGORY_NAME,
                                    stockvalue = m.PriceInr,
                                    unit = m.Unit,
                                    prefix = m.OrderTypePrefix,
                                    billdesc = m.OrderTypePrefix + "/" + m.Unit + "/" + m.BillId,
                                    billid = m.BillId

                                }).ToList().GroupBy(m => m.orderid)
    .Select(g => new Edititemprintdeatils
    {
        orderid = g.Key,
        customername = g.FirstOrDefault().customername,
        stockvalue = g.Sum(a => a.stockvalue),
        desc = g.Select(a => a.stockdesc).ToList(),
        bills = g.Select(a => a.billdesc).Distinct().ToList(),
        unit = g.FirstOrDefault().unit,
        // p= g.Where(c => c.agentcode == "pi").SelectMany(a=>a.name).SingleOrDefault().ToString()
    }).ToList();

            return _pdetails;
        }
        public EditVM getAllTempOrders()
        {
            int userid = _comm.GetLoggedInUserId();
            EditVM _pdetails = new EditVM();

            _pdetails.Orders = (from master in this._DBERP.OrderMaster
                                join m in this._DBERP.OrderItemDetails
                                on master.Id equals m.OrderId
                                join c in this._DBERP.CustomerDetails
                                on m.OrderId equals c.OrderId into orderdetails
                                from order in orderdetails.Where(c => c.Isactive == true).DefaultIfEmpty()
                                join car in this._DBERP.CarpetNumber
                                on m.StockId equals car.TStockNo into carpet
                                from sc in carpet.DefaultIfEmpty()
                                join view in this._DBERP.V_FinishedItemDetail
                                on sc.item_finished_id equals view.ITEM_FINISHED_ID into view
                                from finish in view.DefaultIfEmpty()

                                where m.IsActive == true && m.CreatedBy == userid && m.ItemType == 1 && m.BillId==0
                                select new Edititemprintdeatils
                                {
                                    orderid = m.OrderId,
                                    customername = order.Name,
                                    stockdesc = finish.CATEGORY_NAME,
                                    stockvalue = m.PriceInr,
                                    unit = m.Unit,
                                    prefix = m.OrderTypePrefix,
                                    billdesc = m.OrderTypePrefix + "/" + m.Unit + "/" + m.BillId,
                                    billid = m.BillId,
                                    itemorderid=m.Id

                                }).ToList().GroupBy(m => m.orderid)
    .Select(g => new Edititemprintdeatils
    {
        orderid = g.Key,
        customername = g.FirstOrDefault().customername,
        stockvalue = g.Sum(a => a.stockvalue),
        desc = g.Select(a => a.stockdesc).ToList(),
        bills = g.Select(a => a.billdesc).Distinct().ToList(),
        unit = g.FirstOrDefault().unit,
        itemorderid= g.FirstOrDefault().itemorderid
        // p= g.Where(c => c.agentcode == "pi").SelectMany(a=>a.name).SingleOrDefault().ToString()
    }).ToList();

            return _pdetails;
        }

        public async Task<long?> Delete(long orderid, int userid)
        {
            bool result = false, innerresult = false;
            Int64? _orderid = 0;
            try
            {


                using (var dbusertrans = await this._DBERP.Database.BeginTransactionAsync().ConfigureAwait(false))
                {


                    var entity = await _DBERP.OrderItemDetails.FirstOrDefaultAsync(item => item.Id == orderid).ConfigureAwait(false);

                    if (entity != null)
                    {

                        entity.CreatedDatetime = DateTime.Now;
                        entity.IsActive = false;
                        entity.UpdatedBy = userid;
                        this._DBERP.OrderItemDetails.Update(entity);
                        innerresult = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;

                        if (entity.StockId != null || entity.StockId != "")
                        {
                            var entitycarpet = await _DBERP.CarpetNumber.FirstOrDefaultAsync(c => c.TStockNo == entity.StockId).ConfigureAwait(false);
                            if (entitycarpet != null && entitycarpet.StockNo > 0)
                            {
                                entitycarpet.PackDate = null;
                                entitycarpet.Pack = 0;
                                entitycarpet.PackSource = "";
                                entitycarpet.PackingDetailId = 0;
                                //   entitycarpet.PackingId = (Int32)item.OrderId;
                                this._DBERP.CarpetNumber.Update(entitycarpet);
                                innerresult = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                            }
                        }
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
