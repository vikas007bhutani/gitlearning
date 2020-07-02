using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SALEERP.ViewModel;
using SALEERP.Repository;
using SALEERP.Repository.Interface;
using SALEERP.Models;
using System.Collections;
using Microsoft.AspNetCore.Authorization;
using PagedList;
using SALEERP.Utility;

namespace SALEERP.Controllers
{
    public class PoolController : Controller
    {
        IPoolRepository _poll;
        ICommonRepository _comm;
        public PoolController(IPoolRepository _pollrepo, ICommonRepository comm)
        {
            this._poll = _pollrepo;
            this._comm = comm;
        }
        public IActionResult Index(string sortOrder, string searchString, int pn)
        {
            try
            {

                List<PoolVM> _allPools = new List<PoolVM>();
                if (ModelState.IsValid)
                {
                    ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                    ViewData["StartDateSortParm"] = sortOrder == "sdate" ? "sdate_desc" : "sdate";
                    ViewData["EndDateSortParm"] = sortOrder == "edate" ? "edate_desc" : "edate";

                    _allPools = _poll.getAllPools();
                    if (_allPools != null)
                    {
                        if (!String.IsNullOrEmpty(searchString))
                        {
                            _allPools = _allPools.Where(s => s.PoolName.Contains(searchString)).ToList();
                        }
                        switch (sortOrder)
                        {
                            case "name_desc":
                                _allPools = _allPools.OrderByDescending(s => s.PoolName).ToList();
                                break;
                            case "sdate":
                                _allPools = _allPools.OrderBy(s => s.PoolStartDate).ToList();
                                break;
                            case "sdate_desc":
                                _allPools = _allPools.OrderByDescending(s => s.PoolStartDate).ToList();
                                break;
                            case "edate":
                                _allPools = _allPools.OrderBy(s => s.PoolEndDate).ToList();
                                break;
                            case "edate_desc":
                                _allPools = _allPools.OrderByDescending(s => s.PoolEndDate).ToList();
                                break;
                            default:
                                _allPools = _allPools.OrderBy(s => s.PoolName).ToList();
                                break;
                        }
                        _allPools = _poll.getAllPools().OrderBy(d => d.PoolId).Skip((pn - 1) * 10).Take(10).ToList();
                        if (_allPools.Count > 0)
                        {
                            ViewBag.Paging = Set_Paging(pn, 10, _poll.getAllPools().Count(), "activeLink", Url.Action("Index", "User"), "disableLink");
                        }
                    }
                    else { ModelState.AddModelError(string.Empty, "No Records Found!!!"); } 
                }


                return View(_allPools);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind("PoolName,PoolDesc,PoolStartDate,PoolEndDate")] PoolVM _pool)
        {
            if (ModelState.IsValid)
            {
                string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                   .Select(c => c.Value).SingleOrDefault();
                bool result = this._poll.AddPool(_pool, Convert.ToInt32(userid));
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Username/Password did not matched.Invalid Login!");
                }


            }
            return RedirectToAction("Index", _poll.getAllPools());

        }

