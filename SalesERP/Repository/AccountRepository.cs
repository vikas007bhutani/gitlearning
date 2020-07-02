using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEERP.Data;
using SALEERP.Models;
using SALEERP.ViewModel;
using SALEERP.Repository.Interface;
using System.Web.Providers.Entities;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;

namespace SALEERP.Repository
{
    public class AccountRepository:IAccountRepository
    {
        private readonly object Session;
        private Sales_ERPContext _DBERP;
        public AccountRepository(Sales_ERPContext dbcontext)
        {

            this._DBERP = dbcontext;

        }
        public bool ManageUser(UserLoginVM _user,out int uid)
        {
            bool isuserauthenticated = false;
            int userid = 0 ;
            uid = 0;
            if (!string.IsNullOrWhiteSpace(_user.UserName))
            {
                var result = this._DBERP.UserLogin.FirstOrDefault(us => us.Name == _user.UserName && us.Password==_user.UserPass  && us.IsActive==true);

                if (result != null)
                {
                    isuserauthenticated = true;
                    uid = result.Id;
                }
                        
               

            }
            return isuserauthenticated;
        }
    }
}
