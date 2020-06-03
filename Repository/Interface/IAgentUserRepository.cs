using System;
using System.Collections.Generic;
using SALEERP.ViewModel;

namespace SALEERP.Repository.Interface
{
    public interface IAgentUserRepository
    {
        AgentDetailsVM getAllAgentUsers();
        bool AddUser(AgentUserVM _user, int userid);
        bool DeleteAgent(int pid);
        bool UpdateAgent(AgentUserVM _agent, int uid);
        AgentUserVM EditAgent(int uid);
    }
}
