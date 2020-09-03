﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SALEERP.Models;
using SalesApp.Repository;
using SalesApp.Repository.Interface;
using SalesApp.ViewModel;

namespace SalesApp.Controllers
{
    public class CashSaleController : Controller
    {
        ICashSaleRepository _csale;
        ICommonRepository _comm;
        public CashSaleController(ICashSaleRepository _cashsale, ICommonRepository commonRepository)
        {
            this._csale = _cashsale;
            this._comm = commonRepository;
        }
        public async  Task<IActionResult> Index(int id)
        {
            CashSaleVM cashdetails = new CashSaleVM();
            cashdetails = await _csale.Init(id);
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
            CashSaleVM cashdetails = new CashSaleVM();
            Int64 _orderid = 0;
            ModelState.Clear();
            try
            {
                 _orderid=   await _csale.AddCashSale(saleetails, _comm.GetLoggedInUserId());
                cashdetails = await _csale.GetSales(_orderid);
             
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Error:Add Sale Item");
            }
            return View("Index", cashdetails);


        }
        public async Task<IActionResult> DeleteSale(int orderItemId)
        {
            try
            {
                await _csale.DeleteCashSale(orderItemId, _comm.GetLoggedInUserId());
            }
            catch (Exception ex)
            {
                ModelState.AddModelError("Error", "Error:Delete Sale Item");
            }
            CashSaleVM cashdetails = new CashSaleVM();
            long orderid = await _csale.GetOderIdByOrderItemId(orderItemId);
            cashdetails = await _csale.GetSales(orderid);
            return View("Index", cashdetails);

        }
        public async Task<IActionResult> FinishSale(int orderId)
        {
            try
            {
            //    await _csale.FinishCashSale(orderId, _comm.GetLoggedInUserId());
            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Error:Finished Sale Item");
            }
            CashSaleVM cashdetails = new CashSaleVM();
            cashdetails = await _csale.GetSales(orderId);
            return View("Payment", cashdetails);

        }

       /* public async Task<IActionResult> AddSale(OrderPaymentVM paymentDetails)
        {
            OrderPaymentVM orderPayment = new OrderPaymentVM();
            Int64 _orderPaymentid = 0;
            ModelState.Clear();
            try
            {
                //_orderPaymentid = await _csale.AddCashSale(paymentDetails, _comm.GetLoggedInUserId());
                //cashdetails = await _csale.GetSales(_orderid);

            }
            catch (Exception ex)
            {

                ModelState.AddModelError("Error", "Error:Add Sale Item");
            }
            //return View("Index", cashdetails);


        } */
    }
}
