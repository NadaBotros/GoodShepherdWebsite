using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;

namespace DAL
{
    public class QuizManage
    {
        #region Variable
        dbDataContext db;
        Quiz _obj;
        #endregion
        public QuizManage()
        { db = new dbDataContext(); }
        public string Add(string QuizTitle, string QuizDate, string QuizDeliveryDate, string QuizPDF, string QuizCover, string CreatedBy)
        {
            try
            {
                _obj = new Quiz();
                _obj.QuizId = Guid.NewGuid();
                _obj.QuizTitle = QuizTitle;

                DateTime dt;
                if (DateTime.TryParseExact(QuizDate, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                    _obj.QuizDate = dt;
                else
                    _obj.QuizDate = null;

                DateTime dtDelivery;
                if (DateTime.TryParseExact(QuizDeliveryDate, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtDelivery))
                    _obj.QuizDeliveryDate = dtDelivery;
                else
                    _obj.QuizDeliveryDate = null;

                _obj.QuizPDF = QuizPDF;
                _obj.QuizCover = QuizCover;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.Quizs.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.QuizId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string Id, string QuizTitle, string QuizDate, string QuizDeliveryDate, string QuizPDF, string QuizCover, string ModifiedBy)
        {
            try
            {
                _obj = db.Quizs.FirstOrDefault(ad => ad.QuizId == new Guid(Id));
                if (_obj != null)
                {
                    _obj.QuizTitle = QuizTitle;
                    DateTime dt;
                    if (DateTime.TryParseExact(QuizDate, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                        _obj.QuizDate = dt;
                    else
                        _obj.QuizDate = null;

                    DateTime dtDelivery;
                    if (DateTime.TryParseExact(QuizDeliveryDate, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dtDelivery))
                        _obj.QuizDeliveryDate = dtDelivery;
                    else
                        _obj.QuizDeliveryDate = null;

                    if (!string.IsNullOrEmpty(QuizPDF))
                        _obj.QuizPDF = QuizPDF;
                    if (!string.IsNullOrEmpty(QuizCover))
                        _obj.QuizCover = QuizCover;
                    _obj.QuizPDF = QuizPDF;
                    _obj.ModifiedBy = new Guid(ModifiedBy);
                    _obj.ModifiedOn = DateTime.Now;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool Delete(string Id, string ModifiedBy)
        {
            try
            {
                _obj = db.Quizs.FirstOrDefault(ad => ad.QuizId == new Guid(Id));
                if (_obj != null)
                {
                    _obj.Active = false;
                    _obj.ModifiedBy = new Guid(ModifiedBy);
                    _obj.ModifiedOn = DateTime.Now;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool Restore(string Id, string ModifiedBy)
        {
            try
            {
                _obj = db.Quizs.FirstOrDefault(ad => ad.QuizId == new Guid(Id));
                if (_obj != null)
                {
                    _obj.Active = true;
                    _obj.ModifiedBy = new Guid(ModifiedBy);
                    _obj.ModifiedOn = DateTime.Now;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public Quiz LoadById(string NewId)
        {
            try
            {
                if (!string.IsNullOrEmpty(NewId))
                {
                    _obj = db.Quizs.FirstOrDefault(ad => ad.QuizId == new Guid(NewId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public DataTable LoadByDeleteState(string Active)
        {
            var query = (from x in db.Quizs
                         where x.Active == bool.Parse(Active)
                         select new { x.Active, x.CreatedBy, x.CreatedOn, x.QuizCover, x.QuizTitle, x.QuizId, date = x.QuizDate, QuizDate = GeneralMethods.ConvertToDateString(x.QuizDate.ToString()), x.QuizPDF, x.ModifiedBy, x.ModifiedOn }).Distinct();
            return query.ToDataTable("date", "DESC");
        }
    }
}
