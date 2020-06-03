using System;
using System.Collections.Generic;
using System.Linq;
using SALEERP.ViewModel;
using SALEERP.Repository.Interface;
using SALEERP.Data;
using SALEERP.Models;
using SALEERP.Utility;
using Microsoft.EntityFrameworkCore;

namespace SALEERP.Repository
{
    public class AgentUserRepository : IAgentUserRepository
    {
        private Sales_ERPContext _DBERP;

        public AgentUserRepository(Sales_ERPContext dbcontext)
        {

            this._DBERP = dbcontext;

        }

        public bool AddUser(AgentUserVM _user, int userid)
        {
            bool result = false, innerresult = false;
            using (var dbusertrans = this._DBERP.Database.BeginTransaction())
            {
                this._DBERP.AgentUser.Add(new AgentUser()
                {
                     AgentCode=_user.AgentCode,
                     Name=_user.Name,
                     Address=_user.Address,
                    IsActive = true
                }); 

                result = this._DBERP.SaveChanges() > 0;
                if (result)
                {
                    int uid = this._DBERP.AgentUser.Max(p => p.AgentId);
                    foreach (var item in _user.AgentContact)
                    {
                        if (item != null)
                        {
                            this._DBERP.AgentContact.Add(new AgentContact() { AgentId = uid, Email = item.Email, Mobile = item.Mobile, IsActive = true });
                        }
                    }
                    innerresult = this._DBERP.SaveChanges() > 0;
                    foreach (var itembank in _user.BankDetails)
                    {
                        if (itembank != null)
                        {
                            this._DBERP.BankDetails.Add(new BankDetails() { AgentId = uid, BankId = itembank.BankId, AccountNo = itembank.AccountNo, IsActive = true });
                        }
                    }
                    innerresult = this._DBERP.SaveChanges() > 0;
                    if (innerresult)
                    {
                        dbusertrans.Commit();
                    }
                    else
                    { dbusertrans.Rollback(); result = false; }

                }
                else { dbusertrans.Rollback(); }

            }
            return result;
        }

        public bool DeleteAgent(int aid)
        {
            bool result = false;
            
            try
            {

                using (var dbusertrans = this._DBERP.Database.BeginTransaction())
                {

                    int innerresult = 0;
                    var entity = _DBERP.AgentUser.FirstOrDefault(item => item.AgentId == aid);

                    if (entity != null)
                    {

                        entity.Createddatetime = DateTime.Now;
                        entity.IsActive = false;
                        // entity.UpdatedBy=
                        this._DBERP.AgentUser.Update(entity);
                        innerresult = this._DBERP.SaveChanges();
                    }

                    if (innerresult > 0)
                    {
                        var entitycontact = _DBERP.AgentContact.FirstOrDefault(item => item.AgentId == aid);

                        if (entitycontact != null)
                        {

                            entitycontact.Createddatetime = DateTime.Now;
                            entitycontact.IsActive = false;
                            this._DBERP.AgentContact.Update(entitycontact);
                            innerresult = this._DBERP.SaveChanges();
                        }
                        var entitybank = _DBERP.BankDetails.FirstOrDefault(item => item.AgentId == aid);

                        if (entitybank != null)
                        {

                            entitybank.Createddatetime = DateTime.Now;
                            entitybank.IsActive = false;
                            this._DBERP.BankDetails.Update(entitybank);
                            innerresult = this._DBERP.SaveChanges();
                        }
                        result = true;
                        dbusertrans.Commit();

                    }
                    else { dbusertrans.Rollback(); }
                }
            }
            catch (Exception)
            {
               
                throw;
            }

            return result;
        }


        public AgentUserVM EditAgent(int uid)
        {
            AgentUserVM _agentdetails = new AgentUserVM();
            try
            {
              AgentUser  var  = this._DBERP.AgentUser.Include("AgentContact").Where(c=>c.IsActive==true).Include("BankDetails").Where(c => c.IsActive == true).SingleOrDefault(a=>a.AgentId==uid && a.IsActive==true);

                _agentdetails.Name = var.Name;
                _agentdetails.Address = var.Address;
                _agentdetails.AgentId = var.AgentId;
                _agentdetails.AgentContact = var.AgentContact.Where(g=>g.IsActive==true).ToList();
                _agentdetails.BankDetails = var.BankDetails.Where(g => g.IsActive == true).ToList();
              
            }
            catch (Exception)
            {

                throw;
            }
            return _agentdetails;
        }
        public AgentUserVM CreateAgent(string uid)
        {
            AgentUserVM _agentdetails = new AgentUserVM();
            try
            {
                AgentUser var = this._DBERP.AgentUser.Include("AgentContact").Where(c => c.IsActive == true).Include("BankDetails").Where(c => c.IsActive == true).SingleOrDefault(a => a.AgentCode == uid && a.IsActive == true);

                _agentdetails.Name = var.Name;
                _agentdetails.Address = var.Address;
                _agentdetails.AgentId = var.AgentId;
                _agentdetails.AgentContact = var.AgentContact.Where(g => g.IsActive == true).ToList();
                _agentdetails.BankDetails = var.BankDetails.Where(g => g.IsActive == true).ToList();

            }
            catch (Exception)
            {

                throw;
            }
            return _agentdetails;
        }

