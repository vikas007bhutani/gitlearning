﻿using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using SALEERP.Data;
using SALEERP.Models;
using SalesApp.Repository.Interface;
using SalesApp.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SalesApp.ViewModel;

namespace SalesApp.Repository
{
    public class CommonRepository : ICommonRepository
    {
        private Sales_ERPContext _DBERP;
        public CommonRepository(Sales_ERPContext dbcontext)
        {

            this._DBERP = dbcontext;

        }
        public CommonRepository()
        { }
        public async Task<List<SelectListItem>> GetCurrency()
        {
            List<SelectListItem> _getcurrency = new List<SelectListItem>();
            List<CurrencyMaster> _currencymaster = await this._DBERP.CurrencyMaster.Where(i => i.IsActive == true).ToListAsync();
            _getcurrency = BindingListUtillity.GenerateSelectList(_currencymaster);
            return _getcurrency;
        }
        public async Task<List<SelectListItem>> GetSpecialAddition()
        {
            List<SelectListItem> _getspecialaddition = new List<SelectListItem>();
            List<SpecialEdition> _samaster = await this._DBERP.SpecialEdition.Where(i => i.IsActive == true).ToListAsync();
            _getspecialaddition = BindingListUtillity.GenerateSelectList(_samaster);
            return _getspecialaddition;
        }
        public async Task<List<SelectListItem>>  getroles()
        {
            List<SelectListItem> _getroles = new List<SelectListItem>();
            List<LoginRoles> userroles = await this._DBERP.LoginRoles.Where(a => a.IsActive == true && a.Name.ToUpper()=="SALES").ToListAsync();
            _getroles = BindingListUtillity.GenerateSelectList(userroles);
            return _getroles;
        }

        public async Task<List<SelectListItem>> GetShapes()
        {
            List<SelectListItem> _getshapes = new List<SelectListItem>();
            List<Shape> allshapes = await this._DBERP.Shape.ToListAsync();
            _getshapes = BindingListUtillity.GenerateSelectList(allshapes);
            return _getshapes;
           // throw new NotImplementedException();
        }

        public async Task<List<SelectListItem>> GetMarbleColor()
        {
            List<SelectListItem> _getmarble = new List<SelectListItem>();
            List<DesignIntricacyComponentPlacementMarblecolor> allmarblecolor = await this._DBERP.DesignIntricacyComponentPlacementMarblecolor.Where(a=>a.TypeId==13).ToListAsync();
            _getmarble = BindingListUtillity.GenerateSelectList(allmarblecolor);
            return _getmarble;
        }

        public async Task<List<SelectListItem>> GetCategory()
        {
            List<SelectListItem> _getcategory = new List<SelectListItem>();
            List<ItemCategoryMaster> allcategory = await(from m in this._DBERP.ItemCategoryMaster 
                                                                                      join od in this._DBERP.CategorySeparate
                                                                                     on m.CategoryId equals od.Categoryid select new ItemCategoryMaster
                                                                                     {
                                                                                        CategoryId=m.CategoryId,
                                                                                         CategoryName=m.CategoryName

                                                                                     }

                                                                                   
                                                                                    ).ToListAsync();



            _getcategory = BindingListUtillity.GenerateSelectList(allcategory);
            return _getcategory;
        }
        public async Task<List<SelectListItem>> GetNationality()
        {
            List<SelectListItem> _getnationality = new List<SelectListItem>();
            List<Nationality> _nationalitymaster = await this._DBERP.Nationality.ToListAsync();
            _getnationality = BindingListUtillity.GenerateSelectList(_nationalitymaster);
            return _getnationality;
        }

        public async Task<List<SelectListItem>> GetCountries()
        {
            List<SelectListItem> _getcountry = new List<SelectListItem>();
            List<CountriesMaster> _countrymaster = await this._DBERP.CountriesMaster.Where(c=>c.IsActive==true).ToListAsync();
            _getcountry = BindingListUtillity.GenerateSelectList(_countrymaster);
            return _getcountry;
        }
    }
}
