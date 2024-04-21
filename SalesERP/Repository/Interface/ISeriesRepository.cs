using System;
using System.Collections.Generic;

using SALEERP.ViewModel;

namespace SALEERP.Repository.Interface
{
   public interface ISeriesRepository
    {

        List<SeriesVM> getAllSeries();
        bool AddSeries(SeriesVM _agent, int userid);
        bool UpdateSeries(SeriesVM _agent, int uid);
        SeriesVM EditSeries(int uid);
        bool DeleteSeries(int pid);
    }
}
