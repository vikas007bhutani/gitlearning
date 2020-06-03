using System;
using System.Collections.Generic;

using SALEERP.ViewModel;


namespace SALEERP.Repository.Interface
{
   public interface IAgentRepository
    {
        List<AgentMasterVM> getAllAgents();
        bool AddAgent(AgentMasterVM _agent, int userid);
        bool UpdateAgent(AgentMasterVM _agent, int uid);
        AgentMasterVM EditAgent(int uid);
        bool DeleteAgent(int pid);
      
    }
}
