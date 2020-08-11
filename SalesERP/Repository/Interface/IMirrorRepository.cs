using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SALEERP.Models;
using SALEERP.ViewModel;

namespace SALEERP.Repository.Interface
{
   public interface IMirrorRepository
    {
        public  Task<List<AgentUserVM>> GetNames(string terms);
        //public Task<List<AgentUserVM>> GetNames1(string terms);
        public Task<MirrorDetailsVM> getAllMirrors();
        public Task<bool> AddMirror(MirrorDetailsVM _mirror, int userid);
        MirrorDetailsVM EditMirror(Int64 uid);
        public Task<bool> UpdateMirror(MirrorDetailsVM _agent, int uid);

    }
}
