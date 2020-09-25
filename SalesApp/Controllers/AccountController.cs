using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using SalesApp.Repository.Interface;
using SalesApp.ViewModel;

namespace SalesApp.Controllers
{
    public class AccountController : Controller
    {
       
            IAccountRepository _acc;
            ICommonRepository _comm;
            public AccountController(IAccountRepository _accrepo, ICommonRepository comm)
            {
                this._acc = _accrepo;
                this._comm = comm;

            }
            public async Task<IActionResult> Index()
            {
            UserLoginVM _init= new UserLoginVM();
            
            try
                {
                    if (ModelState.IsValid)
                    {
                    _init.loginroles =await this._comm.getroles();
                    }
                }
                catch (Exception)
                {

                    throw;
                }
                return View(_init);
            }
            public async Task<IActionResult> Login(UserLoginVM _usr)
            {
                int userid = 0;
                try
                {
                    if (ModelState.IsValid)
                    {
                        bool result = this._acc.ManageUser(_usr, out userid);
                        if (result)
                        {
                            var uClaims = new List<Claim>()
                {
                new Claim(ClaimTypes.Name, _usr.UserName),
                new Claim(ClaimTypes.PrimarySid, userid.ToString()),
                 };

                        var UIdentity = new ClaimsIdentity(uClaims, "User Identity");

                            var uPrincipal = new ClaimsPrincipal(new[] { UIdentity });
                            await HttpContext.SignInAsync(uPrincipal);
                        //---
                        _comm.SetLoggedInUserId(userid);
                        _comm.SetLoggedInUserName (_usr.UserName);
                        // userid = result.UserId;
                        return RedirectToAction("Index", "Mirror");
                        }
                        else { ModelState.AddModelError(string.Empty, "Username did not matched.Invalid Login!"); }


                    }

                }

                catch (Exception)
                {
                    throw;
                }
                return View("Index",null);
            }

            public async Task<IActionResult> Logout()
            {
                await HttpContext.SignOutAsync(
        CookieAuthenticationDefaults.AuthenticationScheme);
                return View();
            }
        }
    
}
