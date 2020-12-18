using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEERP.Data;
using SALEERP.Models;
using SalesApp.Repository.Interface;
using SalesApp.ViewModel;

namespace SalesApp.Repository
{
    public class PrintRepository : IPrintRepository
    {
        private Sales_ERPContext _DBERP;
        private readonly ICommonRepository _comm;
        public PrintRepository(Sales_ERPContext dbcontext, ICommonRepository commonRepository)
        {

            this._DBERP = dbcontext;
            this._comm = commonRepository;

        }
        public PrintVM getAllOrders()
        {
            int userid = _comm.GetLoggedInUserId();
            PrintVM _pdetails = new PrintVM();

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

                                where m.IsActive == true && m.CreatedBy == userid && m.ItemType==1 
                                select new itemprintdeatils
                                {
                                    orderid = m.OrderId,
                                    customername = order.Name,
                                    stockdesc = finish.CATEGORY_NAME,
                                    stockvalue = m.PriceInr,
                                    unit = m.Unit,
                                    prefix = m.OrderTypePrefix,
                                    billdesc= m.OrderTypePrefix+"/"+m.Unit+"/"+m.BillId,
                                    billid=m.BillId

                                }).ToList().GroupBy(m => m.orderid)
    .Select(g => new itemprintdeatils
    {
        orderid = g.Key,
        customername = g.FirstOrDefault().customername,
        stockvalue=g.Sum(a=>a.stockvalue),
        desc = g.Select(a=>a.stockdesc).ToList(),
        bills=g.Select(a=>a.billdesc).Distinct().ToList(),
        unit= g.FirstOrDefault().unit,
        // p= g.Where(c => c.agentcode == "pi").SelectMany(a=>a.name).SingleOrDefault().ToString()
    }).ToList();

            return _pdetails;
        }
    }
}
