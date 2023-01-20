using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.SqlClient;
namespace DAL
{
    public class PersonHobbiesManagement
    {
        #region Variable
        dbDataContext db;
        Prg_PersonHobby _obj;
        #endregion
        #region Method
        public PersonHobbiesManagement()
        { db = new dbDataContext(); }
        public string Add(string PersonId,List<string> HobbiesId, string CreatedBy)
        {
            try
            {
                foreach (string HobbyId in HobbiesId)
                {
                    _obj = new Prg_PersonHobby();
                    _obj.PersonHobbyId = Guid.NewGuid();
                    _obj.HobbyId = new Guid(HobbyId);
                    _obj.PersonId = new Guid(PersonId);
                    db.Prg_PersonHobbies.InsertOnSubmit(_obj);
                }
                db.SubmitChanges();
                return _obj.HobbyId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Delete(string PersonId, string ModifiedBy)
        {
            try
            {
                var obj = db.Prg_PersonHobbies.Where(ad => ad.PersonId == new Guid(PersonId));
                foreach (Prg_PersonHobby p in obj)
                {
                    db.Prg_PersonHobbies.DeleteOnSubmit(p);
                }
                db.SubmitChanges();
                return true;
            }
            catch
            {
                return false;
            }
        }
        public List<string> LoadPersonIds(string PersonId)
        {
            try
            {
                var query = (from P in db.Prg_PersonHobbies
                             where P.PersonId == new Guid(PersonId)
                             select P.HobbyId.ToString().ToLower()).Distinct();
                return query.ToList();
            }
            catch
            {
                return null;
            }
        }
        public List<string> LoadPersonNames(string PersonId)
        {
            try
            {
                var query = (from P in db.Prg_PersonHobbies
                             where P.PersonId == new Guid(PersonId)
                             select P.Prg_Hobby.HobbyName).Distinct();
                return query.ToList();
            }
            catch
            {
                return null;
            }
        }
        #endregion
    }
}
