using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.SqlClient;
namespace DAL
{
    public class QuizWinnersManage
    {
        #region Variable
        dbDataContext db;
        QuizWinner _obj;
        #endregion
        #region Method
        public QuizWinnersManage()
        { db = new dbDataContext(); }
        public string Add(string QuizId, string PersonId, string WinnerNo, string WinnerTitle, string CreatedBy)
        {
            try
            {
                _obj = new QuizWinner();
                _obj.QuizWinnerId = Guid.NewGuid();
                _obj.QuizId = new Guid(QuizId);
                _obj.PersonId = new Guid(PersonId);
                _obj.WinnerNo = int.Parse(WinnerNo);
                _obj.WinnerTitle = WinnerTitle;              
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.QuizWinners.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.QuizWinnerId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string QuizWinnerId, string PersonId, string WinnerNo, string WinnerTitle, string ModifiedBy)
        {
            try
            {
                _obj = db.QuizWinners.FirstOrDefault(lb => lb.QuizWinnerId == new Guid(QuizWinnerId));
                if (_obj != null)
                {
                    _obj.PersonId = new Guid(PersonId);
                    _obj.WinnerNo = int.Parse(WinnerNo);
                    _obj.WinnerTitle = WinnerTitle;            
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
        public bool Delete(string QuizWinnerId, string ModifiedBy)
        {
            try
            {
                _obj = db.QuizWinners.FirstOrDefault(ad => ad.QuizWinnerId == new Guid(QuizWinnerId));
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
        public bool Restore(string QuizWinnerId, string ModifiedBy)
        {
            try
            {
                _obj = db.QuizWinners.FirstOrDefault(ad => ad.QuizWinnerId == new Guid(QuizWinnerId));
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
        public DataTable LoadByDeleteState(string QuizId, string Active)
        {
            try
            {
                var query = (from x in db.QuizWinners
                             where x.Active == bool.Parse(Active) && x.QuizId == new Guid(QuizId)
                             select new { x.Active,x.CreatedBy,x.CreatedOn,x.ModifiedBy,x.ModifiedOn,x.PersonId,x.Prg_Person.PersonName,x.Prg_Person.PersonCode,x.QuizId,x.QuizWinnerId,x.WinnerNo,x.WinnerTitle }).Distinct();
                return query.ToDataTable("WinnerNo ASC, PersonName", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public QuizWinner LoadById(string QuizWinnerId)
        {
            try
            {
                if (!string.IsNullOrEmpty(QuizWinnerId))
                {
                    _obj = db.QuizWinners.FirstOrDefault(lb => lb.QuizWinnerId == new Guid(QuizWinnerId));
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
