using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace DAL
{
    public class ActivitiesManage
    {
        #region Variable
        dbDataContext db;
        Activity _obj;
        #endregion
        #region Method
        public ActivitiesManage()
        { db = new dbDataContext(); }
        public string Add(string ActivityTitle, string ActivityDesc, string ActivityPrice, string ActivityPlace, string DaysNo,
            string ServantName, string ServantMobile, string RefuseReasons, string ActivityImage, string LastRequestDate, string ActivityDate,string videoUrl, string CreatedBy)
        {
            try
            {
                _obj = new Activity();
                _obj.ActivityId = Guid.NewGuid();
                _obj.ActivityTitle = ActivityTitle;
                _obj.ActivityDesc = ActivityDesc;
                _obj.ActivityPrice = ActivityPrice;
                _obj.ActivityPlace = ActivityPlace;
                _obj.DaysNo = DaysNo;
                _obj.ServantName = ServantName;
                _obj.ServantMobile = ServantMobile;
                _obj.RefuseReasons = RefuseReasons;
                _obj.ActivityImage = ActivityImage;
                _obj.VideoUrl = videoUrl;
                if (!string.IsNullOrEmpty(LastRequestDate))
                    _obj.LastRequestDate = DateTime.Parse(LastRequestDate);
                if (!string.IsNullOrEmpty(ActivityDate))
                    _obj.ActivityDate = DateTime.Parse(ActivityDate);

                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.Activities.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.ActivityId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string ActivityId, string ActivityTitle, string ActivityDesc, string ActivityPrice, string ActivityPlace, string DaysNo,
            string ServantName, string ServantMobile, string RefuseReasons, string ActivityImage, string LastRequestDate, string ActivityDate,string videoUrl, string ModifiedBy)
        {
            try
            {
                _obj = db.Activities.FirstOrDefault(lb => lb.ActivityId == new Guid(ActivityId));
                if (_obj != null)
                {
                    _obj.ActivityTitle = ActivityTitle;
                    _obj.ActivityDesc = ActivityDesc;
                    _obj.ActivityPrice = ActivityPrice;
                    _obj.ActivityPlace = ActivityPlace;
                    _obj.DaysNo = DaysNo;
                    _obj.ServantName = ServantName;
                    _obj.ServantMobile = ServantMobile;
                    _obj.VideoUrl = videoUrl;
                    _obj.RefuseReasons = RefuseReasons;
                    if (!string.IsNullOrEmpty(ActivityImage))
                        _obj.ActivityImage = ActivityImage;
                    if (!string.IsNullOrEmpty(LastRequestDate))
                        _obj.LastRequestDate = DateTime.Parse(LastRequestDate);
                    else
                        _obj.LastRequestDate = null;
                    if (!string.IsNullOrEmpty(ActivityDate))
                        _obj.ActivityDate = DateTime.Parse(ActivityDate);
                    else
                        _obj.ActivityDate = null;

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
        public bool Delete(string ActivityId, string ModifiedBy)
        {
            try
            {
                _obj = db.Activities.FirstOrDefault(ad => ad.ActivityId == new Guid(ActivityId));
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
        public bool Restore(string ActivityId, string ModifiedBy)
        {
            try
            {
                _obj = db.Activities.FirstOrDefault(ad => ad.ActivityId == new Guid(ActivityId));
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
        public DataTable LoadByDeleteState(string Active)
        {
            try
            {
                var query = (from P in db.Activities
                             where P.Active == bool.Parse(Active)
                             select P).Distinct();
                return query.ToDataTable("ActivityDate", "DESC");
            }
            catch
            {
                return null;
            }
        }
        public Activity LoadById(string ActivityId)
        {
            try
            {
                if (!string.IsNullOrEmpty(ActivityId))
                {
                    _obj = db.Activities.FirstOrDefault(lb => lb.ActivityId == new Guid(ActivityId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
