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
    public class UserController : Controller
    {
        IUserRepository _usr;
        ICommonRepository _comm;
        public UserController(IUserRepository _usrrepo, ICommonRepository comm)
        {
            this._usr = _usrrepo;
            this._comm = comm;
          

        }
        [Authorize]
        //public  IActionResult Index()
        //{
        //    List<UserLoginVM> _alluser = _usr.getAllUsers();

        //    return View(_alluser);
        //}

        public async  Task<IActionResult> Index(string sortOrder,string searchString, int pn)
        {

            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewData["DateSortParm"] = sortOrder == "role" ? "role_desc" : "role";
            List<UserLoginVM> _alluser = new List<UserLoginVM>();
          
            _alluser = _usr.getAllUsers();
            
            if (!String.IsNullOrEmpty(searchString))
            {
                _alluser = _alluser.Where(s => s.UserName.Contains(searchString)
                                       || s.UserEmail.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    _alluser = _alluser.OrderByDescending(s => s.UserName).ToList();
                    break;
                case "role":
                    _alluser = _alluser.OrderBy(s => s.RoleName).ToList();
                    break;
                case "role_desc":
                    _alluser = _alluser.OrderByDescending(s => s.RoleName).ToList();
                    break;
                default:
                    _alluser = _alluser.OrderBy(s => s.UserName).ToList();
                    break;
            }
            _alluser = _usr.getAllUsers().OrderBy(d => d.UserId).Skip((pn - 1) * 10).Take(10).ToList();
            ViewBag.Paging = Set_Paging(pn, 10, _usr.getAllUsers().Count(), "activeLink", Url.Action("Index", "User"), "disableLink");
            
           
            return View(_alluser);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind("UserName,UserPass,UserEmail,RoleId")] UserLoginVM _user)
        {
            if (ModelState.IsValid)
            {
                string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                   .Select(c => c.Value).SingleOrDefault();
                bool result = this._usr.AddUser(_user,Convert.ToInt32(userid));
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Username/Password did not matched.Invalid Login!");
                }
               

            }
            return RedirectToAction("Index", _usr.getAllUsers());

        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind("UserId,UserName,UserPass,UserEmail,RoleId")] UserLoginVM _user)
        {
            if (ModelState.IsValid)
            {
                string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                  .Select(c => c.Value).SingleOrDefault();
                bool result = this._usr.UpdateUser(_user, Convert.ToInt32(userid));
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Userid not found!");
                }


            }
            return RedirectToAction("Index", _usr.getAllUsers());

        }
        [HttpPost]
        public PartialViewResult ShowCreatePartailView()
        {
            return PartialView("ManageUser",this._comm.getroles());
        }
        [HttpPost]
        public ActionResult ShowEditPartailView(int id)
        {
            UserLoginVM result = new UserLoginVM();
            if (ModelState.IsValid)
            {
               result  = this._usr.EditUser(id);
               
            }
            //return View(staff);
            return PartialView("UpdateUser", result);
        }
        public ActionResult GetUserDeleted(int id)
        {
            bool result;
            if (ModelState.IsValid)
            {
                result = this._usr.DeleteUser(id);
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Userid not found!Contact Administrator");
                }

            }
            //return View(staff);
            return RedirectToAction("Index", _usr.getAllUsers());
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