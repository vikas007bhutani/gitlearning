using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SalesApp.Repository;
using SalesApp.Repository.Interface;
using SalesApp.ViewModel;
using Microsoft.AspNetCore.Hosting;
using Syncfusion.HtmlConverter;
using Syncfusion.Pdf;
using System.IO;
using System.Text;
using Microsoft.AspNetCore.Http;
using Aspose.BarCode.BarCodeRecognition;
using Rotativa;
using Microsoft.Extensions.Configuration;
using System.Net;
using Microsoft.Extensions.Logging;

namespace SalesApp.Controllers
{
    public class AddSaleController : Controller
    {
        private readonly INormalSaleRepository _csale;
        private readonly ICommonRepository _comm;
        private readonly ILogger _logger;

        public IConfiguration Configuration { get; }
        //  private readonly IWebHostEnvironment _honstingEnvironment;
        IWebHostEnvironment _hostingenv;
        public AddSaleController(INormalSaleRepository _cashsale, ICommonRepository commonRepository, IWebHostEnvironment _env, IConfiguration _config, ILogger<AddSaleController> logger)
        {
            this._csale = _cashsale;
            this._comm = commonRepository;
         //   _honstingEnvironment = hostingEnvironment;
            this._hostingenv = _env;
            this.Configuration = _config;
            this._logger = logger;
        }

        public async Task<IActionResult> Index(int id)
        {
            NormalSaleVM cashdetails = new NormalSaleVM();
            try
            {
                cashdetails = await _csale.Init(id);
                // string q = HttpContext.Request.Query["pgno"].ToString();
                ViewBag.pgno = 1;
                cashdetails.unitid = _comm.GetUnitId();
                cashdetails.IPaddress = _comm.GetLoggedIP();
              //  _logger.Log(LogLevel.Information, "Indexpage:", id);
            }
            catch (Exception)
            {

              //  _logger.Log(LogLevel.Information, "AddSale:Index", id);
            }
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
        public async Task<string> GetTeleCodeDetails(int countryid)
        {
            string _telecode = string.Empty;
            try
            {
                _telecode = await _csale.GetTeleCode(countryid);
                _telecode = "+" + _telecode;
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "TeleCode Not Found!!");
            }

            return _telecode;

        }
        public async Task<IActionResult> AddSale(NormalSaleVM saleetails)
        {
            NormalSaleVM cashdetails = new NormalSaleVM();
            Int64 _orderid = 0;
            int UID;
            ModelState.Clear();
            try
            {
                if (ModelState.IsValid)
                {
                    UID = _comm.GetLoggedInUserId();
                    string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                  .Select(c => c.Value).SingleOrDefault();
                    //if (UID > 0)
                    //{
                        _orderid = await _csale.AddCashSale(saleetails, UID);
                        if (_orderid == -1)
                        {

                            TempData["NormalMessage"] = new MessageVM() { title = "", msg = "This stock no. already added!!!" };
                            cashdetails = await _csale.GetSales(saleetails.orderid, saleetails);

                        }
                        else if (_orderid == -2)
                        {
                            TempData["NormalMessage"] = new MessageVM() { title = "Unauthorized:", msg = "Invalid Request!!!" };

                        }
                        else
                        {
                            cashdetails = await _csale.GetSales(_orderid, saleetails);
                        }

                        // cashdetails = await _csale.GetSales(_orderid, saleetails);

                        ViewBag.totalbalance = cashdetails.balinr;
                    //}
                    //else
                    //{
                    //    RedirectToAction("Logout", "Account");


                    //}
                    
                }
                else
                {
                    ModelState.AddModelError("Error", "Error:Please specify item or required fields.");
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Error:Add Sale Item");
            }
            ViewBag.pgno = 1;
            ViewBag.totalbalance = cashdetails.balinr;
            return View("Index", cashdetails);


        }
        public async Task<IActionResult> EditSale(Int64 orderid)
        {
            NormalSaleVM cashdetails = new NormalSaleVM();
           // Int64 _orderid = 0;
            ModelState.Clear();
            try
            {
                if (ModelState.IsValid)
                {

                  //  _orderid = await _csale.AddCashSale(saleetails, _comm.GetLoggedInUserId());
                    if (orderid >0)
                    {

                        //TempData["NormalMessage"] = new MessageVM() { title = "", msg = "This stock no. already added!!!" };
                        cashdetails = await _csale.GetSales(orderid, null);

                    }
                   
                    else
                    {
                        return RedirectToAction("Index", "Edit");
                    }

                    // cashdetails = await _csale.GetSales(_orderid, saleetails);

                    ViewBag.totalbalance = cashdetails.balinr;
                }
                else
                {
                    ModelState.AddModelError("Error", "Error:Please specify item or required fields.");
                }

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Error:Add Sale Item");
            }
            ViewBag.pgno = 1;
            ViewBag.totalbalance = cashdetails.balinr;
            return View("Index", cashdetails);


        }
        public async Task<IActionResult> DeleteSale(int itemorderid)
        {
            Int64? masterorderid = 0;
            int UID;
            try
            {
                UID = _comm.GetLoggedInUserId();
                //if (UID > 0)
                //{
                    masterorderid = await _csale.DeleteCashSale(itemorderid,UID);
                //}
                //else
                //{
                //    RedirectToAction("Logout", "Account");

                //}
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Error:Delete Sale Item");
            }
            NormalSaleVM cashdetails = new NormalSaleVM();
            cashdetails = await _csale.GetSales(masterorderid, null);
            ViewBag.pgno = 1;
            return View("Index", cashdetails);

        }
        public async Task<IActionResult> FinishSale(int orderid)
        {
            bool result;
            int UID;
            try
            {
                UID = _comm.GetLoggedInUserId();
                //if (UID > 0)
                //{
                    result = await _csale.FinishCashSale(orderid, UID);
                    if (result)
                    {
                        return RedirectToAction("Index", "Mirror");

                    }
                    else
                    {
                        TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "Please Fill Payment or Delivery Details to Continue!!." };
                        // return View("Index", standdetails);

                    }
                //}
                //else
                //{
                //    RedirectToAction("Logout", "Account");

                //}

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Error:Finished Sale Item");
            }
            NormalSaleVM cashdetails = new NormalSaleVM();
            cashdetails = await _csale.GetSales(orderid, null);
            ViewBag.pgno = 5;
            ViewBag.totalbalance = cashdetails.balinr;
            return View("Index", cashdetails);

        }

