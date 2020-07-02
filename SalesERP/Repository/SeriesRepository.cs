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
                    Name = _series.SeriesName,
                    Code = _series.SeriesCode,
                    CreatedDatetime = DateTime.Now,
                    CreatedBy = userid,
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
                var entity = _DBERP.SeriesMaster.FirstOrDefault(item => item.Id == sid);

                if (entity != null)
                {



                    entity.UpdatedDatetime = DateTime.Now;
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
                _serdetails.SeriesName = ser.Name;
                _serdetails.SeriesCode = ser.Code;

                _serdetails.SeriesId = ser.Id;

               

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

            _allseriesdetails = this._DBERP.SeriesMaster.ToList().Where(ar => ar.IsActive == true).Select(p => new SeriesVM { SeriesName = p.Name, SeriesCode = p.Code, SeriesId = p.Id, IsActive = p.IsActive });

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
                var entity = _DBERP.SeriesMaster.FirstOrDefault(item => item.Id == _ser.SeriesId);

                if (entity != null)
                {

                    entity.Name = _ser.SeriesName;
                    entity.Code = _ser.SeriesCode;

                    entity.UpdatedDatetime = DateTime.Now;
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