        [HttpPost]
        public PartialViewResult ShowCreatePartailView()
        {
            return PartialView("ManagePool");
        }
        public ActionResult ShowEditPartailView(Int64 id)
        {
            PoolVM result = new PoolVM();
            if (ModelState.IsValid)
            {
                result = this._poll.EditPool(id);

            }
            //return View(staff);
            return PartialView("UpdatePool", result);
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddPool(PoolVM _pollmodel)
        {
            List<PoolVM> _allPools = new List<PoolVM>();
            try
            {
               
                if (ModelState.IsValid)
                {
                    _allPools = _poll.getAllPools();
                  //  return View("Index", this._comm.getroles());
                }


            }
            catch (Exception)
            {

                throw;
            }
            return View("Index", _allPools);
        }
       
        public ActionResult GetPoolDeleted(int id)
        {
            bool result;
            if (ModelState.IsValid)
            {
                result = this._poll.DeletePool(id);
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Poolid not found!Contact Administrator");
                }

            }
            //return View(staff);
            return RedirectToAction("Index", _poll.getAllPools());
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind("PoolId,PoolName,PoolDesc,PoolStartDate,PoolEndDate")] PoolVM _pool)
        {
            if (ModelState.IsValid)
            {
                string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                  .Select(c => c.Value).SingleOrDefault();
                bool result = this._poll.UpdatePool(_pool, Convert.ToInt32(userid));
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Username/Password did not matched.Invalid Login!");
                }


            }
           // return View("Index", _allPools);
            return RedirectToAction("Index", _poll.getAllPools());

        }
        public string Set_Paging(int PageNumber, int PageSize, Int64 TotalRecords, string ClassName, string PageUrl, string DisableClassName)
        {
            string ReturnValue = "";
            try
            {
                Int64 TotalPages = Convert.ToInt64(Math.Ceiling((double)TotalRecords / PageSize));
                if (PageNumber > 1)
                {
                    if (PageNumber == 2)
                        ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim() + "?pn=" + Convert.ToString(PageNumber - 1) + "' class='" + ClassName + "'>Previous</a>   ";
                    else
                    {
                        ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim();
                        if (PageUrl.Contains("?"))
                            ReturnValue = ReturnValue + "&";
                        else
                            ReturnValue = ReturnValue + "?";
                        ReturnValue = ReturnValue + "pn=" + Convert.ToString(PageNumber - 1) + "' class='" + ClassName + "'>Previous</a>   ";
                    }
                }
                else
                    ReturnValue = ReturnValue + "<span class='" + DisableClassName + "'>Previous</span>   ";
                if ((PageNumber - 3) > 1)
                    ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim() + "' class='" + ClassName + "'>1</a> ..... | ";
                for (int i = PageNumber - 3; i <= PageNumber; i++)
                    if (i >= 1)
                    {
                        if (PageNumber != i)
                        {
                            ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim();
                            if (PageUrl.Contains("?"))
                                ReturnValue = ReturnValue + "&";
                            else
                                ReturnValue = ReturnValue + "?";
                            ReturnValue = ReturnValue + "pn=" + i.ToString() + "' class='" + ClassName + "'>" + i.ToString() + "</a> | ";
                        }
                        else
                        {
                            ReturnValue = ReturnValue + "<span style='font-weight:bold;'>" + i + "</span> | ";
                        }
                    }
                for (int i = PageNumber + 1; i <= PageNumber + 3; i++)
                    if (i <= TotalPages)
                    {
                        if (PageNumber != i)
                        {
                            ReturnValue = ReturnValue + "<a href='" + PageUrl.Trim();
                            if (PageUrl.Contains("?"))
                                ReturnValue = ReturnValue + "&";
                            else
                                ReturnValue = ReturnValue + "?";
                            ReturnValue = ReturnValue + "pn=" + i.ToString() + "' class='" + ClassName + "'>" + i.ToString() + "</a> | ";
                        }
                        else
                        {
                            ReturnValue = ReturnValue + "<span style='font-weight:bold;'>" + i + "</span> | ";
                        }
                    }
                if ((PageNumber + 3) < TotalPages)
                {
                    ReturnValue = ReturnValue + "..... <a href='" + PageUrl.Trim();
                    if (PageUrl.Contains("?"))
                        ReturnValue = ReturnValue + "&";
                    else
                        ReturnValue = ReturnValue + "?";
                    ReturnValue = ReturnValue + "pn=" + TotalPages.ToString() + "' class='" + ClassName + "'>" + TotalPages.ToString() + "</a>";
                }
                if (PageNumber < TotalPages)
                {
                    ReturnValue = ReturnValue + "   <a href='" + PageUrl.Trim();
                    if (PageUrl.Contains("?"))
                        ReturnValue = ReturnValue + "&";
                    else
                        ReturnValue = ReturnValue + "?";
                    ReturnValue = ReturnValue + "pn=" + Convert.ToString(PageNumber + 1) + "' class='" + ClassName + "'>Next</a>";
                }
                else
                    ReturnValue = ReturnValue + "   <span class='" + DisableClassName + "'>Next</span>";
            }
            catch (Exception ex)
            {
            }
            return (ReturnValue);
        }
    }
}