        public async Task<IActionResult> AddCustomerInfo(NormalSaleVM saledetails)
        {
            bool result = true;
            int UID;
            try
            {
                UID = _comm.GetLoggedInUserId();
                //if (UID > 0)
                //{

                    if (saledetails.cinfo != null)
                    {
                        result = await _csale.AddCustomerinfo(saledetails, UID);
                        if (result)
                        {
                            TempData["CustomerUserMessage"] = new MessageVM() { title = "Success!", msg = "Customer details has been updated succesfully!!!" };

                        }

                    }
                //}
                //else
                //{
                //    RedirectToAction("Logout", "Account");

                //}
            }
            catch (Exception)
            {
                TempData["CustomerUserMessage"] = new MessageVM() { title = "Error!", msg = "Operation Failed." };

            }
            ModelState.Clear();
            NormalSaleVM cashdetails = new NormalSaleVM();
            cashdetails = await _csale.GetSales(saledetails.orderid, saledetails);
            ViewBag.pgno = 5;
            ViewBag.totalbalance = cashdetails.balinr;
            return View("Index", cashdetails);

        }

        public async Task<IActionResult> AddStandInfo(NormalSaleVM standdetails)
        {
            bool result = true;
            int UID;
            try
            {
                UID = _comm.GetLoggedInUserId();
                //if (UID > 0)
                //{

                    if (standdetails.orderid >= 0)
                    {
                        result = await _csale.AddStandSale(standdetails, UID);
                        if (result)
                        {
                            //TempData["StandMessage"] = new MessageVM() { title = "Success!", msg = "Operation Done." };

                        }
                        else
                        {
                            TempData["StandMessage"] = new MessageVM() { title = "Error!", msg = "Please,Add Item,before addding stand." };
                            // return View("Index", standdetails);

                        }

                    }
                    else
                    {
                        // ModelState.IsValid;
                        TempData["StandMessage"] = new MessageVM() { title = "Error!", msg = "Please,Add  Item,before addding stand." };
                        // return View("Index", standdetails);

                    }
                //}
                //else
                //{
                //    RedirectToAction("Logout", "Account");

                //}
            }
            catch (Exception)
            {
                TempData["StandMessage"] = new MessageVM() { title = "Error!", msg = "Operation Failed." };
                return View("Index", standdetails);

            }
            ModelState.Clear();
            NormalSaleVM cashdetails = new NormalSaleVM();
            cashdetails = await _csale.GetSales(standdetails.orderid, standdetails);
            ViewBag.pgno = 2;
            //if(!string.IsNullOrEmpty(cashdetails.standsaletype))
            //{
            //    if(cashdetails.standsaletype.ToUpper()=="OF")
            //    {

            //        ViewBag.section = 2;
            //    }
            //    else { ViewBag.section = 1; }


            //}
            //else { ViewBag.section = 1; }
        
            ViewBag.totalbalance = cashdetails.balinr;

            return View("Index", cashdetails);

        }

        public async Task<IActionResult> AddDeliveryInfo(NormalSaleVM standdetails)
        {
            bool result = true;
            int UID;
            try
            {
                UID = _comm.GetLoggedInUserId();
                //if (UID > 0)
                //{

                    if (standdetails.orderid >= 0)
                    {
                        result = await _csale.AddDeliveryDetails(standdetails, UID);
                        if (result)
                        {
                            //TempData["DeliveryMessage"] = new MessageVM() { title = "Success!", msg = "Operation Done." };

                        }
                        else
                        {
                            TempData["DeliveryMessage"] = new MessageVM() { title = "Error!", msg = "Delivery details are empty." };
                            // return View("Index", standdetails);

                        }

                    }
                    else
                    {
                        // ModelState.IsValid;
                        TempData["DeliveryMessage"] = new MessageVM() { title = "Error!", msg = "First,Add Order Item,before addding stand." };
                        // return View("Index", standdetails);

                    }
                //}
                //else
                //{
                //    RedirectToAction("Logout", "Account");

                //}
            }
            catch (Exception)
            {
                TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "Operation Failed." };
                return View("Index", standdetails);

            }
            ModelState.Clear();
            NormalSaleVM cashdetails = new NormalSaleVM();
            cashdetails = await _csale.GetSales(standdetails.orderid, standdetails);
            ViewBag.pgno = 3;
        //    ViewBag.section = 2;
            ViewBag.totalbalance = cashdetails.balinr;

            return View("Index", cashdetails);

        }

        public async Task<IActionResult> DeleteStand(int itemorderid)
        {
            Int64? orderid = 0;
            int UID;
            try
            {
                UID = _comm.GetLoggedInUserId();
                //if (UID > 0)
                //{
                    orderid = await _csale.DeleteCashSale(itemorderid, UID);
                //}
                //else
                //{
                //    RedirectToAction("Logout", "Account");

                //}

            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Error:Delete Sale Item");
            }
            ModelState.Clear();
            NormalSaleVM cashdetails = new NormalSaleVM();
            cashdetails = await _csale.GetSales((Int64)orderid, null);
            ViewBag.pgno = 2;
            ViewBag.section = 1;
            ViewBag.totalbalance = cashdetails.balinr;
            return View("Index", cashdetails);
        }

