using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Data.Linq.SqlClient;
using System.Globalization;
namespace DAL
{
    public class FamilyManagement
    {
        #region Variable
        dbDataContext db;
        Prg_Family _obj;
        #endregion
        #region Method
        public FamilyManagement()
        { db = new dbDataContext(); }
        public string Add(string CreatedBy)
        {
            try
            {
                _obj = new Prg_Family();
                _obj.FamilyId = Guid.NewGuid();
                _obj.FamilyCode = GetFamilyCode();
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.Prg_Families.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.FamilyId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public string GetFamilyCode()
        {
            try
            {
                string MaxCode = db.Prg_Families.Select(x => x.FamilyCode).Max();
                if (!string.IsNullOrEmpty(MaxCode))
                {
                    return (int.Parse(MaxCode) + 1).ToString("00000");
                }
                else
                    return "00001";
            }
            catch { return "00001"; }
        }
        public bool ChangeResponsable(string FamilyId, string PersonId, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_Families.FirstOrDefault(lb => lb.FamilyId == new Guid(FamilyId));
                if (_obj != null)
                {
                    _obj.ResponsableId = new Guid(PersonId);
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
        public bool Edit(string FamilyId, string ResponsableId, string AddressNotes, string AreaId, string BuildingNextTo, string BuildingNo, string FlatNo, string FloorNo, string HomePhone, string StreetName, string MarriageDate, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_Families.FirstOrDefault(lb => lb.FamilyId == new Guid(FamilyId));
                if (_obj != null)
                {
                    _obj.AddressNotes = AddressNotes;
                    _obj.ResponsableId = new Guid(ResponsableId);
                    _obj.AreaId = new Guid(AreaId);
                    _obj.BuildingNextTo = BuildingNextTo;
                    _obj.BuildingNo = BuildingNo;
                    _obj.FlatNo = FlatNo;
                    _obj.FloorNo = FloorNo;
                    _obj.HomePhone = HomePhone;
                    DateTime md;
                    if (DateTime.TryParseExact(MarriageDate, "d/M/yyyy", CultureInfo.InvariantCulture,
           DateTimeStyles.None, out md))
                        _obj.MarriageDate = md;
                    else
                        _obj.MarriageDate = null;
                    _obj.StreetName = StreetName;
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
        public bool Delete(string FamilyId, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_Families.FirstOrDefault(ad => ad.FamilyId == new Guid(FamilyId));
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
        public bool Restore(string FamilyId, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_Families.FirstOrDefault(ad => ad.FamilyId == new Guid(FamilyId));
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
                var query = (from P in db.Prg_Families
                             where P.Active == bool.Parse(Active)
                             select new { P.FamilyId, P.Active, P.CreatedBy, P.CreatedOn, P.ModifiedBy, P.ModifiedOn, P.Prg_Person.PersonName, MobileNo = P.Prg_Person.MobileNo1, P.Prg_Area.AreaName, P.HomePhone }).Distinct();
                return query.ToDataTable("PersonName", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public DataTable LoadByDeleteState(string Active, string SearchWord)
        {
            try
            {
                // ** added to search wth National ID 
                var query = (from P in db.Prg_Families
                             where P.Active == bool.Parse(Active) && (string.IsNullOrEmpty(SearchWord) || P.FamilyCode == SearchWord || P.HomePhone.Contains(SearchWord) || P.StreetName.Contains(SearchWord) || P.Prg_Persons.Any(x => x.PersonName.Contains(SearchWord) || x.MobileNo1.Contains(SearchWord) || x.NationalID.Contains(SearchWord)))
                             select new { P.FamilyId, P.Active, P.CreatedBy, P.CreatedOn, P.ModifiedBy, P.ModifiedOn, P.Prg_Person.PersonName, MobileNo = P.Prg_Person.MobileNo1, P.Prg_Area.AreaName, P.HomePhone }).Distinct();
                return query.ToDataTable("PersonName", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public Prg_Family LoadById(string FamilyId)
        {
            try
            {
                if (!string.IsNullOrEmpty(FamilyId))
                {
                    _obj = db.Prg_Families.FirstOrDefault(lb => lb.FamilyId == new Guid(FamilyId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public string[] StreetsName(string StreetSearch)
        {
            try
            {
                StreetSearch = GeneralMethods.ConvertToSearchWord(StreetSearch);
                var query = (from P in db.Prg_Families
                             where P.Active == true && SqlMethods.Like(P.StreetName, StreetSearch)
                             select P.StreetName).Take(10).Distinct();
                return query.ToArray();
            }
            catch
            {
                return null;
            }
        }
        #endregion

    }
}
