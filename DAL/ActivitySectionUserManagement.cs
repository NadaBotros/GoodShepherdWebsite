using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.SqlClient;
namespace DAL
{
    public class ActivitySectionUserManagement
    {
        #region Variable
        dbDataContext db;
        ActivitySectionUser _obj;
        #endregion
        #region Method
        public ActivitySectionUserManagement()
        { db = new dbDataContext(); }
        public string Add(string ActivitySectionId, string ActivityUserId, string Notes, string CreatedBy)
        {
            try
            {
                _obj = new ActivitySectionUser();
                _obj.ActivitySectionUserId = Guid.NewGuid();
                _obj.ActivitySectionId = new Guid(ActivitySectionId);
                _obj.ActivityUserId = new Guid(ActivityUserId);
                _obj.Notes = Notes;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.ActivitySectionUsers.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.ActivitySectionUserId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public string Add(List<string> PersonsId, string ActivitySectionId, string Notes, string CreatedBy)
        {
            StringBuilder str = new StringBuilder();
            foreach (string PersonId in PersonsId)
            {
                ActivitySectionUser user = db.ActivitySectionUsers.FirstOrDefault(
                    x =>
                    x.ActivityUserId == new Guid(PersonId) && x.ActivitySectionId == new Guid(ActivitySectionId) &&
                    x.Active.Value);
                if (user != null)
                {
                    str.Append("  - " + user.ActivityUser.FullName + "<br>");
                }
                else
                {
                    _obj = new ActivitySectionUser();
                    _obj.ActivitySectionUserId = Guid.NewGuid();
                    _obj.ActivitySectionId = new Guid(ActivitySectionId);
                    _obj.ActivityUserId = new Guid(PersonId);
                    _obj.Notes = Notes;
                    _obj.Active = true;
                    _obj.CreatedBy = new Guid(CreatedBy);
                    _obj.CreatedOn = DateTime.Now;
                    db.ActivitySectionUsers.InsertOnSubmit(_obj);
                }
            }
            db.SubmitChanges();
            return str.ToString();
        }

        public bool Edit(string ActivitySectionUserId, string ActivitySectionId, string ActivityUserId, string Notes, string ModifiedBy)
        {
            try
            {
                _obj = db.ActivitySectionUsers.FirstOrDefault(lb => lb.ActivitySectionUserId == new Guid(ActivitySectionUserId));
                if (_obj != null)
                {
                    if (!string.IsNullOrEmpty(ActivitySectionId))
                        _obj.ActivitySectionId = new Guid(ActivitySectionId);
                    if (!string.IsNullOrEmpty(ActivityUserId))
                        _obj.ActivityUserId = new Guid(ActivityUserId);
                    _obj.Notes = Notes;
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
        public bool Delete(string ActivitySectionUserId, string ModifiedBy)
        {
            try
            {
                _obj = db.ActivitySectionUsers.FirstOrDefault(ad => ad.ActivitySectionUserId == new Guid(ActivitySectionUserId));
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
        public bool Delete(List<string> ActivitySectionUserIds)
        {
            try
            {
                foreach (string activitySectionUserId in ActivitySectionUserIds)
                {
                    _obj =
                        db.ActivitySectionUsers.FirstOrDefault(
                            ad => ad.ActivitySectionUserId == new Guid(activitySectionUserId));
                    if (_obj != null)
                    {
                        db.ActivitySectionUsers.DeleteOnSubmit(_obj);
                        db.SubmitChanges();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Restore(string ActivitySectionUserId, string ModifiedBy)
        {
            try
            {
                _obj = db.ActivitySectionUsers.FirstOrDefault(ad => ad.ActivitySectionUserId == new Guid(ActivitySectionUserId));
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
        public DataTable LoadByDeleteState(string ActivitySectionId, string Active)
        {
            try
            {
                var query = (from x in db.ActivitySectionUsers
                             where x.Active == bool.Parse(Active) && x.ActivitySectionId == new Guid(ActivitySectionId)
                             select new { x.Active, x.ActivityUserId, x.ActivitySectionUserId, x.ActivityUser.Code, x.CreatedBy, x.CreatedOn, x.ModifiedBy, x.ModifiedOn, x.Notes, x.ActivityUser.FullName, x.ActivityUser.RoomNo, x.ActivityUser.Mobile });
                return query.ToDataTable("FullName", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public ActivitySectionUser LoadById(string ActivitySectionUserId)
        {
            try
            {
                if (!string.IsNullOrEmpty(ActivitySectionUserId))
                {
                    _obj = db.ActivitySectionUsers.FirstOrDefault(lb => lb.ActivitySectionUserId == new Guid(ActivitySectionUserId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public List<spActivitiesAttendResult> ActivitiesAttendReport(string sectionsIds)
        {
            return (from x in db.spActivitiesAttend(sectionsIds) select x).ToList();
        }
        public DataTable ActivitiesAttendReportDT(string sectionsIds)
        {
            return
              (from x in db.spActivitiesAttend(sectionsIds) select x).AsQueryable<spActivitiesAttendResult>().ToDataTable();
        }       
        #endregion

    }
}
