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
    public class SaleRepository : ISaleRepository
    {
        private Sales_ERPContext _DBERP;
        ICommonRepository _comm;
        public SaleRepository(Sales_ERPContext dbcontext, ICommonRepository comm)
        {

            this._DBERP = dbcontext;
            this._comm = comm;

        }
       
        public SaleVM Init_commission()
        {
            SaleVM _cms = new SaleVM();
            List<UnitMaster> _unitmaster = new List<UnitMaster>();
            _cms.unitdetails = _comm.getunits();
            _cms.fromdate = DateTime.Now;
            _cms.Todate = DateTime.Now;
            return _cms;
        }
        public CommissionVM getAllMirorforCommission(CommissionVM cm)
        {
            CommissionVM _cms = new CommissionVM();


            // List<UnitMaster> _unitmaster = new List<UnitMaster>();
            _cms.unitdetails = _comm.getunits();

            var mirrors = (from m in this._DBERP.MirrorDetails
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
                           join vm in this._DBERP.VehicleMaster
                           on vh.VehicleId equals vm.Id into vehiclemaster
                           from vm in vehiclemaster.Where(f => f.IsActive == true).DefaultIfEmpty()
                               //join om in this._DBERP.OrderMaster
                               //on m.Id equals om.MirrorId into order
                               //from om in order.Where(f => f.IsActive == true).DefaultIfEmpty()
                               //join item in this._DBERP.OrderItemDetails
                               //on om.Id equals item.OrderId into itemmaster
                               //from item in itemmaster.Where(f => f.IsActive == true).DefaultIfEmpty()


                           where ma.IsActive == true && m.unitid == cm.unitid && ((cm.fromdate == null ? m.Date > DateTime.Now && m.Date < DateTime.Now : m.Date > cm.fromdate && m.Date < cm.Todate) || (m.Date > cm.fromdate && m.Date < cm.Todate))
                           select new MirrorDetailVM
                           {
                               mirrorid = m.Id,
                               mirrordate = m.Date,
                               name = au.Name,
                               mob = d.Mobile,
                               agentcode = au.Code,
                               vehicleNo = vh.Number,
                               vehicle = vm.Type,
                               SaleValue = 0




                           }).ToList();
            _cms.Mirrors = mirrors.GroupBy(m => m.mirrorid)
            .Select(g => new MirrorDetailVM
            {
                mirrorid = g.Key,
                mirrordate = g.FirstOrDefault().mirrordate,
                Unit = g.FirstOrDefault().Unit,
                principle = g.Where(c => c.agentcode == "pi").ToList(),
                driver = g.Where(c => c.agentcode == "dr").ToList(),
                guide = g.Where(c => c.agentcode == "gu").ToList(),
                excursion = g.Where(c => c.agentcode == "ex").ToList(),
                teamescort = g.Where(c => c.agentcode == "ec").ToList(),
                teamlead = g.Where(c => c.agentcode == "te").ToList(),
                demo = g.Where(c => c.agentcode == "de").ToList(),
                SaleValue = this._DBERP.OrderMaster.Where(a => a.MirrorId == g.Key).Sum(s => s.SaleValue)
        // p= g.Where(c => c.agentcode == "pi").SelectMany(a=>a.name).SingleOrDefault().ToString()
    }).ToList();


            //if(_cms.Mirrors)
            //_cms.Mirrors =
            //_mirror.Mirrors = this._DBERP.MirrorDetails.ToList().Where(i => i.IsActive == true).Select(u => new MirrorDetailVM { mirrordate = u.MirrorDate, d_list = this._DBERP.AgentUser.Include("AgentContact").Where(d => d.IsActive == true && d.AgentCode == "pi").Include("VehicleDetails").Select(a=>new driverdetails { name=a.Name,mob=a.AgentContact.Where(b=>b.AgentId==a.AgentId).FirstOrDefault().Mobile }).ToList(), mirrorid = u.MirrorId }).ToList();


            //Vehicle = this._DBERP.AgentUser.Include("VehicleDetails").Where(d => d.IsActive == true && d.AgentTypeId == 1 && d.AgentId == u.DriverId).SingleOrDefault().VehicleDetails.Where(v => v.IsActive == true).SingleOrDefault().VehicleNo, DriverMobile = this._DBERP.AgentUser.Include("AgentContact").Where(d => d.IsActive == true && d.AgentTypeId == 1 && d.AgentId == u.DriverId).SingleOrDefault().AgentContact.Where(v => v.IsActive == true).SingleOrDefault().Mobile, VehicleTypeid = this._DBERP.AgentUser.Include("VehicleDetails").Where(d => d.IsActive == true && d.AgentTypeId == 1 && d.AgentId == u.DriverId).SingleOrDefault().VehicleDetails.Where(v => v.IsActive == true).SingleOrDefault().VehicleId
            // _mirror = this._DBERP.MirrorDetails.Where(i => i.IsActive == true).Select(u=>new MirrorDetailsVM { MirrorDate = u.MirrorDate, vehicledetails = BindingListUtillity.GenerateSelectList(_vehiclemaster) } ).SingleOrDefault();



            return _cms;


        }
    }
}
