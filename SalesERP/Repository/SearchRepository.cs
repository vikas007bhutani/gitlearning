using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEERP.Models;
using SALEERP.ViewModel;
using SALEERP.Repository.Interface;
using SALEERP.Data;

namespace SALEERP.Repository
{
    public class SearchRepository : ISearchRepository
    {
        private Sales_ERPContext _DBERP;
        public SearchRepository(Sales_ERPContext dbcontext)
        {

            this._DBERP = dbcontext;

        }
        public  Task<List<SearchVM>> Driver_Search(string terms)
        {
            //List<SearchVM> _auvm = new List<SearchVM>();
            //var result =  _DBERP.AgentUser.Where(c => c.Name.StartsWith(terms)).Select(a => new SearchVM { label = a.Name, id = a.AgentId }).ToList();
            //return _auvm;
            throw new NotImplementedException();
        }

        public Task<List<SearchVM>> PAgent_Search(string terms)
        {
            throw new NotImplementedException();
        }
    }
}
