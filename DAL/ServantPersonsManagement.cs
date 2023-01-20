using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.SqlClient;
namespace DAL
{
    public class ServantPersonsManagement
    {
        #region Variable
        dbDataContext db;
        Prg_ServantPerson _obj;
        #endregion
        #region Method
        public ServantPersonsManagement()
        { db = new dbDataContext(); }
        public string Add(string ServantId, string PersonId)
        {
            try
            {

                _obj = new Prg_ServantPerson();
                _obj.ServantPersonId = Guid.NewGuid();
                _obj.ServantId = new Guid(ServantId);
                _obj.PersonId = new Guid(PersonId);
                db.Prg_ServantPersons.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.PersonId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public string Add(string ServantId, List<string> PersonsId)
        {
            try
            {
                foreach (string PersonId in PersonsId)
                {
                    _obj = new Prg_ServantPerson();
                    _obj.ServantPersonId = Guid.NewGuid();
                    _obj.ServantId = new Guid(ServantId);
                    _obj.PersonId = new Guid(PersonId);
                    db.Prg_ServantPersons.InsertOnSubmit(_obj);
                }
                db.SubmitChanges();
                return _obj.PersonId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Delete(List<string> PersonsId)
        {
            try
            {
                var obj = db.Prg_ServantPersons.Where(ad => PersonsId.Contains(ad.PersonId.ToString()));
                foreach (Prg_ServantPerson p in obj)
                {
                    db.Prg_ServantPersons.DeleteOnSubmit(p);
                }
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(List<string> PersonsId, string ServantId)
        {
            try
            {
                var obj = db.Prg_ServantPersons.Where(ad => PersonsId.Contains(ad.PersonId.ToString()));
                foreach (Prg_ServantPerson p in obj)
                {
                    p.ServantId = new Guid(ServantId);
                }
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public DataTable ServantPersons(string ServantId)
        {
            var query = (from x in db.Prg_ServantPersons
                         where x.ServantId == new Guid(ServantId)
                         select new
                         {
                             x.Prg_Person.Prg_Family.Prg_Area.AreaName,
                             Mobile = x.Prg_Person.MobileNo1,
                             x.Prg_Person.PersonCode,
                             x.Prg_Person.PersonName,
                             x.PersonId,
                             x.Prg_Person.Relationship,
                             Studious = x.Prg_Person.Studious == 0 ? "لا يحضر" :
                                x.Prg_Person.Studious == 1 ? "يحضر بالكنيسة" : "يحضر بالقاعة"
                         }).Distinct();
            return query.ToDataTable("PersonName", "ASC");

        }
        public DataTable ServantPersonsReport(string ServantId)
        {
            PersonManagement obj = new PersonManagement();
            var query = (from x in db.Prg_ServantPersons
                         where x.ServantId == new Guid(ServantId) && x.Prg_Person.Active == true && x.Prg_Person.Prg_Family.Active == true
                         select new
                         {
                             Address = obj.ConvertToFullAddress(
                             x.Prg_Person.Prg_Family.Prg_Area.Prg_City.CityName,
                             x.Prg_Person.Prg_Family.Prg_Area.AreaName,
                              x.Prg_Person.Prg_Family.StreetName,
                             x.Prg_Person.Prg_Family.BuildingNextTo,
                             x.Prg_Person.Prg_Family.BuildingNo,
                             x.Prg_Person.Prg_Family.FloorNo,
                              x.Prg_Person.Prg_Family.FlatNo,
                               x.Prg_Person.Prg_Family.AddressNotes),
                             MarriageDate = ConvertToDate((x.Prg_Person.Relationship == "الزوج" || x.Prg_Person.Relationship == "الزوجة") ? x.Prg_Person.Prg_Family.MarriageDate : null),
                             BirthDate = ConvertToDate(x.Prg_Person.BirthDate),
                             x.Prg_Person.PersonCode,
                             Studious = x.Prg_Person.Studious == 0 ? "لا يحضر" :
                                x.Prg_Person.Studious == 1 ? "يحضر بالكنيسة" : "يحضر بالقاعة",
                             x.Prg_Person.Prg_Family.HomePhone,
                             x.Prg_Person.PersonName,
                             x.Prg_Person.Prg_Family.StreetName,
                             x.Prg_Person.Prg_Family.Prg_Area.AreaName,
                             x.Prg_Person.Prg_Family.BuildingNo,
                             Mobile = x.Prg_Person.MobileNo1
                         }).Distinct();
            return query.ToDataTable("PersonName", "ASC");

        }
        string ConvertToDate(object date)
        {
            if (date != null && !string.IsNullOrEmpty(date.ToString()))
                return DateTime.Parse(date.ToString()).ToString("dd/MM/yyyy");
            else
                return string.Empty;
        }
        #endregion
    }
}
