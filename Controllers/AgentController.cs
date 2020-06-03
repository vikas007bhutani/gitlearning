using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using SALEERP.ViewModel;
using SALEERP.Repository.Interface;
using Microsoft.AspNetCore.Authorization;


namespace SALEERP.Controllers
{
    public class AgentController : Controller
    {
        IAgentRepository _agent;
        ICommonRepository _comm;
        public AgentController(IAgentRepository _agentrepo, ICommonRepository comm)
        {
            this._agent = _agentrepo;
            this._comm = comm;
        }
        public IActionResult Index(string sortOrder, string searchString, int pn)
        {
            try
            {

                List<AgentMasterVM> _allAgents = new List<AgentMasterVM>();
                if (ModelState.IsValid)
                {
                    ViewData["NameSortParm"] = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
                   

                    _allAgents = _agent.getAllAgents();
                    if (_allAgents != null)
                    {
                        if (!String.IsNullOrEmpty(searchString))
                        {
                            _allAgents = _allAgents.Where(s => s.AgentType.Contains(searchString)).ToList();
                        }
                        switch (sortOrder)
                        {
                            case "name_desc":
                                _allAgents = _allAgents.OrderByDescending(s => s.AgentType).ToList();
                                break;
                            default:
                                _allAgents = _allAgents.OrderBy(s => s.AgentType).ToList();
                                break;
                        }
                        _allAgents = _agent.getAllAgents().OrderBy(d => d.AgentId).Skip((pn - 1) * 10).Take(10).ToList();
                        if (_allAgents.Count > 0)
                        {
                            ViewBag.Paging = Set_Paging(pn, 10, _agent.getAllAgents().Count(), "activeLink", Url.Action("Index", "User"), "disableLink");
                        }
                    }
                    else { ModelState.AddModelError(string.Empty, "No Records Found!!!"); }
                }


                return View(_allAgents);
            }
            catch (Exception)
            {

                throw;
            }
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index([Bind("AgentId,AgentType,AgentCode")] AgentMasterVM _ag)
        {
            if (ModelState.IsValid)
            {
                string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                   .Select(c => c.Value).SingleOrDefault();
                bool result = this._agent.AddAgent(_ag, Convert.ToInt32(userid));
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Username/Password did not matched.Invalid Login!");
                }


            }
            return RedirectToAction("Index", _agent.getAllAgents());

        }

        [HttpPost]
        public PartialViewResult ShowCreatePartailView()
        {
            return PartialView("ManageAgent");
        }
        public ActionResult ShowEditPartailView(int id)
        {
            AgentMasterVM result = new AgentMasterVM();
            if (ModelState.IsValid)
            {
                result = this._agent.EditAgent(id);

            }
            //return View(staff);
            return PartialView("UpdateAgent", result);
        }
        [Authorize]
        
        [ValidateAntiForgeryToken]
        public ActionResult GetAgentDeleted(int id)
        {
            bool result;
            if (ModelState.IsValid)
            {
                result = this._agent.DeleteAgent(id);
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "agent id not found!Contact Administrator");
                }

            }
            //return View(staff);
            return RedirectToAction("Index", _agent.getAllAgents());
        }
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind("AgentId,AgentType,AgentCode")] AgentMasterVM _ag)
        {
            if (ModelState.IsValid)
            {
                string userid = HttpContext.User.Claims.Where(c => c.Type == System.Security.Claims.ClaimTypes.PrimarySid)
                  .Select(c => c.Value).SingleOrDefault();
                bool result = this._agent.UpdateAgent(_ag, Convert.ToInt32(userid));
                if (!result)
                {
                    ModelState.AddModelError(string.Empty, "Username/Password did not matched.Invalid Login!");
                }


            }
            // return View("Index", _allPools);
            return RedirectToAction("Index", _agent.getAllAgents());

        }

        public IActionResult User()
        {

            return View("AgentUser");
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
