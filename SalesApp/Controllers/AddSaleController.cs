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
    public class AddSaleController : Controller
    {
        ICashSaleRepository _csale;
        public AddSaleController(ICashSaleRepository _cashsale)
        {
            this._csale = _cashsale;
        }

        public async Task<IActionResult> Index(int id)
        {
            CashSaleVM cashdetails = new CashSaleVM();
            cashdetails = await _csale.Init();
            return View("Index", cashdetails);
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
    }
}
