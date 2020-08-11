using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesApp.Repository;
using SalesApp.Repository.Interface;
using SalesApp.ViewModel;

namespace SalesApp.Controllers
{
    public class CashSaleController : Controller
    {
        ICashSaleRepository _csale;
        public CashSaleController(ICashSaleRepository _cashsale)
        {
            this._csale = _cashsale;
           
        }
        public async  Task<IActionResult> Index(int id)
        {
            CashSaleVM cashdetails = new CashSaleVM();
            cashdetails = await _csale.Init();
            return View("Index",cashdetails);
        }
        public async Task<List<StockDetailVM>> GetStockDetails(string stockid)
        {
            List<StockDetailVM> _stock = new List<StockDetailVM>();
            try
            {
                _stock = await _csale.GetStock(stockid);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Stocknotfound");
            }
          
          
            return _stock;

        }
        public async Task<IActionResult> AddSale(CashSaleVM saleetails)
        {
            try
            {
                 await _csale.AddCashSale(saleetails,1);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Error:Add Sale Item");
            }
            CashSaleVM cashdetails = new CashSaleVM();
            cashdetails = await _csale.GetSales(10009);
            return View("Index",cashdetails);
           
        }
        public async Task<IActionResult> DeleteSale(int orderid)
        {
            try
            {
                await _csale.DeleteCashSale(orderid, 1);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Error:Delete Sale Item");
            }
            CashSaleVM cashdetails = new CashSaleVM();
            cashdetails = await _csale.GetSales(10009);
            return View("Index", cashdetails);

        }
        public async Task<IActionResult> FinishSale(int mirrorid)
        {
            try
            {
                await _csale.FinishCashSale(mirrorid, 1);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Error:Finished Sale Item");
            }
            CashSaleVM cashdetails = new CashSaleVM();
            cashdetails = await _csale.GetSales(10009);
            return View("Index", cashdetails);

        }
    }
}
