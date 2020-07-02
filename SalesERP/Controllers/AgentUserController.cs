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
    public class AgentUserController : Controller
    {
        IAgentUserRepository _agntusr;
        ICommonRepository _comm;
        Dictionary<string, String> Astatus = new Dictionary<string, String>
{
    { "dr", "UpdateDriver" },
     { "pi", "UpdatePrinciple" },
     { "te", "UpdateLeader" },
     { "ec", "UpdateEscort" },
     { "ex", "UpdateExcursion" },
     { "gu", "UpdateGuide" },

        };
        public AgentUserController(IAgentUserRepository _agentuserrepo, ICommonRepository comm)
        {
            this._agntusr = _agentuserrepo;
            this._comm = comm;


        }
        public IActionResult Index(string sortOrder, string searchString, int pn)
        {
            ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
          //  ViewData["DateSortParm"] = sortOrder == "role" ? "role_desc" : "role";
            AgentDetailsVM _alluser = new AgentDetailsVM();

            _alluser = _agntusr.getAllAgentUsers();

            if (!String.IsNullOrEmpty(searchString))
            {
                _alluser.AgentDetails = _alluser.AgentDetails.Where(s => s.Name.Contains(searchString)).ToList();
            }
            switch (sortOrder)
            {
                case "name_desc":
                    _alluser.AgentDetails = _alluser.AgentDetails.OrderByDescending(s => s.Name).ToList();
                    break;
                //case "role":
                //    _alluser = _alluser.OrderBy(s => s.RoleName).ToList();
                //    break;
                //case "role_desc":
                //    _alluser = _alluser.OrderByDescending(s => s.RoleName).ToList();
                //    break;
                default:
                    _alluser.AgentDetails = _alluser.AgentDetails.OrderBy(s => s.Name).ToList();
                    break;
            }
            _alluser.AgentDetails = _alluser.AgentDetails.OrderBy(d => d.AgentId).Skip((pn - 1) * 10).Take(10).ToList();
            ViewBag.Paging = Set_Paging(pn, 10, _alluser.AgentDetails.Count(), "activeLink", Url.Action("Index", "User"), "disableLink");


            return View("Index",_alluser);
            //return View();
        }

        public PartialViewResult Driver(string agentid)
        {
            AgentUserVM _agent = new AgentUserVM();
            _agent.vdetails = _comm.getvehicles();
            _agent.bdetails = _comm.getBanks();
            _agent.AgentCode = agentid;
            return PartialView("Driver", _agent);
        }
        public PartialViewResult Principle(string agentid)
        {
            AgentUserVM _agent = new AgentUserVM();
          //  _agent.vdetails = _comm.getvehicles();
            _agent.bdetails = _comm.getBanks();
            _agent.AgentCode = agentid;
            return PartialView("Principle", _agent);
        }
        public PartialViewResult Leader(string agentid)
        {
            AgentUserVM _agent = new AgentUserVM();
            //  _agent.vdetails = _comm.getvehicles();
            _agent.bdetails = _comm.getBanks();
            _agent.AgentCode = agentid;
            return PartialView("Leader", _agent);
        }
        public PartialViewResult Escort(string agentid)
        {
            AgentUserVM _agent = new AgentUserVM();
            //  _agent.vdetails = _comm.getvehicles();
            _agent.bdetails = _comm.getBanks();
            _agent.AgentCode = agentid;
            return PartialView("Escort", _agent);
        }
        public PartialViewResult Demo(string agentid)
        {
            AgentUserVM _agent = new AgentUserVM();
            //  _agent.vdetails = _comm.getvehicles();
            _agent.bdetails = _comm.getBanks();
            _agent.countrydetails = _comm.getcountry();
            _agent.AgentCode = agentid;
            return PartialView("Demo", _agent);
        }

        public IActionResult SaveDemo(AgentUserVM _details)
        {
            AgentDetailsVM _alluser = new AgentDetailsVM();
            string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                  .Select(c => c.Value).SingleOrDefault();
            _agntusr.AddDemo(_details, Convert.ToInt32(userid));

            _alluser = _agntusr.getAllAgentUsers();
            return RedirectToAction("Index");
        }
        public PartialViewResult Excursion(string agentid)
        {
            AgentUserVM _agent = new AgentUserVM();
            //  _agent.vdetails = _comm.getvehicles();
            _agent.bdetails = _comm.getBanks();
            _agent.AgentCode = agentid;
            return PartialView("Excursion", _agent);
        }
        public IActionResult SaveDriver(AgentUserVM _details)
        {
            AgentDetailsVM _alluser = new AgentDetailsVM();
            string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                  .Select(c => c.Value).SingleOrDefault();
            _agntusr.AddUser(_details, Convert.ToInt32(userid));
            
            _alluser = _agntusr.getAllAgentUsers();
            return RedirectToAction("Index");
        }
        public ActionResult GetAgentDeleted(int id)
        {
            bool result;
            if (ModelState.IsValid)
            {
                result = this._agntusr.DeleteAgent(id);
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "agent id not found!Contact Administrator");
                }

            }
            //return View(staff);
            return View("Index", _agntusr.getAllAgentUsers());
        }

        public ActionResult ShowEditAgent(int id,string code)
        {
            string ViewName = string.Empty;
            AgentUserVM result = new AgentUserVM();
            if (ModelState.IsValid)

            {
                result = this._agntusr.EditAgent(id);
                result.bdetails = _comm.getBanks();


            }
           
          //  ViewName=
            //return View(staff);
            return PartialView(Astatus[code], result);
        }
        //public ActionResult ShowEditDemo(int id)
        //{
        //    AgentUserVM result = new AgentUserVM();
        //    if (ModelState.IsValid)
        //    {
        //        result = this._agntusr.EditAgent(id);
        //        result.bdetails = _comm.getBanks();


        //    }
           
        //    return PartialView("UpdateDemo", result);
        //}
        public ActionResult UpdateDriver(AgentUserVM _ag)
        {
            if (ModelState.IsValid)
            {
                string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                  .Select(c => c.Value).SingleOrDefault();
                bool result = this._agntusr.UpdateAgent(_ag, Convert.ToInt32(userid));
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Username/Password did not matched.Invalid Login!");
                }


            }
            // return View("Index", _allPools);
            return RedirectToAction("Index");

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