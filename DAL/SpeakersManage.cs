using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class SpeakersManage
    {
        #region Variable
        dbDataContext db;
        Speaker _obj;
        #endregion
        public SpeakersManage()
        { db = new dbDataContext(); }
        public string Add(string SpeakerName, string ChurchName, string SpeakerImage, string Notes, string CreatedBy)
        {
            try
            {
                _obj = new Speaker();
                _obj.SpeakerId = Guid.NewGuid();
                _obj.SpeakerName = SpeakerName;
                _obj.ChurchName = ChurchName;
                _obj.SpeakerImage = SpeakerImage;
                _obj.Notes = Notes;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.Speakers.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.SpeakerId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string Id, string SpeakerName, string ChurchName, string SpeakerImage, string Notes, string ModifiedBy)
        {
            try
            {
                _obj = db.Speakers.FirstOrDefault(ad => ad.SpeakerId == new Guid(Id));
                if (_obj != null)
                {
                    _obj.SpeakerName = SpeakerName;
                    _obj.ChurchName = ChurchName;
                    if (!string.IsNullOrEmpty(SpeakerImage))
                        _obj.SpeakerImage = SpeakerImage;
                    _obj.Notes = Notes;
                    _obj.SpeakerName = SpeakerName;
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
                _obj = db.Speakers.FirstOrDefault(ad => ad.SpeakerId == new Guid(Id));
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
                _obj = db.Speakers.FirstOrDefault(ad => ad.SpeakerId == new Guid(Id));
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
        public Speaker LoadById(string NewId)
        {
            try
            {
                if (!string.IsNullOrEmpty(NewId))
                {
                    _obj = db.Speakers.FirstOrDefault(ad => ad.SpeakerId == new Guid(NewId));
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

            var query = (from x in db.Speakers
                         where x.Active == bool.Parse(Active)
                         select x).Distinct();
            return query.ToDataTable("SpeakerName", "ASC");
        }
    }
}
