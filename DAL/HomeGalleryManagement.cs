using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class HomeGalleryManagement
    {
        #region Variable
        dbDataContext db;
        HomeGallery _obj;
        #endregion
        #region Method
        public HomeGalleryManagement()
        { db = new dbDataContext(); }
        public string Add(string ImageName, string ImageDesc, string CreatedBy)
        {
            try
            {
                _obj = new HomeGallery();
                _obj.ImageId = Guid.NewGuid();
                _obj.ImageFile = ImageName;
                _obj.ImageDescription = ImageDesc;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.HomeGalleries.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.ImageId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string ImageId, string ImageName, string ImageDesc, string ModifiedBy)
        {
            try
            {
                _obj = db.HomeGalleries.FirstOrDefault(lb => lb.ImageId == new Guid(ImageId));
                if (_obj != null)
                {
                    if (!string.IsNullOrEmpty(ImageName))
                        _obj.ImageFile = ImageName;
                    _obj.ImageDescription = ImageDesc;
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
        public bool Delete(string ImageId, string ModifiedBy)
        {
            try
            {
                _obj = db.HomeGalleries.FirstOrDefault(ad => ad.ImageId == new Guid(ImageId));
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
        public bool Restore(string ImageId, string ModifiedBy)
        {
            try
            {
                _obj = db.HomeGalleries.FirstOrDefault(ad => ad.ImageId == new Guid(ImageId));
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
                var query = (from P in db.HomeGalleries
                             where P.Active == bool.Parse(Active)
                             select P).Distinct();
                return query.ToDataTable("CreatedOn", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public HomeGallery LoadByImageId(string ImageId)
        {
            try
            {
                if (!string.IsNullOrEmpty(ImageId))
                {
                    _obj = db.HomeGalleries.FirstOrDefault(lb => lb.ImageId == new Guid(ImageId));
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
