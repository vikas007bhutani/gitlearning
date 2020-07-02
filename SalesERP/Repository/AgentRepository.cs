using System;
using System.Collections.Generic;
using System.Linq;
using SALEERP.ViewModel;
using SALEERP.Repository.Interface;
using SALEERP.Data;
using SALEERP.Models;


namespace SALEERP.Repository
{
    public class AgentRepository : IAgentRepository
    {
        private Sales_ERPContext _DBERP;

        public AgentRepository(Sales_ERPContext dbcontext)
        {

            this._DBERP = dbcontext;

        }
        public bool AddAgent(AgentMasterVM _agent, int userid)
        {
            bool result = false;
            try
            {

                this._DBERP.AgentMaster.Add(new AgentMaster()
                {
                    Type = _agent.AgentType,
                    Code = _agent.AgentCode,
                    CreatedDatetime = DateTime.Now,
                    UpdatedBy = userid,
                    IsActive = true
                }); ;

                result = this._DBERP.SaveChanges() > 0;

            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public bool DeleteAgent(int aid)
        {
            bool result = false;
            try
            {



                int innerresult = 0;
                var entity = _DBERP.AgentMaster.FirstOrDefault(item => item.Id == aid);

                if (entity != null)
                {

                  

                    entity.UpdatedDatetime = DateTime.Now;
                    entity.IsActive = false;
                   // entity.UpdatedBy=
                    this._DBERP.AgentMaster.Update(entity);
                    innerresult = this._DBERP.SaveChanges();
                }

                if (innerresult > 0)
                {
                    result = true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public AgentMasterVM EditAgent(int uid)
        {
            AgentMasterVM _agentdetails = new AgentMasterVM();
            try
            {

           
            AgentMaster agent = this._DBERP.AgentMaster.Find(uid);
           
            // List<LoginRoles> userroles = this._DBERP.LoginRoles.ToList();
            //  _userdetails.loginroles = BindingListUtillity.GenerateSelectList(userroles);
            _agentdetails.AgentType = agent.Type;
            _agentdetails.AgentCode = agent.Code;

            _agentdetails.AgentId = agent.Id;
            }
            catch (Exception)
            {

                throw;
            }
            return _agentdetails;

        }

        public List<AgentMasterVM> getAllAgents()
        {
            IEnumerable<AgentMasterVM> _allagentdetails;
            try
            {
                _allagentdetails = this._DBERP.AgentMaster.ToList().Where(ar => ar.IsActive == true).Select(p => new AgentMasterVM { AgentType = p.Type, AgentCode = p.Code, AgentId = p.Id, IsActive = p.IsActive });
            }
            catch (Exception)
            {

                throw;
            }

          

            return _allagentdetails.ToList();
        }

        public bool UpdateAgent(AgentMasterVM _agent, int uid)
        {
            bool result = false;
            try
            {



                int innerresult = 0;
                var entity = _DBERP.AgentMaster.FirstOrDefault(item => item.Id == _agent.AgentId);

                if (entity != null)
                {

                    entity.Type = _agent.AgentType;
                    entity.Code = _agent.AgentCode;
                   
                    entity.UpdatedDatetime = DateTime.Now;
                    this._DBERP.AgentMaster.Update(entity);
                    innerresult = this._DBERP.SaveChanges();
                }

                if (innerresult > 0)
                {
                    result = true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }
    }
}
