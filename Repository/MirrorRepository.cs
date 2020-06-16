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
using Microsoft.AspNetCore.Mvc;
using System.Web.Http.ModelBinding;

namespace SALEERP.Repository
{
    public class MirrorRepository : IMirrorRepository
    {
        private Sales_ERPContext _DBERP;
        public MirrorRepository(Sales_ERPContext dbcontext)
        {

            this._DBERP = dbcontext;

        }

        public MirrorDetailsVM getAllMirrors()
        {
            MirrorDetailsVM _mirror = new MirrorDetailsVM();


            List<MirrorDetailVM> _allmirrordetails = new List<MirrorDetailVM>();
            List<AgentMaster> _agentmaster = this._DBERP.AgentMaster.Where(i => i.IsActive == true).ToList();
            List<LanguagesMaster> _langmaster = this._DBERP.LanguagesMaster.Where(i => i.IsActive == true).ToList();
            List<CountriesMaster> _countrymaster = this._DBERP.CountriesMaster.Where(i => i.IsActive == true).ToList();
            List<VehicleMaster> _vehiclemaster = this._DBERP.VehicleMaster.Where(i => i.IsActive == true).ToList();
            List<SeriesMaster> _seriesmaster = this._DBERP.SeriesMaster.Where(i => i.IsActive == true).ToList();

            _mirror.Mirrors = (from m in this._DBERP.MirrorDetails
                               join ma in this._DBERP.MirrorAgent
                               on m.Id equals ma.Id
                               join au in this._DBERP.AgentUser
                               on ma.AgentId equals au.Id
                               join c in this._DBERP.AgentContact
                               on ma.AgentId equals c.AgentId into contactdetails
                               from d in contactdetails.Where(c => c.IsActive == true).DefaultIfEmpty()
                               join v in this._DBERP.VehicleDetails
                               on ma.AgentId equals v.AgentId into vehicledetails
                               from vh in vehicledetails.Where(f => f.IsActive == true).DefaultIfEmpty()

                               where ma.IsActive == true
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
            _mirror.vehicledetails = BindingListUtillity.GenerateSelectList(_vehiclemaster);
            _mirror.languagedetails = BindingListUtillity.GenerateSelectList(_langmaster);
            _mirror.countrydetails = BindingListUtillity.GenerateSelectList(_countrymaster);
            _mirror.seriesdetails = BindingListUtillity.GenerateSelectList(_seriesmaster);


            return _mirror;

        }

        //public AgentUserVM EditAgent(int uid)
        //{
        //    AgentUserVM _agentdetails = new AgentUserVM();
        //    try
        //    {
        //        AgentUser var = this._DBERP.AgentUser.Include("AgentContact").Where(c => c.IsActive == true).Include("BankDetails").Where(c => c.IsActive == true).SingleOrDefault(a => a.AgentId == uid && a.IsActive == true);

        //        _agentdetails.Name = var.Name;
        //        _agentdetails.Address = var.Address;
        //        _agentdetails.AgentId = var.AgentId;
        //        _agentdetails.AgentContact = var.AgentContact.Where(g => g.IsActive == true).ToList();
        //        _agentdetails.BankDetails = var.BankDetails.Where(g => g.IsActive == true).ToList();

        //    }
        //    catch (Exception)
        //    {

        //        throw;
        //    }
        //    return _agentdetails;
        //}

        public async Task<List<AgentUserVM>> GetNames(string term)
        {
            List<AgentUserVM> _allagentdetails = new List<AgentUserVM>();
            var _result = await _DBERP.AgentUser.Include("AgentContact").Where(c => c.IsActive == true).Include("BankDetails").Where(c => c.IsActive == true).ToListAsync();
            _allagentdetails = _result.Where(p => p.Name.Contains(term)).Select(a => new AgentUserVM
            {
                AgentId = a.Id,
                Name = a.Name,
                AgentContact = a.AgentContact.ToList(),
                BankDetails = a.BankDetails.ToList()



            }).ToList();
            return _allagentdetails;
        }

        public bool AddMirror(MirrorDetailsVM _mirror, int userid)
        {
            bool result = false, innerresult = false, innerresultvehicle = false, innerresultcontact = false;
            using (var dbusertrans = this._DBERP.Database.BeginTransaction())
            {
                this._DBERP.MirrorDetails.Add(new MirrorDetails()
                {
                    Date = _mirror.MirrorDate,
                    Pax = _mirror.Pax,
                    CountryId = _mirror.Countryid,
                    LanguageId = _mirror.Languageid,
                    SeriesId=_mirror.SeriesId,
                    CreatedBy = userid,
                    CreatedDatetime = DateTime.Now

                });

                result = this._DBERP.SaveChanges() > 0;
                if (result)
                {
                    Int64 mid = this._DBERP.MirrorDetails.Max(p => p.Id);
                    foreach (var item in _mirror.Driver_List)
                    {
                        if (item != null)
                        {

                            if (item.agentId == 0 || item.agentId == null)
                            {
                                this._DBERP.AgentUser.Add(new AgentUser()
                                {
                                    Code = "dr",
                                    Name = item.Drivername,
                                    Status = "new",
                                    IsActive = true,
                                    CreatedDatetime = DateTime.Now,
                                    CreatedBy = userid
                                });

                                var resultnewuser = this._DBERP.SaveChanges() > 0;
                                int New_uid = 0;
                                if (resultnewuser)
                                {
                                    New_uid = this._DBERP.AgentUser.Max(p => p.Id);
                                }
                                this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now, ParchiAmount = item.parchiamount, IsParchi = item.isparchiamount });
                                innerresult = this._DBERP.SaveChanges() > 0;
                                this._DBERP.VehicleDetails.Add(new VehicleDetails() { AgentId = New_uid, VehicleId = item.vehicletypeid, Number = item.vehicleNo, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                innerresultvehicle = this._DBERP.SaveChanges() > 0;
                                var entitycontactnew = _DBERP.AgentContact.FirstOrDefault(i => i.Mobile == item.Mob);
                                if (entitycontactnew != null)
                                {
                                    if (entitycontactnew.AgentId != item.agentId)
                                    {
                                        result = false;
                                        dbusertrans.Rollback();
                                        return result;

                                    }

                                }
                                else
                                {

                                    this._DBERP.AgentContact.Add(new AgentContact() { AgentId = New_uid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                    innerresultcontact = this._DBERP.SaveChanges() > 0;
                                }


                            }

                            else
                            {


                                this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentId, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now, ParchiAmount = item.parchiamount, IsParchi = item.isparchiamount });
                                innerresult = this._DBERP.SaveChanges() > 0;
                                this._DBERP.VehicleDetails.Add(new VehicleDetails() { AgentId = item.agentId, VehicleId = item.vehicletypeid, Number = item.vehicleNo, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                innerresultvehicle = this._DBERP.SaveChanges() > 0;




                                var entitycontact = _DBERP.AgentContact.FirstOrDefault(i => i.Mobile == item.Mob);
                                if (entitycontact != null)
                                {
                                    if (entitycontact.AgentId != item.agentId)
                                    {
                                        result = false;
                                        dbusertrans.Rollback();
                                        return result;

                                    }

                                }
                                else
                                {

                                    this._DBERP.AgentContact.Add(new AgentContact() { AgentId = item.agentId, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                    innerresultcontact = this._DBERP.SaveChanges() > 0;
                                }

                            }


                        }
                    }
                    bool innerresultpagent = false, innerresulteagnet = false, innerresultguide = false, innerresultleader = false, innerresultescort = false, innerresultdemo = false;
                    foreach (var item in _mirror.PAgentID_List)
                    {
                        if (item.agentid == 0 || item.agentid == null)
                        {
                            this._DBERP.AgentUser.Add(new AgentUser()
                            {
                                Code = "pi",
                                Name = item.agentname,
                                Status = "new",
                                IsActive = true,
                                CreatedDatetime = DateTime.Now,
                                CreatedBy = userid
                            });

                            var resultnewuser = this._DBERP.SaveChanges() > 0;
                            int New_uid = 0;
                            if (resultnewuser)
                            {
                                New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                                this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                            }
                            else
                            {

                                result = false;
                                dbusertrans.Rollback();
                                return result;

                            }
                        }
                        else { this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }); }
                        innerresultpagent = this._DBERP.SaveChanges() > 0;
                    }
                    foreach (var item in _mirror.EAgentID_List)
                    {
                        if (item.agentid == 0 || item.agentid == null)
                        {
                            this._DBERP.AgentUser.Add(new AgentUser()
                            {
                                Code = "ex",
                                Name = item.agentname,
                                Status = "new",
                                IsActive = true,
                                CreatedDatetime = DateTime.Now,
                                CreatedBy = userid
                            });

                            var resultnewuser = this._DBERP.SaveChanges() > 0;
                            int New_uid = 0;
                            if (resultnewuser)
                            {
                                New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                                this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                            }
                            else
                            {

                                result = false;
                                dbusertrans.Rollback();
                                return result;

                            }
                        }
                        else
                        {
                            this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        }
                        innerresulteagnet = this._DBERP.SaveChanges() > 0;
                    }
                    foreach (var item in _mirror.EscortAgentID_List)
                    {
                        if (item.agentid == 0 || item.agentid == null)
                        {
                            this._DBERP.AgentUser.Add(new AgentUser()
                            {
                                Code = "ec",
                                Name = item.agentname,
                                Status = "new",
                                IsActive = true,
                                CreatedDatetime = DateTime.Now,
                                CreatedBy = userid
                            });

                            var resultnewuser = this._DBERP.SaveChanges() > 0;
                            int New_uid = 0;
                            if (resultnewuser)
                            {
                                New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                                this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                            }
                            else
                            {

                                result = false;
                                dbusertrans.Rollback();
                                return result;

                            }
                        }
                        else
                        {
                            this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        }
                        innerresultescort = this._DBERP.SaveChanges() > 0;
                    }
                    foreach (var item in _mirror.GuideAgentID_List)
                    {
                        if (item.agentid == 0 || item.agentid == null)
                        {
                            this._DBERP.AgentUser.Add(new AgentUser()
                            {
                                Code = "gu",
                                Name = item.agentname,
                                Status = "new",
                                IsActive = true,
                                CreatedDatetime = DateTime.Now,
                                CreatedBy = userid
                            });

                            var resultnewuser = this._DBERP.SaveChanges() > 0;
                            int New_uid = 0;
                            if (resultnewuser)
                            {
                                New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                                this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                            }
                            else
                            {

                                result = false;
                                dbusertrans.Rollback();
                                return result;

                            }
                        }
                        else
                        {
                            this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        }
                        innerresultguide = this._DBERP.SaveChanges() > 0;
                    }
                    foreach (var item in _mirror.LeaderAgentID_List)
                    {
                        if (item.agentid == 0 || item.agentid == null)
                        {
                            this._DBERP.AgentUser.Add(new AgentUser()
                            {
                                Code = "le",
                                Name = item.agentname,
                                Status = "new",
                                IsActive = true,
                                CreatedDatetime = DateTime.Now,
                                CreatedBy = userid
                            });

                            var resultnewuser = this._DBERP.SaveChanges() > 0;
                            int New_uid = 0;
                            if (resultnewuser)
                            {
                                New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                                this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                            }
                            else
                            {

                                result = false;
                                dbusertrans.Rollback();
                                return result;

                            }
                        }
                        else
                        {
                            this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        }
                        innerresultleader = this._DBERP.SaveChanges() > 0;
                    }
                    foreach (var item in _mirror.DemoAgentID_List)
                    {
                        if (item.agentid == 0 || item.agentid == null)
                        {
                            this._DBERP.AgentUser.Add(new AgentUser()
                            {
                                Code = "de",
                                Name = item.agentname,
                                Status = "new",
                                IsActive = true,
                                CreatedDatetime = DateTime.Now,
                                CreatedBy = userid
                            });

                            var resultnewuser = this._DBERP.SaveChanges() > 0;
                            int New_uid = 0;
                            if (resultnewuser)
                            {
                                New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                                this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                            }
                            else
                            {

                                result = false;
                                dbusertrans.Rollback();
                                return result;

                            }
                        }
                        else
                        {
                            this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                        }
                        innerresultdemo = this._DBERP.SaveChanges() > 0;
                    }

                    dbusertrans.Commit();


                }
                else { dbusertrans.Rollback(); }

            }
            return result;
        }

        public bool afterupdateMirror(MirrorDetailsVM _mirror, int userid)
        {
            bool result = false, innerresult = false, innerresultvehicle = false, innerresultcontact = false;
            bool innerresultpagent = false, innerresulteagnet = false, innerresultguide = false, innerresultleader = false, innerresultescort = false, innerresultdemo = false;

            using (var dbusertrans = this._DBERP.Database.BeginTransaction())
            {
                var entitymirror = _DBERP.MirrorDetails.Where(item => item.Id == _mirror.MirrorId);
                if (entitymirror != null)
                {
                    foreach (var r in entitymirror)
                    {
                        r.Date = _mirror.MirrorDate;
                        r.Pax = _mirror.Pax;
                        r.CountryId = _mirror.Countryid;
                        r.LanguageId = _mirror.Languageid;
                        r.CreatedBy = userid;
                        r.SeriesId = _mirror.SeriesId;
                        r.UpdatedDatetime = DateTime.Now;
                    }
                    result = this._DBERP.SaveChanges() > 0;
                }

                if (_mirror.Mirrors != null)
                {
                    Int64 mid = _mirror.MirrorId;
                    foreach (var item in _mirror.Mirrors)
                    {
                        if (item.driver != null)
                        {
                            foreach (var driver in item.driver)
                            {

                                if (driver != null)
                                {

                                    if (driver.agentid == 0)
                                    {
                                        this._DBERP.AgentUser.Add(new AgentUser()
                                        {
                                            Code = "dr",
                                            Name = driver.name,
                                            Status = "update",
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        });

                                        var resultnewuser = this._DBERP.SaveChanges() > 0;
                                        int New_uid = 0;
                                        if (resultnewuser)
                                        {
                                            New_uid = this._DBERP.AgentUser.Max(p => p.Id);
                                        }
                                        this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now, ParchiAmount = 50, IsParchi = item.Isparchi });
                                        innerresult = this._DBERP.SaveChanges() > 0;
                                        this._DBERP.VehicleDetails.Add(new VehicleDetails() { AgentId = New_uid, VehicleId = driver.vehicletypeid, Number = driver.vehicleNo, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        innerresultvehicle = this._DBERP.SaveChanges() > 0;
                                        var entitycontactnew = _DBERP.AgentContact.FirstOrDefault(i => i.Mobile == driver.mob);
                                        if (entitycontactnew != null)
                                        {
                                            if (entitycontactnew.AgentId != driver.agentid)
                                            {
                                                result = false;

                                                return result;

                                            }

                                        }
                                        else
                                        {

                                            this._DBERP.AgentContact.Add(new AgentContact() { AgentId = New_uid, Mobile = driver.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                            innerresultcontact = this._DBERP.SaveChanges() > 0;
                                        }


                                    }

                                    else
                                    {


                                        this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = driver.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now, ParchiAmount = 50, IsParchi = driver.Isparchi });
                                        innerresult = this._DBERP.SaveChanges() > 0;
                                        this._DBERP.VehicleDetails.Add(new VehicleDetails() { AgentId = (int)driver.agentid, VehicleId = driver.vehicletypeid, Number = driver.vehicleNo, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        innerresultvehicle = this._DBERP.SaveChanges() > 0;




                                        var entitycontact = _DBERP.AgentContact.FirstOrDefault(i => i.Mobile == driver.mob);
                                        if (entitycontact != null)
                                        {
                                            if (entitycontact.AgentId != driver.agentid)
                                            {
                                                result = false;

                                                return result;

                                            }

                                        }
                                        else
                                        {

                                            this._DBERP.AgentContact.Add(new AgentContact() { AgentId = (int)driver.agentid, Mobile = driver.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                            innerresultcontact = this._DBERP.SaveChanges() > 0;
                                        }

                                    }


                                }


                            }

                        }
                        if (item.principle != null)
                        {
                            foreach (var pi in item.principle)
                            {

                                if (pi != null)
                                {



                                    if (pi.agentid == 0 || pi.agentid == null)
                                    {
                                        this._DBERP.AgentUser.Add(new AgentUser()
                                        {
                                            Code = "pi",
                                            Name = pi.name,
                                            Status = "update",
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        });

                                        var resultnewuser = this._DBERP.SaveChanges() > 0;
                                        int New_uid = 0;
                                        if (resultnewuser)
                                        {
                                            New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                                            this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        }
                                        else
                                        {

                                            result = false;

                                            return result;

                                        }
                                    }
                                    else { this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = pi.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }); }
                                    innerresultpagent = this._DBERP.SaveChanges() > 0;


                                }


                            }
                        }
                        if (item.excursion != null)
                        {
                            foreach (var ex in item.excursion)
                            {

                                if (item != null)
                                {



                                    if (ex != null)
                                    {



                                        if (ex.agentid == 0 || ex.agentid == null)
                                        {
                                            this._DBERP.AgentUser.Add(new AgentUser()
                                            {
                                                Code = "ex",
                                                Name = ex.name,
                                                Status = "update",
                                                IsActive = true,
                                                CreatedDatetime = DateTime.Now,
                                                CreatedBy = userid
                                            });

                                            var resultnewuser = this._DBERP.SaveChanges() > 0;
                                            int New_uid = 0;
                                            if (resultnewuser)
                                            {
                                                New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                                                this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                            }
                                            else
                                            {

                                                result = false;

                                                return result;

                                            }
                                        }
                                        else { this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = ex.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }); }
                                        innerresultpagent = this._DBERP.SaveChanges() > 0;


                                    }


                                }


                            }
                        }
                        if (item.teamescort != null)
                        {
                            foreach (var ec in item.teamescort)
                            {

                                if (ec != null)
                                {



                                    if (ec.agentid == 0 || ec.agentid == null)
                                    {
                                        this._DBERP.AgentUser.Add(new AgentUser()
                                        {
                                            Code = "ec",
                                            Name = ec.name,
                                            Status = "update",
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        });

                                        var resultnewuser = this._DBERP.SaveChanges() > 0;
                                        int New_uid = 0;
                                        if (resultnewuser)
                                        {
                                            New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                                            this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        }
                                        else
                                        {

                                            result = false;

                                            return result;

                                        }
                                    }
                                    else { this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = ec.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }); }
                                    innerresultpagent = this._DBERP.SaveChanges() > 0;


                                }


                            }
                        }
                        if (item.teamlead != null)
                        {
                            foreach (var te in item.teamlead)
                            {

                                if (te != null)
                                {



                                    if (te.agentid == 0 || te.agentid == null)
                                    {
                                        this._DBERP.AgentUser.Add(new AgentUser()
                                        {
                                            Code = "te",
                                            Name = te.name,
                                            Status = "update",
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        });

                                        var resultnewuser = this._DBERP.SaveChanges() > 0;
                                        int New_uid = 0;
                                        if (resultnewuser)
                                        {
                                            New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                                            this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        }
                                        else
                                        {

                                            result = false;

                                            return result;

                                        }
                                    }
                                    else { this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = te.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }); }
                                    innerresultpagent = this._DBERP.SaveChanges() > 0;


                                }


                            }
                        }
                        if (item.guide != null)
                        {
                            foreach (var gu in item.guide)
                            {

                                if (gu != null)
                                {



                                    if (gu.agentid == 0 || gu.agentid == null)
                                    {
                                        this._DBERP.AgentUser.Add(new AgentUser()
                                        {
                                            Code = "gu",
                                            Name = gu.name,
                                            Status = "update",
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        });

                                        var resultnewuser = this._DBERP.SaveChanges() > 0;
                                        int New_uid = 0;
                                        if (resultnewuser)
                                        {
                                            New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                                            this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        }
                                        else
                                        {

                                            result = false;

                                            return result;

                                        }
                                    }
                                    else { this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = gu.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }); }
                                    innerresultpagent = this._DBERP.SaveChanges() > 0;


                                }


                            }
                        }
                        if (item.demo != null)
                        {
                            foreach (var de in item.demo)
                            {

                                if (de != null)
                                {



                                    if (de.agentid == 0 || de.agentid == null)
                                    {
                                        this._DBERP.AgentUser.Add(new AgentUser()
                                        {
                                            Code = "de",
                                            Name = de.name,
                                            Status = "update",
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        });

                                        var resultnewuser = this._DBERP.SaveChanges() > 0;
                                        int New_uid = 0;
                                        if (resultnewuser)
                                        {
                                            New_uid = this._DBERP.AgentUser.Max(p => p.Id);


                                            this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        }
                                        else
                                        {

                                            result = false;

                                            return result;

                                        }
                                    }
                                    else { this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = de.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }); }
                                    innerresultpagent = this._DBERP.SaveChanges() > 0;


                                }


                            }
                        }
                    }




                    result = true;


                }
                else { result = false; }

                if (result)
                {

                    dbusertrans.Commit();
                }
                else { dbusertrans.Rollback(); }

            }
          
            return result;
        }

        public MirrorDetailsVM EditMirror(Int64 mid)
        {
            MirrorDetailsVM _mirror = new MirrorDetailsVM();

            List<MirrorDetailVM> _allmirrordetails = new List<MirrorDetailVM>();
            List<AgentMaster> _agentmaster = this._DBERP.AgentMaster.Where(i => i.IsActive == true).ToList();
            List<LanguagesMaster> _langmaster = this._DBERP.LanguagesMaster.Where(i => i.IsActive == true).ToList();
            List<CountriesMaster> _countrymaster = this._DBERP.CountriesMaster.Where(i => i.IsActive == true).ToList();
            List<VehicleMaster> _vehiclemaster = this._DBERP.VehicleMaster.Where(i => i.IsActive == true).ToList();
            List<SeriesMaster> _seriesmaster = this._DBERP.SeriesMaster.Where(i => i.IsActive == true).ToList();

            _mirror.Mirrors = (from m in this._DBERP.MirrorDetails
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


                               where m.Id == mid && ma.IsActive==true 
                               select new MirrorDetailVM
                               {
                                   mirrorid = m.Id,
                                   mirrordate = m.Date,
                                   name = au.Name,
                                   mob = d.Mobile,
                                   agentcode = au.Code,
                                   SeriesId = m.SeriesId,
                                   Countryid = m.CountryId,
                                   Languageid = m.LanguageId,
                                   Pax = m.Pax,
                                   Isparchi = ma.IsParchi,
                                   vehicleNo = vh.Number,
                                   vehicletypeid = vh.VehicleId,
                                   agentid = ma.AgentId


                               }).ToList().GroupBy(c => c.mirrorid)
    .Select(g => new MirrorDetailVM
    {
        mirrorid = g.Key,
        mirrordate = g.FirstOrDefault().mirrordate,
        Pax=g.FirstOrDefault().Pax,
        SeriesId = g.FirstOrDefault().SeriesId,
        Languageid = g.FirstOrDefault().Languageid,
        Countryid = g.FirstOrDefault().Countryid,
        vehicleNo = g.FirstOrDefault().vehicleNo,
        vehicletypeid = g.FirstOrDefault().vehicletypeid,
        principle = g.Where(c => c.agentcode == "pi" ).ToList(), 
        driver = g.Where(c => c.agentcode == "dr").ToList(),
        excursion = g.Where(c => c.agentcode == "ex").ToList(),
        guide = g.Where(c => c.agentcode == "gu").ToList(),
        teamescort = g.Where(c => c.agentcode == "ec").ToList(),
        teamlead = g.Where(c => c.agentcode == "le").ToList(),
        demo = g.Where(c => c.agentcode == "de").ToList()
        // p= g.Where(c => c.agentcode == "pi").SelectMany(a=>a.name).SingleOrDefault().ToString()
    }).ToList();


            _mirror.vehicledetails = BindingListUtillity.GenerateSelectList(_vehiclemaster);
            _mirror.languagedetails = BindingListUtillity.GenerateSelectList(_langmaster);
            _mirror.countrydetails = BindingListUtillity.GenerateSelectList(_countrymaster);
            _mirror.seriesdetails = BindingListUtillity.GenerateSelectList(_seriesmaster);


            return _mirror;

        }

        public bool UpdateMirror(MirrorDetailsVM _mirror, int uid)
        {
            bool result = false, innerresult = false, innerresultvehicle = false, innerresultcontact = false;
            try
            {

                using (var dbusertrans = this._DBERP.Database.BeginTransaction())
                {


                    var entitycontact = _DBERP.MirrorDetails.Where(a => a.Id == _mirror.MirrorId);
                    if (entitycontact != null)
                    {
                        var mirrorUsers = _DBERP.MirrorAgent.Where(item => item.MirrorId == _mirror.MirrorId);

                        if (mirrorUsers != null)
                        {
                            foreach (var r in mirrorUsers)
                            {
                                r.IsActive = false;
                                r.UpdatedDatetime = DateTime.Now;
                            }
                            result = this._DBERP.SaveChanges() > 0;
                        } 


                        foreach (var _mir in _mirror.Mirrors)
                        {
                            if (_mir != null)
                            {
                                if (_mir.driver != null)
                                {
                                    foreach (var d in _mir.driver)
                                    {
                                        if (d != null)
                                        {


                                          //  var agentUsers = _DBERP.AgentUser.Where(item => item.AgentId == d.agentid);
                                            var agentcontact = _DBERP.AgentContact.Where(item => item.AgentId == d.agentid);
                                            var agentvehicle = _DBERP.VehicleDetails.Where(item => item.AgentId == d.agentid);
                                            //if (agentUsers != null)
                                            //{
                                            //    foreach (var r in agentUsers)
                                            //    {
                                            //        r.IsActive = false;
                                            //        r.Updateddatetime = DateTime.Now;
                                            //    }
                                            //    result = this._DBERP.SaveChanges() > 0;
                                            //}
                                            if (agentcontact != null)
                                            {
                                                foreach (var r in agentcontact)
                                                {
                                                    r.IsActive = false;
                                                    r.UpdatedDatetime = DateTime.Now;
                                                }
                                                result = this._DBERP.SaveChanges() > 0;
                                            }
                                            if (agentvehicle != null)
                                            {
                                                foreach (var r in agentvehicle)
                                                {
                                                    r.IsActive = false;
                                                    r.UpdatedDatetime = DateTime.Now;
                                                }
                                                result = this._DBERP.SaveChanges() > 0;
                                            }


                                        }
                                    }
                                }
                                //if (_mir.principle != null)
                                //{
                                //    foreach (var d in _mir.principle)
                                //    {
                                //        if (d != null)
                                //        {
                                //            var agentUsers = _DBERP.AgentUser.Where(item => item.AgentId == d.agentid);
                                //            if (agentUsers != null)
                                //            {
                                //                foreach (var r in agentUsers)
                                //                {
                                //                    r.IsActive = false;
                                //                    r.Updateddatetime = DateTime.Now;
                                //                }
                                //                result = this._DBERP.SaveChanges() > 0;
                                //            }

                                //        }
                                //    }
                                //}
                                //if (_mir.excursion != null)
                                //{
                                //    foreach (var d in _mir.excursion)
                                //    {
                                //        if (d != null)
                                //        {
                                //            var agentUsers = _DBERP.AgentUser.Where(item => item.AgentId == d.agentid);
                                //            if (agentUsers != null)
                                //            {
                                //                foreach (var r in agentUsers)
                                //                {
                                //                    r.IsActive = false;
                                //                    r.Updateddatetime = DateTime.Now;
                                //                }
                                //                result = this._DBERP.SaveChanges() > 0;
                                //            }

                                //        }
                                //    }
                                //}
                                //if (_mir.teamescort != null)
                                //{
                                //    foreach (var d in _mir.teamescort)
                                //    {
                                //        if (d != null)
                                //        {
                                //            var agentUsers = _DBERP.AgentUser.Where(item => item.AgentId == d.agentid);
                                //            if (agentUsers != null)
                                //            {
                                //                foreach (var r in agentUsers)
                                //                {
                                //                    r.IsActive = false;
                                //                    r.Updateddatetime = DateTime.Now;
                                //                }
                                //                result = this._DBERP.SaveChanges() > 0;
                                //            }

                                //        }
                                //    }
                                //}
                                //if (_mir.guide != null)
                                //{
                                //    foreach (var d in _mir.guide)
                                //    {
                                //        if (d != null)
                                //        {
                                //            var agentUsers = _DBERP.AgentUser.Where(item => item.AgentId == d.agentid);
                                //            if (agentUsers != null)
                                //            {
                                //                foreach (var r in agentUsers)
                                //                {
                                //                    r.IsActive = false;
                                //                    r.Updateddatetime = DateTime.Now;
                                //                }
                                //                result = this._DBERP.SaveChanges() > 0;
                                //            }

                                //        }
                                //    }
                                //}
                                //if (_mir.teamlead != null)
                                //{
                                //    foreach (var d in _mir.teamlead)
                                //    {
                                //        if (d != null)
                                //        {
                                //            var agentUsers = _DBERP.AgentUser.Where(item => item.AgentId == d.agentid);
                                //            if (agentUsers != null)
                                //            {
                                //                foreach (var r in agentUsers)
                                //                {
                                //                    r.IsActive = false;
                                //                    r.Updateddatetime = DateTime.Now;
                                //                }
                                //                result = this._DBERP.SaveChanges() > 0;
                                //            }

                                //        }
                                //    }
                                //}
                                //if (_mir.demo != null)
                                //{
                                //    foreach (var d in _mir.demo)
                                //    {
                                //        if (d != null)
                                //        {
                                //            var agentUsers = _DBERP.AgentUser.Where(item => item.AgentId == d.agentid);
                                //            if (agentUsers != null)
                                //            {
                                //                foreach (var r in agentUsers)
                                //                {
                                //                    r.IsActive = false;
                                //                    r.Updateddatetime = DateTime.Now;
                                //                }
                                //                result = this._DBERP.SaveChanges() > 0;
                                //            }

                                //        }
                                //    }
                                //}

                            }
                        }


                    }
                    if (result)
                    {
                        dbusertrans.Commit();
                        bool resultinner;
                       resultinner= afterupdateMirror(_mirror, uid);

                       

                    }
                    else { dbusertrans.Rollback(); }
                    return true;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
    }
}
