using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SALEERP.ViewModel;
using SALEERP.Repository;
using SALEERP.Repository.Interface;
using SALEERP.Models;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace SALEERP.Controllers
{
    public class AccountController : Controller
    {
         IAccountRepository _acc;
        ICommonRepository _comm;
        public AccountController(IAccountRepository _accrepo,ICommonRepository comm)
        {
            this._acc = _accrepo;
            this._comm = comm;

        }
        public IActionResult Index()
        {
            try
            {
                if (ModelState.IsValid)
                {
                   // this._comm.getroles();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View(this._comm.getroles());
        }
        public async Task<IActionResult>  Login(UserLoginVM _usr)
        {
            int userid = 0;
            try
            {
                if (ModelState.IsValid)
                {
                    bool result = this._acc.ManageUser(_usr,out userid);
                    if (result)
                    {
                        var userClaims = new List<Claim>()
                {
                new Claim(ClaimTypes.Name, _usr.UserName),
                new Claim(ClaimTypes.PrimarySid, userid.ToString()),
                 };

                        var SalesIdentity = new ClaimsIdentity(userClaims, "User Identity");

                        var userPrincipal = new ClaimsPrincipal(new[] { SalesIdentity });
                        await HttpContext.SignInAsync(userPrincipal);
                        // userid = result.UserId;
                        return RedirectToAction("Index", "Dashboard");
                    }
                    else { ModelState.AddModelError(string.Empty, "Username/Password did not matched.Invalid Login!"); }


                }
                
            }

            catch (Exception)
            {
                throw;
            }
            return  View("Index", this._comm.getroles());
        }
      
            public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(
    CookieAuthenticationDefaults.AuthenticationScheme);
            return View();
        }
    }
}