        public AgentDetailsVM getAllAgentUsers()
        {
           AgentDetailsVM _allagentdetails=new AgentDetailsVM();
            List<AgentMaster> _agentmaster = this._DBERP.AgentMaster.Where(i => i.IsActive == true).ToList();
            List<VehicleMaster> _vehiclemaster = this._DBERP.VehicleMaster.Where(i => i.IsActive == true).ToList();

            _allagentdetails.AgentDetails = this._DBERP.AgentUser.ToList().Where(i => i.IsActive == true).Select(u => new AgentUserVM { AgentCode=u.AgentCode, AgentId=u.AgentId, Address=u.Address, City=u.City,Name=u.Name,  BankDetails = this._DBERP.BankDetails.Where(b => b.AgentId == u.AgentId && b.IsActive == true).ToList(), AgentContact= this._DBERP.AgentContact.Where(c => c.AgentId == u.AgentId && c.IsActive == true).ToList(),vdetails= BindingListUtillity.GenerateSelectList(_vehiclemaster) }).ToList();
            _allagentdetails._agentuser = BindingListUtillity.GenerateSelectList(_agentmaster);
            

            return _allagentdetails;

        }
        public bool UpdateAgent(AgentUserVM _agent, int uid)
        {
            bool result = false;
            try
            {
                using (var dbusertrans = this._DBERP.Database.BeginTransaction())
                {

                    int innerresult = 0;
                    var entity = _DBERP.AgentUser.FirstOrDefault(item => item.AgentId == _agent.AgentId);

                    if (entity != null)
                    {
                        entity.Name = _agent.Name;
                        entity.Address = _agent.Address;
                        entity.Createddatetime = DateTime.Now;
                        entity.IsActive = true;
                        this._DBERP.AgentUser.Update(entity);
                        innerresult = this._DBERP.SaveChanges();
                    }

                    if (innerresult > 0)
                    {
                        if (_agent.AgentContact != null)
                        {
                            var entitycontact = _DBERP.AgentContact.Where(item => item.AgentId == _agent.AgentId);
                            if (entitycontact != null)
                            {
                                foreach (var r in entitycontact)
                                {
                                    r.IsActive = false;
                                    r.Updateddatetime = DateTime.Now;
                                }
                                this._DBERP.SaveChanges();
                            }


                            foreach (var _item in _agent.AgentContact)
                            {
                                this._DBERP.AgentContact.Add(new AgentContact()
                                {
                                    Mobile = _item.Mobile,
                                    Email = _item.Email,
                                    AgentId = _agent.AgentId,
                                    Createddatetime = DateTime.Now,
                                    IsActive = true
                                });

                               
                            }
                            innerresult = this._DBERP.SaveChanges();

                        }
                        if (_agent.BankDetails != null)
                        {
                            var entitybank = _DBERP.BankDetails.Where(item => item.AgentId == _agent.AgentId);

                            if (entitybank != null)
                            {
                                foreach (var r in entitybank)
                                {
                                    r.IsActive = false;
                                    r.Updateddatetime = DateTime.Now;
                                }
                                this._DBERP.SaveChanges();
                               
                            }
                            foreach (var _item in _agent.BankDetails)
                            {
                               
                              
                                    this._DBERP.BankDetails.Add(new BankDetails()
                                    {
                                        AgentId = _agent.AgentId,
                                        BankId = _item.BankId,
                                        AccountNo = _item.AccountNo,
                                        Createddatetime=DateTime.Now,
                                        Createdby=uid,
                                        IsActive = true
                                    });


                                   
                                
                                
                            }
                            innerresult = this._DBERP.SaveChanges();
                        }
                        result = true;
                        dbusertrans.Commit();

                    }
                    else { dbusertrans.Rollback(); }
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
