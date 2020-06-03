using System;
using System.Collections.Generic;
using System.Linq;
using SALEERP.ViewModel;
using SALEERP.Repository.Interface;
using SALEERP.Data;
using SALEERP.Models;

namespace SALEERP.Repository
{
    public class SeriesRepository : ISeriesRepository
    {
        private Sales_ERPContext _DBERP;

        public SeriesRepository(Sales_ERPContext dbcontext)
        {

            this._DBERP = dbcontext;

        }
        public bool AddSeries(SeriesVM _series, int userid)
        {
            bool result = false;
            try
            {

                this._DBERP.SeriesMaster.Add(new SeriesMaster()
                {
                    SeriesName = _series.SeriesName,
                    SeriesCode = _series.SeriesCode,
                    Createddatetime = DateTime.Now,
                    Createdby = userid,
                    IsActive = true
                }); ;

                result = this._DBERP.SaveChanges() > 0;

            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public bool DeleteSeries(int sid)
        {
            bool result = false;
            try
            {



                int innerresult = 0;
                var entity = _DBERP.SeriesMaster.FirstOrDefault(item => item.SeriesId == sid);

                if (entity != null)
                {



                    entity.Updateddatetime = DateTime.Now;
                    entity.IsActive = false;
                    // entity.UpdatedBy=
                    this._DBERP.SeriesMaster.Update(entity);
                    innerresult = this._DBERP.SaveChanges();
                }

                if (innerresult > 0)
                {
                    result = true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }

        public SeriesVM EditSeries(int uid)
        {
            SeriesVM _serdetails = new SeriesVM();
            try
            {
                SeriesMaster ser = this._DBERP.SeriesMaster.Find(uid);

                // List<LoginRoles> userroles = this._DBERP.LoginRoles.ToList();
                //  _userdetails.loginroles = BindingListUtillity.GenerateSelectList(userroles);
                _serdetails.SeriesName = ser.SeriesName;
                _serdetails.SeriesCode = ser.SeriesCode;

                _serdetails.SeriesId = ser.SeriesId;

               

            }
            catch (Exception)
            {

                throw;
            }
            return _serdetails;
        }

        public List<SeriesVM> getAllSeries()
        {
            IEnumerable<SeriesVM> _allseriesdetails;
            try
            {

            _allseriesdetails = this._DBERP.SeriesMaster.ToList().Where(ar => ar.IsActive == true).Select(p => new SeriesVM { SeriesName = p.SeriesName, SeriesCode = p.SeriesCode, SeriesId = p.SeriesId, IsActive = p.IsActive });

            }
            catch (Exception)
            {

                throw;
            }
            return _allseriesdetails.ToList();
        }

        public bool UpdateSeries(SeriesVM _ser, int uid)
        {
            bool result = false;
            try
            {



                int innerresult = 0;
                var entity = _DBERP.SeriesMaster.FirstOrDefault(item => item.SeriesId == _ser.SeriesId);

                if (entity != null)
                {

                    entity.SeriesName = _ser.SeriesName;
                    entity.SeriesCode = _ser.SeriesCode;

                    entity.Updateddatetime = DateTime.Now;
                    this._DBERP.SeriesMaster.Update(entity);
                    innerresult = this._DBERP.SaveChanges();
                }

                if (innerresult > 0)
                {
                    result = true;
                }
            }
            catch (Exception)
            {

                throw;
            }

            return result;
        }
    }
}
