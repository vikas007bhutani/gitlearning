using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalesApp.ViewModel;
using SalesApp.Repository;
using SalesApp.Repository.Interface;
using SALEERP.Data;
using Microsoft.EntityFrameworkCore;

namespace SalesApp.Controllers
{
    public class MirrorController : Controller
    {
        IMirrorRepository _mir;
        private Sales_ERPContext _DBERP;
        public MirrorController(IMirrorRepository _seriesrepo, Sales_ERPContext dbcontext)
        {
            this._mir = _seriesrepo;
            this._DBERP = dbcontext;
        }
        public IActionResult Index()
        {
            // _mir = _mir.getAllAgentUsers();
            //return View(_mir.getAllMirrors());
            return View(_mir.getAllMirrors());
        }

        public IActionResult AddCashSale()
        {
            return View();
        }
        public IActionResult AddSale()
        {
            return View();
        }
    }
}