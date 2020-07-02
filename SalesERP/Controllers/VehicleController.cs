using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SALEERP.ViewModel;
using SALEERP.Repository.Interface;
using Microsoft.AspNetCore.Authorization;

namespace SALEERP.Controllers
{
    public class VehicleController : Controller
    {
        IVehicleRepository _vehicle;
        ICommonRepository _comm;
        public VehicleController(IVehicleRepository _vehiclerepo, ICommonRepository comm)
        {
            this._vehicle = _vehiclerepo;
            this._comm = comm;
        }
        public IActionResult Index(string sortOrder, string searchString, int pn)
        {
            try
            {

                List<VehicleVM> _allVehicle = new List<VehicleVM>();
                if (ModelState.IsValid)
                {
                    ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";


                    _allVehicle = _vehicle.getAllVehicle();
                    if (_allVehicle != null)
                    {
                        if (!String.IsNullOrEmpty(searchString))
                        {
                            _allVehicle = _allVehicle.Where(s => s.VehicleType.Contains(searchString)).ToList();
                        }
                        switch (sortOrder)
                        {
                            case "name_desc":
                                _allVehicle = _allVehicle.OrderByDescending(s => s.VehicleType).ToList();
                                break;
                            default:
                                _allVehicle = _allVehicle.OrderBy(s => s.VehicleType).ToList();
                                break;
                        }
                        _allVehicle = _vehicle.getAllVehicle().OrderBy(d => d.VehicleId).Skip((pn - 1) * 10).Take(10).ToList();
                        if (_allVehicle.Count > 0)
                        {
                            ViewBag.Paging = Set_Paging(pn, 10, _vehicle.getAllVehicle().Count(), "activeLink", Url.Action("Index", "User"), "disableLink");
                        }
                    }
                    else { ModelState.AddModelError(string.Empty, "No Records Found!!!"); }
                }


                return View(_allVehicle);
            }
            catch (Exception)
            {

                throw;
            }
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind("VehicleId,VehicleType")] VehicleVM _ve)
        {
            if (ModelState.IsValid)
            {
                string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                   .Select(c => c.Value).SingleOrDefault();
                bool result = this._vehicle.AddVehicle(_ve, Convert.ToInt32(userid));
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Username/Password did not matched.Invalid Login!");
                }


            }
            return RedirectToAction("Index", _vehicle.getAllVehicle());

        }
     
        [HttpPost]
        public PartialViewResult ShowCreatePartailView()
        {
            return PartialView("ManageVehicle");
        }
        public ActionResult ShowEditPartailView(int id)
        {
            VehicleVM result = new VehicleVM();
            if (ModelState.IsValid)
            {
                result = this._vehicle.EditVehicle(id);

            }
            //return View(staff);
            return PartialView("UpdateVehicle", result);
        }

        public ActionResult GetVehicleDeleted(int id)
        {
            bool result;
            if (ModelState.IsValid)
            {
                result = this._vehicle.DeleteVehicle(id);
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "agent id not found!Contact Administrator");
                }

            }
            //return View(staff);
            return RedirectToAction("Index", _vehicle.getAllVehicle());
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind("VehicleId,VehicleType")] VehicleVM _ve)
        {
            if (ModelState.IsValid)
            {
                string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                  .Select(c => c.Value).SingleOrDefault();
                bool result = this._vehicle.UpdateVehicle(_ve, Convert.ToInt32(userid));
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Username/Password did not matched.Invalid Login!");
                }


            }
            // return View("Index", _allPools);
            return RedirectToAction("Index", _vehicle.getAllVehicle());

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