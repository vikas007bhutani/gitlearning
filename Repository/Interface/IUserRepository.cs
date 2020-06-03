using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using SALEERP.ViewModel;

namespace SALEERP.Repository.Interface
{
    public interface IUserRepository
    {
        List<UserLoginVM> getAllUsers();
       // List<UserLoginVM> getAllUsers(string sortingorder);
        bool AddUser(UserLoginVM _user,int userid);
        bool UpdateUser(UserLoginVM _user,int uid);
        bool DeleteUser(int uid);
        UserLoginVM EditUser(int uid);
        Task<List<UserLoginVM>> GetPaginatedResult(int currentPage, int pageSize = 10);
        Task<int> GetCount();
    }
}
