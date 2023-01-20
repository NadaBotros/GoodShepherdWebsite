using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class AlbumManagement
    {
        #region Variable
        dbDataContext db;
        //static dbDataContext dbstatic;
        static dbDataContext dbs = new dbDataContext();
        static Album _objs = new Album();
        Album _obj;
        #endregion
        #region Method
        public AlbumManagement()
        { db = new dbDataContext(); }
        public string Add(string AlbumName, string albumType, string AlbumDate, string AlbumCover, string PamfletFile, string albumDescription, string CreatedBy)
        {
            //try
            //{
                _obj = new Album();
                _obj.AlbumId = Guid.NewGuid();
                _obj.AlbumName = AlbumName;
                _obj.PamfletFile = PamfletFile;
                if (!string.IsNullOrEmpty(AlbumDate))
                    _obj.AlbumDate = DateTime.Parse(AlbumDate);
                _obj.AlbumCover = AlbumCover;
                _obj.AlbumType = int.Parse(albumType);
                _obj.AlbumDescription = albumDescription;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.Albums.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.AlbumId.ToString();
            //}
            //catch
            //{
            //    return string.Empty;
            //}
        }
        public bool Edit(string AlbumID, string albumType, string AlbumName, string PamfletFile, string AlbumDate, string AlbumCover, string albumDescribtion, string ModifiedBy)
        {
            try
            {
                _obj = db.Albums.FirstOrDefault(lb => lb.AlbumId == new Guid(AlbumID));
                if (_obj != null)
                {
                    _obj.AlbumName = AlbumName;
                    _obj.AlbumType = int.Parse(albumType);
                    if (!string.IsNullOrEmpty(PamfletFile))
                        _obj.PamfletFile = PamfletFile;
                    _obj.AlbumDescription = albumDescribtion;
                    if (!string.IsNullOrEmpty(AlbumDate))
                        _obj.AlbumDate = DateTime.Parse(AlbumDate);
                    if (!string.IsNullOrEmpty(AlbumCover))
                        _obj.AlbumCover = AlbumCover;
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
        public bool Delete(string AlbumId, string ModifiedBy)
        {
            try
            {
                _obj = db.Albums.FirstOrDefault(ad => ad.AlbumId == new Guid(AlbumId));
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
        public bool Restore(string AlbumId, string ModifiedBy)
        {
            try
            {
                _obj = db.Albums.FirstOrDefault(ad => ad.AlbumId == new Guid(AlbumId));
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
                var query = (from P in db.Albums
                             where P.Active == bool.Parse(Active)
                             select new { P.AlbumId, P.CreatedBy,P.AlbumCover, P.CreatedOn, P.AlbumDate, P.AlbumName, P.ModifiedBy, P.ModifiedOn, P.Active, date = ConvertToDate(P.AlbumDate) }).Distinct();
                return query.ToDataTable("AlbumDate", "DESC");
            }
            catch
            {
                return null;
            }
        }
        public DataTable LoadWebsiteAlbums(string Active)
        {
            try
            {
                var query = (from P in db.Albums
                             where P.Active == bool.Parse(Active) && P.AlbumType==1
                             select new { P.AlbumId, P.CreatedBy, P.AlbumCover, P.CreatedOn, P.AlbumDate, P.AlbumName, P.ModifiedBy, P.ModifiedOn, P.Active, date = ConvertToDate(P.AlbumDate) }).Distinct();
                return query.ToDataTable("AlbumDate", "DESC");
            }
            catch
            {
                return null;
            }
        }
        public Album LoadByAlbumId(string AlbumId)
        {
            try
            {
                if (!string.IsNullOrEmpty(AlbumId))
                {
                    _obj = db.Albums.FirstOrDefault(lb => lb.AlbumId == new Guid(AlbumId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        string ConvertToDate(object date)
        {
            try
            {
                DateTime dt;
                if (DateTime.TryParse(date.ToString(), out dt))
                    return dt.ToString("yyyy/MM/dd");
                else
                    return string.Empty;
            }
            catch { return string.Empty; }
        }
        #endregion
    }
}
