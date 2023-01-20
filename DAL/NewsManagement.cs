using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class NewsManagement
    {
        #region Variable
        dbDataContext db;
        New _obj;
        #endregion
        public NewsManagement()
        { db = new dbDataContext(); }
        public string Add(string NewsTitle, string NewsImage, string NewsContent, string NewsTypeId, string ShowInGallery, string ShowInNewsBar, string CreatedBy)
        {
            try
            {
                _obj = new New();
                _obj.NewsId = Guid.NewGuid();
                _obj.NewsTitle = NewsTitle;
                _obj.NewsImage = NewsImage;
                _obj.NewsContent = NewsContent;
                _obj.NewsTypeId = new Guid(NewsTypeId);
                _obj.ShowInGallery = bool.Parse(ShowInGallery);
                _obj.ShowInNewsBar = bool.Parse(ShowInNewsBar);
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.News.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.NewsId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string Id, string NewsTitle, string NewsImage, string NewsContent, string NewsTypeId, string ShowInGallery, string ShowInNewsBar, string ModifiedBy)
        {
            try
            {
                _obj = db.News.FirstOrDefault(ad => ad.NewsId == new Guid(Id));
                if (_obj != null)
                {
                    _obj.NewsTitle = NewsTitle;
                    if (!string.IsNullOrEmpty(NewsImage))
                        _obj.NewsImage = NewsImage;
                    _obj.NewsContent = NewsContent;
                    _obj.NewsTypeId = new Guid(NewsTypeId);
                    _obj.ShowInGallery = bool.Parse(ShowInGallery);
                    _obj.ShowInNewsBar = bool.Parse(ShowInNewsBar);
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
                _obj = db.News.FirstOrDefault(ad => ad.NewsId == new Guid(Id));
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
                _obj = db.News.FirstOrDefault(ad => ad.NewsId == new Guid(Id));
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
        public New LoadById(string NewId)
        {
            try
            {
                if (!string.IsNullOrEmpty(NewId))
                {
                    _obj = db.News.FirstOrDefault(ad => ad.NewsId == new Guid(NewId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public DataTable LoadByDeleteState(string Active, string NewsTypeId)
        {
            try
            {
                if (string.IsNullOrEmpty(NewsTypeId))
                {
                    var query = (from x in db.News
                                 where x.Active == bool.Parse(Active)
                                 select new { x.Active, x.CreatedBy, x.CreatedOn, x.ModifiedBy, x.ModifiedOn, x.NewsId, x.NewsImage, x.NewsTitle, x.NewsType.NewsTypeName }).Distinct();
                    return query.ToDataTable("CreatedOn", "Desc");
                }
                else
                {
                    var query = (from x in db.News
                                 where x.Active == bool.Parse(Active) && x.NewsTypeId == new Guid(NewsTypeId)
                                 select new { x.Active, x.CreatedBy, x.CreatedOn, x.ModifiedBy, x.ModifiedOn, x.NewsId, x.NewsImage, x.NewsTitle, x.NewsType.NewsTypeName }).Distinct();
                    return query.ToDataTable("CreatedOn", "Desc");
                }
            }
            catch
            {
                return null;
            }
        }
        public DataTable LoadAll()
        {
            try
            {
                var query = (from x in db.News
                             where x.Active == true
                             select new { x.CreatedOn,x.NewsId, x.NewsImage, x.NewsTitle, x.NewsType.NewsTypeName }).Distinct();
                return query.ToDataTable("CreatedOn", "Desc");
            }
            catch
            {
                return null;
            }
        }
    }
}
