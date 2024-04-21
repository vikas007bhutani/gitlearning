﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SALEERP.ViewModel;
using SALEERP.Repository;
using SALEERP.Repository.Interface;
using System.Web.Http;
using SALEERP.Data;
using Microsoft.EntityFrameworkCore;
using System.Net;
using System.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Hosting;


namespace SALEERP.Controllers
{
    public class SaleController : Controller
    {
        public IConfiguration Configuration { get; }
        private readonly ISaleRepository _sale;
        private readonly ICommonRepository _comm;
        IWebHostEnvironment _hostingenv;
        public SaleController(ISaleRepository _sale, ICommonRepository commonRepository, IWebHostEnvironment _env, IConfiguration _config)
        {
            this._sale = _sale;
            this._comm = commonRepository;
            //   _honstingEnvironment = hostingEnvironment;
            this._hostingenv = _env;
            this.Configuration = _config;
           // this._logger = logger;
        }
        public IActionResult Index(SaleVM _salevm)
        {
            var addlist = Dns.GetHostEntry(Dns.GetHostName());
            string unit1ip = Configuration.GetSection("IPSettings").GetSection("UNIT1").Value;
            string unit2ip = Configuration.GetSection("IPSettings").GetSection("UNIT2").Value;
            //   string GetHostName = addlist.HostName.ToString();
            // string GetIPV6 = addlist.AddressList[0].ToString();
            string clientIP = addlist.AddressList[1].ToString();


            _salevm = _sale.Init_commission();
            if (unit1ip == clientIP)
            {
                _salevm.unitid = 1;

            }
            else if (unit2ip == clientIP)
            {
                _salevm.unitid = 2;

            }
            return View(_salevm);
        }
        public IActionResult GetMirror(string unitid, DateTime fromdate, DateTime Todate)
        {
            CommissionVM _filter = new CommissionVM();
            _filter.unitid = Convert.ToInt16(unitid);
            _filter.fromdate = fromdate.AddSeconds(1);
            _filter.Todate = Todate.AddHours(23).AddMinutes(59).AddSeconds(59);
            //string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
            //      .Select(c => c.Value).SingleOrDefault();
            //_agntusr.AddUser(_details, Convert.ToInt32(userid));

            //_alluser = _agntusr.getAllAgentUsers();
            return View("Index", _sale.getAllMirorforCommission(_filter));
        }
    }
}