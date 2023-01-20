using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.SqlClient;
using System.Globalization;
namespace DAL
{
    public class MeetingManage
    {
        #region Variable
        dbDataContext db;
        Meeting _obj;
        #endregion
        #region Method
        public MeetingManage()
        { db = new dbDataContext(); }
        public string Add(string SpeakerId, string MeetingTitle, string MeetingDate, string SoundFile, string VideoUrl, string CreatedBy)
        {
            try
            {
                _obj = new Meeting();
                _obj.MeetingId = Guid.NewGuid();
                _obj.SpeakerId = new Guid(SpeakerId);
                _obj.MeetingTitle = MeetingTitle;
                _obj.SoundFile = SoundFile;
                _obj.VideoUrl = VideoUrl;
                DateTime dt;
                if (DateTime.TryParseExact(MeetingDate, "d/M/yyyy", CultureInfo.InvariantCulture,
       DateTimeStyles.None, out dt))
                    _obj.MeetingDate = dt;
                else
                    _obj.MeetingDate = null;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.Meetings.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.MeetingId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public void TransferMeeting()
        {
            var query = from x in db.TempSounds where x.SpeakerId != null select x;
            foreach (TempSound y in query)
            {
                if (y.SpeakerId != null)
                    Add(y.SpeakerId.ToString(), y.SoundTitle, y.SoundDate.Value.ToString("d/M/yyyy"), y.SoundFile, "", "540A303F-0310-41BC-BB34-121628D541EB");
            }

        }
        public bool Edit(string MeetingId, string SpeakerId, string MeetingTitle, string MeetingDate, string SoundFile, string VideoUrl, string ModifiedBy)
        {
            try
            {
                _obj = db.Meetings.FirstOrDefault(lb => lb.MeetingId == new Guid(MeetingId));
                if (_obj != null)
                {
                    _obj.SpeakerId = new Guid(SpeakerId);
                    _obj.MeetingTitle = MeetingTitle;
                    if (!string.IsNullOrEmpty(SoundFile))
                        _obj.SoundFile = SoundFile;
                    _obj.VideoUrl = VideoUrl;
                    DateTime dt;
                    if (DateTime.TryParseExact(MeetingDate, "d/M/yyyy", CultureInfo.InvariantCulture,
           DateTimeStyles.None, out dt))
                        _obj.MeetingDate = dt;
                    else
                        _obj.MeetingDate = null;
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
        public bool Delete(string MeetingId, string ModifiedBy)
        {
            try
            {
                _obj = db.Meetings.FirstOrDefault(ad => ad.MeetingId == new Guid(MeetingId));
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
        public bool Restore(string MeetingId, string ModifiedBy)
        {
            try
            {
                _obj = db.Meetings.FirstOrDefault(ad => ad.MeetingId == new Guid(MeetingId));
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
                var query = (from x in db.Meetings
                             where x.Active == bool.Parse(Active)
                             select new { x.Active, x.CreatedBy, x.CreatedOn, meetingdt = x.MeetingDate, MeetingDate = ConvertToDate(x.MeetingDate), x.MeetingId, x.MeetingTitle, x.ModifiedBy, x.ModifiedOn, x.SoundFile, x.Speaker.SpeakerName, x.Speaker.ChurchName, x.VideoUrl, x.Speaker.SpeakerImage }).Distinct();
                return query.ToDataTable("meetingdt", "DESC");
            }
            catch
            {
                return null;
            }
        }
        public DataTable LoadMeetings(string SpeakerId)
        {
            try
            {
                if (string.IsNullOrEmpty(SpeakerId))
                {
                    var query = (from x in db.Meetings
                                 where x.Active == true
                                 select new { x.Active, x.CreatedBy, x.CreatedOn, meetingdt = x.MeetingDate, MeetingDate = ConvertToDate(x.MeetingDate), x.MeetingId, x.MeetingTitle, x.ModifiedBy, x.ModifiedOn, x.SoundFile, x.Speaker.SpeakerName, x.Speaker.ChurchName, x.VideoUrl, x.Speaker.SpeakerImage }).Distinct();
                    return query.ToDataTable("meetingdt", "DESC");
                }
                else
                {
                    var query = (from x in db.Meetings
                                 where x.Active == true && x.SpeakerId == new Guid(SpeakerId)
                                 select new { x.Active, x.CreatedBy, x.CreatedOn, meetingdt = x.MeetingDate, MeetingDate = ConvertToDate(x.MeetingDate), x.MeetingId, x.MeetingTitle, x.ModifiedBy, x.ModifiedOn, x.SoundFile, x.Speaker.SpeakerName, x.Speaker.ChurchName, x.VideoUrl, x.Speaker.SpeakerImage }).Distinct();
                    return query.ToDataTable("meetingdt", "DESC");
                }
            }
            catch
            {
                return null;
            }
        }
        public DataTable LoadMeetingsSite(string SpeakerId)
        {
            try
            {
                if (string.IsNullOrEmpty(SpeakerId))
                {
                    var query = (from x in db.Meetings
                                 where x.Active == true && x.SoundFile != null && x.SoundFile.Length > 4
                                 select new { x.Active, x.CreatedBy, x.CreatedOn, meetingdt = x.MeetingDate, MeetingDate = ConvertToDate(x.MeetingDate), x.MeetingId, x.MeetingTitle, x.ModifiedBy, x.ModifiedOn, x.SoundFile, x.Speaker.SpeakerName, x.Speaker.ChurchName, x.VideoUrl, x.Speaker.SpeakerImage }).Distinct();
                    return query.ToDataTable("meetingdt", "DESC");
                }
                else
                {
                    var query = (from x in db.Meetings
                                 where x.Active == true && x.SpeakerId == new Guid(SpeakerId) && x.SoundFile != null && x.SoundFile.Length>4
                                 select new { x.Active, x.CreatedBy, x.CreatedOn, meetingdt = x.MeetingDate, MeetingDate = ConvertToDate(x.MeetingDate), x.MeetingId, x.MeetingTitle, x.ModifiedBy, x.ModifiedOn, x.SoundFile, x.Speaker.SpeakerName, x.Speaker.ChurchName, x.VideoUrl, x.Speaker.SpeakerImage }).Distinct();
                    return query.ToDataTable("meetingdt", "DESC");
                }
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
        public Meeting LoadById(string MeetingId)
        {
            try
            {
                if (!string.IsNullOrEmpty(MeetingId))
                {
                    _obj = db.Meetings.FirstOrDefault(lb => lb.MeetingId == new Guid(MeetingId));
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
