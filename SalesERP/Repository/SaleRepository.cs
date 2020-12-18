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
    }
}
