using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Globalization;

namespace DAL
{
    public class MeetingArchiveManage
    {
        #region Variable
        dbDataContext db;
        MeetingArchive _obj;
        #endregion
        public MeetingArchiveManage()
        { db = new dbDataContext(); }
        public string Add(string ArchiveType, string Title, string Date, string Negatives, string Positives, string Suggestions, string CreatedBy)
        {
            try
            {
                _obj = new MeetingArchive();
                _obj.MeetingArchiveId = Guid.NewGuid();
                _obj.ArchiveType = ArchiveType;
                _obj.Title = Title;
                DateTime dt;
                if (DateTime.TryParseExact(Date, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                    _obj.Date = dt;              
                _obj.Negatives = Negatives;
                _obj.Positives = Positives;
                _obj.Suggestions = Suggestions;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.MeetingArchives.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.MeetingArchiveId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string Id, string ArchiveType, string Title, string Date, string Negatives, string Positives, string Suggestions, string ModifiedBy)
        {
            try
            {
                _obj = db.MeetingArchives.FirstOrDefault(ad => ad.MeetingArchiveId == new Guid(Id));
                if (_obj != null)
                {
                    _obj.ArchiveType = ArchiveType;
                    _obj.Title = Title;
                    DateTime dt;
                    if (DateTime.TryParseExact(Date, "d/M/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out dt))
                        _obj.Date = dt;
                    _obj.Negatives = Negatives;
                    _obj.Positives = Positives;
                    _obj.Suggestions = Suggestions;
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
                _obj = db.MeetingArchives.FirstOrDefault(ad => ad.MeetingArchiveId == new Guid(Id));
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
                _obj = db.MeetingArchives.FirstOrDefault(ad => ad.MeetingArchiveId == new Guid(Id));
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
        public MeetingArchive LoadById(string NewId)
        {
            try
            {
                if (!string.IsNullOrEmpty(NewId))
                {
                    _obj = db.MeetingArchives.FirstOrDefault(ad => ad.MeetingArchiveId == new Guid(NewId));
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
            var query = (from x in db.MeetingArchives
                         where x.Active == bool.Parse(Active)
                         select new { x.Active, x.CreatedBy, x.CreatedOn, x.ArchiveType, x.Negatives, x.Positives, x.Title, x.MeetingArchiveId, date2 = x.Date, Date = GeneralMethods.ConvertToDateString(x.Date.ToString()), x.Suggestions, x.ModifiedBy, x.ModifiedOn }).Distinct();
            return query.ToDataTable("date2", "DESC");
        }
    }
}
