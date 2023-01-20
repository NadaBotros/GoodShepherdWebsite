using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.Linq.SqlClient;

namespace DAL
{
    public class HomeManage
    {
        #region Variable
        dbDataContext db;
        #endregion
        public HomeManage()
        { db = new dbDataContext(); }
        public ViewAya Aya()
        {
            return db.ViewAyas.FirstOrDefault();
        }
        public ViewA2wal A2wal()
        {
            return db.ViewA2wals.FirstOrDefault();
        }
        public ViewSpeaker Speaker()
        {
            return db.ViewSpeakers.FirstOrDefault();
        }
        public List<ViewQuizWinner> QuizWinners()
        {
            return (from x in db.ViewQuizWinners select x).ToList();
        }
        public ViewSpiritualTraining SpiritualTrainings()
        {
            return db.ViewSpiritualTrainings.FirstOrDefault();
        }
        public List<ViewNewsGallery> NewsGalleries()
        {
            return (from x in db.ViewNewsGalleries select x).ToList();
        }
        public DataTable MeetingInHomes()
        {
            return (from x in db.ViewMeetingInHomes select x).ToDataTable("MeetingDate", "ASC");
        }
        public List<ViewMagazineInHome> MagazineInHome()
        {
            return (from x in db.ViewMagazineInHomes select x).ToList();
        }
        public List<procedureBirthDaysResult> BirthDays()
        {
            DateTime dt = GeneralMethods.GetEgyptTime();
            return (from x in db.procedureBirthDays(dt.Day, dt.Month) select x).ToList();
        }
        public List<procedureMarriageDateResult> MarriageDate()
        {
            DateTime dt = GeneralMethods.GetEgyptTime();
            return (from x in db.procedureMarriageDate(dt.Day, dt.Month) select x).ToList();
        }
        public ViewDailyMeetingWithGod DailyMeetingWithGods()
        {
            return db.ViewDailyMeetingWithGods.FirstOrDefault();
        }
        public ViewMagazineStory MagazineStories()
        {
            return db.ViewMagazineStories.FirstOrDefault();
        }
        public DataTable Activities()
        {
            var query = from x in db.Activities where x.Active == true && x.ActivityDate > DateTime.Now select new { ActivityDate = x.ActivityDate.Value.ToShortDateString(), x.DaysNo, x.ActivityTitle, x.ActivityPlace, x.ActivityId };
            return query.ToDataTable();
        }
    }
}
