using System;
using System.Collections.Generic;
using System.Linq;
using SalesApp.ViewModel;
using SalesApp.Repository.Interface;
using SALEERP.Data;
using SalesApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace SalesApp.Repository
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
            /*List<AgentMaster> _agentmaster = this._DBERP.AgentMaster.Where(i => i.IsActive == true).ToList();
            List<LanguagesMaster> _langmaster = this._DBERP.LanguagesMaster.Where(i => i.IsActive == true).ToList();
            List<CountriesMaster> _countrymaster = this._DBERP.CountriesMaster.Where(i => i.IsActive == true).ToList();
            List<VehicleMaster> _vehiclemaster = this._DBERP.VehicleMaster.Where(i => i.IsActive == true).ToList();
            List<SeriesMaster> _seriesmaster = this._DBERP.SeriesMaster.Where(i => i.IsActive == true).ToList(); */
            _mirror.MirrorDate = Convert.ToDateTime(DateTime.Now.ToString("dd/MM/yyyy hh: mm"));
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

                               where ma.IsActive == true
                               select new MirrorDetailVM
                               {
                                   mirrorid = m.Id,
                                   mirrordate = m.Date,
                                   name = au.Name,
                                   mob = d.Mobile,
                                   agentcode = au.Code,
                                   Pax = m.Pax
                                   //Languageid = m.LanguageId

                               }).ToList().GroupBy(c => c.mirrorid)
    .Select(g => new MirrorDetailVM
    {
        mirrorid = g.Key,
        mirrordate = g.FirstOrDefault().mirrordate,
        principle = g.Where(c => c.agentcode == "pi").ToList(),
        driver = g.Where(c => c.agentcode == "dr").ToList(),
        excursion = g.Where(c => c.agentcode == "gu").ToList(),
        Pax = g.FirstOrDefault().Pax

        // p= g.Where(c => c.agentcode == "pi").SelectMany(a=>a.name).SingleOrDefault().ToString()
    }).ToList();

            return _mirror;

        }
    }
}
