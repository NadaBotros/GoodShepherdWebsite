using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.SqlClient;
namespace DAL
{
    public class ActivitySectionsManagement
    {
        #region Variable
        dbDataContext db;
        ActivitySection _obj;
        #endregion
        #region Method
        public ActivitySectionsManagement()
        { db = new dbDataContext(); }
        public string Add(string ActivityId, string SectionTitle, string SectionDesc, string SectionDate, string CreatedBy)
        {
            try
            {
                _obj = new ActivitySection();
                _obj.ActivitySectionId = Guid.NewGuid();
                _obj.ActivityId = new Guid(ActivityId);
                _obj.SectionTitle = SectionTitle;
                _obj.SectionDesc = SectionDesc;
                _obj.SectionDate = DateTime.Parse(SectionDate);
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.ActivitySections.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.ActivitySectionId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string ActivitySectionId, string ActivityId, string SectionTitle, string SectionDesc, string SectionDate, string ModifiedBy)
        {
            try
            {
                _obj = db.ActivitySections.FirstOrDefault(lb => lb.ActivitySectionId == new Guid(ActivitySectionId));
                if (_obj != null)
                {
                    if (!string.IsNullOrEmpty(ActivityId))
                        _obj.ActivityId = new Guid(ActivityId);
                    _obj.SectionTitle = SectionTitle;
                    _obj.SectionDesc = SectionDesc;
                    _obj.SectionDate = DateTime.Parse(SectionDate);
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
        public bool Delete(string ActivitySectionId, string ModifiedBy)
        {
            try
            {
                _obj = db.ActivitySections.FirstOrDefault(ad => ad.ActivitySectionId == new Guid(ActivitySectionId));
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
        public bool Restore(string ActivitySectionId, string ModifiedBy)
        {
            try
            {
                _obj = db.ActivitySections.FirstOrDefault(ad => ad.ActivitySectionId == new Guid(ActivitySectionId));
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
        public DataTable LoadByDeleteState(string ActivityId, string Active)
        {
            try
            {
                var query = (from P in db.ActivitySections
                             where P.Active == bool.Parse(Active) && P.ActivityId == new Guid(ActivityId)
                             select P).Distinct();
                return query.ToDataTable("SectionDate", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public DataTable LoadList(string ActivityId, string Active)
        {

            var query = (from P in db.ActivitySections
                         where P.Active == bool.Parse(Active) && P.ActivityId == new Guid(ActivityId)
                         select new { P.ActivitySectionId, P.SectionDate, SectionTitle = P.SectionDate != null ? string.Format("{0} - {1}", P.SectionDate.Value.ToShortDateString(), P.SectionTitle) : P.SectionTitle }).Distinct();
            return query.ToDataTable("SectionDate", "ASC");

        }
        public ActivitySection LoadById(string ActivitySectionId)
        {
            try
            {
                if (!string.IsNullOrEmpty(ActivitySectionId))
                {
                    _obj = db.ActivitySections.FirstOrDefault(lb => lb.ActivitySectionId == new Guid(ActivitySectionId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool CheckIfExists(string ActivitySectionId, string SectionTitle)
        {
            //try
            //{
            SectionTitle = GeneralMethods.ConvertToSearchWord(SectionTitle);
            if (!string.IsNullOrEmpty(ActivitySectionId))
            {
                _obj = db.ActivitySections.FirstOrDefault(lb => lb.ActivitySectionId != new Guid(ActivitySectionId) && SqlMethods.Like(lb.SectionTitle, SectionTitle) && lb.Active == true);
                if (_obj != null)
                    return true;
            }
            else
            {
                _obj = db.ActivitySections.FirstOrDefault(lb => SqlMethods.Like(lb.SectionTitle, SectionTitle) && lb.Active == true);
                if (_obj != null)
                    return true;
            }
            return false;
            //}
            //catch
            //{
            //    return false;
            //}
        }
        #endregion

    }
}
