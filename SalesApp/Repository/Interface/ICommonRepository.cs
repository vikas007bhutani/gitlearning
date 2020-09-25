using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesApp.ViewModel;

namespace SalesApp.Repository.Interface
{
    public interface ICommonRepository
    {
        void SetLoggedInUserId(int UserId);
        void SetLoggedInUserName(string username);
        int GetLoggedInUserId();
        string GetLoggedInUserName();
        Task<List<SelectListItem>> GetCurrency();
        Task<List<SelectListItem>> GetShapes();
        Task<List<SelectListItem>> GetMarbleColor();
        Task<List<SelectListItem>> GetCategory();
        Task<List<SelectListItem>> GetSpecialAddition();
        Task<List<SelectListItem>>  getroles();
    }
}
