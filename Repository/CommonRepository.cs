using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEERP.Repository.Interface;
using SALEERP.ViewModel;
using SALEERP.Data;
using SALEERP.Models;
using SALEERP.Utility;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace SALEERP.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private Sales_ERPContext _DBERP;
        public CommonRepository(Sales_ERPContext dbcontext)
        {

            this._DBERP = dbcontext;

        }
     
        public UserLoginVM getroles()
        {
            UserLoginVM _loginroles = new UserLoginVM();
            List<LoginRoles> userroles = this._DBERP.LoginRoles.ToList();
            _loginroles.loginroles = BindingListUtillity.GenerateSelectList(userroles);
            return _loginroles;
        }
        public List<SelectListItem> getvehicles()
        {
            List<SelectListItem> _ag_user = new List<SelectListItem>();
            List<VehicleMaster> _vehiclemaster = this._DBERP.VehicleMaster.Where(i => i.IsActive == true).ToList();
            _ag_user = BindingListUtillity.GenerateSelectList(_vehiclemaster);
            return _ag_user;
        }
        public List<SelectListItem> getBanks()
        {
            List<SelectListItem> _ag_bank = new List<SelectListItem>();
            List<BankMaster> _bankmaster = this._DBERP.BankMaster.Where(i => i.IsActive == true).ToList();
            _ag_bank = BindingListUtillity.GenerateSelectList(_bankmaster);
            return _ag_bank;
        }
        public List<SelectListItem> getunits()
        {
            List<SelectListItem> _ag_unit = new List<SelectListItem>();
            List<UnitMaster> _unitmaster = new List<UnitMaster>();
            _unitmaster.Add(new UnitMaster { unitid = 1, unitname = "UNIT-1" });
            _unitmaster.Add(new UnitMaster { unitid = 2, unitname = "UNIT-2" });

            _ag_unit = BindingListUtillity.GenerateSelectList(_unitmaster);
            return _ag_unit;
        }
       

    }
}
