using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Globalization;
namespace DAL
{
    public class LibraryFilesManage
    {
        #region Variable
        dbDataContext db;
        LibraryFile _obj;
        #endregion
        #region Method
        public LibraryFilesManage()
        { db = new dbDataContext(); }
        public string Add(string LibraryItemId, string FileTitle, string FileOwner, string FileDesc, string FileType, string FileDate, string FileName, string YoutubeLink, string CreatedBy)
        {
            try
            {
                if (!CheckVideoFound(YoutubeLink))
                {
                    _obj = new LibraryFile();
                    _obj.FileId = Guid.NewGuid();
                    _obj.LibraryItemId = new Guid(LibraryItemId);
                    _obj.FileTitle = FileTitle;
                    _obj.FileName = FileName;
                    _obj.YoutubeLink = YoutubeLink;
                    _obj.FileOwner = FileOwner;
                    _obj.FileType = FileType;
                    _obj.FileDesc = FileDesc;
                    DateTime dt;
                    if (DateTime.TryParseExact(FileDate, "d/M/yyyy", CultureInfo.InvariantCulture,
                                               DateTimeStyles.None, out dt))
                        _obj.FileDate = dt;
                    else
                        _obj.FileDate = null;
                    _obj.Active = true;
                    _obj.CreatedBy = new Guid(CreatedBy);
                    _obj.CreatedOn = DateTime.Now;
                    db.LibraryFiles.InsertOnSubmit(_obj);
                    db.SubmitChanges();
                    return _obj.FileId.ToString();
                }
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }

        public bool Edit(string FileId, string FileTitle, string FileOwner, string FileDesc, string FileType, string FileDate, string FileName, string YoutubeLink, string ModifiedBy)
        {
            try
            {
                _obj = db.LibraryFiles.FirstOrDefault(lb => lb.FileId == new Guid(FileId));
                if (_obj != null)
                {

                    _obj.FileTitle = FileTitle;
                    if (!string.IsNullOrEmpty(FileName))
                        _obj.FileName = FileName;
                    _obj.YoutubeLink = YoutubeLink;
                    DateTime dt;
                    if (DateTime.TryParseExact(FileDate, "d/M/yyyy", CultureInfo.InvariantCulture,
           DateTimeStyles.None, out dt))
                        _obj.FileDate = dt;
                    else
                        _obj.FileDate = null;
                    _obj.FileOwner = FileOwner;
                    _obj.FileType = FileType;
                    _obj.FileDesc = FileDesc;
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
        public bool Delete(string FileId, string ModifiedBy)
        {
            try
            {
                _obj = db.LibraryFiles.FirstOrDefault(ad => ad.FileId == new Guid(FileId));
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
        public bool Restore(string FileId, string ModifiedBy)
        {
            try
            {
                _obj = db.LibraryFiles.FirstOrDefault(ad => ad.FileId == new Guid(FileId));
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
        public DataTable LoadByDeleteState(string Active, string LibraryItemId)
        {
            try
            {
                var query = (from x in db.LibraryFiles
                             where x.Active == bool.Parse(Active) && x.LibraryItemId == new Guid(LibraryItemId) && !x.FileTitle.Contains("المحذوف") && !x.FileTitle.Contains("Delted")
                             select new { x.Active, x.CreatedBy, x.CreatedOn, FileDate = ConvertToDate(x.FileDate), x.FileId, x.FileTitle, x.ModifiedBy, x.YoutubeLink, x.ModifiedOn, x.FileName, x.FileOwner, x.FileDesc }).Distinct();
                return query.ToDataTable("CreatedOn", "DESC");
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
        public LibraryFile LoadById(string FileId)
        {
            try
            {
                if (!string.IsNullOrEmpty(FileId))
                {
                    _obj = db.LibraryFiles.FirstOrDefault(lb => lb.FileId == new Guid(FileId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool CheckVideoFound(string youtubeLink)
        {
            try
            {
                if (!string.IsNullOrEmpty(youtubeLink))
                {
                    return db.LibraryFiles.Any(lb => lb.YoutubeLink == youtubeLink && lb.Active.Value == true);
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        #endregion
    }
}
