using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Globalization;
namespace DAL
{
    public class ServantVisitsManagement
    {
        #region Variable
        dbDataContext db;
        Prg_ServantVisit _obj;
        #endregion
        #region Method
        public ServantVisitsManagement()
        { db = new dbDataContext(); }
        public string Add(string ServantId, string PersonId, string VisitDate, string VisitNotes, string ImpotantNotes, string ReminderDate, string CreatedBy)
        {
            try
            {

                _obj = new Prg_ServantVisit();
                _obj.ServantVisitId = Guid.NewGuid();
                _obj.ServantId = new Guid(ServantId);
                _obj.PersonId = new Guid(PersonId);
                DateTime dtv;
                if (DateTime.TryParseExact(VisitDate, "d/M/yyyy", CultureInfo.InvariantCulture,
           DateTimeStyles.None, out dtv))
                    _obj.VisitDate = dtv;
                _obj.VisitNotes = VisitNotes;
                _obj.ImpotantNotes = bool.Parse(ImpotantNotes);
                DateTime dt;
                if (DateTime.TryParseExact(ReminderDate, "d/M/yyyy", CultureInfo.InvariantCulture,
           DateTimeStyles.None, out dt))
                    _obj.ReminderDate = dt;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.Prg_ServantVisits.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.PersonId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string ServantVisitId, string ServantId, string PersonId, string VisitDate, string VisitNotes, string ImpotantNotes, string ReminderDate, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_ServantVisits.FirstOrDefault(x => x.ServantVisitId == new Guid(ServantVisitId));
                if (_obj != null)
                {
                    _obj.ServantId = new Guid(ServantId);
                    _obj.PersonId = new Guid(PersonId);
                    DateTime dtv;
                    if (DateTime.TryParseExact(VisitDate, "d/M/yyyy", CultureInfo.InvariantCulture,
               DateTimeStyles.None, out dtv))
                        _obj.VisitDate = dtv;
                    _obj.VisitNotes = VisitNotes;
                    _obj.ImpotantNotes = bool.Parse(ImpotantNotes);
                    DateTime dt;
                    if (DateTime.TryParseExact(ReminderDate, "d/M/yyyy", CultureInfo.InvariantCulture,
           DateTimeStyles.None, out dt))
                        _obj.ReminderDate = dt;
                    else
                        _obj.ReminderDate = null;
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
        public bool Delete(string ServantVisitId)
        {
            try
            {
                var obj = db.Prg_ServantVisits.Where(ad => ad.ServantVisitId == new Guid(ServantVisitId));
                foreach (Prg_ServantVisit p in obj)
                {
                    db.Prg_ServantVisits.DeleteOnSubmit(p);
                }
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public DataTable ServantVisits(string ServantId)
        {
            var query = (from x in db.Prg_ServantVisits
                         where x.ServantId == new Guid(ServantId)
                         select new { x.CreatedBy, x.ModifiedBy, x.ModifiedOn, x.CreatedOn, Mobile = x.Prg_Person.MobileNo1, x.Prg_Person.PersonCode, x.Prg_Person.PersonName, date = x.VisitDate, x.ServantVisitId, VisitDate = x.VisitDate.ToShortDateString(), RemiderDate = ConvertToDate(x.ReminderDate) }).Distinct();
            return query.ToDataTable("date", "DESC");

        }
        string ConvertToDate(object date)
        {
            try
            {
                DateTime dt;
                if (DateTime.TryParse(date.ToString(), out dt))
                    return dt.ToShortDateString();
                else
                    return string.Empty;
            }
            catch { return string.Empty; }
        }
        public Prg_ServantVisit LoadById(string ServantVisitId)
        {
            try
            {
                _obj = db.Prg_ServantVisits.FirstOrDefault(x => x.ServantVisitId == new Guid(ServantVisitId));
                return _obj;
            }
            catch { return null; }
        }
        #endregion
    }

}
