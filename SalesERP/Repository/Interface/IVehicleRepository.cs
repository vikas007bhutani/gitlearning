using System;
using System.Collections.Generic;
using SALEERP.ViewModel;

namespace SALEERP.Repository.Interface
{
   public interface IVehicleRepository
    {
        List<VehicleVM> getAllVehicle();
        bool AddVehicle(VehicleVM _vehicle, int userid);
        bool UpdateVehicle(VehicleVM _vechicle, int uid);
        VehicleVM EditVehicle(int uid);
        bool DeleteVehicle(int pid);
    }
}
