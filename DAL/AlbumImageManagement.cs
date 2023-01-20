using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class AlbumImageManagement
    {
        #region Variable
        dbDataContext db;
        AlbumImage _obj;
        #endregion
        #region Method
        public AlbumImageManagement()
        { db = new dbDataContext(); }
        public string Add(string AlbumId,string ImageName, string CreatedBy)
        {
            try
            {
                _obj = new AlbumImage();
                _obj.ImageId = Guid.NewGuid();
                _obj.AlbumId = new Guid(AlbumId);
                _obj.ImageFile = ImageName;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.AlbumImages.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.ImageId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string ImageId, string ImageName, string ModifiedBy)
        {
            try
            {
                _obj = db.AlbumImages.FirstOrDefault(lb => lb.ImageId == new Guid(ImageId));
                if (_obj != null)
                {
                    if (!string.IsNullOrEmpty(ImageName))
                        _obj.ImageFile = ImageName;
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
                _obj = db.AlbumImages.FirstOrDefault(ad => ad.ImageId == new Guid(ImageId));
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
                _obj = db.AlbumImages.FirstOrDefault(ad => ad.ImageId == new Guid(ImageId));
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
        public DataTable LoadByDeleteState(string AlbumId, string Active)
        {
            try
            {
                var query = (from P in db.AlbumImages
                             where P.Active == bool.Parse(Active) && P.AlbumId == new Guid(AlbumId)
                             select P);
                return query.ToDataTable("CreatedOn", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public AlbumImage LoadByImageId(string ImageId)
        {
            try
            {
                if (!string.IsNullOrEmpty(ImageId))
                {
                    _obj = db.AlbumImages.FirstOrDefault(lb => lb.ImageId == new Guid(ImageId));
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
