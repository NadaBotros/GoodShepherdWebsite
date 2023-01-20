using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class SpiritualTrainingManage
    {
        #region Variable
        dbDataContext db;
        SpiritualTraining _obj;
        #endregion
        public SpiritualTrainingManage()
        { db = new dbDataContext(); }
        public string Add(string SpiritualTrainingTitle, string SpiritualTrainingDesc, string CreatedBy)
        {
            try
            {
                _obj = new SpiritualTraining();
                _obj.SpiritualTrainingId = Guid.NewGuid();
                _obj.SpiritualTrainingTitle = SpiritualTrainingTitle;
                _obj.SpiritualTrainingDesc = SpiritualTrainingDesc;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.SpiritualTrainings.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.SpiritualTrainingId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string Id, string SpiritualTrainingTitle, string SpiritualTrainingDesc, string ModifiedBy)
        {
            try
            {
                _obj = db.SpiritualTrainings.FirstOrDefault(ad => ad.SpiritualTrainingId == new Guid(Id));
                if (_obj != null)
                {
                    _obj.SpiritualTrainingTitle = SpiritualTrainingTitle;
                    _obj.SpiritualTrainingDesc = SpiritualTrainingDesc;
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
                _obj = db.SpiritualTrainings.FirstOrDefault(ad => ad.SpiritualTrainingId == new Guid(Id));
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
                _obj = db.SpiritualTrainings.FirstOrDefault(ad => ad.SpiritualTrainingId == new Guid(Id));
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
        public SpiritualTraining LoadById(string NewId)
        {
            try
            {
                if (!string.IsNullOrEmpty(NewId))
                {
                    _obj = db.SpiritualTrainings.FirstOrDefault(ad => ad.SpiritualTrainingId == new Guid(NewId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }

        public DataTable LoadByDeleteState(string Active)
        {

            var query = (from x in db.SpiritualTrainings
                         where x.Active == bool.Parse(Active)
                         select x).Distinct();
            return query.ToDataTable("CreatedOn", "DESC");
        }
    }
}
