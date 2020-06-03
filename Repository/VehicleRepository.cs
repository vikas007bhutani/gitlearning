using System;
using System.Collections.Generic;
using System.Linq;
using SALEERP.ViewModel;
using SALEERP.Repository.Interface;
using SALEERP.Data;
using SALEERP.Models;

namespace SALEERP.Repository
{
    public class VehicleRepository : IVehicleRepository
    {
        private Sales_ERPContext _DBERP;

        public VehicleRepository(Sales_ERPContext dbcontext)
        {

            this._DBERP = dbcontext;

        }
        public bool AddVehicle(VehicleVM _vehicle, int userid)
        {
            bool result = false;
            try
            {

                this._DBERP.VehicleMaster.Add(new VehicleMaster()
                {
                    VehicleType = _vehicle.VehicleType,
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

        public bool DeleteVehicle(int vid)
        {
            bool result = false;
            try
            {



                int innerresult = 0;
                var entity = _DBERP.VehicleMaster.FirstOrDefault(item => item.VehicleId == vid);

                if (entity != null)
                {



                    entity.UpdatedDatetime = DateTime.Now;
                    entity.IsActive = false;
                    // entity.UpdatedBy=
                    this._DBERP.VehicleMaster.Update(entity);
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

        public VehicleVM EditVehicle(int uid)
        {
            VehicleVM _vehicledetails = new VehicleVM();
            try
            {


                VehicleMaster vehicle = this._DBERP.VehicleMaster.Find(uid);

                // List<LoginRoles> userroles = this._DBERP.LoginRoles.ToList();
                //  _userdetails.loginroles = BindingListUtillity.GenerateSelectList(userroles);
                _vehicledetails.VehicleType = vehicle.VehicleType;


                _vehicledetails.VehicleId = vehicle.VehicleId;
            }
            catch (Exception)
            {

                throw;
            }
            return _vehicledetails;
        }

        public List<VehicleVM> getAllVehicle()
        {
            IEnumerable<VehicleVM> _allvehicledetails;
            try
            {
                _allvehicledetails = this._DBERP.VehicleMaster.ToList().Where(ar => ar.IsActive == true).Select(v => new VehicleVM { VehicleType = v.VehicleType, VehicleId = v.VehicleId, IsActive = v.IsActive });
            }
            catch (Exception)
            {

                throw;
            }



            return _allvehicledetails.ToList();
        }

        public bool UpdateVehicle(VehicleVM _vechicle, int uid)
        {
            bool result = false;
            try
            {



                int innerresult = 0;
                var entity = _DBERP.VehicleMaster.FirstOrDefault(item => item.VehicleId == _vechicle.VehicleId);

                if (entity != null)
                {

                    entity.VehicleType = _vechicle.VehicleType;
                  
                    entity.UpdatedDatetime = DateTime.Now;
                    this._DBERP.VehicleMaster.Update(entity);
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
