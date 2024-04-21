using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEERP.Models;

namespace SALEERP.ViewModel
{
    public class AgentDetailsVM
    {
        public List<AgentUserVM> AgentDetails { get; set; }
        public List<SelectListItem> _agentuser { get; set; }
    }
}
