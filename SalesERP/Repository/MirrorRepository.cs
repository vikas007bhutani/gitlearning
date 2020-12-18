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
    public class MirrorRepository : IMirrorRepository
    {
        private Sales_ERPContext _DBERP;
        public MirrorRepository(Sales_ERPContext dbcontext)
        {

            this._DBERP = dbcontext;

        }

        public async Task<MirrorDetailsVM> getAllMirrors()
        {
            MirrorDetailsVM _mirror = new MirrorDetailsVM();


            List<MirrorDetailVM> _allmirrordetails = new List<MirrorDetailVM>();
            List<AgentMaster> _agentmaster = await this._DBERP.AgentMaster.Where(i => i.IsActive == true).ToListAsync().ConfigureAwait(false);
            List<LanguagesMaster> _langmaster =await this._DBERP.LanguagesMaster.Where(i => i.IsActive == true).ToListAsync().ConfigureAwait(false);
            List<CountriesMaster> _countrymaster =await this._DBERP.CountriesMaster.Where(i => i.IsActive == true).ToListAsync().ConfigureAwait(false);
            List<VehicleMaster> _vehiclemaster =await this._DBERP.VehicleMaster.Where(i => i.IsActive == true).ToListAsync().ConfigureAwait(false);
            List<SeriesMaster> _seriesmaster =await this._DBERP.SeriesMaster.Where(i => i.IsActive == true).ToListAsync().ConfigureAwait(false);
            //_mirror.MirrorDate= DateTime.ParseExact(txtexpirydate.Text, "dd/MM/yyyy",
            //                              CultureInfo.InvariantCulture); Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh:mm"));
            var mirrors = await (from m in this._DBERP.MirrorDetails
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
                                join l in this._DBERP.LanguagesMaster
                                on m.LanguageId equals l.Id into language
                                from la in language.Where(f => f.IsActive == true).DefaultIfEmpty()

                                 where m.IsActive == true
                                 select new MirrorDetailVM
                                 {
                                     mirrorid = m.Id,
                                     mirrordate = m.Date,
                                     name = au.Name,
                                     mob = d.Mobile,
                                     agentcode = au.Code,
                                     language=la.Name


                                 }).ToListAsync().ConfigureAwait(false);
            _mirror.Mirrors = mirrors.GroupBy(c => c.mirrorid)
                                .Select(g => new MirrorDetailVM
                                {
                                    mirrorid = g.Key,
                                    mirrordate = g.FirstOrDefault().mirrordate,
                                    principle = g.Where(c => c.agentcode == "pi").ToList(),
                                    driver = g.Where(c => c.agentcode == "dr").ToList(),
                                    excursion = g.Where(c => c.agentcode == "gu").ToList(),
                                    demo = g.Where(c => c.agentcode == "de").ToList(),
                                    language=g.FirstOrDefault().language
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

        public async Task<bool> AddMirror(MirrorDetailsVM _mirror, int userid)
        {
            bool result = false, innerresult = false, innerresultvehicle = false, innerresultcontact = false;
            try
            {

            
           
            using (var dbusertrans = await this._DBERP.Database.BeginTransactionAsync().ConfigureAwait(false))
            {
              await  this._DBERP.MirrorDetails.AddAsync(new MirrorDetails()
                {
                    Date = _mirror.MirrorDate,
                    Pax = _mirror.Pax,
                    CountryId = _mirror.Countryid,
                    LanguageId = _mirror.Languageid,
                    SeriesId=_mirror.SeriesId,
                    CreatedBy = userid,
                    CreatedDatetime = DateTime.Now,
                    unitid=_mirror.UnitId

                }).ConfigureAwait(false);

                result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                if (result)
                {
                    Int64 mid =await this._DBERP.MirrorDetails.MaxAsync(p => p.Id).ConfigureAwait(false);

                        await this._DBERP.MirrorList.AddAsync(new MirrorList()
                        {
                           MirrorId=mid,
                            Status=1,
                            CreatedBy = userid,
                            CreatedDatetime = DateTime.Now,
                            unitid=_mirror.UnitId

                        }).ConfigureAwait(false);
                        result = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                        foreach (var item in _mirror.Driver_List)
                    {
                        if (item != null)
                        {

                            if (item.agentId == 0 || item.agentId == null)
                            {
                               await this._DBERP.AgentUser.AddAsync(new AgentUser()
                                {
                                    Code = "dr",
                                    Name = item.Drivername,
                                    Status = "new",
                                    IsActive = true,
                                    CreatedDatetime = DateTime.Now,
                                    CreatedBy = userid
                                }).ConfigureAwait(false);

                                var resultnewuser =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                int New_uid = 0;
                                if (resultnewuser)
                                {
                                    New_uid =await this._DBERP.AgentUser.MaxAsync(p => p.Id).ConfigureAwait(false);
                                }
                                await this._DBERP.MirrorAgent.AddAsync(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now, ParchiAmount = item.parchiamount, IsParchi = item.isparchiamount }).ConfigureAwait(false);
                                innerresult =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                await this._DBERP.VehicleDetails.AddAsync(new VehicleDetails() { AgentId = New_uid, VehicleId = item.vehicletypeid, Number = item.vehicleNo, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                innerresultvehicle =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == item.Mob && i.IsActive==true).ConfigureAwait(false);
                                if (entitycontactnew != null)
                                {
                                    if (entitycontactnew.AgentId != item.agentId)
                                    {
                                        result = false;
                                       await dbusertrans.RollbackAsync().ConfigureAwait(false);
                                        return result;

                                    }


                                }
                                else
                                {

                                   await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                    innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                }


                            }

                            else
                            {


                                await this._DBERP.MirrorAgent.AddAsync(new MirrorAgent() { AgentId = item.agentId, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now, ParchiAmount = item.parchiamount, IsParchi = item.isparchiamount }).ConfigureAwait(false);
                                innerresult =await this._DBERP.SaveChangesAsync() > 0;

                                    var agentcontact = await _DBERP.AgentContact.Where(a => a.AgentId == item.agentId && a.IsActive == true).ToListAsync().ConfigureAwait(false);
                                    var agentvehicle = await _DBERP.VehicleDetails.Where(a => a.AgentId == item.agentId && a.IsActive == true).ToListAsync().ConfigureAwait(false);
                                    //if (agentUsers != null)
                                    //{
                                    //    foreach (var r in agentUsers)
                                    //    {
                                    //        r.IsActive = false;
                                    //        r.Updateddatetime = DateTime.Now;
                                    //    }
                                    //    result = this._DBERP.SaveChanges() > 0;
                                    //}
                                    if (agentcontact.Count>0 && agentcontact != null)
                                    {
                                        foreach (var r in agentcontact)
                                        {
                                           
                                            r.Mobile = item.Mob;
                                            r.UpdatedDatetime = DateTime.Now;
                                        }
                                        result = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    }
                                    else
                                    {

                                        await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = item.agentId, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                        innerresultcontact = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    }
                                    if (agentvehicle.Count > 0 &&  agentvehicle != null)
                                    {
                                        foreach (var r in agentvehicle)
                                        {
                                           
                                            r.VehicleId = item.vehicletypeid;
                                            r.Number = item.vehicleNo;
                                            r.UpdatedDatetime = DateTime.Now;
                                        }
                                        result = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    }
                                    else
                                    {   await this._DBERP.VehicleDetails.AddAsync(new VehicleDetails() { AgentId = item.agentId, VehicleId = item.vehicletypeid, Number = item.vehicleNo, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                        innerresultvehicle = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    }

                                    #region old
                                    //await this._DBERP.VehicleDetails.AddAsync(new VehicleDetails() { AgentId = item.agentId, VehicleId = item.vehicletypeid, Number = item.vehicleNo, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                    //innerresultvehicle =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;




                                    //var entitycontact =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == item.Mob && i.IsActive == true).ConfigureAwait(false);
                                    //if (entitycontact != null)
                                    //{
                                    //    if (entitycontact.AgentId != item.agentId)
                                    //    {
                                    //        result = false;
                                    //       await dbusertrans.RollbackAsync().ConfigureAwait(false);
                                    //        return result;

                                    //    }

                                    //}
                                    //else
                                    //{

                                    //   await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = item.agentId, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                    //    innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    //}
                                    #endregion

                                }


                                }
                    }
                    bool innerresultpagent = false, innerresulteagnet = false, innerresultguide = false, innerresultleader = false, innerresultescort = false, innerresultdemo = false;
                    foreach (var item in _mirror.PAgentID_List)
                    {
                        if (item.agentid == 0 || item.agentid == null)
                        {
                           await this._DBERP.AgentUser.AddAsync(new AgentUser()
                            {
                                Code = "pi",
                                Name = item.agentname,
                                Status = "new",
                                IsActive = true,
                                CreatedDatetime = DateTime.Now,
                                CreatedBy = userid
                            }).ConfigureAwait(false);

                            var resultnewuser =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                            int New_uid = 0;
                            if (resultnewuser)
                            {
                                New_uid =await this._DBERP.AgentUser.MaxAsync(p => p.Id).ConfigureAwait(false);


                               await this._DBERP.MirrorAgent.AddAsync(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);

                                var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == item.Mob && i.IsActive==true).ConfigureAwait(false);
                                if (entitycontactnew != null)
                                {
                                    if (entitycontactnew.AgentId != item.agentid)
                                    {
                                        result = false;
                                        await dbusertrans.RollbackAsync().ConfigureAwait(false);
                                        return result;

                                    }

                                }
                                else
                                {

                                   await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                    innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                }





                            }
                            else
                            {

                              

                                result = false;
                                await dbusertrans.RollbackAsync().ConfigureAwait(false);
                                return result;

                            }
                        }
                        else {
                                this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });

                                var agentcontact = await _DBERP.AgentContact.Where(a => a.AgentId == item.agentid && a.IsActive == true).ToListAsync().ConfigureAwait(false);
                            
                                if (agentcontact.Count>0 && agentcontact != null)
                                {
                                    foreach (var r in agentcontact)
                                    {
                                      
                                        r.Mobile = item.Mob;
                                        r.UpdatedDatetime = DateTime.Now;
                                    }
                                    result = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                }
                                else
                                {

                                    await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = item.agentid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                    innerresultcontact = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                }


                                
                                #region old
                                //    var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == item.Mob && i.IsActive==true).ConfigureAwait(false);
                                //if (entitycontactnew != null)
                                //{
                                //    if (entitycontactnew.AgentId != item.agentid)
                                //    {
                                //        result = false;
                                //       await dbusertrans.RollbackAsync().ConfigureAwait(false);
                                //        return result;

                                //    }

                                //}
                                //else
                                //{

                                //   await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = item.agentid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                //    innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                //}
                                #endregion




                            }
                            innerresultpagent = this._DBERP.SaveChanges() > 0;
                    }
                    foreach (var item in _mirror.EAgentID_List)
                    {
                            if (!string.IsNullOrEmpty(item.agentname))
                            {
                                if (item.agentid == 0 || item.agentid == null)
                                {
                                    await this._DBERP.AgentUser.AddAsync(new AgentUser()
                                    {
                                        Code = "ex",
                                        Name = item.agentname,
                                        Status = "new",
                                        IsActive = true,
                                        CreatedDatetime = DateTime.Now,
                                        CreatedBy = userid
                                    }).ConfigureAwait(false);

                                    var resultnewuser = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    int New_uid = 0;
                                    if (resultnewuser)
                                    {
                                        New_uid = await this._DBERP.AgentUser.MaxAsync(p => p.Id).ConfigureAwait(false);

                                        var entitycontactnew = await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == item.Mob && i.IsActive == true).ConfigureAwait(false);
                                        if (entitycontactnew != null)
                                        {
                                            if (entitycontactnew.AgentId != item.agentid)
                                            {
                                                result = false;
                                                await dbusertrans.RollbackAsync().ConfigureAwait(false);
                                                return result;

                                            }

                                        }
                                        else
                                        {

                                            await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                            innerresultcontact = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }





                                        await this._DBERP.MirrorAgent.AddAsync(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                    }
                                    else
                                    {

                                        result = false;
                                        await dbusertrans.RollbackAsync().ConfigureAwait(false);
                                        return result;

                                    }
                                }
                                else
                                {
                                    this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                    var agentcontact = await _DBERP.AgentContact.Where(a => a.AgentId == item.agentid && a.IsActive == true).ToListAsync().ConfigureAwait(false);

                                    if (agentcontact.Count > 0 && agentcontact != null)
                                    {
                                        foreach (var r in agentcontact)
                                        {

                                            r.Mobile = item.Mob;
                                            r.UpdatedDatetime = DateTime.Now;
                                        }
                                        result = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    }
                                    else
                                    {

                                        await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = item.agentid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                        innerresultcontact = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    }


                                    #region old
                                    //    var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == item.Mob && i.IsActive == true).ConfigureAwait(false);
                                    //if (entitycontactnew != null)
                                    //{
                                    //    if (entitycontactnew.AgentId != item.agentid)
                                    //    {
                                    //        result = false;
                                    //       await dbusertrans.RollbackAsync().ConfigureAwait(false);
                                    //        return result;

                                    //    }

                                    //}
                                    //else
                                    //{

                                    //  await  this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = item.agentid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                    //    innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    //}
                                    #endregion


                                }
                                innerresulteagnet = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                            }
                    }
                    foreach (var item in _mirror.EscortAgentID_List)
                    {
                            if (!string.IsNullOrEmpty(item.agentname))
                            {
                                if (item.agentid == 0 || item.agentid == null)
                                {
                                    await this._DBERP.AgentUser.AddAsync(new AgentUser()
                                    {
                                        Code = "ec",
                                        Name = item.agentname,
                                        Status = "new",
                                        IsActive = true,
                                        CreatedDatetime = DateTime.Now,
                                        CreatedBy = userid
                                    }).ConfigureAwait(false);

                                    var resultnewuser = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    int New_uid = 0;
                                    if (resultnewuser)
                                    {
                                        New_uid = await this._DBERP.AgentUser.MaxAsync(p => p.Id).ConfigureAwait(false);

                                        var entitycontactnew = await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == item.Mob && i.IsActive == true).ConfigureAwait(false);
                                        if (entitycontactnew != null)
                                        {
                                            if (entitycontactnew.AgentId != item.agentid)
                                            {
                                                result = false;
                                                await dbusertrans.RollbackAsync().ConfigureAwait(false);
                                                return result;

                                            }

                                        }
                                        else
                                        {

                                            await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                            innerresultcontact = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }





                                        await this._DBERP.MirrorAgent.AddAsync(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                    }
                                    else
                                    {

                                        result = false;
                                        await dbusertrans.RollbackAsync().ConfigureAwait(false);
                                        return result;

                                    }
                                }
                                else
                                {
                                    this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                    var agentcontact = await _DBERP.AgentContact.Where(a => a.AgentId == item.agentid && a.IsActive == true).ToListAsync().ConfigureAwait(false);

                                    if (agentcontact.Count > 0 && agentcontact != null)
                                    {
                                        foreach (var r in agentcontact)
                                        {

                                            r.Mobile = item.Mob;
                                            r.UpdatedDatetime = DateTime.Now;
                                        }
                                        result = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    }
                                    else
                                    {

                                        await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = item.agentid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                        innerresultcontact = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    }

                                    //this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                    #region old
                                    //    var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == item.Mob && i.IsActive == true).ConfigureAwait(false);
                                    //if (entitycontactnew != null)
                                    //{
                                    //    if (entitycontactnew.AgentId != item.agentid)
                                    //    {
                                    //        result = false;
                                    //      await  dbusertrans.RollbackAsync().ConfigureAwait(false);
                                    //        return result;

                                    //    }

                                    //}
                                    //else
                                    //{

                                    //   await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = item.agentid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                    //    innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    //}
                                    #endregion

                                }
                                innerresultescort = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                            }
                    }
                    foreach (var item in _mirror.GuideAgentID_List)
                    {
                            if (!string.IsNullOrEmpty(item.agentname))
                            {
                                if (item.agentid == 0 || item.agentid == null)
                                {
                                    await this._DBERP.AgentUser.AddAsync(new AgentUser()
                                    {
                                        Code = "gu",
                                        Name = item.agentname,
                                        Status = "new",
                                        IsActive = true,
                                        CreatedDatetime = DateTime.Now,
                                        CreatedBy = userid
                                    }).ConfigureAwait(false);

                                    var resultnewuser = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    int New_uid = 0;
                                    if (resultnewuser)
                                    {
                                        New_uid = await this._DBERP.AgentUser.MaxAsync(p => p.Id);
                                        var entitycontactnew = await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == item.Mob && i.IsActive == true).ConfigureAwait(false);
                                        if (entitycontactnew != null)
                                        {
                                            if (entitycontactnew.AgentId != item.agentid)
                                            {
                                                result = false;
                                                await dbusertrans.RollbackAsync().ConfigureAwait(false);
                                                return result;

                                            }

                                        }
                                        else
                                        {

                                            await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                            innerresultcontact = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }


                                        await this._DBERP.MirrorAgent.AddAsync(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                    }
                                    else
                                    {

                                        result = false;
                                        await dbusertrans.RollbackAsync().ConfigureAwait(false);
                                        return result;

                                    }
                                }
                                else
                                {
                                    this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                    var agentcontact = await _DBERP.AgentContact.Where(a => a.AgentId == item.agentid && a.IsActive == true).ToListAsync().ConfigureAwait(false);

                                    if (agentcontact.Count > 0 && agentcontact != null)
                                    {
                                        foreach (var r in agentcontact)
                                        {

                                            r.Mobile = item.Mob;
                                            r.UpdatedDatetime = DateTime.Now;
                                        }
                                        result = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    }
                                    else
                                    {

                                        await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = item.agentid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        innerresultcontact = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    }
                                    #region old
                                    //this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                    //var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == item.Mob && i.IsActive == true).ConfigureAwait(false);
                                    //if (entitycontactnew != null)
                                    //{
                                    //    if (entitycontactnew.AgentId != item.agentid)
                                    //    {
                                    //        result = false;
                                    //      await  dbusertrans.RollbackAsync().ConfigureAwait(false);
                                    //        return result;

                                    //    }

                                    //}
                                    //else
                                    //{

                                    //  await  this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = item.agentid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                    //    innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    //}
                                    #endregion


                                }
                                innerresultguide = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                            }
                    }
                    foreach (var item in _mirror.LeaderAgentID_List)
                    {
                            if (!string.IsNullOrEmpty(item.agentname))
                            {
                                if (item.agentid == 0 || item.agentid == null)
                                {
                                    await this._DBERP.AgentUser.AddAsync(new AgentUser()
                                    {
                                        Code = "te",
                                        Name = item.agentname,
                                        Status = "new",
                                        IsActive = true,
                                        CreatedDatetime = DateTime.Now,
                                        CreatedBy = userid
                                    }).ConfigureAwait(false);

                                    var resultnewuser = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    int New_uid = 0;
                                    if (resultnewuser)
                                    {
                                        New_uid = await this._DBERP.AgentUser.MaxAsync(p => p.Id).ConfigureAwait(false);
                                        var entitycontactnew = await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == item.Mob && i.IsActive == true).ConfigureAwait(false);
                                        if (entitycontactnew != null)
                                        {
                                            if (entitycontactnew.AgentId != item.agentid)
                                            {
                                                result = false;
                                                await dbusertrans.RollbackAsync().ConfigureAwait(false);
                                                return result;

                                            }

                                        }
                                        else
                                        {

                                            await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                            innerresultcontact = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }


                                        await this._DBERP.MirrorAgent.AddAsync(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                    }
                                    else
                                    {

                                        result = false;
                                        await dbusertrans.RollbackAsync().ConfigureAwait(false);
                                        return result;

                                    }
                                }
                                else
                                {
                                    this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });

                                    var agentcontact = await _DBERP.AgentContact.Where(a => a.AgentId == item.agentid && a.IsActive == true).ToListAsync().ConfigureAwait(false);

                                    if (agentcontact.Count > 0 && agentcontact != null)
                                    {
                                        foreach (var r in agentcontact)
                                        {

                                            r.Mobile = item.Mob;
                                            r.UpdatedDatetime = DateTime.Now;
                                        }
                                        result = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    }
                                    else
                                    {

                                        await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = item.agentid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                        innerresultcontact = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    }
                                    #region old


                                    //var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == item.Mob && i.IsActive==true).ConfigureAwait(false);
                                    //if (entitycontactnew != null)
                                    //{
                                    //    if (entitycontactnew.AgentId != item.agentid)
                                    //    {
                                    //        result = false;
                                    //     await   dbusertrans.RollbackAsync().ConfigureAwait(false);
                                    //        return result;

                                    //    }

                                    //}
                                    //else
                                    //{

                                    //   await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = item.agentid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                    //    innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    //}
                                    #endregion


                                }
                                innerresultleader = await this._DBERP.SaveChangesAsync() > 0;
                            }
                    }
                    foreach (var item in _mirror.DemoAgentID_List)
                    {
                            if (!string.IsNullOrEmpty(item.agentname))
                            {
                                if (item.agentid == 0 || item.agentid == null)
                                {
                                    await this._DBERP.AgentUser.AddAsync(new AgentUser()
                                    {
                                        Code = "de",
                                        Name = item.agentname,
                                        Status = "new",
                                        IsActive = true,
                                        CreatedDatetime = DateTime.Now,
                                        CreatedBy = userid
                                    }).ConfigureAwait(false);

                                    var resultnewuser = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    int New_uid = 0;
                                    if (resultnewuser)
                                    {
                                        New_uid = await this._DBERP.AgentUser.MaxAsync(p => p.Id).ConfigureAwait(false);

                                        var entitycontactnew = await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == item.Mob && i.IsActive == true).ConfigureAwait(false);
                                        if (entitycontactnew != null)
                                        {
                                            if (entitycontactnew.AgentId != item.agentid)
                                            {
                                                result = false;
                                                await dbusertrans.RollbackAsync().ConfigureAwait(false);
                                                return result;

                                            }

                                        }
                                        else
                                        {

                                            await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                            innerresultcontact = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }

                                        await this._DBERP.MirrorAgent.AddAsync(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                    }
                                    else
                                    {

                                        result = false;
                                        await dbusertrans.RollbackAsync().ConfigureAwait(false);
                                        return result;

                                    }
                                }
                                else
                                {
                                    this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = item.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                    var agentcontact = await _DBERP.AgentContact.Where(a => a.AgentId == item.agentid && a.IsActive == true).ToListAsync().ConfigureAwait(false);

                                    if (agentcontact.Count > 0 && agentcontact != null)
                                    {
                                        foreach (var r in agentcontact)
                                        {

                                            r.Mobile = item.Mob;
                                            r.UpdatedDatetime = DateTime.Now;
                                        }
                                        result = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    }
                                    else
                                    {

                                        await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = item.agentid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        innerresultcontact = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    }
                                    #region old

                                    //var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == item.Mob && i.IsActive==true).ConfigureAwait(false);
                                    //if (entitycontactnew != null)
                                    //{
                                    //    if (entitycontactnew.AgentId != item.agentid)
                                    //    {
                                    //        result = false;
                                    //      await  dbusertrans.RollbackAsync().ConfigureAwait(false);
                                    //        return result;

                                    //    }

                                    //}
                                    //else
                                    //{

                                    //   await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = item.agentid, Mobile = item.Mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                    //    innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                    //}
                                    #endregion


                                }
                                innerresultdemo = await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                            }
                    }

                   await dbusertrans.CommitAsync().ConfigureAwait(false);


                }
                else { await dbusertrans.RollbackAsync().ConfigureAwait(false); }

            }
            }
            catch (Exception)
            {

                throw;
            }
            return result;
        }

        public async Task<bool> afterupdateMirror(MirrorDetailsVM _mirror, int userid)
        {
            bool result = false, innerresult = false, innerresultvehicle = false, innerresultcontact = false;
            bool innerresultpagent = false, innerresulteagnet = false, innerresultguide = false, innerresultleader = false, innerresultescort = false, innerresultdemo = false;

            using (var dbusertrans =await this._DBERP.Database.BeginTransactionAsync().ConfigureAwait(false))
            {
                var entitymirror =await _DBERP.MirrorDetails.Where(item => item.Id == _mirror.MirrorId).ToListAsync().ConfigureAwait(false);
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
                    result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
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
                                       await this._DBERP.AgentUser.AddAsync(new AgentUser()
                                        {
                                            Code = "dr",
                                            Name = driver.name,
                                            Status = "update",
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        }).ConfigureAwait(false);

                                        var resultnewuser =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        int New_uid = 0;
                                        if (resultnewuser)
                                        {
                                            New_uid =await this._DBERP.AgentUser.MaxAsync(p => p.Id).ConfigureAwait(false);
                                        }
                                        await this._DBERP.MirrorAgent.AddAsync(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now, ParchiAmount = 50, IsParchi = item.Isparchi }).ConfigureAwait(false);
                                        innerresult =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        await this._DBERP.VehicleDetails.AddAsync(new VehicleDetails() { AgentId = New_uid, VehicleId = driver.vehicletypeid, Number = driver.vehicleNo, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                        innerresultvehicle =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == driver.mob).ConfigureAwait(false);
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

                                          await  this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = driver.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                            innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }


                                    }

                                    else
                                    {
                                        var entityagentuser =await _DBERP.AgentUser.FirstOrDefaultAsync(i => i.Id == driver.agentid).ConfigureAwait(false);
                                        if (entityagentuser != null)
                                        {

                                            entityagentuser.IsActive = true;
                                            entityagentuser.UpdatedDatetime = DateTime.Now;
                                            entityagentuser.UpdatedBy = userid;
                                            entityagentuser.Name = driver.name;
                                            entityagentuser.Code = "dr";


                                            result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }

                                      //  var resultnewuser = this._DBERP.SaveChanges() > 0;

                                       await this._DBERP.MirrorAgent.AddAsync(new MirrorAgent() { AgentId = driver.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now, ParchiAmount = 50, IsParchi = driver.Isparchi }).ConfigureAwait(false);
                                        innerresult =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                      await  this._DBERP.VehicleDetails.AddAsync(new VehicleDetails() { AgentId = (int)driver.agentid, VehicleId = driver.vehicletypeid, Number = driver.vehicleNo, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                        innerresultvehicle =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;




                                        var entitycontact =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == driver.mob).ConfigureAwait(false);
                                        if (entitycontact != null)
                                        {
                                            if (entitycontact.AgentId != driver.agentid)
                                            {
                                                result = false;

                                                return result;

                                            }
                                            else
                                            {

                                                entitycontact.IsActive = true;
                                                entitycontact.UpdatedDatetime = DateTime.Now;
                                                entitycontact.UpdatedBy = userid;
                                                entitycontact.Mobile = driver.mob;

                                            }

                                        }
                                        else
                                        {

                                           await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = (int)driver.agentid, Mobile = driver.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now,UpdatedDatetime=DateTime.Now,UpdatedBy=userid }).ConfigureAwait(false);
                                            innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
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
                                      await  this._DBERP.AgentUser.AddAsync(new AgentUser()
                                        {
                                            Code = "pi",
                                            Name = pi.name,
                                            Status = "update",
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        }).ConfigureAwait(false);

                                        var resultnewuser =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        int New_uid = 0;
                                        if (resultnewuser)
                                        {
                                            New_uid =await this._DBERP.AgentUser.MaxAsync(p => p.Id).ConfigureAwait(false);


                                           await this._DBERP.MirrorAgent.AddAsync(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                            var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == item.mob).ConfigureAwait(false);
                                            if (entitycontactnew != null)
                                            {
                                                if (entitycontactnew.AgentId != pi.agentid)
                                                {
                                                    result = false;
                                                    //dbusertrans.Rollback();
                                                    //return result;

                                                }
                                                else
                                                {
                                                   await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = pi.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                                    innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                                }

                                            }
                                            else
                                            {

                                                await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = pi.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                                innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }





                                        }
                                        else
                                        {

                                            result = false;

                                            return result;

                                        }
                                    }
                                    else
                                    {
                                        var entityagentuser =await _DBERP.AgentUser.FirstOrDefaultAsync(i => i.Id == pi.agentid).ConfigureAwait(false);
                                        // this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = pi.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        if (entityagentuser != null)
                                        {

                                            entityagentuser.IsActive = true;
                                            entityagentuser.UpdatedDatetime = DateTime.Now;
                                            entityagentuser.UpdatedBy = userid;
                                            entityagentuser.Name = pi.name;
                                            entityagentuser.Code = "pi";


                                            result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }

                                        var entityagentmirror =await _DBERP.MirrorAgent.FirstOrDefaultAsync(i => i.AgentId == pi.agentid && i.MirrorId == mid).ConfigureAwait(false);
                                        // this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = pi.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        if (entityagentmirror != null)
                                        {

                                            entityagentmirror.IsActive = true;
                                            entityagentmirror.UpdatedDatetime = DateTime.Now;
                                            entityagentmirror.UpdatedBy = userid;
                                          


                                            result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }
                                        var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == pi.mob).ConfigureAwait(false);
                                        if (entitycontactnew != null)
                                        {
                                            if (entitycontactnew.AgentId != pi.agentid)
                                            {
                                                result = false;
                                                //dbusertrans.Rollback();
                                                //return result;

                                            }
                                            else
                                            {
                                                entitycontactnew.IsActive = true;
                                                entitycontactnew.UpdatedDatetime = DateTime.Now;
                                                entitycontactnew.UpdatedBy = userid;
                                                entitycontactnew.Mobile = pi.mob;

                                                result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                              
                                            }

                                        }
                                        else
                                        {
                                            //entitycontactnew.IsActive = true;
                                            //entitycontactnew.UpdatedDatetime = DateTime.Now;
                                            //entitycontactnew.UpdatedBy = userid;
                                            //entitycontactnew.Mobile = pi.mob;

                                         //    this._DBERP.AgentContact.Add(new AgentContact() { AgentId = ex.agentid, Mobile = ex.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                          //  innerresultcontact = this._DBERP.SaveChanges() > 0;
                                             this._DBERP.AgentContact.Add(new AgentContact() { AgentId = pi.agentid, Mobile = pi.mob, IsActive = true, CreatedBy = userid, UpdatedDatetime = DateTime.Now, UpdatedBy = userid });
                                            innerresultcontact = this._DBERP.SaveChanges() > 0;
                                        }


                                    }
                                    innerresultpagent =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;


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
                                           await this._DBERP.AgentUser.AddAsync(new AgentUser()
                                            {
                                                Code = "ex",
                                                Name = ex.name,
                                                Status = "update",
                                                IsActive = true,
                                                CreatedDatetime = DateTime.Now,
                                                CreatedBy = userid
                                            }).ConfigureAwait(false);

                                            var resultnewuser =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            int New_uid = 0;
                                            if (resultnewuser)
                                            {
                                                New_uid =await this._DBERP.AgentUser.MaxAsync(p => p.Id).ConfigureAwait(false);


                                               await this._DBERP.MirrorAgent.AddAsync(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);

                                                var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == ex.mob).ConfigureAwait(false);
                                                if (entitycontactnew != null)
                                                {
                                                    if (entitycontactnew.AgentId != ex.agentid)
                                                    {
                                                        result = false;
                                                        //dbusertrans.Rollback();
                                                        //return result;

                                                    }
                                                    else
                                                    {
                                                      await  this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = ex.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                                        innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                                    }

                                                }
                                                else
                                                {

                                                 await   this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = ex.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                                    innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                                }



                                            }
                                            else
                                            {

                                                result = false;

                                                return result;

                                            }
                                        }
                                        else
                                        {


                                            var entityagentuser =await _DBERP.AgentUser.FirstOrDefaultAsync(i => i.Id == ex.agentid).ConfigureAwait(false);
                                            // this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = pi.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                            if (entityagentuser != null)
                                            {

                                                entityagentuser.IsActive = true;
                                                entityagentuser.UpdatedDatetime = DateTime.Now;
                                                entityagentuser.UpdatedBy = userid;
                                                entityagentuser.Name = ex.name;
                                                entityagentuser.Code = "ex";


                                                result = this._DBERP.SaveChanges() > 0;
                                            }

                                            var entityagentmirror =await _DBERP.MirrorAgent.FirstOrDefaultAsync(i => i.AgentId == ex.agentid && i.MirrorId == mid).ConfigureAwait(false);
                                            // this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = pi.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                            if (entityagentmirror != null)
                                            {

                                                entityagentmirror.IsActive = true;
                                                entityagentmirror.UpdatedDatetime = DateTime.Now;
                                                entityagentmirror.UpdatedBy = userid;



                                                result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }




                                            var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == ex.mob).ConfigureAwait(false);
                                            if (entitycontactnew != null)
                                            {
                                                if (entitycontactnew.AgentId != ex.agentid)
                                                {
                                                    result = false;
                                                    //dbusertrans.Rollback();
                                                    //return result;

                                                }
                                                else
                                                {
                                                    entitycontactnew.IsActive = true;
                                                    entitycontactnew.UpdatedDatetime = DateTime.Now;
                                                    entitycontactnew.UpdatedBy = userid;
                                                    entitycontactnew.Mobile = ex.mob;

                                                   // this._DBERP.AgentContact.Add(new AgentContact() { AgentId = ex.agentid, Mobile = ex.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                                    innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                                }

                                            }
                                            else
                                            {
                                                //entitycontactnew.IsActive = true;
                                                //entitycontactnew.UpdatedDatetime = DateTime.Now;
                                                //entitycontactnew.UpdatedBy = userid;
                                                //entitycontactnew.Mobile = ex.mob;

                                              await   this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = ex.agentid, Mobile = ex.mob, IsActive = true, CreatedBy = userid, UpdatedDatetime = DateTime.Now, UpdatedBy = userid }).ConfigureAwait(false);
                                                innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;

                                                //  this._DBERP.AgentContact.Add(new AgentContact() { AgentId = ex.agentid, Mobile = ex.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                              //  innerresultcontact = this._DBERP.SaveChanges() > 0;
                                            }

                                        }
                                        innerresultpagent =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;


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
                                       await this._DBERP.AgentUser.AddAsync(new AgentUser()
                                        {
                                            Code = "ec",
                                            Name = ec.name,
                                            Status = "update",
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        });

                                        var resultnewuser =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        int New_uid = 0;
                                        if (resultnewuser)
                                        {
                                            New_uid =await this._DBERP.AgentUser.MaxAsync(p => p.Id).ConfigureAwait(false);


                                          await  this._DBERP.MirrorAgent.AddAsync(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);


                                            var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == ec.mob).ConfigureAwait(false);
                                            if (entitycontactnew != null)
                                            {
                                                if (entitycontactnew.AgentId != ec.agentid)
                                                {
                                                    result = false;
                                                    //dbusertrans.Rollback();
                                                    //return result;

                                                }
                                                else
                                                {

                                                   await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = ec.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                                    innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                                }

                                            }
                                            else
                                            {

                                              await  this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = ec.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                                innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }




                                        }
                                        else
                                        {

                                            result = false;

                                            return result;

                                        }
                                    }
                                    else
                                    {
                                        var entityagentuser =await _DBERP.AgentUser.FirstOrDefaultAsync(i => i.Id == ec.agentid).ConfigureAwait(false);
                                        // this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = pi.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        if (entityagentuser != null)
                                        {

                                            entityagentuser.IsActive = true;
                                            entityagentuser.UpdatedDatetime = DateTime.Now;
                                            entityagentuser.UpdatedBy = userid;
                                            entityagentuser.Name = ec.name;
                                            entityagentuser.Code = "ec";


                                            result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }

                                        var entityagentmirror =await _DBERP.MirrorAgent.FirstOrDefaultAsync(i => i.AgentId == ec.agentid && i.MirrorId == mid).ConfigureAwait(false);
                                        // this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = pi.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        if (entityagentmirror != null)
                                        {

                                            entityagentmirror.IsActive = true;
                                            entityagentmirror.UpdatedDatetime = DateTime.Now;
                                            entityagentmirror.UpdatedBy = userid;



                                            result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }



                                        var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == ec.mob).ConfigureAwait(false);
                                        if (entitycontactnew != null)
                                        {
                                            if (entitycontactnew.AgentId != ec.agentid)
                                            {
                                                result = false;
                                                //dbusertrans.Rollback();
                                                //return result;

                                            }
                                            else
                                            {

                                                //  this._DBERP.AgentContact.Add(new AgentContact() { AgentId = ec.agentid, Mobile = ec.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                                entitycontactnew.IsActive = true;
                                                entitycontactnew.UpdatedDatetime = DateTime.Now;
                                                entitycontactnew.UpdatedBy = userid;
                                                entitycontactnew.Mobile = ec.mob;
                                                innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }

                                        }
                                        else
                                        {
                                            //entitycontactnew.IsActive = true;
                                            //entitycontactnew.UpdatedDatetime = DateTime.Now;
                                            //entitycontactnew.UpdatedBy = userid;
                                            //entitycontactnew.Mobile = ec.mob;
                                           await  this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = ec.agentid, Mobile = ec.mob, IsActive = true, CreatedBy = userid, UpdatedDatetime = DateTime.Now, UpdatedBy = userid }).ConfigureAwait(false);
                                            innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }

                                    }
                                    innerresultpagent =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;


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
                                       await this._DBERP.AgentUser.AddAsync(new AgentUser()
                                        {
                                            Code = "te",
                                            Name = te.name,
                                            Status = "update",
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        }).ConfigureAwait(false);

                                        var resultnewuser =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        int New_uid = 0;
                                        if (resultnewuser)
                                        {
                                            New_uid =await this._DBERP.AgentUser.MaxAsync(p => p.Id).ConfigureAwait(false);


                                           await this._DBERP.MirrorAgent.AddAsync(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);

                                            var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == te.mob).ConfigureAwait(false);
                                            if (entitycontactnew != null)
                                            {
                                                if (entitycontactnew.AgentId != te.agentid)
                                                {
                                                    result = false;
                                                    //dbusertrans.Rollback();
                                                    //return result;

                                                }
                                                else
                                                {

                                                  await  this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = te.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                                    innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                                }

                                            }
                                            else
                                            {

                                              await  this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = te.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                                innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }



                                        }
                                        else
                                        {

                                            result = false;

                                            return result;

                                        }
                                    }
                                    else
                                    {

                                        var entityagentuser =await _DBERP.AgentUser.FirstOrDefaultAsync(i => i.Id == te.agentid).ConfigureAwait(false);
                                        // this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = pi.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        if (entityagentuser != null)
                                        {

                                            entityagentuser.IsActive = true;
                                            entityagentuser.UpdatedDatetime = DateTime.Now;
                                            entityagentuser.UpdatedBy = userid;
                                            entityagentuser.Name = te.name;
                                            entityagentuser.Code = "te";


                                            result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }

                                        var entityagentmirror =await _DBERP.MirrorAgent.FirstOrDefaultAsync(i => i.AgentId == te.agentid && i.MirrorId == mid).ConfigureAwait(false);
                                        // this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = pi.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        if (entityagentmirror != null)
                                        {

                                            entityagentmirror.IsActive = true;
                                            entityagentmirror.UpdatedDatetime = DateTime.Now;
                                            entityagentmirror.UpdatedBy = userid;



                                            result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }




                                        var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == te.mob).ConfigureAwait(false);
                                        if (entitycontactnew != null)
                                        {
                                            if (entitycontactnew.AgentId != te.agentid)
                                            {
                                                result = false;
                                                //dbusertrans.Rollback();
                                                //return result;

                                            }
                                            else
                                            {

                                                entitycontactnew.IsActive = true;
                                                entitycontactnew.UpdatedDatetime = DateTime.Now;
                                                entitycontactnew.UpdatedBy = userid;
                                                entitycontactnew.Mobile = te.mob;

                                                // this._DBERP.AgentContact.Add(new AgentContact() { AgentId = te.agentid, Mobile = te.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                                innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }

                                        }
                                        else
                                        {
                                            //entitycontactnew.IsActive = true;
                                            //entitycontactnew.UpdatedDatetime = DateTime.Now;
                                            //entitycontactnew.UpdatedBy = userid;
                                            //entitycontactnew.Mobile = te.mob;

                                          await    this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = te.agentid, Mobile = te.mob, IsActive = true, CreatedBy = userid, UpdatedDatetime = DateTime.Now,UpdatedBy=userid }).ConfigureAwait(false);
                                            innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }

                                    }
                                    innerresultpagent =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;


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
                                       await this._DBERP.AgentUser.AddAsync(new AgentUser()
                                        {
                                            Code = "gu",
                                            Name = gu.name,
                                            Status = "update",
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        }).ConfigureAwait(false);

                                        var resultnewuser =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        int New_uid = 0;
                                        if (resultnewuser)
                                        {
                                            New_uid =await this._DBERP.AgentUser.MaxAsync(p => p.Id).ConfigureAwait(false);


                                           await this._DBERP.MirrorAgent.AddAsync(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);

                                            var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == gu.mob).ConfigureAwait(false);
                                            if (entitycontactnew != null)
                                            {
                                                if (entitycontactnew.AgentId != gu.agentid)
                                                {
                                                    result = false;
                                                    //dbusertrans.Rollback();
                                                    //return result;

                                                }
                                                else
                                                {

                                                  await  this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = gu.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                                    innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                                }

                                            }
                                            else
                                            {

                                               await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = gu.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                                innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }

                                        }
                                        else
                                        {

                                            result = false;

                                            return result;

                                        }
                                    }
                                    else
                                    {


                                        var entityagentuser =await _DBERP.AgentUser.FirstOrDefaultAsync(i => i.Id == gu.agentid).ConfigureAwait(false);
                                        // this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = pi.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        if (entityagentuser != null)
                                        {

                                            entityagentuser.IsActive = true;
                                            entityagentuser.UpdatedDatetime = DateTime.Now;
                                            entityagentuser.UpdatedBy = userid;
                                            entityagentuser.Name = gu.name;
                                            entityagentuser.Code = "gu";


                                            result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }

                                        var entityagentmirror =await _DBERP.MirrorAgent.FirstOrDefaultAsync(i => i.AgentId == gu.agentid && i.MirrorId == mid).ConfigureAwait(false);
                                        // this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = pi.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        if (entityagentmirror != null)
                                        {

                                            entityagentmirror.IsActive = true;
                                            entityagentmirror.UpdatedDatetime = DateTime.Now;
                                            entityagentmirror.UpdatedBy = userid;



                                            result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }

                                        //   this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = gu.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }); 
                                        var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == gu.mob).ConfigureAwait(false);
                                        if (entitycontactnew != null)
                                        {
                                            if (entitycontactnew.AgentId != gu.agentid)
                                            {
                                                result = false;
                                                //dbusertrans.Rollback();
                                                //return result;

                                            }
                                            else
                                            {
                                                entitycontactnew.IsActive = true;
                                                entitycontactnew.UpdatedDatetime = DateTime.Now;
                                                entitycontactnew.UpdatedBy = userid;
                                                entitycontactnew.Mobile = gu.mob;


                                                // this._DBERP.AgentContact.Add(new AgentContact() { AgentId = gu.agentid, Mobile = gu.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                                innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }

                                        }
                                        else
                                        {
                                            //entitycontactnew.IsActive = true;
                                            //entitycontactnew.UpdatedDatetime = DateTime.Now;
                                            //entitycontactnew.UpdatedBy = userid;
                                            //entitycontactnew.Mobile = gu.mob;


                                            await this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = gu.agentid, Mobile = gu.mob, IsActive = true, CreatedBy = userid, UpdatedDatetime = DateTime.Now, UpdatedBy = userid }).ConfigureAwait(false);
                                            innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }

                                    }
                                    innerresultpagent =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;


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
                                      await  this._DBERP.AgentUser.AddAsync(new AgentUser()
                                        {
                                            Code = "de",
                                            Name = de.name,
                                            Status = "update",
                                            IsActive = true,
                                            CreatedDatetime = DateTime.Now,
                                            CreatedBy = userid
                                        }).ConfigureAwait(false);

                                        var resultnewuser =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        int New_uid = 0;
                                        if (resultnewuser)
                                        {
                                            New_uid =await this._DBERP.AgentUser.MaxAsync(p => p.Id).ConfigureAwait(false);


                                           await this._DBERP.MirrorAgent.AddAsync(new MirrorAgent() { AgentId = New_uid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);


                                            var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == de.mob).ConfigureAwait(false);
                                            if (entitycontactnew != null)
                                            {
                                                if (entitycontactnew.AgentId != de.agentid)
                                                {
                                                    result = false;
                                                    //dbusertrans.Rollback();
                                                    //return result;

                                                }
                                                else
                                                {

                                                await    this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = de.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                                    innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                                }

                                            }
                                            else
                                            {

                                              await  this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = New_uid, Mobile = de.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                                innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }



                                        }
                                        else
                                        {

                                            result = false;

                                            return result;

                                        }
                                    }
                                    else
                                    {

                                        var entityagentuser =await _DBERP.AgentUser.FirstOrDefaultAsync(i => i.Id == de.agentid).ConfigureAwait(false);
                                        // this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = pi.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        if (entityagentuser != null)
                                        {

                                            entityagentuser.IsActive = true;
                                            entityagentuser.UpdatedDatetime = DateTime.Now;
                                            entityagentuser.UpdatedBy = userid;
                                            entityagentuser.Name = de.name;
                                            entityagentuser.Code = "de";


                                            result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }

                                        var entityagentmirror =await _DBERP.MirrorAgent.FirstOrDefaultAsync(i => i.AgentId == de.agentid && i.MirrorId == mid).ConfigureAwait(false);
                                        // this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = pi.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        if (entityagentmirror != null)
                                        {

                                            entityagentmirror.IsActive = true;
                                            entityagentmirror.UpdatedDatetime = DateTime.Now;
                                            entityagentmirror.UpdatedBy = userid;



                                            result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }


                                        // this._DBERP.MirrorAgent.Add(new MirrorAgent() { AgentId = de.agentid, MirrorId = mid, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                        var entitycontactnew =await _DBERP.AgentContact.FirstOrDefaultAsync(i => i.Mobile == de.mob).ConfigureAwait(false);
                                        if (entitycontactnew != null)
                                        {
                                            if (entitycontactnew.AgentId != de.agentid)
                                            {
                                                result = false;
                                                //dbusertrans.Rollback();
                                                //return result;

                                            }
                                            else
                                            {

                                                entitycontactnew.IsActive = true;
                                                entitycontactnew.UpdatedDatetime = DateTime.Now;
                                                entitycontactnew.UpdatedBy = userid;
                                                entitycontactnew.Mobile = de.mob;

                                                //  this._DBERP.AgentContact.Add(new AgentContact() { AgentId = de.agentid, Mobile = de.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now });
                                                innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }

                                        }
                                        else
                                        {
                                            //entitycontactnew.IsActive = true;
                                            //entitycontactnew.UpdatedDatetime = DateTime.Now;
                                            //entitycontactnew.UpdatedBy = userid;
                                            //entitycontactnew.Mobile = de.mob;
                                          await   this._DBERP.AgentContact.AddAsync(new AgentContact() { AgentId = de.agentid, Mobile = de.mob, IsActive = true, CreatedBy = userid, CreatedDatetime = DateTime.Now }).ConfigureAwait(false);
                                            innerresultcontact =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                        }


                                    }
                                    innerresultpagent =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;


                                }


                            }
                        }
                    }

                    result = true;

                }
                else { result = false; }

                if (result)
                {

                   await dbusertrans.CommitAsync().ConfigureAwait(false);
                }
                else {await dbusertrans.RollbackAsync().ConfigureAwait(false); }

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
        teamlead = g.Where(c => c.agentcode == "te").ToList(),
        demo = g.Where(c => c.agentcode == "de").ToList()
        // p= g.Where(c => c.agentcode == "pi").SelectMany(a=>a.name).SingleOrDefault().ToString()
    }).ToList();


            _mirror.vehicledetails = BindingListUtillity.GenerateSelectList(_vehiclemaster);
            _mirror.languagedetails = BindingListUtillity.GenerateSelectList(_langmaster);
            _mirror.countrydetails = BindingListUtillity.GenerateSelectList(_countrymaster);
            _mirror.seriesdetails = BindingListUtillity.GenerateSelectList(_seriesmaster);


            return _mirror;

        }

        public async Task<bool> UpdateMirror(MirrorDetailsVM _mirror, int uid)
        {
            bool result = false, innerresult = false, innerresultvehicle = false, innerresultcontact = false;
            try
            {

                using (var dbusertrans =await this._DBERP.Database.BeginTransactionAsync().ConfigureAwait(false))
                {


                    var entitycontact =await _DBERP.MirrorDetails.Where(a => a.Id == _mirror.MirrorId).FirstOrDefaultAsync().ConfigureAwait(false);
                    if (entitycontact != null)
                    {
                        var mirrorUsers =await _DBERP.MirrorAgent.Where(item => item.MirrorId == _mirror.MirrorId).ToListAsync().ConfigureAwait(false);

                        if (mirrorUsers != null)
                        {
                            foreach (var r in mirrorUsers)
                            {
                                r.IsActive = false;
                                r.UpdatedDatetime = DateTime.Now;
                            }
                            result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
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
                                            var agentcontact = await _DBERP.AgentContact.Where(item => item.AgentId == d.agentid && item.IsActive==true).ToListAsync().ConfigureAwait(false);
                                            var agentvehicle =await _DBERP.VehicleDetails.Where(item => item.AgentId == d.agentid && item.IsActive == true).ToListAsync().ConfigureAwait(false);
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
                                                result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }
                                            if (agentvehicle != null)
                                            {
                                                foreach (var r in agentvehicle)
                                                {
                                                    r.IsActive = false;
                                                    r.UpdatedDatetime = DateTime.Now;
                                                }
                                                result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }


                                        }
                                    }
                                }
                                if (_mir.principle != null)
                                {
                                    foreach (var d in _mir.principle)
                                    {
                                        if (d != null)
                                        {
                                            var agentUsers =await _DBERP.AgentUser.Where(item => item.Id == d.agentid).ToListAsync().ConfigureAwait(false);
                                            if (agentUsers != null)
                                            {
                                                foreach (var r in agentUsers)
                                                {
                                                    r.IsActive = false;
                                                    r.UpdatedDatetime = DateTime.Now;
                                                }
                                                result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }
                                            var agentcontact =await _DBERP.AgentContact.Where(item => item.AgentId == d.agentid && item.IsActive == true).ToListAsync().ConfigureAwait(false);
                                            if (agentcontact != null)
                                            {
                                                foreach (var r in agentcontact)
                                                {
                                                    r.IsActive = false;
                                                    r.UpdatedDatetime = DateTime.Now;
                                                }
                                                result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }

                                        }
                                    }
                                }
                                if (_mir.excursion != null)
                                {
                                    foreach (var d in _mir.excursion)
                                    {
                                        if (d != null)
                                        {
                                            var agentUsers =await _DBERP.AgentUser.Where(item => item.Id == d.agentid).ToListAsync().ConfigureAwait(false);
                                            if (agentUsers != null)
                                            {
                                                foreach (var r in agentUsers)
                                                {
                                                    r.IsActive = false;
                                                    r.UpdatedDatetime = DateTime.Now;
                                                }
                                                result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }
                                            var agentcontact =await _DBERP.AgentContact.Where(item => item.AgentId == d.agentid && item.IsActive == true).ToListAsync().ConfigureAwait(false);
                                            if (agentcontact != null)
                                            {
                                                foreach (var r in agentcontact)
                                                {
                                                    r.IsActive = false;
                                                    r.UpdatedDatetime = DateTime.Now;
                                                }
                                                result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }

                                        }
                                    }
                                }
                                if (_mir.teamescort != null)
                                {
                                    foreach (var d in _mir.teamescort)
                                    {
                                        if (d != null)
                                        {
                                            var agentUsers =await _DBERP.AgentUser.Where(item => item.Id == d.agentid).ToListAsync().ConfigureAwait(false);
                                            if (agentUsers != null)
                                            {
                                                foreach (var r in agentUsers)
                                                {
                                                    r.IsActive = false;
                                                    r.UpdatedDatetime = DateTime.Now;
                                                }
                                                result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }

                                            var agentcontact =await _DBERP.AgentContact.Where(item => item.AgentId == d.agentid && item.IsActive == true).ToListAsync().ConfigureAwait(false);
                                            if (agentcontact != null)
                                            {
                                                foreach (var r in agentcontact)
                                                {
                                                    r.IsActive = false;
                                                    r.UpdatedDatetime = DateTime.Now;
                                                }
                                                result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }

                                        }
                                    }
                                }
                                if (_mir.guide != null)
                                {
                                    foreach (var d in _mir.guide)
                                    {
                                        if (d != null)
                                        {
                                            var agentUsers =await _DBERP.AgentUser.Where(item => item.Id == d.agentid).ToListAsync().ConfigureAwait(false);
                                            if (agentUsers != null)
                                            {
                                                foreach (var r in agentUsers)
                                                {
                                                    r.IsActive = false;
                                                    r.UpdatedDatetime = DateTime.Now;
                                                }
                                                result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }
                                            var agentcontact =await _DBERP.AgentContact.Where(item => item.AgentId == d.agentid && item.IsActive == true).ToListAsync().ConfigureAwait(false);
                                            if (agentcontact != null)
                                            {
                                                foreach (var r in agentcontact)
                                                {
                                                    r.IsActive = false;
                                                    r.UpdatedDatetime = DateTime.Now;
                                                }
                                                result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }

                                        }
                                    }
                                }
                                if (_mir.teamlead != null)
                                {
                                    foreach (var d in _mir.teamlead)
                                    {
                                        if (d != null)
                                        {
                                            var agentUsers =await _DBERP.AgentUser.Where(item => item.Id == d.agentid).ToListAsync().ConfigureAwait(false);
                                            if (agentUsers != null)
                                            {
                                                foreach (var r in agentUsers)
                                                {
                                                    r.IsActive = false;
                                                    r.UpdatedDatetime = DateTime.Now;
                                                }
                                                result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }
                                            var agentcontact =await _DBERP.AgentContact.Where(item => item.AgentId == d.agentid && item.IsActive == true).ToListAsync().ConfigureAwait(false);
                                            if (agentcontact != null)
                                            {
                                                foreach (var r in agentcontact)
                                                {
                                                    r.IsActive = false;
                                                    r.UpdatedDatetime = DateTime.Now;
                                                }
                                                result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }

                                        }
                                    }
                                }
                                if (_mir.demo != null)
                                {
                                    foreach (var d in _mir.demo)
                                    {
                                        if (d != null)
                                        {
                                            var agentUsers =await _DBERP.AgentUser.Where(item => item.Id == d.agentid).ToListAsync().ConfigureAwait(false);
                                            if (agentUsers != null)
                                            {
                                                foreach (var r in agentUsers)
                                                {
                                                    r.IsActive = false;
                                                    r.UpdatedDatetime = DateTime.Now;
                                                }
                                                result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }
                                            var agentcontact =await _DBERP.AgentContact.Where(item => item.AgentId == d.agentid && item.IsActive == true).ToListAsync().ConfigureAwait(false);
                                            if (agentcontact != null)
                                            {
                                                foreach (var r in agentcontact)
                                                {
                                                    r.IsActive = false;
                                                    r.UpdatedDatetime = DateTime.Now;
                                                }
                                                result =await this._DBERP.SaveChangesAsync().ConfigureAwait(false) > 0;
                                            }

                                        }
                                    }
                                }

                            }
                        }


                    }
                    await dbusertrans.CommitAsync();
                    bool resultinner;
                    resultinner = await afterupdateMirror(_mirror, uid);
                    //if (result)
                    //{
                      

                       

                    //}
                   // else { dbusertrans.Rollback(); }
                    return true;
                }


            }
            catch (Exception ex)
            {

                throw ex;
            }
        }
        public bool DeleteMirror(int mirrorid,int uid)
        {
            bool result = false;
            int innerresult = 0, innerresultrole = 0;
            using (var dbusertrans = this._DBERP.Database.BeginTransaction())
            {

                var entity = _DBERP.MirrorDetails.FirstOrDefault(item => item.Id == mirrorid);

                if (entity != null)
                {


                    entity.UpdatedDatetime = DateTime.Now;
                    entity.UpdatedBy = uid;
                    entity.IsActive = false;
                    this._DBERP.MirrorDetails.Update(entity);
                    innerresult = this._DBERP.SaveChanges();
                }
                if (innerresult > 0)
                {
                  
                        dbusertrans.Commit();
                        result = true;
                   

                }
                else { dbusertrans.Rollback(); }


            }
            return result;
        }

       
           
    }
}
