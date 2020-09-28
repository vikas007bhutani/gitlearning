using SALEERP.Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using SALEERP.ViewModel;
using SALEERP.Repository.Interface;
using SALEERP.Data;
using SALEERP.Models;
using SALEERP.Utility;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SALEERP.Repository
{
    public class CommissionRepository : IcommissionRepository
    {
        private Sales_ERPContext _DBERP;
        ICommonRepository _comm;
        public CommissionRepository(Sales_ERPContext dbcontext, ICommonRepository comm)
        {

            this._DBERP = dbcontext;
            this._comm = comm;

        }

        public bool AddCommission(CommissionVM _comm, int userid)
        {
            bool result = false;
            long? mirror = 0;
            using (var dbusertrans = this._DBERP.Database.BeginTransaction())
            {
                mirror = _comm.MirrorId;
              
                if (_comm.agent_List.Count() > 0)
                {
                    foreach (var item in _comm.agent_List)
                    {
                        if (item != null)
                        {
                            foreach (var inneritem in item.driver)
                            {
                               
                                if (inneritem.AgentId > 0 || inneritem.AgentId != null)
                                {
                                    var entitycontact = _DBERP.CommissionDetails.FirstOrDefault(i => i.AgentId == inneritem.AgentId && i.MirrorId==mirror && i.IsActive==true);
                                    if (entitycontact != null)
                                    {
                                        entitycontact.UpdatedBy = userid;
                                        entitycontact.Amount = inneritem.commission_amount;
                                        entitycontact.Pecentage = inneritem.commission_percentage;
                                        entitycontact.UpdatedDatetime = DateTime.Now;

                                        var result1 = this._DBERP.SaveChanges() > 0;
                                    }

                                    else
                                    {

                                        this._DBERP.CommissionDetails.Add(new CommissionDetails()
                                        {
                                            AgentId = inneritem.AgentId,
                                            MirrorId = mirror,
                                            Date = DateTime.Now,
                                            Amount = inneritem.commission_amount,
                                            Pecentage = inneritem.commission_percentage,
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        });
                                        result = this._DBERP.SaveChanges() > 0;
                                    }

                                }
                               
                            }
                            foreach (var innerprn in item.principle)
                            {

                                if (innerprn.AgentId > 0 || innerprn.AgentId != null)
                                {
                                    var entitycontact = _DBERP.CommissionDetails.FirstOrDefault(i => i.AgentId == innerprn.AgentId && i.MirrorId == mirror && i.IsActive == true);
                                    if (entitycontact != null)
                                    {

                                        entitycontact.UpdatedBy = userid;
                                        entitycontact.Amount = innerprn.commission_amount;
                                        entitycontact.Pecentage = innerprn.commission_percentage;
                                        entitycontact.UpdatedDatetime = DateTime.Now;

                                        var result1 = this._DBERP.SaveChanges() > 0;
                                    }


                                    else
                                    {
                                        this._DBERP.CommissionDetails.Add(new CommissionDetails()
                                        {
                                            AgentId = innerprn.AgentId,
                                            MirrorId = mirror,
                                            Date = DateTime.Now,
                                            Amount = innerprn.commission_amount,
                                            Pecentage = innerprn.commission_percentage,
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        });
                                        result = this._DBERP.SaveChanges() > 0;
                                    }

                                }

                            }

                            foreach (var innergud in item.guide)
                            {

                                if (innergud.AgentId > 0 || innergud.AgentId != null)
                                {
                                    var entitycontact = _DBERP.CommissionDetails.FirstOrDefault(i => i.AgentId == innergud.AgentId && i.MirrorId == mirror && i.IsActive==true);
                                    if (entitycontact != null)
                                    {

                                        entitycontact.UpdatedBy = userid;
                                        entitycontact.Amount = innergud.commission_amount;
                                        entitycontact.Pecentage = innergud.commission_percentage;
                                        entitycontact.UpdatedDatetime = DateTime.Now;

                                        var result1 = this._DBERP.SaveChanges() > 0;
                                    }


                                    else
                                    {
                                        this._DBERP.CommissionDetails.Add(new CommissionDetails()
                                        {
                                            AgentId = innergud.AgentId,
                                            MirrorId = mirror,
                                            Date = DateTime.Now,
                                            Amount = innergud.commission_amount,
                                            Pecentage = innergud.commission_percentage,
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        });
                                        result = this._DBERP.SaveChanges() > 0;
                                    }

                                }

                            }

                            foreach (var innerex in item.excursion)
                            {

                                if (innerex.AgentId > 0 || innerex.AgentId != null)
                                {
                                    var entitycontact = _DBERP.CommissionDetails.FirstOrDefault(i => i.AgentId == innerex.AgentId && i.MirrorId == mirror && i.IsActive == true);
                                    if (entitycontact != null)
                                    {

                                        entitycontact.UpdatedBy = userid;
                                        entitycontact.Amount = innerex.commission_amount;
                                        entitycontact.Pecentage = innerex.commission_percentage;
                                        entitycontact.UpdatedDatetime = DateTime.Now;

                                        var result1 = this._DBERP.SaveChanges() > 0;
                                    }


                                    else
                                    {
                                        this._DBERP.CommissionDetails.Add(new CommissionDetails()
                                        {
                                            AgentId = innerex.AgentId,
                                            MirrorId = mirror,
                                            Date = DateTime.Now,
                                            Amount = innerex.commission_amount,
                                            Pecentage = innerex.commission_percentage,
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        });
                                        result = this._DBERP.SaveChanges() > 0;
                                    }

                                }
                            }
                            foreach (var innerect in item.teamescort)
                            {

                                if (innerect.AgentId > 0 || innerect.AgentId != null)
                                {
                                    var entitycontact = _DBERP.CommissionDetails.FirstOrDefault(i => i.AgentId == innerect.AgentId && i.MirrorId == mirror && i.IsActive == true);
                                    if (entitycontact != null)
                                    {

                                        entitycontact.UpdatedBy = userid;
                                        entitycontact.Amount = innerect.commission_amount;
                                        entitycontact.Pecentage = innerect.commission_percentage;
                                        entitycontact.UpdatedDatetime = DateTime.Now;

                                        var result1 = this._DBERP.SaveChanges() > 0;
                                    }


                                    else
                                    {
                                        this._DBERP.CommissionDetails.Add(new CommissionDetails()
                                        {
                                            AgentId = innerect.AgentId,
                                            MirrorId = mirror,
                                            Date = DateTime.Now,
                                            Amount = innerect.commission_amount,
                                            Pecentage = innerect.commission_percentage,
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        });
                                        result = this._DBERP.SaveChanges() > 0;
                                    }

                                }

                            }
                            foreach (var innerled in item.teamlead)
                            {

                                if (innerled.AgentId > 0 || innerled.AgentId != null)
                                {
                                    var entitycontact = _DBERP.CommissionDetails.FirstOrDefault(i => i.AgentId == innerled.AgentId && i.MirrorId == mirror && i.IsActive==true);
                                    if (entitycontact != null)
                                    {

                                        entitycontact.UpdatedBy = userid;
                                        entitycontact.Amount = innerled.commission_amount;
                                        entitycontact.Pecentage = innerled.commission_percentage;
                                        entitycontact.UpdatedDatetime = DateTime.Now;

                                        var result1 = this._DBERP.SaveChanges() > 0;
                                    }


                                    else
                                    {
                                        this._DBERP.CommissionDetails.Add(new CommissionDetails()
                                        {
                                            AgentId = innerled.AgentId,
                                            MirrorId = mirror,
                                            Date = DateTime.Now,
                                            Amount = innerled.commission_amount,
                                            Pecentage = innerled.commission_percentage,
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        });
                                        result = this._DBERP.SaveChanges() > 0;
                                    }

                                }

                            }
                          
                            foreach (var innerdemo in item.demo)
                            {

                                if (innerdemo.AgentId > 0 || innerdemo.AgentId != null)
                                {
                                    var entitycontact = _DBERP.CommissionDetails.FirstOrDefault(i => i.AgentId == innerdemo.AgentId && i.MirrorId == mirror && i.IsActive == true);
                                    if (entitycontact != null)
                                    {

                                        entitycontact.UpdatedBy = userid;
                                        entitycontact.Amount = innerdemo.commission_amount;
                                        entitycontact.Pecentage = innerdemo.commission_percentage;
                                        entitycontact.UpdatedDatetime = DateTime.Now;

                                        var result1 = this._DBERP.SaveChanges() > 0;
                                    }


                                    else
                                    {
                                        this._DBERP.CommissionDetails.Add(new CommissionDetails()
                                        {
                                            AgentId = innerdemo.AgentId,
                                            MirrorId = mirror,
                                            Date = DateTime.Now,
                                            Amount = innerdemo.commission_amount,
                                            Pecentage = innerdemo.commission_percentage,
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        });
                                        result = this._DBERP.SaveChanges() > 0;
                                    }

                                }

                            }
                            dbusertrans.Commit();
                            //if (result)
                            //{
                              

                            //}
                            //else
                            //{

                            //    dbusertrans.Rollback();
                            //}
                        }
                    }
                }
                        //            else
                        //            {


                        //                this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentId, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now, ParchiAmount = item.parchiamount, IsParchi = item.isparchiamount });
                        //                innerresult = this._DBERP.SaveChanges() > 0;
                        //                this._DBERP.VehicleDetails.Add(new VehicleDetails() { AgentId = item.agentId, VehicleId = item.vehicletypeid, Number = item.vehicleNo, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        //                innerresultvehicle = this._DBERP.SaveChanges() > 0;




                        //                var entitycontact = _DBERP.AgentContact.FirstOrDefault(i => i.Mobile == item.Mob);
                        //                if (entitycontact != null)
                        //                {
                        //                    if (entitycontact.AgentId != item.agentId)
                        //                    {
                        //                        result = false;
                        //                        dbusertrans.Rollback();
                        //                        return result;

                        //                    }

                        //                }
                        //                else
                        //                {

                        //                    this._DBERP.AgentContact.Add(new AgentContact() { AgentId = item.agentId, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        //                    innerresultcontact = this._DBERP.SaveChanges() > 0;
                        //                }

                        //            }


                        //        }
                        //    }
                        //    bool innerresultpagent = false, innerresulteagnet = false, innerresultguide = false, innerresultleader = false, innerresultescort = false, innerresultdemo = false;
                        //    foreach (var item in _mirror.PAgentID_List)
                        //    {
                        //        if (item.agentid == 0 || item.agentid == null)
                        //        {
                        //            this._DBERP.AgentUser.Add(new AgentUser()
                        //            {
                        //                Code = "pi",
                        //                Name = item.agentname,
                        //                Status = "new",
                        //                IsActive = true,
                        //                CreatedDatetime = DateTime.Now,
                        //                CreatedBy = userid
                        //            });

                        //            var resultnewuser = this._DBERP.SaveChanges() > 0;
                        //            int New_uid = 0;
                        //            if (resultnewuser)
                        //            {
                        //                New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                        //                this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        //            }
                        //            else
                        //            {

                        //                result = false;
                        //                dbusertrans.Rollback();
                        //                return result;

                        //            }
                        //        }
                        //        else { this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }); }
                        //        innerresultpagent = this._DBERP.SaveChanges() > 0;
                        //    }
                        //    foreach (var item in _mirror.EAgentID_List)
                        //    {
                        //        if (item.agentid == 0 || item.agentid == null)
                        //        {
                        //            this._DBERP.AgentUser.Add(new AgentUser()
                        //            {
                        //                Code = "ex",
                        //                Name = item.agentname,
                        //                Status = "new",
                        //                IsActive = true,
                        //                CreatedDatetime = DateTime.Now,
                        //                CreatedBy = userid
                        //            });

                        //            var resultnewuser = this._DBERP.SaveChanges() > 0;
                        //            int New_uid = 0;
                        //            if (resultnewuser)
                        //            {
                        //                New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                        //                this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        //            }
                        //            else
                        //            {

                        //                result = false;
                        //                dbusertrans.Rollback();
                        //                return result;

                        //            }
                        //        }
                        //        else
                        //        {
                        //            this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        //        }
                        //        innerresulteagnet = this._DBERP.SaveChanges() > 0;
                        //    }
                        //    foreach (var item in _mirror.EscortAgentID_List)
                        //    {
                        //        if (item.agentid == 0 || item.agentid == null)
                        //        {
                        //            this._DBERP.AgentUser.Add(new AgentUser()
                        //            {
                        //                Code = "ec",
                        //                Name = item.agentname,
                        //                Status = "new",
                        //                IsActive = true,
                        //                CreatedDatetime = DateTime.Now,
                        //                CreatedBy = userid
                        //            });

                        //            var resultnewuser = this._DBERP.SaveChanges() > 0;
                        //            int New_uid = 0;
                        //            if (resultnewuser)
                        //            {
                        //                New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                        //                this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        //            }
                        //            else
                        //            {

                        //                result = false;
                        //                dbusertrans.Rollback();
                        //                return result;

                        //            }
                        //        }
                        //        else
                        //        {
                        //            this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        //        }
                        //        innerresultescort = this._DBERP.SaveChanges() > 0;
                        //    }
                        //    foreach (var item in _mirror.GuideAgentID_List)
                        //    {
                        //        if (item.agentid == 0 || item.agentid == null)
                        //        {
                        //            this._DBERP.AgentUser.Add(new AgentUser()
                        //            {
                        //                Code = "gu",
                        //                Name = item.agentname,
                        //                Status = "new",
                        //                IsActive = true,
                        //                CreatedDatetime = DateTime.Now,
                        //                CreatedBy = userid
                        //            });

                        //            var resultnewuser = this._DBERP.SaveChanges() > 0;
                        //            int New_uid = 0;
                        //            if (resultnewuser)
                        //            {
                        //                New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                        //                this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        //            }
                        //            else
                        //            {

                        //                result = false;
                        //                dbusertrans.Rollback();
                        //                return result;

                        //            }
                        //        }
                        //        else
                        //        {
                        //            this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        //        }
                        //        innerresultguide = this._DBERP.SaveChanges() > 0;
                        //    }
                        //    foreach (var item in _mirror.LeaderAgentID_List)
                        //    {
                        //        if (item.agentid == 0 || item.agentid == null)
                        //        {
                        //            this._DBERP.AgentUser.Add(new AgentUser()
                        //            {
                        //                Code = "le",
                        //                Name = item.agentname,
                        //                Status = "new",
                        //                IsActive = true,
                        //                CreatedDatetime = DateTime.Now,
                        //                CreatedBy = userid
                        //            });

                        //            var resultnewuser = this._DBERP.SaveChanges() > 0;
                        //            int New_uid = 0;
                        //            if (resultnewuser)
                        //            {
                        //                New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                        //                this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        //            }
                        //            else
                        //            {

                        //                result = false;
                        //                dbusertrans.Rollback();
                        //                return result;

                        //            }
                        //        }
                        //        else
                        //        {
                        //            this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        //        }
                        //        innerresultleader = this._DBERP.SaveChanges() > 0;
                        //    }
                        //    foreach (var item in _mirror.DemoAgentID_List)
                        //    {
                        //        if (item.agentid == 0 || item.agentid == null)
                        //        {
                        //            this._DBERP.AgentUser.Add(new AgentUser()
                        //            {
                        //                Code = "de",
                        //                Name = item.agentname,
                        //                Status = "new",
                        //                IsActive = true,
                        //                CreatedDatetime = DateTime.Now,
                        //                CreatedBy = userid
                        //            });

                        //            var resultnewuser = this._DBERP.SaveChanges() > 0;
                        //            int New_uid = 0;
                        //            if (resultnewuser)
                        //            {
                        //                New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                        //                this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        //            }
                        //            else
                        //            {

                        //                result = false;
                        //                dbusertrans.Rollback();
                        //                return result;

                        //            }
                        //        }
                        //        else
                        //        {
                        //            this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        //        }
                        //        innerresultdemo = this._DBERP.SaveChanges() > 0;
                        //    }

                        //   dbusertrans.Commit();


                        //}
                        //else { dbusertrans.Rollback(); }

                    }
            return result;
        }

        public CommissionVM EditCommission(long uid)
        {

            CommissionVM _cms = new CommissionVM();


            // List<UnitMaster> _unitmaster = new List<UnitMaster>();
            _cms.unitdetails = _comm.getunits();
            var sale_details = (from m in this._DBERP.MirrorDetails
                                join om in this._DBERP.OrderMaster
                                on m.Id equals om.MirrorId
                                join pay in this._DBERP.OrderPayment
                                on om.Id equals pay.OrderId
                                //on ma.AgentId equals v.AgentId into vehicledetails
                                //from vh in vehicledetails.Where(f => f.IsActive == true).DefaultIfEmpty()

                                where om.IsActive == true && m.Id == uid
                                select new
                                {
                                    mirror = m.Id,
                                    mirrodate = m.Date,
                                    saleamount = pay.Amount,
                                    hdcharge = pay.AmoutHd,
                                    saleid = pay.OrderId,
                                    sale_type = pay.PayMode,
                                    gst = pay.Gst,
                                    cardpercentage = pay.Igst


                                }

                               ).ToList().GroupBy(m => m.mirror).Select(g => new
                               {
                                   MirrorId = g.Key,
                                   MirrorDate=g.FirstOrDefault().mirrodate,
                                   cashamount = g.Where(s => s.sale_type == 1).Sum(c => c.saleamount),
                                   HDamount = g.Sum(c => c.hdcharge),
                                   cardamount = g.Where(s => s.sale_type == 2).Sum(c => c.saleamount),
                                   paylateramount = g.Where(s => s.sale_type == 3).Sum(c => c.saleamount),
                                   gst = g.FirstOrDefault().gst,
                                   cardper = g.FirstOrDefault().cardpercentage


                                   // p= g.Where(c => c.agentcode == "pi").SelectMany(a=>a.name).SingleOrDefault().ToString()
                               }).ToList();

            
            foreach (var item in sale_details)
            {

                _cms.cashamount = item.cashamount;
                _cms.cardamount = item.cardamount;
                _cms.HDamount = item.HDamount;
                _cms.paylateramount = item.paylateramount;
                _cms.NetSaleAmount = 0;
                _cms.GST = item.gst;
                _cms.CardCharges = item.cardper;
                _cms.MirrorId = item.MirrorId;
                _cms.MirrorDate = item.MirrorDate;



            }


            _cms.agent_List = (from m in this._DBERP.MirrorDetails
                            join ma in this._DBERP.MirrorAgent
                            on m.Id equals ma.MirrorId
                            join au in this._DBERP.AgentUser
                            on ma.AgentId equals au.Id
                            join c in this._DBERP.CommissionDetails
                            on au.Id equals c.AgentId   into commdetails
                            from d in commdetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                            //join v in this._DBERP.VehicleDetails
                            //on ma.AgentId equals v.AgentId into vehicledetails
                            //from vh in vehicledetails.Where(f => f.IsActive == true).DefaultIfEmpty()

                            where ma.IsActive == true && m.Id==uid
                            select new AddCommissionDetails
                            {
                                AgentId = ma.AgentId,
                                Name = au.Name,
                                commission_amount = d.Amount,
                                commission_percentage = d.Pecentage,
                                agentcode = au.Code,
                                MirrorId = m.Id


                            }).ToList().GroupBy(c => c.MirrorId)
     .Select(g => new AddCommissionDetails
     {
         MirrorId = g.Key,
        commission_amount=g.FirstOrDefault().commission_amount,
        commission_percentage=g.FirstOrDefault().commission_percentage,
         principle = g.Where(c => c.agentcode == "pi").ToList(),
         driver = g.Where(c => c.agentcode == "dr").ToList(),
         excursion = g.Where(c => c.agentcode == "ex").ToList(),
         guide = g.Where(c => c.agentcode == "gu").ToList(),
         teamescort = g.Where(c => c.agentcode == "ec").ToList(),
         teamlead = g.Where(c => c.agentcode == "le").ToList(),
         demo = g.Where(c => c.agentcode == "de").ToList()
         // p= g.Where(c => c.agentcode == "pi").SelectMany(a=>a.name).SingleOrDefault().ToString()
     }).ToList();


            return _cms;
        }

        public PayCommissionVM getAllAgentCommission(PayCommissionVM pcm)
        {
            PayCommissionVM _Pcms = new PayCommissionVM();
            List<AgentMaster> _agentmaster = this._DBERP.AgentMaster.Where(i => i.IsActive == true).ToList();
          
            _Pcms._agentuser = BindingListUtillity.GenerateSelectList(_agentmaster);

            _Pcms.pay_list = (from m in this._DBERP.MirrorDetails
                             join om in this._DBERP.OrderMaster
                             on m.Id equals om.MirrorId
                             join comm in this._DBERP.CommissionDetails
                             on om.MirrorId equals comm.MirrorId into commdetails
                             from d in commdetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                             join au in this._DBERP.AgentUser
                             on d.AgentId equals au.Id
                              join am in this._DBERP.AgentMaster
                             on au.Code equals am.Code
                              join p in this._DBERP.PayDetails
                             on d.AgentId equals p.AgentId into payments
                             from g in payments.Where(c=>c.IsActive==true).DefaultIfEmpty()

                                 //on ma.AgentId equals c.AgentId into contactdetails
                                 //from d in contactdetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                                 //join v in this._DBERP.VehicleDetails
                                 //on ma.AgentId equals v.AgentId into vehicledetails
                                 //from vh in vehicledetails.Where(f => f.IsActive == true).DefaultIfEmpty()
            //d.AgentId == pcm.AgentId
                             where m.IsActive == true && ((pcm.AgentId == 0) || (Convert.ToString(pcm.AgentId) == null) || d.AgentId == pcm.AgentId) && ((pcm.fromdate == null ? d.Date >= DateTime.Now && d.Date <= DateTime.Now : d.Date >= pcm.fromdate && d.Date <= pcm.Todate) || (d.Date >= pcm.fromdate && d.Date <= pcm.Todate))
                            select new PayCommissionDetails
                            {
                                MirrorId = m.Id,
                                MirrorDate = m.Date,
                                Name = au.Name,
                                agentcode = au.Code,
                                AgentId=d.AgentId,
                                paid=g.Amount,
                                commission_amount=d.Amount,
                                tds=0,
                                AgentType= am.Type,
                                balance= 0,
                                OrderId=om.Id



                            }).ToList().GroupBy(c => c.AgentId)
    .Select(g => new PayCommissionDetails
    {
        MirrorId = g.Key,
        MirrorDate = g.FirstOrDefault().MirrorDate,
        paid = g.Sum(s=>s.paid),
        commission_amount = g.FirstOrDefault().commission_amount,
        tds = g.FirstOrDefault().tds,
        Name=g.FirstOrDefault().Name,
        AgentId=g.FirstOrDefault().AgentId,
        AgentType = g.FirstOrDefault().AgentType,
        balance= g.FirstOrDefault().commission_amount- g.Sum(s => s.paid),
        OrderId= g.FirstOrDefault().OrderId

        // p= g.Where(c => c.agentcode == "pi").SelectMany(a=>a.name).SingleOrDefault().ToString()
    }).ToList(); 

            return _Pcms;                
        }

        public CommissionVM getAllMirorforCommission(CommissionVM cm)
        {
            CommissionVM _cms = new CommissionVM();


           // List<UnitMaster> _unitmaster = new List<UnitMaster>();
            _cms.unitdetails = _comm.getunits();

            _cms.Mirrors = (from m in this._DBERP.MirrorDetails
                            join ma in this._DBERP.MirrorAgent
                            on m.Id equals ma.MirrorId
                            join au in this._DBERP.AgentUser
                            on ma.AgentId equals au.Id
                            join c in this._DBERP.AgentContact
                            on ma.AgentId equals c.AgentId into contactdetails
                            from d in contactdetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                            join v in this._DBERP.VehicleDetails
                            on ma.AgentId equals v.AgentId into vehicledetails
                            from vh in vehicledetails.Where(f => f.IsActive == true).DefaultIfEmpty()

                            where m.IsActive == true && ((cm.fromdate == null ? m.Date >= DateTime.Now && m.Date <= DateTime.Now: m.Date >= cm.fromdate && m.Date <= cm.Todate) || (m.Date >= cm.fromdate && m.Date <= cm.Todate))
                               select new MirrorDetailVM
                               {
                                   mirrorid = m.Id,
                                   mirrordate = m.Date,
                                   name = au.Name,
                                   mob = d.Mobile,
                                   agentcode = au.Code,


                               }).ToList().GroupBy(c => c.mirrorid)
    .Select(g => new MirrorDetailVM
    {
        mirrorid = g.Key,
        mirrordate = g.FirstOrDefault().mirrordate,
        principle = g.Where(c => c.agentcode == "pi").ToList(),
        driver = g.Where(c => c.agentcode == "dr").ToList(),
        excursion = g.Where(c => c.agentcode == "gu").ToList()
        // p= g.Where(c => c.agentcode == "pi").SelectMany(a=>a.name).SingleOrDefault().ToString()
    }).ToList();


            //_mirror.Mirrors = this._DBERP.MirrorDetails.ToList().Where(i => i.IsActive == true).Select(u => new MirrorDetailVM { mirrordate = u.MirrorDate, d_list = this._DBERP.AgentUser.Include("AgentContact").Where(d => d.IsActive == true && d.AgentCode == "pi").Include("VehicleDetails").Select(a=>new driverdetails { name=a.Name,mob=a.AgentContact.Where(b=>b.AgentId==a.AgentId).FirstOrDefault().Mobile }).ToList(), mirrorid = u.MirrorId }).ToList();


            //Vehicle = this._DBERP.AgentUser.Include("VehicleDetails").Where(d => d.IsActive == true && d.AgentTypeId == 1 && d.AgentId == u.DriverId).SingleOrDefault().VehicleDetails.Where(v => v.IsActive == true).SingleOrDefault().VehicleNo, DriverMobile = this._DBERP.AgentUser.Include("AgentContact").Where(d => d.IsActive == true && d.AgentTypeId == 1 && d.AgentId == u.DriverId).SingleOrDefault().AgentContact.Where(v => v.IsActive == true).SingleOrDefault().Mobile, VehicleTypeid = this._DBERP.AgentUser.Include("VehicleDetails").Where(d => d.IsActive == true && d.AgentTypeId == 1 && d.AgentId == u.DriverId).SingleOrDefault().VehicleDetails.Where(v => v.IsActive == true).SingleOrDefault().VehicleId
            // _mirror = this._DBERP.MirrorDetails.Where(i => i.IsActive == true).Select(u=>new MirrorDetailsVM { MirrorDate = u.MirrorDate, vehicledetails = BindingListUtillity.GenerateSelectList(_vehiclemaster) } ).SingleOrDefault();
            


            return _cms;


        }

        public CommissionVM getCommissionDetails()
        {
            throw new NotImplementedException();
        }

        public CommissionVM Init_commission()
        {
            CommissionVM _cms = new CommissionVM();
            List<UnitMaster> _unitmaster = new List<UnitMaster>();
            _cms.unitdetails = _comm.getunits();
            _cms.fromdate = DateTime.Now;
            _cms.Todate = DateTime.Now;
            return _cms;

        }
        public PayCommissionVM Init_paycommission()
        {
            List<AgentMaster> _agentmaster = this._DBERP.AgentMaster.Where(i => i.IsActive == true).ToList();
            PayCommissionVM _cms = new PayCommissionVM();
           
            _cms._agentuser = BindingListUtillity.GenerateSelectList(_agentmaster);
            _cms.fromdate = DateTime.Now;
            _cms.Todate = DateTime.Now;
            return _cms;

        }

        public PayCommissionVM PayCommission(long uid)
        {
            PayCommissionVM _Pcms = new PayCommissionVM();
            List<PayMode> _paymodemaster = this._DBERP.PayMode.Where(i => i.IsActive == true).ToList();

            _Pcms.paymodes = BindingListUtillity.GenerateSelectList(_paymodemaster);

            _Pcms.pay_list = (from m in this._DBERP.MirrorDetails
                              join om in this._DBERP.OrderMaster
                              on m.Id equals om.MirrorId
                              join comm in this._DBERP.CommissionDetails
                              on om.MirrorId equals comm.MirrorId into commdetails
                              from d in commdetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                              join au in this._DBERP.AgentUser
                              on d.AgentId equals au.Id
                              join am in this._DBERP.AgentMaster
                             on au.Code equals am.Code
                              join p in this._DBERP.PayDetails
                             on d.AgentId equals p.AgentId into payments
                              from g in payments.Where(c => c.IsActive == true).DefaultIfEmpty()

                                  //on ma.AgentId equals c.AgentId into contactdetails
                                  //from d in contactdetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                                  //join v in this._DBERP.VehicleDetails
                                  //on ma.AgentId equals v.AgentId into vehicledetails
                                  //from vh in vehicledetails.Where(f => f.IsActive == true).DefaultIfEmpty()
                                  //d.AgentId == pcm.AgentId
                              where m.IsActive == true && d.AgentId == uid
                              select new PayCommissionDetails
                              {
                                  MirrorId = m.Id,
                                  MirrorDate = m.Date,
                                  Name = au.Name,
                                  agentcode = au.Code,
                                  AgentId = d.AgentId,
                                  paid = g.Amount,
                                  commission_amount = d.Amount,
                                  tds = 0,
                                  AgentType = am.Type,
                                  balance = 0,
                                  OrderId=d.Id



                              }).ToList().GroupBy(c => c.AgentId)
    .Select(g => new PayCommissionDetails
    {
        AgentId = g.Key,
        MirrorDate = g.FirstOrDefault().MirrorDate,
        paid = g.Sum(s => s.paid),
        commission_amount = g.FirstOrDefault().commission_amount,
        tds = g.FirstOrDefault().tds,
        Name = g.FirstOrDefault().Name,
        MirrorId = g.FirstOrDefault().MirrorId,
        AgentType = g.FirstOrDefault().AgentType,
        balance = g.FirstOrDefault().commission_amount - g.Sum(s => s.paid),
        OrderId= g.FirstOrDefault().OrderId

        // p= g.Where(c => c.agentcode == "pi").SelectMany(a=>a.name).SingleOrDefault().ToString()
    }).ToList();

            return _Pcms;
        }

        public bool UpdateCommission(PayCommissionVM _comm, int userid)
        {

            bool result = false;
            long? mirror = 0;
            int paymode = 0;
            using (var dbusertrans = this._DBERP.Database.BeginTransaction())
            {
                mirror = _comm.MirrorId;
                paymode = _comm.modeId;
                decimal? lastamount = 0,finalamount=0;
                if (_comm.pay_list.Count() > 0)
                {
                    foreach (var item in _comm.pay_list)
                    {
                        if (item != null)
                        {
                            lastamount = item.amounttopay;
                            Decimal? commissionAMOUNT = _DBERP.CommissionDetails.Where(i => i.AgentId == item.AgentId && i.MirrorId == item.MirrorId  && i.IsActive == true).FirstOrDefault().Amount;

                            decimal? paidamount = _DBERP.PayDetails.Where(i => i.AgentId == item.AgentId && i.MirrorId == item.MirrorId && i.Id == item.OrderId && i.IsActive == true).Sum(a => a.Amount);
                            finalamount = commissionAMOUNT - (paidamount + lastamount);

                            if (finalamount >= 0)
                            {

                                this._DBERP.PayDetails.Add(new PayDetails()
                                {
                                    AgentId = item.AgentId,
                                    MirrorId = item.MirrorId,
                                    CommId = item.OrderId,
                                    Date = DateTime.Now,
                                    Amount = lastamount,
                                    pay_mode=paymode,
                                    IsActive = true,
                                    CreatedDatetime = DateTime.Now,
                                    CreatedBy = userid
                                });
                                result = this._DBERP.SaveChanges() > 0;
                            }

                                //    var entitycontact = _DBERP.CommissionDetails.FirstOrDefault(i => i.AgentId == inneritem.AgentId && i.MirrorId == mirror && i.IsActive == true);
                                //if (inneritem.AgentId > 0 || inneritem.AgentId != null)
                                //{
                                //    var entitycontact = _DBERP.CommissionDetails.FirstOrDefault(i => i.AgentId == inneritem.AgentId && i.MirrorId == mirror && i.IsActive == true);
                                //    if (entitycontact != null)
                                //    {
                                //        entitycontact.UpdatedBy = userid;
                                //        entitycontact.Amount = inneritem.commission_amount;
                                //        entitycontact.Pecentage = inneritem.commission_percentage;
                                //        entitycontact.UpdatedDatetime = DateTime.Now;

                                //        var result1 = this._DBERP.SaveChanges() > 0;
                                //    }

                                //    else
                                //    {

                                //        this._DBERP.CommissionDetails.Add(new CommissionDetails()
                                //        {
                                //            AgentId = inneritem.AgentId,
                                //            MirrorId = mirror,
                                //            Date = DateTime.Now,
                                //            Amount = inneritem.commission_amount,
                                //            Pecentage = inneritem.commission_percentage,
                                //            IsActive = true,
                                //            CreatedDatetime = DateTime.Now,
                                //            CreatedBy = userid
                                //        });
                                //        result = this._DBERP.SaveChanges() > 0;
                                //    }

                                //}

                            }






                        dbusertrans.Commit();
                        
                    }
                }
                

            }
            return result;






        }
    }
}
