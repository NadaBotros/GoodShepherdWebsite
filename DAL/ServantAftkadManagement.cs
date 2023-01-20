using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.SqlClient;
namespace DAL
{
    public class ServantAftkadManagement
    {
        #region Variable
        dbDataContext db;
        Prg_Servant _obj;
        #endregion
        #region Method
        public ServantAftkadManagement()
        { db = new dbDataContext(); }
        public string Add(string PersonId, string Services, string IsServantAftkad, string CreatedBy)
        {
            try
            {
                _obj = new Prg_Servant();
                _obj.ServantId = Guid.NewGuid();
                _obj.PersonId = new Guid(PersonId);
                _obj.Services = Services;
                _obj.IsServantAftkad = bool.Parse(IsServantAftkad);
                db.Prg_Servants.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.PersonId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string ServantId, string PersonId, string Services, string IsServantAftkad, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_Servants.FirstOrDefault(lb => lb.ServantId == new Guid(ServantId));
                if (_obj != null)
                {
                    _obj.PersonId = new Guid(PersonId);
                    _obj.Services = Services;
                    _obj.IsServantAftkad = bool.Parse(IsServantAftkad);
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
        public Prg_Servant LoadById(string servantId)
        {
            try
            {
                var obj = db.Prg_Servants.FirstOrDefault(ad => ad.ServantId == new Guid(servantId));

                return obj;
            }
            catch
            {
                return null;
            }
        }
        public bool Delete(string servantId, string ModifiedBy)
        {
            try
            {
                var obj = db.Prg_Servants.Where(ad => ad.ServantId == new Guid(servantId));
                foreach (Prg_Servant p in obj)
                {
                    db.Prg_Servants.DeleteOnSubmit(p);
                }
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public DataTable LoadAllList()
        {
            return (from x in db.Prg_Servants select new { x.Prg_Person.PersonName, x.ServantId }).ToDataTable("PersonName", "ASC");
        }
        public DataTable LoadAllList(bool IsServantAftkad)
        {
            return (from x in db.Prg_Servants where x.IsServantAftkad == IsServantAftkad select new { x.Prg_Person.PersonName, x.ServantId }).ToDataTable("PersonName", "ASC");
        }
        public DataTable LoadAll()
        {
            return (from x in db.Prg_Servants select new {x.Services,x.CreatedBy,x.CreatedOn,x.ModifiedBy,x.ModifiedOn, x.Prg_Person.PersonName, x.Prg_Person.Email, Mobile = x.Prg_Person.MobileNo1, x.ServantId }).ToDataTable("PersonName", "ASC");
        }
        public bool IfServantExists(string PersonId)
        {
            _obj = db.Prg_Servants.FirstOrDefault(x => x.PersonId == new Guid(PersonId));
            if (_obj == null)
                return false;
            else
                return true;
        }
        public DataTable SearchServant(string SearchWord)
        {
            string SearchWordAr = GeneralMethods.ConvertToSearchWord(SearchWord);
            var query = (from x in db.Prg_Persons where x.Prg_Family.Active == true && x.Active == true && !x.Prg_Servants.Any(y => y.PersonId == x.PersonId) && (x.MobileNo1.Contains(SearchWord) || x.MobileNo2.Contains(SearchWord) || x.PersonCode.Contains(SearchWord) || x.NationalID.Contains(SearchWord) || SqlMethods.Like(x.PersonName, SearchWordAr) || SqlMethods.Like(x.Job, SearchWordAr)) orderby x.PersonName select new { x.PersonName, x.PersonId });
            return query.ToDataTable();

        }
        public int ServantPersonsCount(string ServantId)
        {
            var query = (from x in db.Prg_ServantPersons
                         where x.ServantId == new Guid(ServantId)
                         select new { x.PersonId }).Distinct();
            return query.Count();

        }
        #endregion
    }
}
