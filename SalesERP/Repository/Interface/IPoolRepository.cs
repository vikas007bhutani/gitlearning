using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEERP.Models;
using SALEERP.ViewModel;
using SALEERP.Repository;

namespace SALEERP.Repository.Interface
{
    public interface IPoolRepository
    {
        List<PoolVM> getAllPools();
        // List<UserLoginVM> getAllUsers(string sortingorder);
        bool AddPool(PoolVM _user, int userid);
        bool UpdatePool(PoolVM _user, int uid);
        PoolVM EditPool(Int64 uid);
        bool DeletePool(int pid);
        Task<List<PoolVM>> GetPaginatedResult(int currentPage, int pageSize = 10);
        Task<int> GetCount();
    }
}
