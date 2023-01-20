using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class MagazineManage
    {
        #region Variable
        dbDataContext db;
        Magazine _obj;
        #endregion
        public MagazineManage()
        { db = new dbDataContext(); }
        public string Add(string MagazineTitle, string MagazineMonth, string MagazineYear, string MagazinePDF, string MagazineCover, string CreatedBy)
        {
            try
            {
                _obj = new Magazine();
                _obj.MagazineId = Guid.NewGuid();
                _obj.MagazineTitle = MagazineTitle;
                _obj.MagazineMonth = int.Parse(MagazineMonth);
                _obj.MagazineYear = int.Parse(MagazineYear);
                _obj.MagazinePDF = MagazinePDF;
                _obj.MagazineCover = MagazineCover;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.Magazines.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.MagazineId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string Id, string MagazineTitle, string MagazineMonth, string MagazineYear, string MagazinePDF, string MagazineCover, string ModifiedBy)
        {
            try
            {
                _obj = db.Magazines.FirstOrDefault(ad => ad.MagazineId == new Guid(Id));
                if (_obj != null)
                {
                    _obj.MagazineTitle = MagazineTitle;
                    _obj.MagazineMonth = int.Parse(MagazineMonth);
                    _obj.MagazineYear = int.Parse(MagazineYear);
                    if (!string.IsNullOrEmpty(MagazinePDF))
                        _obj.MagazinePDF = MagazinePDF;
                    if (!string.IsNullOrEmpty(MagazineCover))
                        _obj.MagazineCover = MagazineCover;
                    _obj.MagazinePDF = MagazinePDF;
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
                _obj = db.Magazines.FirstOrDefault(ad => ad.MagazineId == new Guid(Id));
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
                _obj = db.Magazines.FirstOrDefault(ad => ad.MagazineId == new Guid(Id));
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
        public Magazine LoadById(string NewId)
        {
            try
            {
                if (!string.IsNullOrEmpty(NewId))
                {
                    _obj = db.Magazines.FirstOrDefault(ad => ad.MagazineId == new Guid(NewId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool CheckIfExist(string Year, string Month)
        {
            try
            {
                return db.Magazines.Any(ad => ad.MagazineYear == int.Parse(Year) && ad.MagazineMonth == int.Parse(Month));
            }
            catch
            {
                return false;
            }
        }
        public DataTable LoadByDeleteState(string Active)
        {

            var query = (from x in db.Magazines
                         where x.Active == bool.Parse(Active)
                         select new { x.Active, x.CreatedBy, x.CreatedOn, x.MagazineTitle, x.MagazineCover, x.MagazineId, x.MagazineMonth, MagazineDate = GetDate(x.MagazineMonth, x.MagazineYear), x.MagazinePDF, x.MagazineYear, x.ModifiedBy, x.ModifiedOn }).Distinct();
            return query.ToDataTable("MagazineYear DESC,MagazineMonth", "DESC");
        }
        public string GetDate(object MonthNo, object Year)
        {
            try
            {
                switch (int.Parse(MonthNo.ToString()))
                {
                    case 1: return "يناير" + " " + Year;
                    case 2: return "فبراير" + " " + Year;
                    case 3: return "مارس" + " " + Year;
                    case 4: return "ابريل" + " " + Year;
                    case 5: return "مايو" + " " + Year;
                    case 6: return "يونيو" + " " + Year;
                    case 7: return "يوليو" + " " + Year;
                    case 8: return "اغسطس" + " " + Year;
                    case 9: return "سبتمبر" + " " + Year;
                    case 10: return "اكتوبر" + " " + Year;
                    case 11: return "نوفمبر" + " " + Year;
                    case 12: return "ديسمبر" + " " + Year;
                    default: return string.Empty;
                }
            }
            catch { return string.Empty; }
        }
    }
}
