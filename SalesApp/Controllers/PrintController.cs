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
    public class PrintController : Controller
    {
        IPrintRepository _prnt;
        private Sales_ERPContext _DBERP;
        public PrintController(IPrintRepository _print, Sales_ERPContext dbcontext)
        {
            this._prnt = _print;
            this._DBERP = dbcontext;
          
        }
        public IActionResult Index()
        {
            return View(_prnt.getAllOrders());
          
        }

    }
}
