using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class VisitorManagement
    {
        dbDataContext db;
        public VisitorManagement()
        {
            db = new dbDataContext();
        }
        #region Methods
        public string Add(string CountryName, string CountryCode, string BrowserName, string BrowserVersion, string IPAddress, string ImagUrl, int VisitPage)
        {
            try
            {
                if (IPAddress != "::1")
                {
                    Visitor _Obj = new Visitor();
                    _Obj.VisitorId = Guid.NewGuid();
                    _Obj.CountryName = CountryName;
                    _Obj.CountryCode = CountryCode;
                    _Obj.BrowserName = BrowserName;
                    _Obj.BrowserVersion = BrowserVersion;
                    _Obj.IPAddress = IPAddress;                 
                    _Obj.FlagImage = ImagUrl;
                    _Obj.VisitPages = VisitPage;
                    _Obj.CreatedOn =DateTime.Now.AddHours(9);
                    db.Visitors.InsertOnSubmit(_Obj);
                    db.SubmitChanges();
                    return _Obj.VisitorId.ToString();
                }
                else
                    return string.Empty;
            }
            catch (Exception ex) { return null; }
        }
        public int IncVisit(string Id)
        {
            try
            {
                Visitor _Obj = db.Visitors.FirstOrDefault(ad => ad.VisitorId == new Guid(Id));
                if (_Obj != null)
                {
                    _Obj.VisitPages += 1;
                    db.SubmitChanges();
                    return (int)_Obj.VisitPages;
                }
                return 0;
            }
            catch (Exception ex) { return 0; }

        }
        public DataTable LoadAllVisitors()
        {
            return (from x in db.Visitors select x).ToDataTable("CreatedOn","DESC");
        }
        public IEnumerable<SPCountriesVisitorsResult> CountriesVisitorsNo()
        {
            var query = from x in db.SPCountriesVisitors() select x;
            return query;
        }
        public IEnumerable<SpCountriesVisitorsWithDatesResult> CountryDate(string DateFrom, string DateTo)
        {
            var query = from x in db.SpCountriesVisitorsWithDates(DateTime.Parse(DateFrom), DateTime.Parse(DateTo)) select x;
            return query;
        }
        public DataTable CountriesVisitorsNoWithDate(string DateFrom, string DateTo)
        {
            DateTime _Datefrom = DateTime.Now.AddYears(-1);
            if (string.IsNullOrEmpty(DateFrom))
                DateFrom = _Datefrom.ToString();
            if (string.IsNullOrEmpty(DateTo))
                DateTo = DateTime.Now.ToString();            
            var query = from x in db.SpCountriesVisitorsWithDates(DateTime.Parse(DateFrom), DateTime.Parse(DateTo)) select x;
            return query.AsQueryable().ToDataTable();
        }
        public IEnumerable<SPbrowserResult> BrowserUsedWithDate()
        {
            var query = from x in db.SPbrowser() select x;
            return query;
        }
        public int VisitorsNumber()
        {
            int query = (from c in db.Visitors select c).Count();
            return query;
        }
        public int PageVisitorNumber()
        {
            int query =(int)(from c in db.Visitors select c.VisitPages).Sum();
            return query;
        }
        public int VisitorsNumberLast30Days()
        {
            DateTime Now = DateTime.Now;
            DateTime Lasttherty = DateTime.Now.AddDays(-30);
            int query = (from c in db.Visitors
                         where c.CreatedOn >= Lasttherty && c.CreatedOn <= Now
                         select c).Count();
            return query;
        }
        public int MaxVisitYear()
        {
            try
            {
                int query = (from c in db.Visitors select c.CreatedOn).Max().Value.Year;
                return query;
            }
            catch { return DateTime.Now.Year; }
        }
        public int MinVisitYear()
        {
            try
            {
                int query = (from c in db.Visitors select c.CreatedOn).Min().Value.Year;
                return query;
            }
            catch { return DateTime.Now.Year; }
        }
        public int UniqueVisitorsNumber()
        {
            int UniqeVisitor = (from c in db.Visitors
                                select c.IPAddress).Distinct().Count();
            return UniqeVisitor;
        }
        public int UniqueVisitorsNumberLast30Days()
        {
            DateTime Now = DateTime.Now;
            DateTime Lasttherty = DateTime.Now.AddDays(-30);
            int UniqeVisitor = (from c in db.Visitors
                                where c.CreatedOn>=Lasttherty && c.CreatedOn<=Now
                                select c.IPAddress).Distinct().Count();
            return UniqeVisitor;
        }
        public string visitorMounth(string Year)
        {
            string _year = Year;
            if (string.IsNullOrEmpty(Year))
                _year = DateTime.Now.Year.ToString();
            string concatenat = string.Empty;
            for (int i = 1; i <= 12; i++)
            {
                int countVisitMonth = (from v in db.Visitors
                                       where v.CreatedOn.Value.Month == i && v.CreatedOn.Value.Year == int.Parse(_year)
                                       select v).Count();
                concatenat += countVisitMonth + ", ";
            }
            concatenat = concatenat.Remove(concatenat.Length - 2);
            return concatenat;
        }
        public string visitorsLast7Days()
        {
            string VisitorsStr = string.Empty;
            for (int i = 0; i <= 6; i++)
            {
                int VisitorDay = (from v in db.Visitors
                                  where v.CreatedOn.Value.Year == DateTime.Now.AddDays(-i).Year && v.CreatedOn.Value.Month == DateTime.Now.AddDays(-i).Month && v.CreatedOn.Value.Day == DateTime.Now.AddDays(-i).Day
                                  select v).Count();
                VisitorsStr += VisitorDay + ",  ";
            }
            VisitorsStr = VisitorsStr.Remove(VisitorsStr.Length - 2);
            return VisitorsStr;
        }
        public string visitorsLastMonth(string Year,string Month)
        {
            string VisitorsStr = string.Empty;
            string _year = Year;
            if (string.IsNullOrEmpty(Year))
                _year = DateTime.Now.Year.ToString();
            string _Month = Month;
            if (string.IsNullOrEmpty(Month))
                _Month = DateTime.Now.Month.ToString();

            int MonthDay = DateTime.DaysInMonth(int.Parse(_year), int.Parse(_Month));
            for (int i = 1; i <= MonthDay; i++)
            {
                int VisitorDay = (from v in db.Visitors
                                  where v.CreatedOn.Value.Year == int.Parse(_year) && v.CreatedOn.Value.Month == int.Parse(_Month) && v.CreatedOn.Value.Day ==i
                                  select v).Count();
                VisitorsStr += VisitorDay + ",  ";
            }
            VisitorsStr = VisitorsStr.Remove(VisitorsStr.Length - 2);
            return VisitorsStr;
        }

        #endregion

    }

}