        public async Task<IActionResult> AddOrderPayment(NormalSaleVM paydetails)
        {
            bool result = true;
            string message = string.Empty;
            int UID;
            try
            {
                
                UID = _comm.GetLoggedInUserId();
                //if (UID > 0)
                //{
                    if (paydetails.orderid >= 0)
                    {
                        if (validatepayment(paydetails, out message))
                        {

                            result = await _csale.AddSalePayment(paydetails, UID);
                            if (result)
                            {
                                TempData["PayMessage"] = new MessageVM() { title = "Success!", msg = "Payment updated successfully." };

                            }
                            else
                            {
                                TempData["PayMessage"] = new MessageVM() { title = "Error!", msg = "Payment Amount should be equal to Balance Amount." };
                                // return View("Index", standdetails);

                            }
                        }
                        else
                        { TempData["PayMessage"] = new MessageVM() { title = "Error!", msg = message }; }

                    }
                    else
                    {
                        // ModelState.IsValid;
                        TempData["PayMessage"] = new MessageVM() { title = "Error!", msg = "First,Add Order Item,before payment." };
                        // return View("Index", standdetails);

                    }
                //}
                //else
                //{
                //    RedirectToAction("Logout", "Account");

                //}
            }
            catch (Exception)
            {
                TempData["PayMessage"] = new MessageVM() { title = "Error!", msg = "Operation Failed." };
                return View("Index", paydetails);

            }
            ModelState.Clear();
            NormalSaleVM cashdetails = new NormalSaleVM();
            cashdetails = await _csale.GetSales(paydetails.orderid, paydetails);
            ViewBag.pgno = 4;
            ViewBag.totalbalance = cashdetails.balinr;

            return View("Index", cashdetails);

        }
        public async Task<IActionResult> DeletePayment(int payid)
        {
            Int64? orderid = 0;
            int UID;
            try
            {
                UID = _comm.GetLoggedInUserId();
                //if (UID > 0)
                //{
                    orderid = await _csale.DeletePayment(payid, UID);
                //}
                //else
                //{
                //    RedirectToAction("Logout", "Account");

                //}
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Error:Delete Sale Item");
            }
            ModelState.Clear();
            NormalSaleVM cashdetails = new NormalSaleVM();
            cashdetails = await _csale.GetSales((Int64)orderid, null);
            ViewBag.pgno = 4;
            ViewBag.totalbalance = cashdetails.balinr;
            return View("Index", cashdetails);
        }
        public async Task<IActionResult> ExportToPDF(int orderid)
        {
            int UID;
            string htmlfile = string.Empty, htmlfileof = string.Empty, folderpath = string.Empty, filename = string.Empty, filenameof = string.Empty;
            var filepathtoreturn = string.Empty;
            var filepathtoreturnof = string.Empty;
            // string htmlfileof = ;
            NormalSaleVM cashdetails = new NormalSaleVM();
            UID = _comm.GetLoggedInUserId();
            try
            {
                cashdetails = await _csale.Invoice(orderid,UID);
             //    folderpath = _hostingenv.WebRootPath + $@"\uploadedcustomorder\" + orderid.ToString();
                // filename = filepathtoreturn = _hostingenv.WebRootPath + $@"\uploadedcustomorder\" + $@"\{orderid}";
            }
            catch (Exception)
            {

                throw;
            }

            if (cashdetails.cashsaledetailsCM.Count > 0)
            {
                htmlfile = GenerateHTMLCM(cashdetails);
                //HtmlToPdfConverter converter = new HtmlToPdfConverter();
                //WebKitConverterSettings settings = new WebKitConverterSettings();
                //settings.WebKitPath = Path.Combine(_hostingenv.ContentRootPath, "QtBinariesWindows");
                //settings.AdditionalDelay = 5000;
                //converter.ConverterSettings = settings;
                //PdfDocument document = converter.Convert(htmlfile, "");

                //  MemoryStream ms = new MemoryStream();
                //document.Save(ms);
                //document.Close(true);

                //ms.Position = 0;
                filename = filepathtoreturn = _hostingenv.WebRootPath + $@"\uploadedcustomorder\CM" + $@"\{orderid}.html";
                //using (FileStream file = new FileStream(filename+".html", FileMode.Create, System.IO.FileAccess.Write))
                //    ms.CopyTo(file);
                //ms.Flush();
                StreamWriter streamWriter =
           new StreamWriter(new FileStream(filename, FileMode.OpenOrCreate, FileAccess.Write));
                streamWriter.Write(htmlfile);
                streamWriter.Flush();
                streamWriter.Close();
               
            }
            if(cashdetails.cashsaledetailsOF.Count>0)
            {
                htmlfileof = GenerateHTMLOF(cashdetails);
                //HtmlToPdfConverter converter1 = new HtmlToPdfConverter();
                //WebKitConverterSettings settings1 = new WebKitConverterSettings();
                //Set WebKit path
                //settings1.WebKitPath = Path.Combine(_hostingenv.ContentRootPath, "QtBinariesWindows");
                //settings1.AdditionalDelay = 5000;
                //Assign WebKit settings to HTML converter
                //converter1.ConverterSettings = settings1;
                //Convert URL to PDF
              //  PdfDocument document = converter1.Convert(htmlfileof, "");

                MemoryStream msof = new MemoryStream();
                //document.Save(msof);
                //document.Close(true);

                //msof.Position = 0;
                filenameof = filepathtoreturnof = _hostingenv.WebRootPath + $@"\uploadedcustomorder\OF" + $@"\{orderid}.html";
                //using (FileStream fileof = new FileStream(filenameof + ".html", FileMode.Create, System.IO.FileAccess.Write))
                //    msof.CopyTo(fileof);
                //msof.Flush();

                StreamWriter streamWriter =
            new StreamWriter(new FileStream(filenameof, FileMode.OpenOrCreate, FileAccess.Write));
                streamWriter.Write(htmlfileof);
                streamWriter.Flush();
                streamWriter.Close();
               


            }

          
            //FileStreamResult fileStreamResult = new FileStreamResult( ms, "application/pdf");
            //fileStreamResult.FileDownloadName = "Invoice.pdf";
            // fileStreamResult.ExecuteResultAsync()
            //  await ExportToPDF1(orderid).ConfigureAwait(false);
            InvoiceVM invoice = new InvoiceVM();
            invoice.Open2Pdf = true;
            invoice.ShowPDFs = true;
            if (cashdetails.cashsaledetailsCM.Count > 0)
            {
               // invoice.PdfUrl1 = @"https://localhost:44377/uploadedcustomorder/CM/" + orderid + ".pdf";
                invoice.PdfUrl1 = @"http://ec2-13-232-169-227.ap-south-1.compute.amazonaws.com/salesapp/uploadedcustomorder/CM/" + orderid + ".html";
            }
            if (cashdetails.cashsaledetailsOF.Count > 0)
            {
               // invoice.PdfUrl2 = @"https://localhost:44377/uploadedcustomorder/OF/" + orderid + ".pdf";
                invoice.PdfUrl2 = @"http://ec2-13-232-169-227.ap-south-1.compute.amazonaws.com/salesapp/uploadedcustomorder/OF/" + orderid + ".html";
            }
            return View("~/Views/Shared/CashMemo.cshtml",invoice);
            //  return report;
        }
    
