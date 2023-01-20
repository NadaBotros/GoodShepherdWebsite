using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Data.Linq.SqlClient;
namespace DAL
{
    public class MagazineStoryManage
    {
        #region Variable
        dbDataContext db;
        MagazineStory _obj;
        #endregion
        #region Method
        public MagazineStoryManage()
        { db = new dbDataContext(); }
        public string Add(string MagazineId, string StoryTitle, string StoryContent, string CreatedBy)
        {
            try
            {
                _obj = new MagazineStory();
                _obj.MagazineStoryId = Guid.NewGuid();
                _obj.MagazineId = new Guid(MagazineId);
                _obj.StoryTitle = StoryTitle;
                _obj.StoryContent = StoryContent;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.MagazineStories.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.MagazineStoryId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string MagazineStoryId, string StoryTitle, string StoryContent, string ModifiedBy)
        {
            try
            {
                _obj = db.MagazineStories.FirstOrDefault(lb => lb.MagazineStoryId == new Guid(MagazineStoryId));
                if (_obj != null)
                {
                    _obj.StoryTitle = StoryTitle;
                    _obj.StoryContent = StoryContent;
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
        public bool Delete(string MagazineStoryId, string ModifiedBy)
        {
            try
            {
                _obj = db.MagazineStories.FirstOrDefault(ad => ad.MagazineStoryId == new Guid(MagazineStoryId));
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
        public bool Restore(string MagazineStoryId, string ModifiedBy)
        {
            try
            {
                _obj = db.MagazineStories.FirstOrDefault(ad => ad.MagazineStoryId == new Guid(MagazineStoryId));
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
        public DataTable LoadByDeleteState(string MagazineId, string Active)
        {
            try
            {
                var query = (from P in db.MagazineStories
                             where P.Active == bool.Parse(Active) && P.MagazineId == new Guid(MagazineId)
                             select P).Distinct();
                return query.ToDataTable("CreatedOn", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public MagazineStory LoadById(string MagazineStoryId)
        {
            try
            {
                if (!string.IsNullOrEmpty(MagazineStoryId))
                {
                    _obj = db.MagazineStories.FirstOrDefault(lb => lb.MagazineStoryId == new Guid(MagazineStoryId));
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
