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
                    Name = _poll.PoolName,
                    Description=_poll.PoolDesc,
                    StartDate=_poll.PoolStartDate,
                    EndDate=_poll.PoolEndDate,
                    CreatedDatetime = DateTime.Now,
                    CreatedBy = userid,
                    IsActive = true
                }); ;

                result = this._DBERP.SaveChanges() > 0;
              

            
            return result;
        }
        public bool UpdatePool(PoolVM _poll, int uid)
        {
            bool result = false;
            int innerresult =0;
            var entity = _DBERP.PoolMaster.FirstOrDefault(item => item.Id == _poll.PoolId);
          
            if (entity != null)
            {
                
                entity.Name = _poll.PoolName;
                entity.Description = _poll.PoolDesc;
                entity.StartDate = _poll.PoolStartDate;
                entity.EndDate = _poll.PoolEndDate;
                entity.UpdatedDatetime = DateTime.Now;
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
            _pooldetails.PoolName = pool.Name;
            _pooldetails.PoolDesc = pool.Description;
            _pooldetails.PoolStartDate = pool.StartDate;
            _pooldetails.PoolEndDate = pool.EndDate;
            _pooldetails.PoolId = pool.Id;

            return _pooldetails;
        }

        public List<PoolVM> getAllPools()
        {
            //List<PoolVM> _allpooldetails = new List<PoolVM>();
           

           // var u = rolclaims.FirstOrDefault(r => r.UserId == 5).Role.RoleName;
            IEnumerable<PoolVM> _allpooldetails = this._DBERP.PoolMaster.ToList().Where(pr=>pr.IsActive==true).Select(p => new PoolVM {PoolId=p.Id, PoolName=p.Name, PoolDesc=p.Description, PoolEndDate=p.EndDate,PoolStartDate=p.StartDate,IsActive=p.IsActive});

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
            var entity = _DBERP.PoolMaster.FirstOrDefault(item => item.Id == pid);

            if (entity != null)
            {

               
                entity.UpdatedDatetime = DateTime.Now;
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
