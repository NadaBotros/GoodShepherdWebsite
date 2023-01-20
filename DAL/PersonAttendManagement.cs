using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Globalization;
namespace DAL
{
    public class PersonAttendManagement
    {
        #region Variable
        dbDataContext db;
        Prg_PersonAttendance _obj;
        #endregion
        #region Method
        public PersonAttendManagement()
        { db = new dbDataContext(); }
        public string Add(List<string> PersonsId, string AttendDate, string CreatedBy)
        {
            try
            {
                foreach (string PersonId in PersonsId)
                {
                    _obj = new Prg_PersonAttendance();
                    _obj.AttendId = Guid.NewGuid();
                    _obj.PersonId = new Guid(PersonId);
                    _obj.AttendDate = DateTime.Parse(AttendDate);
                    _obj.CreatedBy = new Guid(CreatedBy);
                    _obj.CreatedOn = DateTime.Now;
                    db.Prg_PersonAttendances.InsertOnSubmit(_obj);
                }
                db.SubmitChanges();
                return _obj.PersonId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Delete(List<string> AttendsId)
        {
            try
            {
                foreach (string AttendId in AttendsId)
                {
                    var obj = db.Prg_PersonAttendances.Where(ad => ad.AttendId == new Guid(AttendId));
                    foreach (Prg_PersonAttendance p in obj)
                    {
                        db.Prg_PersonAttendances.DeleteOnSubmit(p);
                    }
                }
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<PersonAttendanceResult> PersonAttend(string PersonId)
        {
            var query = (from x in db.PersonAttendance(new Guid(PersonId))
                         select x).Distinct();
            return query.ToList();
        }
        public DataTable PersonsAttendDate(string AttendDate)
        {
            DateTime dt;
            if (DateTime.TryParseExact(AttendDate, "d/M/yyyy", CultureInfo.InvariantCulture,
           DateTimeStyles.None, out dt))
            {
                var query = (from x in db.Prg_PersonAttendances
                             where x.AttendDate == dt
                             select new { x.CreatedBy, x.AttendId, x.Prg_Person.Prg_Family.Prg_Area.AreaName, x.CreatedOn, Mobile = x.Prg_Person.MobileNo1, x.Prg_Person.PersonCode, x.Prg_Person.PersonName, AttendDate = x.AttendDate.ToShortDateString() }).Distinct();
                return query.ToDataTable("PersonName", "ASC");
            }
            return null;

        }
        public DataTable PersonsAttendReport(string ServantId, string Date, string AttendType)
        {
            Guid id = Guid.NewGuid();
            if (!string.IsNullOrEmpty(ServantId))
                id = new Guid(ServantId);
            DateTime dt;
            if (DateTime.TryParse(Date, out dt))
            {
                if (AttendType == "True")
                {
                    var query = (from x in db.Prg_PersonAttendances
                                 where x.AttendDate == dt && (string.IsNullOrEmpty(ServantId) || db.Prg_ServantPersons.Any(y => y.ServantId == id && y.PersonId == x.Prg_Person.PersonId))
                                 select new { Mobile = x.Prg_Person.MobileNo1, x.Prg_Person.Email, x.Prg_Person.PersonCode, x.Prg_Person.PersonName, x.Prg_Person.Prg_Family.HomePhone }).Distinct();
                    return query.ToDataTable("PersonName", "ASC");
                }
                else
                {
                    var query = (from x in db.Prg_Persons
                                 where x.Active == true && x.Prg_Family.Active == true && x.Studious > 0 &&
                              !db.Prg_PersonAttendances.Any(y => y.PersonId == x.PersonId && y.AttendDate == dt) &&
                                 (string.IsNullOrEmpty(ServantId) || db.Prg_ServantPersons.Any(y => y.ServantId == id && y.PersonId == x.PersonId))
                                 select new { Mobile = x.MobileNo1, x.PersonCode, x.Email, x.PersonName, x.Prg_Family.HomePhone }).Distinct();
                    return query.ToDataTable("PersonName", "ASC");
                }
            }
            return null;

        }
        public List<string> PersonsAttendReportAsList(string ServantId, string Date, string AttendType)
        {
            Guid id = Guid.NewGuid();
            if (!string.IsNullOrEmpty(ServantId))
                id = new Guid(ServantId);
            DateTime dt;
            if (DateTime.TryParse(Date, out dt))
            {
                if (AttendType == "True")
                {
                    var query = (from x in db.Prg_PersonAttendances
                                 where x.AttendDate == dt && (string.IsNullOrEmpty(ServantId) || db.Prg_ServantPersons.Any(y => y.ServantId == id && y.PersonId == x.Prg_Person.PersonId))
                                 select x.PersonId.ToString()).Distinct();
                    return query.ToList();
                }
                else
                {
                    var query = (from x in db.Prg_Persons
                                 where x.Active == true && x.Prg_Family.Active == true && x.Studious>0 &&
                              !db.Prg_PersonAttendances.Any(y => y.PersonId == x.PersonId && y.AttendDate == dt) &&
                                 (string.IsNullOrEmpty(ServantId) || db.Prg_ServantPersons.Any(y => y.ServantId == id && y.PersonId == x.PersonId))
                                 select x.PersonId.ToString()).Distinct();
                    return query.ToList();
                }
            }
            return null;

        }
        public DataTable AttendDates()
        {
            // try
            {
                var query = (from x in db.Prg_PersonAttendances select new { date = x.AttendDate, AttendDate = x.AttendDate.ToShortDateString() }).Distinct();
                return query.ToDataTable("date", "DESC");
            }
            // catch { return null; }
        }

        public DataTable SearchNotAddedByCodes(string SearchCodes, string AttendDate)
        {
            try
            {
                DateTime dt;
                if (DateTime.TryParseExact(AttendDate, "d/M/yyyy", CultureInfo.InvariantCulture,
           DateTimeStyles.None, out dt))
                {
                    List<string> codesList = GeneralMethods.ConvertToPersonodes(SearchCodes);
                    var query = (from p in db.Prg_Persons
                                 where p.Active == true && p.Prg_Family.Active == true && p.Studious >0 && codesList.Contains(p.PersonCode) && !p.Prg_PersonAttendances.Any(y => y.PersonId == p.PersonId && y.AttendDate == dt)
                                 select new { p.PersonCode, p.PersonName, p.PersonId, Mobile = p.MobileNo1, p.Prg_Family.Prg_Area.AreaName }).Distinct();
                    return query.ToDataTable("PersonName", "ASC");
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public DataTable SearchByCodes(string SearchCodes)
        {
            try
            {
                List<string> codesList = GeneralMethods.ConvertToPersonodes(SearchCodes);
                var query = (from p in db.Prg_Persons
                             where p.Active == true && p.Prg_Family.Active == true && p.Studious >0 && codesList.Contains(p.PersonCode)
                             select new { p.PersonCode, p.PersonName, p.PersonId }).Distinct();
                return query.ToDataTable("PersonName", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public DataTable SearchGeneral(string SearchWord)
        {
            try
            {
                SearchWord = GeneralMethods.ConvertToSearchWord(SearchWord);
                var query = (from p in db.Prg_Persons
                             where p.Active == true && p.Prg_Family.Active == true && p.Studious >0 && (SqlMethods.Like(p.PersonName, SearchWord) || SqlMethods.Like(p.MobileNo1, SearchWord) || SqlMethods.Like(p.PersonCode, SearchWord))
                             select new { p.PersonCode, p.PersonName, p.PersonId }).Distinct();
                return query.ToDataTable("PersonName", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public DataTable SearchGeneralNotAdded(string SearchWord, string AttendDate)
        {
            //try
            //{
            DateTime dt;
            if (DateTime.TryParseExact(AttendDate, "d/M/yyyy", CultureInfo.InvariantCulture,
           DateTimeStyles.None, out dt))
            {
                SearchWord = GeneralMethods.ConvertToSearchWord(SearchWord);
                var query = (from p in db.Prg_Persons
                             where 
                             p.Active == true && p.Prg_Family.Active == true 
                             && p.Studious >0 && !(p.Prg_PersonAttendances.Any(y => y.PersonId == p.PersonId && y.AttendDate == dt))
                             && (SqlMethods.Like(p.PersonName, SearchWord) 
                             || SqlMethods.Like(p.MobileNo1, SearchWord) 
                             || SqlMethods.Like(p.PersonCode, SearchWord))

                             select new { p.PersonCode, p.PersonName, p.PersonId, Mobile = p.MobileNo1, p.Prg_Family.Prg_Area.AreaName }).Distinct();

                return query.ToDataTable("PersonName", "ASC");
            }
            return null;
            //}
            //catch
            //{
            //    return null;
            //}
        }
        #endregion
    }

}
