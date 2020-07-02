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
namespace SALEERP.Controllers
{
    public class CommissionController : Controller
    {
        IcommissionRepository _commission;
        ICommonRepository _comm;
        private Sales_ERPContext _DBERP;
        public CommissionController(IcommissionRepository _commissionrepo, ICommonRepository comm, Sales_ERPContext dbcontext)
        {
            this._commission = _commissionrepo;
            this._comm = comm;
            this._DBERP = dbcontext;
        }
        public IActionResult Index(CommissionVM cm)
        {

            return View(_commission.Init_commission());
        }
        public IActionResult GetMirror(string unitid, DateTime fromdate,DateTime Todate)
        {
            CommissionVM _alluser = new CommissionVM();
            _alluser.unitid =Convert.ToInt16( unitid);
            _alluser.fromdate = fromdate;
            _alluser.Todate = Todate;
            //string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
            //      .Select(c => c.Value).SingleOrDefault();
            //_agntusr.AddUser(_details, Convert.ToInt32(userid));

            //_alluser = _agntusr.getAllAgentUsers();
            return View("Index", _commission.getAllMirorforCommission(_alluser));
        }
        public IActionResult Pay()
        {

            return View("PayIndex", _commission.Init_paycommission());
        }
        public ActionResult ShowEditPartailView(int id)
        {
            CommissionVM result = new CommissionVM();
            if (ModelState.IsValid)
            {
                result = this._commission.EditCommission(id);
                //   result.bdetails = _comm.getBanks();


            }
            //return View(staff);
            return PartialView("AddCommission", result);
        }
        public IActionResult AddCommission(CommissionVM _details)
        {
            CommissionVM _allmirror = new CommissionVM();
            string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                   .Select(c => c.Value).SingleOrDefault();
            _commission.AddCommission(_details, Convert.ToInt32(userid));

            return View("Index", _allmirror);
        }

        public async Task<IActionResult> Demo_Search()
        {
            List<SearchVM> newuser = new List<SearchVM>();

            try
            {
                // var result1 = null;
                string term = HttpContext.Request.Query["term"].ToString();
                string agentcode = HttpContext.Request.Query["agentcode"].ToString();
                // var result = _srch.Driver_Search(term);
                var result = await _DBERP.AgentUser.Where(c => c.Name.StartsWith(term) && c.Code == agentcode).Select(a => new { label = a.Name, id = a.Id }).ToListAsync();
                if (result.Count() == 0)
                {
                    newuser.Add(new SearchVM { label = (""), id = 0 });
                    // "{label="+(term+new Guid().ToString())+",id=0 }";
                    return Ok(newuser);
                }
                else
                { return Ok(result); }

                // _auvm = await _mir.GetNames(term);
                //return _auvm;
            }
            catch
            {
                return null;
            }
        }

        public IActionResult GetCommission(string agentid, DateTime fromdate, DateTime Todate)
        {
            PayCommissionVM _alluser = new PayCommissionVM();
            _alluser.AgentId = Convert.ToInt16(agentid);
            _alluser.fromdate = fromdate;
            _alluser.Todate = Todate;
            //string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
            //      .Select(c => c.Value).SingleOrDefault();
            //_agntusr.AddUser(_details, Convert.ToInt32(userid));
                
            //_alluser = _agntusr.getAllAgentUsers();
            return View("PayIndex", _commission.getAllAgentCommission(_alluser));
        }
        public ActionResult ShowEditPartailPayView( int id)
        {
            PayCommissionVM result = new PayCommissionVM();
            if (ModelState.IsValid)
            {
                result = this._commission.PayCommission(id);
                //   result.bdetails = _comm.getBanks();


            }
            //return View(staff);
            return PartialView("PayCommission", result);
        }
        public IActionResult UpdateCommission(PayCommissionVM _details)
        {
            //PayCommissionVM _allmirror = new PayCommissionVM();
            string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                   .Select(c => c.Value).SingleOrDefault();
            _commission.UpdateCommission(_details, Convert.ToInt32(userid));

            return View("payIndex", _commission.Init_paycommission());
        }
    }
}