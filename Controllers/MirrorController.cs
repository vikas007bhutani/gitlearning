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

namespace SALEERP.Controllers
{
    public class MirrorController : Controller
    {
        IMirrorRepository _mir;
        ICommonRepository _comm;
        ISearchRepository _srch;
        private Sales_ERPContext _DBERP;
        public MirrorController(IMirrorRepository _seriesrepo, ICommonRepository comm,ISearchRepository _search, Sales_ERPContext dbcontext)
        {
            this._mir = _seriesrepo;
            this._comm = comm;
            this._srch = _search;
            this._DBERP = dbcontext;
        }
        public IActionResult Index()
        {
           // _mir = _mir.getAllAgentUsers();
            return View(_mir.getAllMirrors());
        }

        public IActionResult Add(MirrorDetailsVM _details)
        {
            MirrorDetailsVM _allmirror = new MirrorDetailsVM();
            string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                   .Select(c => c.Value).SingleOrDefault();
            _mir.AddMirror(_details, Convert.ToInt32(userid));

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
        public ActionResult UpdateMirror(MirrorDetailsVM _mr)
        {
            MirrorDetailsVM _allmirror = new MirrorDetailsVM();
            if (ModelState.IsValid)
            {
                string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                  .Select(c => c.Value).SingleOrDefault();
                bool result = _mir.UpdateMirror(_mr, Convert.ToInt32(userid));
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Username/Password did not matched.Invalid Login!");
                }


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
                var result = await _DBERP.AgentUser.Where(c => c.Name.StartsWith(term) && c.AgentCode == "dr").Select(a => new { label = a.Name+"mob:-"+a.AgentContact.FirstOrDefault().Mobile, id = a.AgentId }).ToListAsync();
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
                var result = await _DBERP.AgentUser.Where(c => c.Name.StartsWith(term) && c.AgentCode == "pi").Select(a => new { label = a.Name, id = a.AgentId }).ToListAsync();
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
                var result = await _DBERP.AgentUser.Where(c => c.Name.StartsWith(term) && c.AgentCode == "ex").Select(a => new { label = a.Name, id = a.AgentId }).ToListAsync();
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
                var result = await _DBERP.AgentUser.Where(c => c.Name.StartsWith(term) && c.AgentCode == "gu").Select(a => new { label = a.Name, id = a.AgentId }).ToListAsync();
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
                var result = await _DBERP.AgentUser.Where(c => c.Name.StartsWith(term) && c.AgentCode == "le").Select(a => new { label = a.Name, id = a.AgentId }).ToListAsync();
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
                var result = await _DBERP.AgentUser.Where(c => c.Name.StartsWith(term) && c.AgentCode == "ec").Select(a => new { label = a.Name, id = a.AgentId }).ToListAsync();
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
                var result = await _DBERP.AgentUser.Where(c => c.Name.StartsWith(term) && c.AgentCode == "dm").Select(a => new { label = a.Name, id = a.AgentId }).ToListAsync();
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

        public int GenerateRandomNo()
        {
            int _min = 1000;
            int _max = 9999;
            Random _rdm = new Random();
            return _rdm.Next(_min, _max);
        }
    }
}