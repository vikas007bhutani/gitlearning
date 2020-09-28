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


namespace SalesApp.Controllers
{
    public class AddSaleController : Controller
    {
        private readonly INormalSaleRepository _csale;
        private readonly ICommonRepository _comm;
      //  private readonly IWebHostEnvironment _honstingEnvironment;
        IWebHostEnvironment _hostingenv;
        public AddSaleController(INormalSaleRepository _cashsale, ICommonRepository commonRepository, IWebHostEnvironment _env)
        {
            this._csale = _cashsale;
            this._comm = commonRepository;
         //   _honstingEnvironment = hostingEnvironment;
            this._hostingenv = _env;
        }

        public async Task<IActionResult> Index(int id)
        {
           NormalSaleVM cashdetails = new NormalSaleVM();
            cashdetails = await _csale.Init(id);
           // string q = HttpContext.Request.Query["pgno"].ToString();
            ViewBag.pgno = 1;
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
            ModelState.Clear();
            try
            {
                if (ModelState.IsValid)
                {
                    _orderid = await _csale.AddCashSale(saleetails, _comm.GetLoggedInUserId());
                    if (_orderid == -1)
                    {

                        TempData["NormalMessage"] = new MessageVM() { title = "Please enter", msg = "This stock no. already added!!!" };
                        cashdetails = await _csale.GetSales(saleetails.orderid, saleetails);

                    }
                    else
                    {
                        cashdetails = await _csale.GetSales(_orderid, saleetails);
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
            try
            {
                masterorderid = await _csale.DeleteCashSale(itemorderid, _comm.GetLoggedInUserId());
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
            try
            {
             result=   await _csale.FinishCashSale(orderid, _comm.GetLoggedInUserId());
                if (result)
                {
                    return RedirectToAction("Index", "Mirror");

                }
                else
                {
                    TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "Please Fill Payment or Delivery Details to Continue!!." };
                    // return View("Index", standdetails);

                }

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
            try
            {


                if (saledetails.cinfo != null)
                {
                    result = await _csale.AddCustomerinfo(saledetails, _comm.GetLoggedInUserId());
                    if (result)
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
            NormalSaleVM cashdetails = new NormalSaleVM();
            cashdetails = await _csale.GetSales(saledetails.orderid, saledetails);
            ViewBag.pgno = 3;
            ViewBag.totalbalance = cashdetails.balinr;
            return View("Index", cashdetails);

        }

        public async Task<IActionResult> AddStandInfo(NormalSaleVM standdetails)
        {
            bool result = true;
            try
            {


                if (standdetails.orderid >= 0)
                {
                    result = await _csale.AddStandSale(standdetails, _comm.GetLoggedInUserId());
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
            }
            catch (Exception)
            {
                TempData["StandMessage"] = new MessageVM() { title = "Error!", msg = "Operation Failed." };
                return View("Index", standdetails);

            }
            ModelState.Clear();
            NormalSaleVM cashdetails = new NormalSaleVM();
            cashdetails = await _csale.GetSales(standdetails.orderid, standdetails);
            ViewBag.pgno = 3;
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
            try
            {


                if (standdetails.orderid >= 0)
                {
                    result = await _csale.AddDeliveryDetails(standdetails, _comm.GetLoggedInUserId());
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
            }
            catch (Exception)
            {
                TempData["UserMessage"] = new MessageVM() { title = "Error!", msg = "Operation Failed." };
                return View("Index", standdetails);

            }
            ModelState.Clear();
            NormalSaleVM cashdetails = new NormalSaleVM();
            cashdetails = await _csale.GetSales(standdetails.orderid, standdetails);
            ViewBag.pgno = 5;
        //    ViewBag.section = 2;
            ViewBag.totalbalance = cashdetails.balinr;

            return View("Index", cashdetails);

        }

        public async Task<IActionResult> DeleteStand(int itemorderid)
        {
            Int64? orderid = 0;
            try
            {
                orderid = await _csale.DeleteCashSale(itemorderid, _comm.GetLoggedInUserId());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Error:Delete Sale Item");
            }
            ModelState.Clear();
            NormalSaleVM cashdetails = new NormalSaleVM();
            cashdetails = await _csale.GetSales((Int64)orderid, null);
            ViewBag.pgno = 3;
            ViewBag.section = 1;
            ViewBag.totalbalance = cashdetails.balinr;
            return View("Index", cashdetails);
        }

        public async Task<IActionResult> AddOrderPayment(NormalSaleVM paydetails)
        {
            bool result = true;
            try
            {


                if (paydetails.orderid >= 0)
                {
                    result = await _csale.AddSalePayment(paydetails, _comm.GetLoggedInUserId());
                    if (result)
                    {
                        TempData["PayMessage"] = new MessageVM() { title = "Success!", msg = "Payment updated successfully." };

                    }
                    else
                    {
                        TempData["PayMessage"] = new MessageVM() { title = "Error!", msg = "First,Add Order Item,before payment." };
                        // return View("Index", standdetails);

                    }

                }
                else
                {
                    // ModelState.IsValid;
                    TempData["PayMessage"] = new MessageVM() { title = "Error!", msg = "First,Add Order Item,before payment." };
                    // return View("Index", standdetails);

                }
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
            try
            {
                orderid = await _csale.DeletePayment(payid, _comm.GetLoggedInUserId());
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
        public async Task<IActionResult> ExportToPDF(long orderid)
        {

            string htmlfile = string.Empty, htmlfileof = string.Empty, folderpath = string.Empty, filename = string.Empty ;
            var filepathtoreturn = string.Empty;
            // string htmlfileof = ;
            NormalSaleVM cashdetails = new NormalSaleVM();
            try
            {
                cashdetails = await _csale.Invoice(orderid);
             //    folderpath = _hostingenv.WebRootPath + $@"\uploadedcustomorder\" + orderid.ToString();
                 filename = filepathtoreturn = _hostingenv.WebRootPath + $@"\uploadedcustomorder\" + $@"\{orderid}";
            }
            catch (Exception)
            {

                throw;
            }

            if (cashdetails.cashsaledetailsCM.Count > 0)
            {
                htmlfile = GenerateHTMLCM(cashdetails);
                HtmlToPdfConverter converter = new HtmlToPdfConverter();
                WebKitConverterSettings settings = new WebKitConverterSettings();
                //Set WebKit path
                settings.WebKitPath = Path.Combine(_hostingenv.ContentRootPath, "QtBinariesWindows");
                settings.AdditionalDelay = 5000;
                //Assign WebKit settings to HTML converter
                converter.ConverterSettings = settings;
                //Convert URL to PDF
                PdfDocument document = converter.Convert(htmlfile, "");

                MemoryStream ms = new MemoryStream();
                document.Save(ms);
                document.Close(true);

                ms.Position = 0;
                filename = filepathtoreturn = _hostingenv.WebRootPath + $@"\uploadedcustomorder\CM" + $@"\{orderid}";
                using (FileStream file = new FileStream(filename+".pdf", FileMode.Create, System.IO.FileAccess.Write))
                    ms.CopyTo(file);
            }
            if(cashdetails.cashsaledetailsOF.Count>0)
            {
                htmlfileof = GenerateHTMLOF(cashdetails);
                HtmlToPdfConverter converter = new HtmlToPdfConverter();
                WebKitConverterSettings settings = new WebKitConverterSettings();
                //Set WebKit path
                settings.WebKitPath = Path.Combine(_hostingenv.ContentRootPath, "QtBinariesWindows");
                settings.AdditionalDelay = 5000;
                //Assign WebKit settings to HTML converter
                converter.ConverterSettings = settings;
                //Convert URL to PDF
                PdfDocument document = converter.Convert(htmlfileof, "");

                MemoryStream ms = new MemoryStream();
                document.Save(ms);
                document.Close(true);

                ms.Position = 0;
                filename = filepathtoreturn = _hostingenv.WebRootPath + $@"\uploadedcustomorder\OF" + $@"\{orderid}";
                using (FileStream file = new FileStream(filename + ".pdf", FileMode.Create, System.IO.FileAccess.Write))
                    ms.CopyTo(file);
                ms.Flush();
                

            }

          
            //FileStreamResult fileStreamResult = new FileStreamResult( ms, "application/pdf");
            //fileStreamResult.FileDownloadName = "Invoice.pdf";
            // fileStreamResult.ExecuteResultAsync()
            //  await ExportToPDF1(orderid).ConfigureAwait(false);
            InvoiceVM invoice = new InvoiceVM();
            invoice.Open2Pdf = true;
            invoice.ShowPDFs = true;
            if (cashdetails.cashsaledetails.Count > 0)
            {
               // invoice.PdfUrl1 = @"https://localhost:44377/uploadedcustomorder/CM/" + orderid + ".pdf";
                invoice.PdfUrl1 = @"https://103.48.50.253/salesapp/uploadedcustomorder/CM/" + orderid + ".pdf";
            }
            if (cashdetails.cashsaledetailsOF.Count > 0)
            {
               // invoice.PdfUrl2 = @"https://localhost:44377/uploadedcustomorder/OF/" + orderid + ".pdf";
                invoice.PdfUrl2 = @"https://103.48.50.253/salesapp/uploadedcustomorder/OF/" + orderid + ".pdf";
            }
            return View("~/Views/Shared/CashMemo.cshtml",invoice);
            //  return report;
        }
    
        [HttpPost]
        public async Task<IActionResult> AddCustomSale(NormalSaleVM saleetails, IFormFile file)
        {
            Int64 orderid = 0;
            string barcode = "";
            try
            {

                long size = 0;
                var filepathtoreturn = string.Empty;
                string folderpath = string.Empty, filename = string.Empty;
                var files = Request.Form.Files;
               
              //  saleetails.stockno = barcode.Split("*")[0];
                orderid = await _csale.AddCustomSale(saleetails, _comm.GetLoggedInUserId());
                if (orderid > 0)
                {
                    folderpath = _hostingenv.WebRootPath + $@"\uploadedcustomorder\" + orderid;
                    filename = filepathtoreturn = folderpath + $@"\{files[0].FileName}";
                }
                else {
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
            int cnt = 0;
            htmlfile.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><title>Emailer</title><style>table{border: 0px solid #333;	}td{padding: 5px;}</style></head><body><table width='900' border='0' align='center' cellpadding='0' cellspacing='0'><tr><td height='15px' style='padding-left:5px;text-align: center;color: #ffffff!important; '><h4 style='font-family:inherit;font-weight:500;color:#fff !important;margin-top:10px;margin-bottom:10px; font-size:18px;font-family:Gotham,Helvetica, Arial,sans-serif;'>Invoice</h4></td></tr><tr><td height='5'></td></tr><tr><td align='center' valign='top' style='font-family: Arial,Helvetica,sans-serif;font-size:22px;font-style:normal;'>Akbar International</td></tr><tr><td align='center' height='15'><img src='http://ec2-13-232-169-227.ap-south-1.compute.amazonaws.com/workofart/akb.png' height='40px' width='200px'><img src='http://ec2-13-232-169-227.ap-south-1.compute.amazonaws.com/workofart/int.png' height='40px' width='200px'></td></td></tr>");
            htmlfile.Append("<tr><td height='15' align='center' valign='top' style='font-family:Arial,Helvetica,sans-serif; font-size: 16px;font-style:normal;'>Manufactures and exporters of Inlaid Articles</td></tr><tr><td style='font-family:Arial,Helvetica,sans-serif;font-size:14px;float:left;font-style:normal;'><table width='900' border='0' cellpadding='0' cellspacing='0'><tbody><tr><td width='290' valign='top' style='border-right:solid 0px #333;line-height:20px;'>GST no: 09AKHPS5832P1ZA<br>State Uttar: Pardesh<br>State Code: 09 </td><td width='309' valign='top' style='border-right:solid 0px #333;line-height:20px;'><span style='font-size: 22px'> Cash Memo</span><br><br>");
            if (_cashsaledetails.cashsaledetailsCM.Count > 0)
            {
                htmlfile.Append("Invoice No.: <strong>CM/" + _cashsaledetails.cashsaledetailsCM[0].unitid + "/" + _cashsaledetails.cashsaledetailsCM[0].InvoiceID + "</strong></td>");
            }
           
            htmlfile.Append("<td width='299' valign='top'><strong>" + _cashsaledetails.cashsaledetailsCM[0].username + "</strong><br><br>Date: " + DateTime.Now.ToString("dd/MM/yyyy") + "<br></td></tr></tbody></table></td></tr><tr><td valign='top' style='font-family: Arial, Helvetica,sans-serif;font-size:15px;float:left;font-style:normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style='border:none'><tbody><tr><td width='85'>Name</td><td width='409'><strong>");
            htmlfile.Append(_cashsaledetails.cinfo.Title + " " + _cashsaledetails.cinfo.Name);
            htmlfile.Append("</strong></td><td width='95'>Nationality</td><td width='311'><strong>" + _cashsaledetails.cinfo.Country + "</strong></td></tr><tr>");
            htmlfile.Append("<td>Address</td><td><strong>" + _cashsaledetails.cinfo.Address + "</strong></td><td>Country</td><td><strong>" + _cashsaledetails.cinfo.Country + "</strong></td></tr>");
            htmlfile.Append("<tr><td>State</td><td><strong>" + _cashsaledetails.cinfo.State + "</strong></td><td>City</td><td><strong>" + _cashsaledetails.cinfo.City + "</strong></td></tr><tr><td>Zip code</td><td><strong>" + _cashsaledetails.cinfo.Zipcode + "</strong></td>");
            htmlfile.Append("<td>Passport No.</td><td><strong>" + _cashsaledetails.cinfo.PassportNo + "</strong></td></tr><tr><td>Email</td><td>" + _cashsaledetails.cinfo.Email + "</td><td>Mobile No.</td><td><strong>+" + _cashsaledetails.cinfo.MobCountryCode + "" + (_cashsaledetails.cinfo.Mobile) + "</strong></td></tr><tr><td>Phone</td><td><strong>+" + _cashsaledetails.cinfo.TeleCountryCode + "" + (_cashsaledetails.cinfo.Telephone) + "</strong></td><td>&nbsp;</td><td>&nbsp;</td></tr></tbody></table></td></tr><tr><td style ='font-family:Arial,Helvetica,sans-serif;font-size:15px;float:left;font-style:normal;'><table width = '900' border = '0' cellspacing = '0' cellpadding='0' style='border:none;padding-top:70px;'><tbody><tr><td width='450'><strong> HSN CODE: 97024555145(Original Engrawing) </strong></td><td width='450'><strong> HSN CODE: 97024555145(Sculptures) </strong></td></tr><tr><td colspan='2' align='center'  style='font-family: Arial, Helvetica, sans-serif; font-size:15px;float:centre;font-style:normal;'><strong>(Original engrawing(work of art) inlaid with semi - precious Stones handmade in Agra, India) </strong></td></tr></tbody></table></td></tr><tr><td valign='top' style ='font-family:Arial,Helvetica,sans-serif;font-size:15px;float:left;font-style:normal;'><table width = '900' border = '0' cellspacing = '0' cellpadding='0' style='border:solid 0px #ccc'><tbody><tr><td colspan='4' valign='top'  height='10px'></td></tr><tr><td width='84' valign='top'> S N0.</td><td width='519' valign='top'> DESCRIPTION </td><td width='170' align='right' valign='top'> VALUE IN FORGEIGN  EX.</td><td width='134' align='right' valign='top'> INR AMOUNT </td></tr><tr><td colspan='4' valign='top'  height='8px'></td></tr>");
            foreach (var item in _cashsaledetails.cashsaledetailsCM)
            {
                cnt++;
                htmlfile.Append("<tr><td valign='top'>" + cnt + "</td><td valign='top'><strong> " + item.itemdesc + " </strong><br> " + item.stockid + ") </td><td align ='right' valign='top'><strong> " + item.currency + " " + item.salevalue + " </strong></td><td align='right' valign='top'><strong> INR " + item.salevalueinr + " </strong></td></tr>");
            }

            htmlfile.Append("</tbody></table></td></tr><tr><td valign='top'  style='font-family:Arial,Helvetica,sans-serif;font-size:15px;float:left;font-style:normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style ='border:none'><tbody><tr><td colspan='4' valign='top'  height='10px'></td></tr><tr><td colspan='4' valign='top' style='padding-left:90px;'> Stand Details:</td></tr>");
            foreach (var stand in _cashsaledetails.standsaledetailsCM)
            {
                htmlfile.Append("<tr><td width='384' valign='top' style='padding-left:90px;'> Type:<strong>" + stand.standdesc + "</strong>|Colour:<strong>" + stand.color + "</strong>|Height:" + stand.height + "''</td><td width='84' valign='top'></td></tr>");
            }
            htmlfile.Append("<tr><td valign='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td width='196' align='right' valign='top'><strong> SGST@.........%</ strong></td><td width='131' align='right' valign='top'><strong>  </strong></td></tr><tr> <td valign='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> CGST@.........%</strong></td><td align='right' valign='top'><strong>  </strong></td></tr><tr><td valign='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> IGST@.........%</strong></td><td align='right' valign='top'><strong>  </strong></td></tr><tr><td colspan='4' valign='top'  height='8px'></td></tr><tr><td valign='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> GROSS TOTAL </strong></td><td align='right' valign='top'><strong> INR " + _cashsaledetails.grandtotalinrCM + " </strong></td></tr><tr><td colspan='4' valign='top'  height='8px'></td></tr><tr><td valign='top'> &nbsp;</td> <td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> PAID AMOUNT </strong></td><td align='right' valign='top'><strong> INR " + _cashsaledetails.grandtotalinrCM + " </strong></td></tr><tr><td colspan='4' valign='top' height='8px'></td></tr><tr><td valign ='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> BALANCE TO BE PAID </strong></td><td align='right' valign='top' ><strong> INR " + _cashsaledetails.balinr + " </strong></td></tr></tbody></table></td></tr><tr><td valign='top' style='font-family:Arial,Helvetica,sans-serif;font-size:15px;float:left; font-style:normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style='border:none'><tbody>");
            foreach (var pay in _cashsaledetails.paymentdetails)
            {
                htmlfile.Append("<tr><td width='450'><strong> Paid by: " + pay.paymode + "(" + pay.paytype + ") </strong></td>");
            }
            htmlfile.Append("<td width='450' align='right'><strong> &quot; Total sale value includes all applicable taxes & quot;</strong></td></tr></tbody></table></td></tr><tr><td valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size:15px;float:left;font-style:normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style='border-top:solid 0px #333;border-right:none;border-left:none;border-bottom:none;'><tbody><tr><td width='310' valign='top'> I the buyer hereby declare that the goods purchased by me will be taken out of the Indian Territory</td><td width='396' align='right' valign='top' style='font-size:20px;'> FOR AKBAR INTERNATIONAL</td></tr><tr><td valign='top'> &nbsp;</td><td align='right' valign='top'> signature </td></tr><tr><td valign='top'><strong> Buyer's Signature</strong></td><td align='right' valign='top'><strong>Sales executive's signature</strong></td></tr></tbody></table></td></tr><tr><td height = '5' valign = 'top'></td></tr></table></body></html>");

            return htmlfile.ToString();

        }
        protected string GenerateHTMLOF(NormalSaleVM _cashsaledetails)
        {
            StringBuilder htmlfile = new StringBuilder();
            StringBuilder htmlfileOF = new StringBuilder();
            int cnt = 0;
            htmlfile.Append("<!DOCTYPE html PUBLIC '-//W3C//DTD XHTML 1.0 Transitional//EN' 'http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd'><html xmlns='http://www.w3.org/1999/xhtml'><head><meta http-equiv='Content-Type' content='text/html; charset=utf-8' /><title>Emailer</title><style>table{border: 0px solid #333;	}td{padding: 5px;}</style></head><body><table width='900' border='0' align='center' cellpadding='0' cellspacing='0'><tr><td height='15px' style='padding-left:5px;text-align: center;color: #ffffff!important; '><h4 style='font-family:inherit;font-weight:500;color:#fff !important;margin-top:10px;margin-bottom:10px; font-size:18px;font-family:Gotham,Helvetica, Arial,sans-serif;'>Invoice</h4></td></tr><tr><td height='5'></td></tr><tr><td align='center' valign='top' style='font-family: Arial,Helvetica,sans-serif;font-size:22px;font-style:normal;'>Akbar International</td></tr><tr><td align='center' height='15'><img src='http://ec2-13-232-169-227.ap-south-1.compute.amazonaws.com/workofart/akb.png' height='40px' width='200px'><img src='http://ec2-13-232-169-227.ap-south-1.compute.amazonaws.com/workofart/int.png' height='40px' width='200px'></td></td></tr>");
            htmlfile.Append("<tr><td height='15' align='center' valign='top' style='font-family:Arial,Helvetica,sans-serif; font-size: 16px;font-style:normal;'>Manufactures and exporters of Inlaid Articles</td></tr><tr><td style='font-family:Arial,Helvetica,sans-serif;font-size:14px;float:left;font-style:normal;'><table width='900' border='0' cellpadding='0' cellspacing='0'><tbody><tr><td width='290' valign='top' style='border-right:solid 0px #333;line-height:20px;'>GST no: 09AKHPS5832P1ZA<br>State Uttar: Pardesh<br>State Code: 09 </td><td width='309' valign='top' style='border-right:solid 0px #333;line-height:20px;'><span style='font-size: 22px'> Order Form</span><br><br>");
            if (_cashsaledetails.cashsaledetailsOF.Count > 0)
            {
                htmlfile.Append("Invoice No.: <strong>OF/" + _cashsaledetails.cashsaledetailsOF[0].unitid + "/" + _cashsaledetails.cashsaledetailsOF[0].InvoiceID + "</strong></td>");
            }

            htmlfile.Append("<td width='299' valign='top'><strong>" + _cashsaledetails.cashsaledetailsOF[0].username + "</strong><br><br>Date: " + DateTime.Now.ToString("dd/MM/yyyy") + "<br></td></tr></tbody></table></td></tr><tr><td valign='top' style='font-family: Arial, Helvetica,sans-serif;font-size:15px;float:left;font-style:normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style='border:none'><tbody><tr><td width='85'>Name</td><td width='409'><strong>");
            htmlfile.Append(_cashsaledetails.cinfo.Title + " " + _cashsaledetails.cinfo.Name);
            htmlfile.Append("</strong></td><td width='95'>Nationality</td><td width='311'><strong>" + _cashsaledetails.cinfo.Country + "</strong></td></tr><tr>");
            htmlfile.Append("<td>Address</td><td><strong>" + _cashsaledetails.cinfo.Address + "</strong></td><td>Country</td><td><strong>" + _cashsaledetails.cinfo.Country + "</strong></td></tr>");
            htmlfile.Append("<tr><td>State</td><td><strong>" + _cashsaledetails.cinfo.State + "</strong></td><td>City</td><td><strong>" + _cashsaledetails.cinfo.City + "</strong></td></tr><tr><td>Zip code</td><td><strong>" + _cashsaledetails.cinfo.Zipcode + "</strong></td>");
            htmlfile.Append("<td>Passport No.</td><td><strong>" + _cashsaledetails.cinfo.PassportNo + "</strong></td></tr><tr><td>Email</td><td>" + _cashsaledetails.cinfo.Email + "</td><td>Mobile No.</td><td><strong>+" + _cashsaledetails.cinfo.MobCountryCode + "" + (_cashsaledetails.cinfo.Mobile) + "</strong></td></tr><tr><td>Phone</td><td><strong>+" + _cashsaledetails.cinfo.TeleCountryCode + "" + (_cashsaledetails.cinfo.Telephone) + "</strong></td><td>&nbsp;</td><td>&nbsp;</td></tr></tbody></table></td></tr><tr><td style ='font-family:Arial,Helvetica,sans-serif;font-size:15px;float:left;font-style:normal;'><table width = '900' border = '0' cellspacing = '0' cellpadding='0' style='border:none;padding-top:70px;'><tbody><tr><td width='450'><strong> HSN CODE: 97024555145(Original Engrawing) </strong></td><td width='450'><strong> HSN CODE: 97024555145(Sculptures) </strong></td></tr><tr><td colspan='2' align='center'  style='font-family: Arial, Helvetica, sans-serif; font-size:15px;float:centre;font-style:normal;'><strong>(Original engrawing(work of art) inlaid with semi - precious Stones handmade in Agra, India) </strong></td></tr></tbody></table></td></tr><tr><td valign='top' style ='font-family:Arial,Helvetica,sans-serif;font-size:15px;float:left;font-style:normal;'><table width = '900' border = '0' cellspacing = '0' cellpadding='0' style='border:solid 0px #ccc'><tbody><tr><td colspan='4' valign='top'  height='10px'></td></tr><tr><td width='84' valign='top'> S N0.</td><td width='519' valign='top'> DESCRIPTION </td><td width='170' align='right' valign='top'> VALUE IN FORGEIGN  EX.</td><td width='134' align='right' valign='top'> INR AMOUNT </td></tr><tr><td colspan='4' valign='top'  height='8px'></td></tr>");
            foreach (var item in _cashsaledetails.cashsaledetailsOF)
            {
                cnt++;
                htmlfile.Append("<tr><td valign='top'>" + cnt + "</td><td valign='top'><strong> " + item.itemdesc + " </strong><br> " + item.stockid + ") </td><td align ='right' valign='top'><strong> " + item.currency + " " + item.salevalue + " </strong></td><td align='right' valign='top'><strong> INR " + item.salevalueinr + " </strong></td></tr>");
            }


            htmlfile.Append("</tbody></table></td></tr><tr><td valign='top'  style='font-family:Arial,Helvetica,sans-serif;font-size:15px;float:left;font-style:normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style ='border:none'><tbody><tr><td colspan='4' valign='top'  height='10px'></td></tr><tr><td colspan='4' valign='top' style='padding-left:90px;'> Stand Details:</td></tr>");
            foreach (var stand in _cashsaledetails.standsaledetailsOF)
            {
                htmlfile.Append("<tr><td width='384' valign='top' style='padding-left:90px;'> Type:<strong>" + stand.standdesc + "</strong>|Colour:<strong>" + stand.color + "</strong>|Height:" + stand.height + "''</td><td width='84' valign='top'></td></tr>");
            }
            htmlfile.Append("<tr><td valign='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td width='196' align='right' valign='top'><strong> SGST@.........%</ strong></td><td width='131' align='right' valign='top'><strong>  </strong></td></tr><tr> <td valign='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> CGST@.........%</strong></td><td align='right' valign='top'><strong>  </strong></td></tr><tr><td valign='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> IGST@.........%</strong></td><td align='right' valign='top'><strong>  </strong></td></tr><tr><td colspan='4' valign='top'  height='8px'></td></tr><tr><td valign='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> GROSS TOTAL </strong></td><td align='right' valign='top'><strong> INR " + _cashsaledetails.grandtotalinrOF + " </strong></td></tr><tr><td colspan='4' valign='top'  height='8px'></td></tr><tr><td valign='top'> &nbsp;</td> <td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> PAID AMOUNT </strong></td><td align='right' valign='top'><strong> INR " + _cashsaledetails.grandtotalinrOF + " </strong></td></tr><tr><td colspan='4' valign='top' height='8px'></td></tr><tr><td valign ='top'> &nbsp;</td><td valign='top'> &nbsp;</td><td align='right' valign='top'><strong> BALANCE TO BE PAID </strong></td><td align='right' valign='top' ><strong> INR " + _cashsaledetails.balinr + " </strong></td></tr></tbody></table></td></tr><tr><td valign='top' style='font-family:Arial,Helvetica,sans-serif;font-size:15px;float:left; font-style:normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style='border:none'><tbody>");
            foreach (var pay in _cashsaledetails.paymentdetails)
            {
                htmlfile.Append("<tr><td width='450'><strong> Paid by: " + pay.paymode + "(" + pay.paytype + ") </strong></td>");
            }
            htmlfile.Append("<td width='450' align='right'><strong> &quot; Total sale value includes all applicable taxes & quot;</strong></td></tr></tbody></table></td></tr><tr><td valign='top' style='font-family: Arial, Helvetica, sans-serif; font-size:15px;float:left;font-style:normal;'><table width='900' border='0' cellspacing='0' cellpadding='0' style='border-top:solid 0px #333;border-right:none;border-left:none;border-bottom:none;'><tbody><tr><td width='310' valign='top'> I the buyer hereby declare that the goods purchased by me will be taken out of the Indian Territory</td><td width='396' align='right' valign='top' style='font-size:20px;'> FOR AKBAR INTERNATIONAL</td></tr><tr><td valign='top'> &nbsp;</td><td align='right' valign='top'> signature </td></tr><tr><td valign='top'><strong> Buyer's Signature</strong></td><td align='right' valign='top'><strong>Sales executive's signature</strong></td></tr></tbody></table></td></tr><tr><td height = '5' valign = 'top'></td></tr></table></body></html>");

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

    }
}
