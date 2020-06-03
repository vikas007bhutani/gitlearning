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
    public class UserRepository : IUserRepository
    {
        private Sales_ERPContext _DBERP;

        public UserRepository(Sales_ERPContext dbcontext)
        {

            this._DBERP = dbcontext;

        }

       
        public bool AddUser(UserLoginVM _user,int id)
        {
            bool result = false, innerresult = false;
            using (var dbusertrans = this._DBERP.Database.BeginTransaction())
            {
                this._DBERP.UserLogin.Add(new UserLogin()
                {
                    UserName = _user.UserName,
                    UserEmail = _user.UserEmail,
                    UserPass = _user.UserPass,
                    UserPhone = _user.UserPhone,
                    CreatedDatetime = DateTime.Now,
                    UpdatedBy=id,
                    IsActive = true
                }); ;

                result = this._DBERP.SaveChanges() > 0;
                if (result)
                {
                    int userid = this._DBERP.UserLogin.Max(p => p.UserId);
                    this._DBERP.Roleclaim.Add(new Roleclaim() { RoleId = _user.RoleId, UserId = userid, CreatedDatetime = DateTime.Now,UpdatedBy=id,IsActive=true });
                    innerresult = this._DBERP.SaveChanges() > 0;
                    if (innerresult)
                    {
                        dbusertrans.Commit();
                    }
                    else
                    { dbusertrans.Rollback(); result = false; }

                }
                else { dbusertrans.Rollback(); }

            }
            return result;

        }

        public bool UpdateUser(UserLoginVM _user,int uid)
        {
            bool result = false;
            int innerresult = 0,innerresultrole=0;
            using (var dbusertrans = this._DBERP.Database.BeginTransaction())
            {
               
                var entity = _DBERP.UserLogin.FirstOrDefault(item => item.UserId == _user.UserId);

                if (entity != null)
                {

                    entity.UserName = _user.UserName;
                    entity.UserEmail = _user.UserEmail;
                    entity.UserPass = _user.UserPass;
                    entity.UserPhone = _user.UserPhone;
                    entity.UpdatedDatetime = DateTime.Now;
                    entity.UpdatedBy = uid;
                    entity.IsActive = true;
                    this._DBERP.UserLogin.Update(entity);
                    innerresult = this._DBERP.SaveChanges();
                }
                if (innerresult > 0)
                {
                    var entityrole = _DBERP.Roleclaim.FirstOrDefault(item => item.UserId == _user.UserId );

                    if (entityrole != null)
                    {

                        entityrole.UserId = _user.UserId;
                        entityrole.RoleId = _user.RoleId;

                        entityrole.UpdatedDatetime = DateTime.Now;
                        entityrole.UpdatedBy = uid;
                        entityrole.IsActive = true;
                        this._DBERP.Roleclaim.Update(entityrole);
                        innerresultrole = this._DBERP.SaveChanges();
                    }
                   
                    if (innerresultrole>0)
                    {
                        dbusertrans.Commit();
                        result = true;
                    }
                    else
                    { dbusertrans.Rollback(); }

                }
                else { dbusertrans.Rollback(); }

            }
            return result;

        }

        public UserLoginVM EditUser(int uid)
        {
            UserLogin user = this._DBERP.UserLogin.Find(uid);
            UserLoginVM _userdetails = new UserLoginVM();
            List<LoginRoles> userroles = this._DBERP.LoginRoles.ToList();
            _userdetails.loginroles = BindingListUtillity.GenerateSelectList(userroles);
            _userdetails.UserName = user.UserName;
            _userdetails.UserPass = user.UserPass;
            _userdetails.UserEmail = user.UserEmail;
            _userdetails.UserPhone = user.UserPhone;
            _userdetails.UserId = user.UserId;
            _userdetails.RoleId = user.Roleclaim.Where(r => r.UserId == uid).FirstOrDefault()?.RoleId;

            return _userdetails;
            //.Where(u => u.UserId == uid).Select(u => new UserLoginVM { UserName = u.UserName, UserEmail = u.UserEmail, UserPass = u.UserPass, roleid = u.Roleclaim.FirstOrDefault().RoleId })


        }

        // db.SaveChanges();
        public List<UserLoginVM> getAllUsers()
        {

            List<UserLoginVM> _alluserdetails = new List<UserLoginVM>();
            List<LoginRoles> userroles = this._DBERP.LoginRoles.ToList();
            List<Roleclaim> rolclaims = this._DBERP.Roleclaim.ToList();

           // var u = rolclaims.FirstOrDefault(r => r.UserId == 5).Role.RoleName;
            IEnumerable<UserLoginVM> alluser = this._DBERP.UserLogin.ToList().Where(i=>i.IsActive==true).Select(u=>new UserLoginVM { UserId = u.UserId, UserName = u.UserName, UserEmail = u.UserEmail, UserPass = u.UserPass, loginroles = BindingListUtillity.GenerateSelectList(userroles),RoleName=(string)rolclaims.Where(r => r.UserId == u.UserId && r.IsActive==true).FirstOrDefault()?.Role.RoleName } );
           
            return alluser.ToList();

        }

        public async Task<List<UserLoginVM>> GetPaginatedResult(int currentPage, int pageSize = 10)
        {
            var data = await getAllUserspaging();
            return data.OrderBy(d => d.UserId).Skip((currentPage - 1) * pageSize).Take(pageSize).ToList();
        }

        public async Task<int> GetCount()
        {
            var data = await getAllUserspaging();
            return data.Count;
        }
        public async Task<List<UserLoginVM>> getAllUserspaging()
        {

            List<UserLoginVM> _alluserdetails = new List<UserLoginVM>();
            List<LoginRoles> userroles = this._DBERP.LoginRoles.ToList();
            List<Roleclaim> rolclaims = this._DBERP.Roleclaim.ToList();
            var u = rolclaims.FirstOrDefault(r => r.UserId == 5).Role.RoleName;
            IEnumerable<UserLoginVM> alluser = this._DBERP.UserLogin.ToList().Select(u => new UserLoginVM { UserId = u.UserId, UserName = u.UserName, UserEmail = u.UserEmail, UserPass = u.UserPass, loginroles = BindingListUtillity.GenerateSelectList(userroles), RoleName = (string)rolclaims.FirstOrDefault(r => r.UserId == u.UserId).Role.RoleName, });

            return alluser.ToList();

        }

        public bool DeleteUser( int uid)
        {
            bool result = false;
            int innerresult = 0, innerresultrole = 0;
            using (var dbusertrans = this._DBERP.Database.BeginTransaction())
            {

                var entity = _DBERP.UserLogin.FirstOrDefault(item => item.UserId == uid);

                if (entity != null)
                {

                   
                    entity.UpdatedDatetime = DateTime.Now;
                    entity.UpdatedBy = uid;
                    entity.IsActive = false;
                    this._DBERP.UserLogin.Update(entity);
                    innerresult = this._DBERP.SaveChanges();
                }
                if (innerresult > 0)
                {
                    var entityrole = _DBERP.Roleclaim.FirstOrDefault(item => item.UserId == uid);

                    if (entityrole != null)
                    {

                     
                        entityrole.UpdatedDatetime = DateTime.Now;
                        entityrole.UpdatedBy = uid;
                        entityrole.IsActive = false;
                        this._DBERP.Roleclaim.Update(entityrole);
                        innerresultrole = this._DBERP.SaveChanges();
                    }

                    if (innerresultrole > 0)
                    {
                        dbusertrans.Commit();
                        result = true;
                    }
                    else
                    { dbusertrans.Rollback(); }

                }
                else { dbusertrans.Rollback(); }

            }
            return result;
        }
    }
}
