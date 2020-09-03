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
            string q = HttpContext.Request.Query["pgno"].ToString();
            ViewBag.pgno = q;

            return View("Index", cashdetails);
        }
        [HttpPost]
        public async Task<IActionResult> AddCustomSale(CustomSaleVM saleetails, IFormFile file)
        {
            Int64 orderid = 0;
            try
            {
                
                long size = 0;
                var files = Request.Form.Files;
                if (files.Count > 0)
                {
                    var filepathtoreturn = string.Empty;
                    string folderpath = _hostingenv.WebRootPath + $@"\uploadedcustomorder\" + saleetails.orderid;
                    string filename = filepathtoreturn = _hostingenv.WebRootPath + $@"\uploadedcustomorder\" + saleetails.orderid + $@"\{files[0].FileName}";
                    bool folderExists = Directory.Exists(folderpath);
                    if (!folderExists)
                        Directory.CreateDirectory(folderpath);
                    size += files[0].Length;

                    using (System.IO.FileStream fs = System.IO.File.Create(filename))
                    {
                        files[0].CopyTo(fs);
                        fs.Flush();
                    }
                }
                orderid=    await _cussale.AddCashSale(saleetails, 1);
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Error:Add Sale Item");
              
                saleetails = await _cussale.Init();
            }
            // CustomSaleVM customsaledetails = new CustomSaleVM();
            ModelState.Clear();
            saleetails = await _cussale.GetSales(orderid, saleetails);
            ViewBag.pgno = 1;
            return View("Index", saleetails);

        }
        public async Task<IActionResult> DeleteSale(int itemorderid)
        {
            Int64? orderid = 0;
            try
            {
                orderid = await _cussale.DeleteCashSale(itemorderid, 1);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Error:Delete Sale Item");
            }
            ModelState.Clear();
            CustomSaleVM cashdetails = new CustomSaleVM();
            cashdetails = await _cussale.GetSales((Int64)orderid, null);
            ViewBag.pgno = 1;
            return View("Index", cashdetails);
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
            customsaledetails = await _cussale.GetSales(2,customsaledetails);
            return View("Index", customsaledetails);

        }

        public async Task<IActionResult> AddCustomerInfo(CustomSaleVM saledetails)
        {
            bool result = true;
            try
            {


                if (saledetails.cinfo != null)
                {
                result=    await _cussale.AddCustomerinfo(saledetails, 1);
                    if(result)
                    {
                        TempData["CustomerUserMessage"] = new MessageVM() { title = "Success!", msg = "Customer details has been updated succesfully!!!" };

                    }

                }
            }
            catch (Exception)
            {
                TempData["CustomerUserMessage"] = new MessageVM() { title = "Error!", msg = "Operation Failed." };
                
            }
            ModelState.Clear();
              CustomSaleVM cashdetails = new CustomSaleVM();
            cashdetails = await _cussale.GetSales(saledetails.orderid, saledetails);
            ViewBag.pgno = 2;

            return View("Index", cashdetails);

        }

        public async Task<IActionResult> AddStandInfo(CustomSaleVM standdetails)
        {
            bool result = true;
            try
            {


                if (standdetails.orderid>=0)
                {
                  result=  await _cussale.AddStandSale(standdetails, 1);
                    if (result)
                    {
                        TempData["UserMessage"] = new MessageVM() { title = "Success!", msg = "Operation Done." };

                    }
                    else
                    {
                        TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "First,Add Order Item,before addding stand." };
                       // return View("Index", standdetails);

                    }

                }
                else
                {
                   // ModelState.IsValid;
                    TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "First,Add Order Item,before addding stand." };
                   // return View("Index", standdetails);

                }
            }
            catch (Exception)
            {
                TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "Operation Failed." };
                return View("Index", standdetails);

            }
            ModelState.Clear();
            CustomSaleVM cashdetails = new CustomSaleVM();
            cashdetails = await _cussale.GetSales(standdetails.orderid, standdetails);
            ViewBag.pgno = 3;

            return View("Index", cashdetails);

        }

        public async Task<IActionResult> AddDeliveryInfo(CustomSaleVM standdetails)
        {
            bool result = true;
            try
            {


                if (standdetails.orderid >= 0)
                {
                    result = await _cussale.AddDeliveryDetails(standdetails, 1);
                    if (result)
                    {
                        TempData["UserMessage"] = new MessageVM() { title = "Success!", msg = "Operation Done." };

                    }
                    else
                    {
                        TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "First,Add Order Item,before addding stand." };
                        // return View("Index", standdetails);

                    }

                }
                else
                {
                    // ModelState.IsValid;
                    TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "First,Add Order Item,before addding stand." };
                    // return View("Index", standdetails);

                }
            }
            catch (Exception)
            {
                TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "Operation Failed." };
                return View("Index", standdetails);

            }
            ModelState.Clear();
            CustomSaleVM cashdetails = new CustomSaleVM();
            cashdetails = await _cussale.GetSales(standdetails.orderid, standdetails);
            ViewBag.pgno = 3;

            return View("Index", cashdetails);

        }

        public async Task<IActionResult> DeleteStand(int standid)
        {
            Int64? orderid = 0;
            try
            {
                orderid = await _cussale.DeleteCashSale(standid, 1);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Error:Delete Sale Item");
            }
            ModelState.Clear();
            CustomSaleVM cashdetails = new CustomSaleVM();
            cashdetails = await _cussale.GetSales((Int64)orderid, null);
            ViewBag.pgno = 3;
            return View("Index", cashdetails);
        }

        public async Task<IActionResult> AddOrderPayment(CustomSaleVM paydetails)
        {
            bool result = true;
            try
            {


                if (paydetails.orderid >= 0)
                {
                    result = await _cussale.AddSalePayment(paydetails, 1);
                    if (result)
                    {
                        TempData["UserMessage"] = new MessageVM() { title = "Success!", msg = "Payment updated successfully." };

                    }
                    else
                    {
                        TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "First,Add Order Item,before payment." };
                        // return View("Index", standdetails);

                    }

                }
                else
                {
                    // ModelState.IsValid;
                    TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "First,Add Order Item,before payment." };
                    // return View("Index", standdetails);

                }
            }
            catch (Exception)
            {
                TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "Operation Failed." };
                return View("Index", paydetails);

            }
            ModelState.Clear();
            CustomSaleVM cashdetails = new CustomSaleVM();
            cashdetails = await _cussale.GetSales(paydetails.orderid, paydetails);
            ViewBag.pgno = 4;

            return View("Index", cashdetails);

        }
        public async Task<IActionResult> DeletePayment(int payid)
        {
            Int64? orderid = 0;
            try
            {
                orderid = await _cussale.DeletePayment(payid, 1);
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Error:Delete Sale Item");
            }
            ModelState.Clear();
            CustomSaleVM cashdetails = new CustomSaleVM();
            cashdetails = await _cussale.GetSales((Int64)orderid, null);
            ViewBag.pgno = 4;
            return View("Index", cashdetails);
        }

    }
}
