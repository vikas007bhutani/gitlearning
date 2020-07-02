using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEERP.ViewModel;


namespace SALEERP.Repository.Interface
{
   public interface ISearchRepository
    {
        public Task<List<SearchVM>> Driver_Search(string terms);
        public Task<List<SearchVM>> PAgent_Search(string terms);
    }
}
