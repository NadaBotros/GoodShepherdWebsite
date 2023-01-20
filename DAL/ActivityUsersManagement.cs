using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.SqlClient;
namespace DAL
{
    public class ActivityUsersManagement
    {
        #region Variable
        dbDataContext db;
        ActivityUser _obj;
        #endregion
        #region Method
        public ActivityUsersManagement()
        { db = new dbDataContext(); }
        public string Add(string ActivityId, string FullName, string Mobile, string Mobile2, string RoomNo, string Code, string Notes, string CreatedBy)
        {
            try
            {
                _obj = new ActivityUser();
                _obj.ActivityUserId = Guid.NewGuid();
                _obj.ActivityId = new Guid(ActivityId);
                _obj.FullName = FullName;
                _obj.Mobile = Mobile;
                _obj.RoomNo = RoomNo;
                _obj.Code = Code;
                _obj.Mobile2 = Mobile2;
                _obj.Notes = Notes;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.ActivityUsers.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.ActivityUserId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string ActivityUserId, string ActivityId, string FullName, string Mobile, string Mobile2, string RoomNo, string Code, string Notes, string ModifiedBy)
        {
            try
            {
                _obj = db.ActivityUsers.FirstOrDefault(lb => lb.ActivityUserId == new Guid(ActivityUserId));
                if (_obj != null)
                {
                    if (!string.IsNullOrEmpty(ActivityId))
                        _obj.ActivityId = new Guid(ActivityId);
                    _obj.FullName = FullName;
                    _obj.Mobile = Mobile;
                    _obj.RoomNo = RoomNo;
                    _obj.Code = Code;
                    _obj.Mobile2 = Mobile2;
                    _obj.Notes = Notes;
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
        public bool Delete(string ActivityUserId, string ModifiedBy)
        {
            try
            {
                _obj = db.ActivityUsers.FirstOrDefault(ad => ad.ActivityUserId == new Guid(ActivityUserId));
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
       
        public bool Restore(string ActivityUserId, string ModifiedBy)
        {
            try
            {
                _obj = db.ActivityUsers.FirstOrDefault(ad => ad.ActivityUserId == new Guid(ActivityUserId));
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
           // try
            {
                var query = (from x in db.ActivityUsers
                             where x.Active == bool.Parse(Active) && x.ActivityId == new Guid(ActivityId)
                             select x);
                return query.ToDataTable("FullName", "ASC");
            }
            //catch
            //{
            //    return null;
            //}
        }
        public DataTable LoadByDeleteState(string SearchWord, string ActivityId, string Active)
        {
            SearchWord = GeneralMethods.ConvertToSearchWord(SearchWord);
            var query = (from p in db.ActivityUsers
                         where
                             p.Active == bool.Parse(Active) && p.ActivityId == new Guid(ActivityId) &&
                             (SqlMethods.Like(p.FullName, SearchWord) || SqlMethods.Like(p.Mobile, SearchWord) ||
                              SqlMethods.Like(p.RoomNo, SearchWord) || SqlMethods.Like(p.Mobile2, SearchWord))
                         select p).Distinct();
            return query.ToDataTable("FullName", "ASC");
        }
        public ActivityUser LoadById(string ActivityUserId)
        {
            try
            {
                if (!string.IsNullOrEmpty(ActivityUserId))
                {
                    _obj = db.ActivityUsers.FirstOrDefault(lb => lb.ActivityUserId == new Guid(ActivityUserId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool CheckIfNameExists(string ActivityUserId, string ActivityId, string UserName)
        {
            UserName = GeneralMethods.ConvertToSearchWord(UserName);
            if (!string.IsNullOrEmpty(ActivityId))
            {
                _obj =
                    db.ActivityUsers.FirstOrDefault(
                        lb =>
                         lb.ActivityUserId != new Guid(ActivityUserId) &&
                        lb.ActivityId == new Guid(ActivityId) && SqlMethods.Like(lb.FullName, UserName) &&
                        lb.Active == true);
                if (_obj != null)
                    return true;
            }
            else
            {
                _obj = db.ActivityUsers.FirstOrDefault(lb => SqlMethods.Like(lb.FullName, UserName) && lb.ActivityId == new Guid(ActivityId) && lb.Active == true);
                if (_obj != null)
                    return true;
            }
            return false;
        }
        public bool CheckIfCodeExists(string ActivityUserId, string ActivityId, string Code)
        {
            if (!string.IsNullOrEmpty(ActivityId))
            {
                _obj =
                    db.ActivityUsers.FirstOrDefault(
                        lb =>
                             lb.ActivityUserId != new Guid(ActivityUserId) &&
                        lb.ActivityId == new Guid(ActivityId) && lb.Code == Code &&
                        lb.Active == true);
                if (_obj != null)
                    return true;
            }
            else
            {
                _obj = db.ActivityUsers.FirstOrDefault(lb => lb.Code == Code && lb.ActivityId == new Guid(ActivityId) && lb.Active == true);
                if (_obj != null)
                    return true;
            }
            return false;
        }
        public DataTable SearchByCodes(string SearchCodes, string ActivityId)
        {
            List<string> codesList = GeneralMethods.ConvertToPersonodes(SearchCodes);
            var query = (from p in db.ActivityUsers
                         where
                             p.Active == true && codesList.Contains(p.Code) && p.ActivityId == new Guid(ActivityId)
                         select p).Distinct();
            return query.ToDataTable("FullName", "ASC");
        }

        public DataTable SearchGeneral(string SearchWord, string ActivityId)
        {

            SearchWord = GeneralMethods.ConvertToSearchWord(SearchWord);
            var query = (from p in db.ActivityUsers
                         where
                             p.Active == true && p.ActivityId == new Guid(ActivityId) &&
                             (SqlMethods.Like(p.FullName, SearchWord) || SqlMethods.Like(p.Mobile, SearchWord) ||
                              SqlMethods.Like(p.RoomNo, SearchWord) || SqlMethods.Like(p.Mobile2, SearchWord))
                         select p).Distinct();
            return query.ToDataTable("FullName", "ASC");
        }

        #endregion

    }
}
