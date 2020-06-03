using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEERP.ViewModel;
using SALEERP.Repository.Interface;
using SALEERP.Data;
using SALEERP.Models;
using SALEERP.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
namespace SALEERP.Repository
{
    public class PoolRepository : IPoolRepository
    {
        private Sales_ERPContext _DBERP;

        public PoolRepository(Sales_ERPContext dbcontext)
        {

            this._DBERP = dbcontext;

        }
        public bool AddPool(PoolVM _poll, int userid)
        {
            bool result = false;
           
                this._DBERP.PoolMaster.Add(new PoolMaster()
                {
                    PoolName = _poll.PoolName,
                    PoolDesc=_poll.PoolDesc,
                    PoolStartDate=_poll.PoolStartDate,
                    PoolEndDate=_poll.PoolEndDate,
                    Createddatetime = DateTime.Now,
                    Createdby = userid,
                    IsActive = true
                }); ;

                result = this._DBERP.SaveChanges() > 0;
              

            
            return result;
        }
        public bool UpdatePool(PoolVM _poll, int uid)
        {
            bool result = false;
            int innerresult =0;
            var entity = _DBERP.PoolMaster.FirstOrDefault(item => item.PoolId == _poll.PoolId);
          
            if (entity != null)
            {
                
                entity.PoolName = _poll.PoolName;
                entity.PoolDesc = _poll.PoolDesc;
                entity.PoolStartDate = _poll.PoolStartDate;
                entity.PoolEndDate = _poll.PoolEndDate;
                entity.Updateddatetime = DateTime.Now;
                this._DBERP.PoolMaster.Update(entity);
                innerresult = this._DBERP.SaveChanges();
            }
            
            if(innerresult > 0)
            {
                result = true;
            }
                
          
            return result;

        }
        public PoolVM EditPool(Int64 uid)
        {
            PoolMaster pool = this._DBERP.PoolMaster.Find(uid);
            PoolVM _pooldetails = new PoolVM();
            // List<LoginRoles> userroles = this._DBERP.LoginRoles.ToList();
            //  _userdetails.loginroles = BindingListUtillity.GenerateSelectList(userroles);
            _pooldetails.PoolName = pool.PoolName;
            _pooldetails.PoolDesc = pool.PoolDesc;
            _pooldetails.PoolStartDate = pool.PoolStartDate;
            _pooldetails.PoolEndDate = pool.PoolEndDate;
            _pooldetails.PoolId = pool.PoolId;

            return _pooldetails;
        }

        public List<PoolVM> getAllPools()
        {
            //List<PoolVM> _allpooldetails = new List<PoolVM>();
           

           // var u = rolclaims.FirstOrDefault(r => r.UserId == 5).Role.RoleName;
            IEnumerable<PoolVM> _allpooldetails = this._DBERP.PoolMaster.ToList().Where(pr=>pr.IsActive==true).Select(p => new PoolVM {PoolId=p.PoolId, PoolName=p.PoolName, PoolDesc=p.PoolDesc, PoolEndDate=p.PoolEndDate,PoolStartDate=p.PoolStartDate,IsActive=p.IsActive});

            return _allpooldetails.ToList();
        }

        public Task<int> GetCount()
        {
            throw new NotImplementedException();
        }

        public Task<List<PoolVM>> GetPaginatedResult(int currentPage, int pageSize = 10)
        {
            throw new NotImplementedException();
        }

        public bool DeletePool(int pid)
        {
            bool result = false;
            int innerresult = 0;
            var entity = _DBERP.PoolMaster.FirstOrDefault(item => item.PoolId == pid);

            if (entity != null)
            {

               
                entity.Updateddatetime = DateTime.Now;
                entity.IsActive = false;
                this._DBERP.PoolMaster.Update(entity);
                innerresult = this._DBERP.SaveChanges();
            }

            if (innerresult > 0)
            {
                result = true;
            }


            return result;
        }
    }
}
