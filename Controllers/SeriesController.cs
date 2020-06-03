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
    public class SeriesController : Controller
    {
        ISeriesRepository _series;
        ICommonRepository _comm;
        public SeriesController(ISeriesRepository _seriesrepo, ICommonRepository comm)
        {
            this._series = _seriesrepo;
            this._comm = comm;
        }
        public IActionResult Index(string sortOrder, string searchString, int pn)
        {
            try
            {

                List<SeriesVM> _allSeries = new List<SeriesVM>();
                if (ModelState.IsValid)
                {
                    ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";


                    _allSeries = _series.getAllSeries();
                    if (_allSeries != null)
                    {
                        if (!String.IsNullOrEmpty(searchString))
                        {
                            _allSeries = _allSeries.Where(s => s.SeriesName.Contains(searchString)).ToList();
                        }
                        switch (sortOrder)
                        {
                            case "name_desc":
                                _allSeries = _allSeries.OrderByDescending(s => s.SeriesName).ToList();
                                break;
                            default:
                                _allSeries = _allSeries.OrderBy(s => s.SeriesName).ToList();
                                break;
                        }
                        _allSeries = _series.getAllSeries().OrderBy(d => d.SeriesId).Skip((pn - 1) * 10).Take(10).ToList();
                        if (_allSeries.Count > 0)
                        {
                            ViewBag.Paging = Set_Paging(pn, 10, _series.getAllSeries().Count(), "activeLink", Url.Action("Index", "User"), "disableLink");
                        }
                    }
                    else { ModelState.AddModelError(string.Empty, "No Records Found!!!"); }
                }


                return View(_allSeries);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind("SeriesId,SeriesName,SeriesCode")] SeriesVM _ser)
        {
            if (ModelState.IsValid)
            {
                string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                   .Select(c => c.Value).SingleOrDefault();
                bool result = this._series.AddSeries(_ser, Convert.ToInt32(userid));
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Username/Password did not matched.Invalid Login!");
                }


            }
            return RedirectToAction("Index", _series.getAllSeries());

        }

        [HttpPost]
        public PartialViewResult ShowCreatePartailView()
        {
            return PartialView("ManageSeries");
        }
        public ActionResult ShowEditPartailView(int id)
        {
            SeriesVM result = new SeriesVM();
            if (ModelState.IsValid)
            {
                result = this._series.EditSeries(id);

            }
            //return View(staff);
            return PartialView("UpdateSeries", result);
        }
       

        public ActionResult GetSeriesDeleted(int id)
        {
            bool result;
            if (ModelState.IsValid)
            {
                result = this._series.DeleteSeries(id);
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Series id not found!Contact Administrator");
                }

            }
            //return View(staff);
            return RedirectToAction("Index", _series.getAllSeries());
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind("SeriesId,SeriesName,SeriesCode")] SeriesVM _ser)
        {
            if (ModelState.IsValid)
            {
                string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                  .Select(c => c.Value).SingleOrDefault();
                bool result = this._series.UpdateSeries(_ser, Convert.ToInt32(userid));
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Username/Password did not matched.Invalid Login!");
                }


            }
            // return View("Index", _allPools);
            return RedirectToAction("Index", _series.getAllSeries());

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