        [HttpPost]
        public async Task<IActionResult> AddCustomSale(NormalSaleVM saleetails, IFormFile file)
        {
            Int64 orderid = 0;
            string barcode = "";
            int UID;
            try
            {

                long size = 0;
                var filepathtoreturn = string.Empty;
                string folderpath = string.Empty, filename = string.Empty;
                var files = Request.Form.Files;
                UID = _comm.GetLoggedInUserId();
                //if (UID > 0)
                //{
                    //  saleetails.stockno = barcode.Split("*")[0];
                    orderid = await _csale.AddCustomSale(saleetails, UID);
                    if (orderid > 0)
                    {
                        folderpath = _hostingenv.WebRootPath + $@"\uploadedcustomorder\" + orderid;
                        filename = filepathtoreturn = folderpath + $@"\{files[0].FileName}";
                    }
                    else
                    {
                        folderpath = _hostingenv.WebRootPath + $@"\uploadedcustomorder\" + 0;
                        filename = filepathtoreturn = folderpath + $@"\{files[0].FileName}";
                    }
                    bool folderExists = Directory.Exists(folderpath);
                    if (!folderExists)
                        Directory.CreateDirectory(folderpath);
                    size += files[0].Length;
                    if (files.Count > 0)
                    {


                        //  BarCodeReader reader = new BarCodeReader()
                        //  System.Drawing.Image img = System.Drawing.Image.FromFile(filename);

                        using (System.IO.FileStream fs = System.IO.File.Create(filename))
                        {
                            files[0].CopyTo(fs);
                            //BarCodeReader reader = new BarCodeReader(fs);
                            //BarCodeResult[] c = reader.ReadBarCodes();
                            //barcode = c[0].CodeText;
                            fs.Flush();
                        }
                    }
                //}
                //else
                //{
                //    RedirectToAction("Logout", "Account");

                //}
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Error:Add Sale Item");

                saleetails = await _csale.Init(0);
            }
            // CustomSaleVM customsaledetails = new CustomSaleVM();
            ModelState.Clear();
            saleetails = await _csale.GetSales(orderid, saleetails);
            ViewBag.pgno = 1;
            // ViewBag.totalbalance = saleetails.balinr;
            return View("Index", saleetails);

        }
        protected string GenerateHTMLCM(NormalSaleVM _cashsaledetails)
        {
            StringBuilder htmlfile = new StringBuilder();
            StringBuilder htmlfileOF = new StringBuilder();
            decimal? paylater = 0;
            decimal? paidamount = 0;
            int cnt = 0;
            foreach (var pay in _cashsaledetails.paymentdetails)
            {
                if(pay.paymodeid==4)
                {
                    paylater += pay.payamountinr;
                }
                else { paidamount += pay.payamountinr; }

            }
                htmlfile.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><title>Emailer</title><style>table{border: 0px solid #333;}td{padding: 5px;}@media print {#printbtn {display:none;}}</style></head><body><table width='900' border='0' align='center' cellpadding='0' cellspacing='0'><tr><td height='15px' style='padding-left:5px;text-align: center;color: #ffffff!important; '><h3 style='font-family:Arial;font-weight:500;color:#999 !important;margin-top:10px;margin-bottom:10px; font-size:22px;font-family:Gotham,Helvetica, Arial,sans-serif;'>Invoice</h3></td><td align='right' style='padding-top:38px;'><input type='button' id='printbtn' value='Print Invoice' onClick='window.print()' style='outline:0px;box-shadow:none;background: #ff6377;color:#fff;border:2px solid #ff7b8c;border-radius:4px;width:90px;height:45px;'/></td></tr><tr><td height='5'></td></tr><tr><td align='center' height='15'><img src='http://ec2-13-232-169-227.ap-south-1.compute.amazonaws.com/salesapp/img/akb.png' height='60px' width='170px'><img src='http://ec2-13-232-169-227.ap-south-1.compute.amazonaws.com/salesapp/img/int.png' height='40px' width='200px'></td></td></tr>");
            htmlfile.Append("<tr><td height='15' align='center' valign='top' style='font-family:Arial,Helvetica,sans-serif; font-size: 16px;font-style:normal;'>Manufacturers and Exporters of Inlaid Articles</td></tr><tr><td style='font-family:Arial,Helvetica,sans-serif;font-size:14px;float:left;font-style:normal;border-bottom-color:black;border-bottom-style:groove;'><table width='900' border='0' cellpadding='0' cellspacing='0'><tbody><tr><td width='400' valign='top' style='border-right:solid 0px #333;line-height:20px;'>GST no: 09AKHPS5832P1ZA<br>State: UttarPradesh<br>State Code: 09 </td><td width='400' valign='top' style='border-right:solid 0px #333;line-height:20px;text-align:center;'><span style='font-size: 24px'> Cash Memo</span><br><br>");
            if (_cashsaledetails.cashsaledetailsCM.Count > 0)
            {
                htmlfile.Append("CashMemo No.: <strong style='font-size: 18px'>CM/" + _cashsaledetails.cashsaledetailsCM[0].unitid + "/" + _cashsaledetails.cashsaledetailsCM[0].billid + "</strong></td>");
            }
           
            htmlfile.Append("<td width='400' valign='top' style='text-align:right'><strong style='text-transform:uppercase;'>" + _cashsaledetails.cashsaledetailsCM[0].username + "</strong><br><br>Date: " + Convert.ToDateTime(_cashsaledetails.cashsaledetailsCM[0].orderdate).ToString("dd-MM-yyyy") + "<br></td></tr></tbody></table></td></tr><tr><td valign='top' style='font-family: Arial, Helvetica,sans-serif;font-size:15px;float:left;font-style:normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style='border:none'><tbody><tr><td width='85'>Name</td><td width='409'><strong style='text-transform:capitalize;'>");
            htmlfile.Append(_cashsaledetails.cinfo.Title + " " + _cashsaledetails.cinfo.Name);
            htmlfile.Append("</strong></td><td width='95'>Nationality</td><td width='311'><strong style='text-transform:capitalize;'>" + _cashsaledetails.cinfo.nationality + "</strong></td></tr><tr>");
            htmlfile.Append("<td>Address</td><td><strong style='text-transform:capitalize;'>" + _cashsaledetails.cinfo.Address + "</strong></td><td>Country</td><td><strong style='text-transform:capitalize;'>" + _cashsaledetails.cinfo.Country + "</strong></td></tr>");
            htmlfile.Append("<tr><td>State</td><td><strong style='text-transform:capitalize;'>" + _cashsaledetails.cinfo.State + "</strong></td><td>City</td><td><strong style='text-transform:capitalize;'>" + _cashsaledetails.cinfo.City + "</strong></td></tr><tr><td>Zip code</td><td><strong>" + _cashsaledetails.cinfo.Zipcode + "</strong></td>");
            htmlfile.Append("<td>Mobile No.</td><td><strong>" + _cashsaledetails.cinfo.MobCountryCode + "" + (_cashsaledetails.cinfo.Mobile) + "</strong></td></tr><tr><td>Phone</td><td><strong>" + _cashsaledetails.cinfo.TeleCountryCode + "" + (_cashsaledetails.cinfo.Telephone) + "</strong></td><td>Email</td><td><strong>" + _cashsaledetails.cinfo.Email + "</strong></td></tr></tbody></table></td></tr><tr><td style ='font-family:Arial,Helvetica,sans-serif;font-size:15px;float:left;font-style:normal;'><table width = '900' border = '0' cellspacing = '0' cellpadding='0' style='border:none;padding-top:70px;'><tbody><tr><td width='450'><strong> HSN CODE: 97020000(Original Engraving) </strong></td><td width='450'><strong> HSN CODE: 97030000(Sculptures) </strong></td></tr><tr><td colspan='2' align='center'  style='font-family: Arial, Helvetica, sans-serif; font-size:15px;float:centre;font-style:normal;'><strong>(Original Engraving(work of art) inlaid with Semi - Precious Stones Handmade in Agra, India) </strong></td></tr></tbody></table></td></tr><tr><td valign='top' style ='font-family:Arial,Helvetica,sans-serif;font-size:15px;float:left;font-style:normal;'><table width = '900' border = '0' cellspacing = '0' cellpadding='0' style='border:solid 0px #ccc'><tbody><tr><td colspan='4' valign='top'  height='10px'></td></tr><tr><td width='84' valign='top' style='text-align:center'> S N0.</td><td width='519' valign='top'> DESCRIPTION </td><td width='198' align='right' valign='top'> VALUE IN FOREIGN EX.</td><td width='134' align='right' valign='top'> INR AMOUNT </td></tr><tr><td colspan='4' valign='top'  height='8px'></td></tr>");
            foreach (var item in _cashsaledetails.cashsaledetailsCM)
            {
                cnt++;
                if (!string.IsNullOrEmpty(item.stockid))
                {
                    htmlfile.Append("<tr style='font-size:16px'><td valign='top' style='text-align:center'>" + cnt + ".</td><td valign='top'><strong> " + item.itemdesc + " </strong><br> (" + item.stockid + ") </td><td align ='right' valign='top'><strong> " + item.currency + " " + String.Format("{0:G29}", item.salevalue) + " </strong></td><td align='right' valign='top'><strong> INR " + String.Format("{0:G29}", item.salevalueinr) + " </strong></td></tr>");
                }
                else {
                    htmlfile.Append("<tr style='font-size:16px'><td valign='top' style='text-align:center'>" + cnt + ".</td><td valign='top'><strong> " + item.itemdesc + " </strong></td><td align ='right' valign='top'><strong> " + item.currency + " " + String.Format("{0:G29}", item.salevalue) + " </strong></td><td align='right' valign='top'><strong> INR " + String.Format("{0:G29}", item.salevalueinr) + " </strong></td></tr>");
                }
            }

            htmlfile.Append("</tbody></table></td></tr><tr><td valign='top'  style='font-family:Arial,Helvetica,sans-serif;font-size:15px;float:left;font-style:normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style ='border:none'><tbody><tr><td colspan='4' valign='top'  height='10px'></td></tr>");
            foreach (var stand in _cashsaledetails.standsaledetailsCM)
            {
                htmlfile.Append("<tr><td colspan='4' valign='top' style='padding-left:90px;'> Stand Details:</td></tr><tr><td width='384' valign='top' style='padding-left:90px;'> Type:<strong>" + stand.standdesc + "</strong>|Colour:<strong>" + stand.color + "</strong>|Height:" + stand.height + "''</td><td width='84' valign='top'></td></tr>");
            }
            htmlfile.Append("<tr><td valign='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td width='196' align='right' valign='top'><strong> SGST@.........%</ strong></td><td width='131' align='right' valign='top'><strong>  </strong></td></tr><tr> <td valign='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> CGST@.........%</strong></td><td align='right' valign='top'><strong>  </strong></td></tr><tr><td valign='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> IGST@.........%</strong></td><td align='right' valign='top'><strong>  </strong></td></tr><tr><td colspan='4' valign='top'  height='8px'></td></tr><tr><td valign='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> GROSS TOTAL </strong></td><td align='right' valign='top'><strong> INR " + String.Format("{0:G29}", _cashsaledetails.grandtotalinrCM) + " </strong></td></tr><tr><td colspan='4' valign='top'  height='8px'></td></tr>");
            if (paylater > 0)
            {
                htmlfile.Append("<tr><td valign='top'> &nbsp;</td> <td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> PAID AMOUNT </strong></td><td align='right' valign='top'><strong> INR " + String.Format("{0:G29}", paidamount) + " </strong></td></tr>");
            }else
            { htmlfile.Append("<tr><td valign='top'> &nbsp;</td> <td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> PAID AMOUNT </strong></td><td align='right' valign='top'><strong> INR " + String.Format("{0:G29}", _cashsaledetails.grandtotalinrCM) + " </strong></td></tr>"); }

            htmlfile.Append("<tr><td colspan='4' valign='top' height='8px'></td></tr><tr><td valign ='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> BALANCE TO BE PAID </strong></td><td align='right' valign='top' ><strong> INR " + String.Format("{0:G29}", paylater)  + " </strong></td></tr></tbody></table></td></tr><tr><td valign='top' style='font-family:Arial,Helvetica,sans-serif;font-size:15px;float:left; font-style:normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style='border:none;border-bottom-color:black;border-bottom-style:groove;'><tbody>");

            foreach (var pay in _cashsaledetails.paymentdetails)
            {
                htmlfile.Append("<tr><td width='450'><strong> Paid by: " + pay.paymode + "(" + pay.paytype + ") </strong></td>");
            }
            htmlfile.Append("<td width='450' align='right'><strong>(Total sale value includes all applicable taxes)</strong></td></tr></tbody></table></td></tr><tr><td valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size:15px;float:left;font-style:normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style='border-top:solid 0px #333;border-right:none;border-left:none;border-bottom:none;'><tbody><tr><td width='310' valign='top'> I, the buyer, hereby declare that the goods purchased by me will be taken out of the Indian Territory</td><td width='396' align='right' valign='top' style='font-size:20px;'> FOR AKBAR INTERNATIONAL</td></tr><tr><td valign='top' style='padding-top:75px'><strong> Buyer's Signature</strong></td><td align='right' valign='top' style='padding-top:75px'><strong>Sales executive's signature</strong></td></tr></tbody></table></td></tr>");
            if (_cashsaledetails.cashsaledetailsCM[0].unitid == 2)
            {
                htmlfile.Append("<tr><td height = '5' valign = 'top' style='font-size:16px' ><strong>Address</strong> : T(PD) Tajnagri, Phase-1,Agra - 282001 (Uttar Pradesh) INDIA</td></tr><tr><td height = '5' valign = 'top' style='font-size:14px'><strong>Tel.No.</strong> : +91 562 4023580,4023581<strong> E-mail</strong> : akbarinternationals@gmail.com<strong> website</strong> : www.akbarinternational.com/</td></tr>");
            }
            else
            {
                htmlfile.Append("<tr><td height = '5' valign = 'top' style='font-size:16px' ><strong>Address</strong> : 289,Fatehabad Road,Agra - 282001 (Uttar Pradesh) INDIA</td></tr><tr><td height = '5' valign = 'top' style='font-size:14px'><strong>Tel.No.</strong> : +91 562 4026600,4026601<strong> E-mail</strong> : akbarinternationals@gmail.com<strong> website</strong> : www.akbarinternational.com/</td></tr>");
            }
            htmlfile.Append("</table></body></html>");

            return htmlfile.ToString();

        }
        protected string GenerateHTMLOF(NormalSaleVM _cashsaledetails)
        {
            StringBuilder htmlfile = new StringBuilder();
            StringBuilder htmlfileOF = new StringBuilder();
            StringBuilder additions = new StringBuilder();
            decimal? paylater = 0;
            decimal? paidamount = 0;
            int cnt = 0;
            string customsp = string.Empty;
            foreach (var pay in _cashsaledetails.paymentdetails)
            {
                if (pay.paymodeid == 4)
                {
                    paylater += pay.payamountinr;
                }
                else { paidamount += pay.payamountinr; }

            }
            htmlfile.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><title>Emailer</title><style>table{border: 0px solid #333;	}td{padding: 5px;}@media print {#printbtn {display:none;}}</style></head><body><table width='900' border='0' align='center' cellpadding='0' cellspacing='0' style='margin-top:35px'><tr><td height='15px' style='padding-left:5px;text-align: center;color: #ffffff!important; '><h3 style='font-family:Arial;font-weight:500;color:#999 !important;margin-top:10px;margin-bottom:10px; font-size:22px;font-family:Gotham,Helvetica, Arial,sans-serif;'>Invoice</h3></td><td align='right' style='padding-top:38px;'><input type='button' id='printbtn' value='Print Invoice' onClick='window.print()' style='outline:0px;box-shadow:none;background: #ff6377;color:#fff;border:2px solid #ff7b8c;border-radius:4px;width:90px;height:45px;'/></td></tr><tr><td height='5'></td></tr><tr><td align='center' height='15'><img src='http://ec2-13-232-169-227.ap-south-1.compute.amazonaws.com/salesapp/img/akb.png' height='60px' width='170px'><img src='http://ec2-13-232-169-227.ap-south-1.compute.amazonaws.com/salesapp/img/int.png' height='40px' width='200px'></td></td></tr>");
            htmlfile.Append("<tr><td height='15' align='center' valign='top' style='font-family:Arial,Helvetica,sans-serif; font-size: 16px;font-style:normal;'>Manufacturers and Exporters of Inlaid Articles</td></tr><tr><td style='font-family:Arial,Helvetica,sans-serif;font-size:14px;float:left;font-style:normal;border-bottom-color:black;border-bottom-style:groove;'><table width='900' border='0' cellpadding='0' cellspacing='0'><tbody><tr><td width='400' valign='top' style='border-right:solid 0px #333;line-height:20px;'>GST no: 09AKHPS5832P1ZA<br>State: Uttar Pradesh<br>State Code: 09 </td><td width='400' valign='top' style='border-right:solid 0px #333;line-height:20px;text-align:center;'><span style='font-size: 22px'> Order Form</span><br><br>");
            if (_cashsaledetails.cashsaledetailsOF.Count > 0)
            {
                customsp = _cashsaledetails.cashsaledetailsOF[0].additiondesc;
                htmlfile.Append("Order No.: <strong style='font-size: 18px'>OF/" + _cashsaledetails.cashsaledetailsOF[0].unitid + "/" + _cashsaledetails.cashsaledetailsOF[0].billid + "</strong></td>");
            }

            htmlfile.Append("<td width='400' valign='top' style='text-align:right'><strong style='text-transform:uppercase;'>" + _cashsaledetails.cashsaledetailsOF[0].username + "</strong><br><br>Date: " +Convert.ToDateTime(_cashsaledetails.cashsaledetailsOF[0].orderdate).ToString("dd-MM-yyyy") + "<br></td></tr></tbody></table></td></tr><tr><td valign='top' style='font-family: Arial, Helvetica,sans-serif;font-size:15px;float:left;font-style:normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style='border:none'><tbody><tr><td width='85'>Name</td><td width='409'><strong style='text-transform:capitalize;'>");
            htmlfile.Append(_cashsaledetails.cinfo.Title + " " + _cashsaledetails.cinfo.Name);
            htmlfile.Append("</strong></td><td width='95'>Nationality</td><td width='311'><strong style='text-transform:capitalize;'>" + _cashsaledetails.cinfo.nationality + "</strong></td></tr><tr>");
            htmlfile.Append("<td>Address</td><td><strong style='text-transform:capitalize;'>" + _cashsaledetails.cinfo.Address + "</strong></td><td>Country</td><td><strong style='text-transform:capitalize;'>" + _cashsaledetails.cinfo.Country + "</strong></td></tr>");
            htmlfile.Append("<tr><td>State</td><td><strong style='text-transform:capitalize;'>" + _cashsaledetails.cinfo.State + "</strong></td><td>City</td><td><strong style='text-transform:capitalize;'>" + _cashsaledetails.cinfo.City + "</strong></td></tr><tr><td>Zip code</td><td><strong>" + _cashsaledetails.cinfo.Zipcode + "</strong></td>");
          
            htmlfile.Append("<td>Mobile No.</td><td><strong>" + _cashsaledetails.cinfo.MobCountryCode + "" + (_cashsaledetails.cinfo.Mobile) + "</strong></td></tr><tr><td>Phone</td><td><strong>" + _cashsaledetails.cinfo.TeleCountryCode + "" + (_cashsaledetails.cinfo.Telephone) + "</strong></td><td>Passport No.</td><td><strong>" + _cashsaledetails.cinfo.PassportNo + "</strong></td></tr><tr><td>Delivery Type</td><td><strong>" + _cashsaledetails.DelieveryTypeName + "</strong></td><td>Port Type</td><td><strong>" + _cashsaledetails.PortTypeName + "</strong></td></tr><tr><td>Port Name</td><td><strong>" + _cashsaledetails.dinfo.PortName + "</strong></td><td>Delivery From</td><td><strong>" + String.Format("{0:dd/MM/yyyy}", _cashsaledetails.dinfo.DeliveryFrom) + "</strong></td></tr><tr><td>Delivery To</td><td><strong>" + String.Format("{0:dd/MM/yyyy}", _cashsaledetails.dinfo.DeliveryTo) + "</strong></td><td>Email</td><td><strong>" + _cashsaledetails.cinfo.Email + "</strong></td></tr></tbody></table></td></tr><tr><td style ='font-family:Arial,Helvetica,sans-serif;font-size:15px;float:left;font-style:normal;'><table width = '900' border = '0' cellspacing = '0' cellpadding='0' style='border:none;padding-top:70px;'><tbody><tr><td width='450'><strong> HSN CODE: 97020000(Original Engraving) </strong></td><td width='450'><strong> HSN CODE: 97030000(Sculptures) </strong></td></tr><tr><td colspan='2' align='center'  style='font-family: Arial, Helvetica, sans-serif; font-size:15px;float:centre;font-style:normal;'><strong>(Original Engraving(work of art) inlaid with Semi - Precious Stones Handmade in Agra, India) </strong></td></tr></tbody></table></td></tr><tr><td valign='top' style ='font-family:Arial,Helvetica,sans-serif;font-size:15px;float:left;font-style:normal;'><table width = '900' border = '0' cellspacing = '0' cellpadding='0' style='border:solid 0px #ccc'><tbody><tr><td colspan='4' valign='top'  height='10px'></td></tr><tr><td width='84' valign='top' style='text-align:center'> S N0.</td><td width='519' valign='top'> DESCRIPTION </td><td width='198' align='right' valign='top'> VALUE IN FOREIGN EX.</td><td width='134' align='right' valign='top'> INR AMOUNT </td></tr><tr><td colspan='4' valign='top'  height='8px'></td></tr>");
            foreach (var item in _cashsaledetails.cashsaledetailsOF)
            {
                cnt++;
                //htmlfile.Append("<tr><td valign='top'>" + cnt + "</td><td valign='top'><strong> " + item.itemdesc + " </strong><br> " + item.stockid + ") </td><td align ='right' valign='top'><strong> " + item.currency + " " + item.salevalue + " </strong></td><td align='right' valign='top'><strong> INR " + item.salevalueinr + " </strong></td></tr>");
                if (!string.IsNullOrEmpty(item.stockid))
                {
                    htmlfile.Append("<tr style='font-size:16px'><td valign='top' style='text-align:center'>" + cnt + ".</td><td valign='top'><strong> " + item.itemdesc + " </strong><br> (" + item.stockid + ") </td><td align ='right' valign='top'><strong> " + item.currency + " " + String.Format("{0:G29}", item.salevalue) + " </strong></td><td align='right' valign='top'><strong> INR " + String.Format("{0:G29}", item.salevalueinr) + " </strong></td></tr>");
                }
                else
                {
                    htmlfile.Append("<tr style='font-size:16px'><td valign='top' style='text-align:center'>" + cnt + ".</td><td valign='top'><strong> " + item.itemdesc + " </strong><br>(TBM)</td><td align ='right' valign='top'><strong> " + item.currency + " " + String.Format("{0:G29}", item.salevalue) + " </strong></td><td align='right' valign='top'><strong> INR " + String.Format("{0:G29}", item.salevalueinr) + " </strong></td></tr>");
                }
            }


            htmlfile.Append("</tbody></table></td></tr><tr><td valign='top'  style='font-family:Arial,Helvetica,sans-serif;font-size:15px;float:left;font-style:normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style ='border:none'><tbody><tr><td colspan='4' valign='top'  height='10px'></td></tr>");
            foreach (var stand in _cashsaledetails.standsaledetailsOF)
            {
                htmlfile.Append("<tr><td colspan='4' valign='top' style='padding-left:90px;'> Stand Details:</td></tr><tr><td width='384' valign='top' style='padding-left:90px;'> Type:<strong>" + stand.standdesc + "</strong>|Colour:<strong>" + stand.color + "</strong>|Height:" + stand.height + "''</td><td width='84' valign='top'></td></tr>");
            }
            if (_cashsaledetails.spaddition.Count > 0)
            {
                htmlfile.Append("<tr><td colspan = '4' valign = 'top' style = 'padding-left:90px;' >Special Additions:</td></tr>");
                foreach (var sp in _cashsaledetails.spaddition)
                {
                    if (!string.IsNullOrEmpty(sp.desc))
                    {
                        additions.Append(sp.desc);
                        additions.Append(",");
                    }
                            
                }
            }
            htmlfile.Append("<tr><td width='384' valign='top' style='padding-left:90px;'><strong>" + additions.ToString()+ "</strong>'<strong>" + customsp + "</strong>'</td><td width='84' valign='top'></td></tr>");
            htmlfile.Append("<tr><td valign='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> GROSS TOTAL </strong></td><td align='right' valign='top'><strong> INR " + String.Format("{0:G29}", _cashsaledetails.grandtotalinrOF) + " </strong></td></tr>");
            //htmlfile.Append("<tr><td valign='top'> &nbsp;</td> <td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> PAID AMOUNT </strong></td><td align='right' valign='top'><strong> INR " + paidamount + " </strong></td></tr>");

            //htmlfile.Append("<tr><td colspan='4' valign='top' height='8px'></td></tr><tr><td valign ='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> BALANCE TO BE PAID </strong></td><td align='right' valign='top' ><strong> INR " + paylater + " </strong></td></tr></tbody></table></td></tr><tr><td valign='top' style='font-family:Arial,Helvetica,sans-serif;font-size:15px;float:left; font-style:normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style='border:none'><tbody>");
            if (paylater > 0)
            {
                htmlfile.Append("<tr><td colspan='4' valign='top'  height='8px'></td></tr><tr><td valign='top'> &nbsp;</td> <td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> PAID AMOUNT </strong></td><td align='right' valign='top'><strong> INR " + String.Format("{0:G29}", paidamount) + " </strong></td></tr>");
            }
            else {

                htmlfile.Append("<tr><td colspan='4' valign='top'  height='8px'></td></tr><tr><td valign='top'> &nbsp;</td> <td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> PAID AMOUNT </strong></td><td align='right' valign='top'><strong> INR " + String.Format("{0:G29}", _cashsaledetails.grandtotalinrOF) + " </strong></td></tr>");
            }

            htmlfile.Append("<tr><td colspan='4' valign='top' height='8px'></td></tr><tr><td valign ='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> BALANCE TO BE PAID </strong></td><td align='right' valign='top' ><strong> INR " + String.Format("{0:G29}", paylater)   + " </strong></td></tr></tbody></table></td></tr><tr><td valign='top' style='font-family:Arial,Helvetica,sans-serif;font-size:15px;float:left; font-style:normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style='border:none;border-bottom-color:black;border-bottom-style:groove;'><tbody>");

            foreach (var pay in _cashsaledetails.paymentdetails)
            {
                htmlfile.Append("<tr><td width='450'><strong> Paid by: " + pay.paymode + "(" + pay.paytype + ") </strong></td>");
            }
            htmlfile.Append("<td width='450' align='right'></td></tr></tbody></table></td></tr><tr><td valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size:15px;float:left;font-style:normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style='border-top:solid 0px #333;border-right:none;border-left:none;border-bottom:none;'><tbody><tr><td width='310' valign='top'>Note : Orders once placed cannot be cancelled.</td><td width='396' align='right' valign='top' style='font-size:20px;'> FOR AKBAR INTERNATIONAL</td></tr><tr><td valign='top' style='padding-top:75px'><strong> Buyer's Signature</strong></td><td align='right' valign='top' style='padding-top:75px'><strong>Sales executive's signature</strong></td></tr></tbody></table></td></tr>");
            if (_cashsaledetails.cashsaledetailsOF[0].unitid == 2)
            {
                htmlfile.Append("<tr><td height = '5' valign = 'top' style='font-size:16px' ><strong>Address</strong> : T(PD) Tajnagri, Phase-1,Agra - 282001 (Uttar Pradesh) INDIA</td></tr><tr><td height = '5' valign = 'top' style='font-size:14px'><strong>Tel.No.</strong> : +91 562 4023580,4023581<strong> E-mail</strong> : akbarinternationals@gmail.com<strong> website</strong> : www.akbarinternational.com/</td></tr>");
            }
            else
            {
                htmlfile.Append("<tr><td height ='5px' valign='top' style='font-size:16px' ><strong>Address</strong> : 289,Fatehabad Road,Agra - 282001 (Uttar Pradesh) INDIA</td></tr><tr><td height ='5px' valign ='top' style='font-size:14px'><strong>Tel.No.</strong> : +91 562 4026600,4026601<strong> E-mail</strong> : akbarinternationals@gmail.com<strong> website</strong> : www.akbarinternational.com/</td></tr>");
            }
            htmlfile.Append("</table></body></html>");

            return htmlfile.ToString();

        }
        public async Task<IActionResult> uploadQRcode()
        {
            string barcode = string.Empty, finalstockno = string.Empty; 
            try
            {
                long size = 0;
                var files = Request.Form.Files;
                Int64 orderid = 0;
               
                if (files.Count > 0)
                {
                    var filepathtoreturn = string.Empty;
                    string folderpath = _hostingenv.WebRootPath + $@"\uploadedcustomorder\QR\";
                    string filename = filepathtoreturn = _hostingenv.WebRootPath + $@"\uploadedcustomorder\" + $@"\{files[0].FileName}";
                    bool folderExists = Directory.Exists(folderpath);
                    if (!folderExists)
                        Directory.CreateDirectory(folderpath);
                    size += files[0].Length;
                    using (System.IO.FileStream fs = System.IO.File.Create(filename))
                    {
                        files[0].CopyTo(fs);
                        BarCodeReader reader = new BarCodeReader(fs);
                        BarCodeResult[] c = reader.ReadBarCodes();
                        barcode = c[0].CodeText;
                        fs.Flush();
                    }

                    finalstockno =await _csale.IsExist(barcode);
                }
            }
            catch (Exception)
            {

                throw;
            }
            return Ok(barcode);


        }

        protected bool validatepayment(NormalSaleVM paydetails,out string msg)
        {
            decimal? amount = 0;
            bool result=true;
            string finalmsg = string.Empty;


            if ((paydetails.paymethodvalue.ToUpper().Contains("PAYTM") == false))
            {
                amount = Convert.ToDecimal(paydetails.PayLaterAmount) + Convert.ToDecimal(paydetails.CashAmount) + Convert.ToDecimal(paydetails.CreditAmount) + Convert.ToDecimal(paydetails.DebitAmount) + Convert.ToDecimal(paydetails.PaytmAmount);

                if (amount <= 0)
                {
                    finalmsg = "Amount Cannot Be 0 Or Less";
                    result = false;

                }
                else
                {
                    if (paydetails.paymethodvalue.ToUpper().Contains("CASH") == false)
                    {
                        if (paydetails.cardid == 0 && paydetails.cardiddebit == 0 && paydetails.paylaterid == 0)
                        {
                            finalmsg = "Please Selet Payment Type.";
                            result = false;

                        }
                    }


                }

            }
            else
            {

                finalmsg = string.Empty;
            }
            msg = finalmsg;
            return result;

        }

    }
}
