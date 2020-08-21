using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalesApp.Repository;
using SalesApp.Repository.Interface;
using SalesApp.ViewModel;

namespace SalesApp.Controllers
{
    public class CustomSaleController : Controller
    {
        ICustomSaleRepository _cussale;
        IWebHostEnvironment _hostingenv;
        public CustomSaleController(ICustomSaleRepository _customsale, IWebHostEnvironment _env)
        {
            this._cussale = _customsale;
            this._hostingenv = _env;

        }
        public async Task<IActionResult> Index()
        {
            CustomSaleVM cashdetails = new CustomSaleVM();
            cashdetails = await _cussale.Init();
            return View("Index", cashdetails);
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomSale(CustomSaleVM saleetails, IFormFile file)
        {
            try
            {
                long size = 0;
                var files = Request.Form.Files;
                var filepathtoreturn = string.Empty;
                string filename = filepathtoreturn = _hostingenv.WebRootPath + $@"\uploadedcustomorder\{files[0].FileName}";
                size += files[0].Length;

                using (System.IO.FileStream fs = System.IO.File.Create(filename))
                {
                    files[0].CopyTo(fs);
                    fs.Flush();
                }
                await _cussale.AddCashSale(saleetails, 1);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Error:Add Sale Item");
            }
            CustomSaleVM customsaledetails = new CustomSaleVM();
            customsaledetails = await _cussale.GetSales(2);
            return View("Index", customsaledetails);

        }
        public async Task<IActionResult> DeleteSale(int orderid)
        {
            try
            {
                await _cussale.DeleteCashSale(orderid, 1);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Error:Delete Sale Item");
            }
            CustomSaleVM customsaledetails = new CustomSaleVM();
            customsaledetails = await _cussale.GetSales(2);
            return View("Index", customsaledetails);

        }

        public async Task<IActionResult> FinishSale(int mirrorid)
        {
            try
            {
                await _cussale.FinishCashSale(mirrorid, 1);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Error:Finished Sale Item");
            }
            CustomSaleVM customsaledetails = new CustomSaleVM();
            customsaledetails = await _cussale.GetSales(2);
            return View("Index", customsaledetails);

        }

        public async Task<IActionResult> AddCustomerInfo(CustomSaleVM saledetails)
        {
            if(saledetails.mirrorid==0)
            {
                saledetails = await _cussale.Init();
            
            }
           // CustomSaleVM saledetail = new CustomSaleVM();
            return View("~/Views/Shared/CustomerDetails.cshtml", saledetails);

        }

    }
}
