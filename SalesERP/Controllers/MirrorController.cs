using System;
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
using Microsoft.Extensions.Configuration;
using System.Net;

namespace SALEERP.Controllers
{
    public class MirrorController : Controller
    {
        IMirrorRepository _mir;
        ICommonRepository _comm;
        ISearchRepository _srch;
        private Sales_ERPContext _DBERP;
        public IConfiguration Configuration { get; }
        public MirrorController(IMirrorRepository _seriesrepo, ICommonRepository comm,ISearchRepository _search, Sales_ERPContext dbcontext, IConfiguration _config)
        {
            this._mir = _seriesrepo;
            this._comm = comm;
            this._srch = _search;
            this._DBERP = dbcontext;
            this.Configuration = _config;
        }
        public async Task<IActionResult> Index()
        {
           // _mir = _mir.getAllAgentUsers();
            return View(await _mir.getAllMirrors());
        }

        public async Task<IActionResult> Add(MirrorDetailsVM _details)
        {
            MirrorDetailsVM _allmirror = new MirrorDetailsVM();
            var addlist = Dns.GetHostEntry(Dns.GetHostName());
            string unit1ip = Configuration.GetSection("IPSettings").GetSection("UNIT1").Value;
            string unit2ip = Configuration.GetSection("IPSettings").GetSection("UNIT2").Value;
            string unit12ip = Configuration.GetSection("IPSettings").GetSection("UNIT21").Value;
            string unit21ip = Configuration.GetSection("IPSettings").GetSection("UNIT12").Value;
            //   string GetHostName = addlist.HostName.ToString();
            // string GetIPV6 = addlist.AddressList[0].ToString();
            string clientIP = addlist.AddressList[1].ToString();


          
            if (unit1ip == clientIP || unit12ip==clientIP)
            {
                _details.UnitId = 2;

            }
            else if (unit2ip == clientIP || unit21ip == clientIP)
            {
                _details.UnitId = 2;

            }
            else
            { _details.UnitId = 1; }
            try
            {
            string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                   .Select(c => c.Value).SingleOrDefault();
            await _mir.AddMirror(_details, Convert.ToInt32(userid));
            }
            catch (Exception e)
            {
                ModelState.AddModelError(e.Message, e.Message);

                return View(_mir.getAllMirrors());
                // throw;
            }

            return RedirectToAction("Index", _allmirror);
        }
        public ActionResult ShowEditPartailView(int id)
        {
            MirrorDetailsVM result = new MirrorDetailsVM();
            if (ModelState.IsValid)
            {
                result = this._mir.EditMirror(id);
             //   result.bdetails = _comm.getBanks();


            }
            //return View(staff);
            return PartialView("UpdateMirror", result);
        }
        public async Task<ActionResult> UpdateMirror(MirrorDetailsVM _mr)
        {
            MirrorDetailsVM _allmirror = new MirrorDetailsVM();
            try
            {


                if (ModelState.IsValid)
                {
                    string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                      .Select(c => c.Value).SingleOrDefault();
                    bool result =await _mir.UpdateMirror(_mr, Convert.ToInt32(userid));
                    if (!result)
                    {
                        ModelState.AddModelError(string.Empty, "Username/Password did not matched.Invalid Login!");
                    }


                }
            }
            catch (Exception e)
            {

                ModelState.AddModelError(e.Message, e.Message);

                return View(_mir.getAllMirrors());
            }
            // return View("Index", _allPools);
            return RedirectToAction("Index", _allmirror);

        }
        public async Task<IActionResult> Driver_Search()
        {
            List<SearchVM> newuser = new List<SearchVM>();
            try
            {
                string term = HttpContext.Request.Query["term"].ToString();
                // var result = _srch.Driver_Search(term);
                var result = await _DBERP.AgentUser.Where(c => c.IsActive==true &&( c.Name.StartsWith(term)  || c.AgentContact.Select(c=>c.Mobile==term).FirstOrDefault()) && c.Code == "dr" ).Select(a => new { label = a.Name+"("+a.AgentContact.FirstOrDefault().Mobile+")", id = a.Id }).ToListAsync();
                if (result.Count() == 0)
                {
                    newuser.Add(new SearchVM { label = (term + GenerateRandomNo()), id = 0 });
                    // "{label="+(term+new Guid().ToString())+",id=0 }";
                    return Ok(newuser);
                }
                else
                { return Ok(result); }
            }
            catch
            {
                return null;
            }
        }
        public async Task<IActionResult> Principle_Search()
        {
            List<SearchVM> newuser = new List<SearchVM>();

            try
            {
               // var result1 = null;
                string term = HttpContext.Request.Query["term"].ToString();
                // var result = _srch.Driver_Search(term);
                var result = await _DBERP.AgentUser.Where(c => c.IsActive == true && (c.Name.StartsWith(term) || c.AgentContact.Select(c => c.Mobile == term).FirstOrDefault()) && c.Code == "pi").Select(a => new { label = a.Name + "(" + a.AgentContact.FirstOrDefault().Mobile + ")", id = a.Id }).ToListAsync();
               // var result = await _DBERP.AgentUser.Where(c => c.Name.StartsWith(term) && c.Code == "pi").Select(a => new { label = a.Name, id = a.Id }).ToListAsync();
                if(result.Count() ==0)
                {
                   newuser.Add(new SearchVM { label = (term + GenerateRandomNo()), id = 0 });
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
        public async Task<IActionResult> EAgent_Search()
        {
            List<SearchVM> newuser = new List<SearchVM>();

            try
            {
                // var result1 = null;
                string term = HttpContext.Request.Query["term"].ToString();
                // var result = _srch.Driver_Search(term);
                var result = await _DBERP.AgentUser.Where(c => c.IsActive == true && (c.Name.StartsWith(term) || c.AgentContact.Select(c => c.Mobile == term).FirstOrDefault()) && c.Code == "ex").Select(a => new { label = a.Name + "(" + a.AgentContact.FirstOrDefault().Mobile + ")", id = a.Id }).ToListAsync();
              //  var result = await _DBERP.AgentUser.Where(c => c.Name.StartsWith(term) && c.Code == "ex").Select(a => new { label = a.Name, id = a.Id }).ToListAsync();
                if (result.Count() == 0)
                {
                    newuser.Add(new SearchVM { label = (term + GenerateRandomNo()), id = 0 });
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
        public async Task<IActionResult> Guide_Search()
        {
            List<SearchVM> newuser = new List<SearchVM>();

            try
            {
                // var result1 = null;
                string term = HttpContext.Request.Query["term"].ToString();
                // var result = _srch.Driver_Search(term);
                var result = await _DBERP.AgentUser.Where(c => c.IsActive == true && (c.Name.StartsWith(term) || c.AgentContact.Select(c => c.Mobile == term).FirstOrDefault()) && c.Code == "gu").Select(a => new { label = a.Name + "(" + a.AgentContact.FirstOrDefault().Mobile + ")", id = a.Id }).ToListAsync();
                //  var result = await _DBERP.AgentUser.Where(c => c.Name.StartsWith(term) && c.Code == "gu").Select(a => new { label = a.Name, id = a.Id }).ToListAsync();
                if (result.Count() == 0)
                {
                    newuser.Add(new SearchVM { label = (term + GenerateRandomNo()), id = 0 });
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
        public async Task<IActionResult> Lead_Search()
        {
            List<SearchVM> newuser = new List<SearchVM>();

            try
            {
                // var result1 = null;
                string term = HttpContext.Request.Query["term"].ToString();
                // var result = _srch.Driver_Search(term);
                var result = await _DBERP.AgentUser.Where(c => c.IsActive == true && (c.Name.StartsWith(term) || c.AgentContact.Select(c => c.Mobile == term).FirstOrDefault()) && c.Code == "le").Select(a => new { label = a.Name + "(" + a.AgentContact.FirstOrDefault().Mobile + ")", id = a.Id }).ToListAsync();
                // var result = await _DBERP.AgentUser.Where(c => c.Name.StartsWith(term) && c.Code == "le").Select(a => new { label = a.Name, id = a.Id }).ToListAsync();
                if (result.Count() == 0)
                {
                    newuser.Add(new SearchVM { label = (term + GenerateRandomNo()), id = 0 });
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
        public async Task<IActionResult> Escort_Search()
        {
            List<SearchVM> newuser = new List<SearchVM>();

            try
            {
                // var result1 = null;
                string term = HttpContext.Request.Query["term"].ToString();
                // var result = _srch.Driver_Search(term);
                var result = await _DBERP.AgentUser.Where(c => c.IsActive == true && (c.Name.StartsWith(term) || c.AgentContact.Select(c => c.Mobile == term).FirstOrDefault()) && c.Code == "ec").Select(a => new { label = a.Name + "(" + a.AgentContact.FirstOrDefault().Mobile + ")", id = a.Id }).ToListAsync();
                //    var result = await _DBERP.AgentUser.Where(c => c.Name.StartsWith(term) && c.Code == "ec").Select(a => new { label = a.Name, id = a.Id }).ToListAsync();
                if (result.Count() == 0)
                {
                    newuser.Add(new SearchVM { label = (term + GenerateRandomNo()), id = 0 });
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
        public async Task<IActionResult> Demo_Search()
        {
            List<SearchVM> newuser = new List<SearchVM>();

            try
            {
                // var result1 = null;
                string term = HttpContext.Request.Query["term"].ToString();
                // var result = _srch.Driver_Search(term);
                var result = await _DBERP.AgentUser.Where(c => c.IsActive == true && (c.Name.StartsWith(term) || c.AgentContact.Select(c => c.Mobile == term).FirstOrDefault()) && c.Code == "de").Select(a => new { label = a.Name + "(" + a.AgentContact.FirstOrDefault().Mobile + ")", id = a.Id }).ToListAsync();
             //   var result = await _DBERP.AgentUser.Where(c => c.Name.StartsWith(term) && c.Code == "de").Select(a => new { label = a.Name, id = a.Id }).ToListAsync();
                if (result.Count() == 0)
                {
                    newuser.Add(new SearchVM { label = (term + GenerateRandomNo()), id = 0 });
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

        public ActionResult GetMirrorDeleted(int id)
        {
            bool result;
            if (ModelState.IsValid)
            {
                string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                   .Select(c => c.Value).SingleOrDefault();
                result = this._mir.DeleteMirror(id,Convert.ToInt32(userid));
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Mirrorid not found!Contact Administrator");
                }

            }
            //return View(staff);
            return RedirectToAction("Index", _mir.getAllMirrors());
        }

        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
    }
}