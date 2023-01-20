using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
namespace DAL
{
    public class ReportManagement
    {
        #region Variable
        dbDataContext db;
        Admin _obj;
        #endregion
        public ReportManagement()
        { db = new dbDataContext(); }
        public List<PersonMainInfo> ServantPersons(string ServantId)
        {
            if (string.IsNullOrEmpty(ServantId))
            {
                return (from x in db.Prg_Persons
                        where x.Active == true && x.Prg_Family.Active == true && x.Studious > 0 orderby x.PersonName select new PersonMainInfo { PersonCode = x.PersonCode, PersonId = x.PersonId, PersonName = x.PersonName, MobileNo1 = x.MobileNo1,MobileNo2=x.MobileNo2 , HomePhone = x.Prg_Family.HomePhone}).ToList();
            }
            else
            {
                return (from x in db.Prg_ServantPersons where x.ServantId == new Guid(ServantId) orderby x.Prg_Person.PersonName select new PersonMainInfo {
                    PersonCode = x.Prg_Person.PersonCode, 
                    PersonId = x.PersonId, 
                    PersonName = x.Prg_Person.PersonName,
                    MobileNo1 = x.Prg_Person.MobileNo1,
                    MobileNo2=x.Prg_Person.MobileNo2,
                    HomePhone = x.Prg_Person.Prg_Family.HomePhone }).ToList();
            }
        }
        public List<Prg_PersonAttendance> PersonsAttendanceBetweenDates(List<string> PersonsId, string DateFrom, string DateTo)
        {
            DateTime dtFrom, dtTo;
            if (PersonsId.Count() > 0 && DateTime.TryParse(DateFrom, out dtFrom) && DateTime.TryParse(DateTo, out dtTo))
            {
                return (from x in db.Prg_PersonAttendances
                        where PersonsId.Contains(x.PersonId.ToString()) && x.AttendDate >= dtFrom && x.AttendDate <= dtTo
                        select x).ToList();
            }
            else
                return null;
        }
        public List<DateTime> MeetingsBetweenDates(string DateFrom, string DateTo)
        {
            DateTime dtFrom, dtTo;
            if (DateTime.TryParse(DateFrom, out dtFrom) && DateTime.TryParse(DateTo, out dtTo))
            {
                return (from x in db.Prg_PersonAttendances where x.AttendDate >= dtFrom && x.AttendDate <= dtTo orderby x.AttendDate select x.AttendDate).ToList();
            }
            else
                return null;
        }
        public DataTable GetByPersonsIds(List<string> Ids, List<string> cols, string orderby)
        {
            //Ids.Contains(x.PersonId.ToString())
            PersonManagement obj = new PersonManagement();
            var query = (from x in db.Prg_Persons
                         where Ids.Contains(x.PersonId.ToString()) && x.Active == true && x.Prg_Family.Active == true
                         select new
                         {
                             x.PersonId,
                             x.PersonName,
                             BirthDate = ConvertToDate(x.BirthDate),
                             x.BloodType,
                             x.Email,
                             x.FaceBook,
                             x.FatherName,
                             x.Job,
                             x.MaritalStatus,
                             x.MobileNo1,
                             x.NationalID,
                             x.PersonCode,
                             x.Prg_Family.HomePhone,
                             x.Relationship,
                             x.Skype,
                             CreatedByName = x.Admin.UserName,
                             Studious = x.Studious == 0 ? "لا يحضر" :
                                x.Studious == 1 ? "يحضر بالكنيسة" : "يحضر بالقاعة",
                             MarriageDate = ConvertToDate((x.Relationship == "الزوج" || x.Relationship == "الزوجة") ? x.Prg_Family.MarriageDate : null),
                             CreatedOn = ConvertToDate(x.CreatedOn),
                             CityName = x.Prg_Family.Prg_Area.Prg_City.CityName,
                             AreaName = x.Prg_Family.Prg_Area.AreaName,
                             Address = obj.ConvertToFullAddress(string.Empty, string.Empty, x.Prg_Family.StreetName, x.Prg_Family.BuildingNextTo, x.Prg_Family.BuildingNo, x.Prg_Family.FloorNo, x.Prg_Family.FlatNo, x.Prg_Family.AddressNotes),
                         });
            return query.ToDataTable(orderby, "ASC");




        }
        public List<Prg_Person> GetByPersonsIds(List<string> Ids)
        {
            var obj = new PersonManagement();
            var query = (from x in db.Prg_Persons
                         where Ids.Contains(x.PersonId.ToString()) && x.Active == true && x.Prg_Family.Active == true
                         select x);
            return query.ToList();

        }


        string ConvertToDate(object date)
        {
            if (date != null && !string.IsNullOrEmpty(date.ToString()))
                return DateTime.Parse(date.ToString()).ToString("dd/MM/yyyy");
            else
                return string.Empty;
        }


        public List<PersonMainInformation> AllPeople()
        {
            PersonManagement obj = new PersonManagement();
            //Get All people in the DB
            var query1 = (from x in db.Prg_Persons
                          where x.Active == true
                          select new PersonMainInformation
                          {
                              PersonCode = x.PersonCode,
                              PersonId = x.PersonId,
                              PersonName = x.PersonName,
                              MobileNo1 = x.MobileNo1,
                              FatherName = x.FatherName,
                              BirthDate = x.BirthDate.ToString(),
                              BloodType = x.BloodType,
                              Email = x.Email,
                              FaceBook = x.FaceBook,
                              Job = x.Job,
                              MaritalStatus = x.MaritalStatus,
                              NationalID = x.NationalID,
                              HomePhone = x.Prg_Family.HomePhone,
                              Relationship = x.Relationship,
                              CreatedByName = x.Admin.UserName,
                              Skype = x.Skype,

                              Studious = x.Studious == 0 ? "لا يحضر" :
                                x.Studious == 1 ? "يحضر بالكنيسة" : "يحضر بالقاعة",
                              MarriageDate = ConvertToDate((x.Relationship == "الزوج" || x.Relationship == "الزوجة") ? x.Prg_Family.MarriageDate : null),
                              CreatedOn = ConvertToDate(x.CreatedOn),
                              CityName = x.Prg_Family.Prg_Area.Prg_City.CityName,
                              AreaName = x.Prg_Family.Prg_Area.AreaName,
                              Address = obj.ConvertToFullAddress(string.Empty, string.Empty, x.Prg_Family.StreetName, x.Prg_Family.BuildingNextTo, x.Prg_Family.BuildingNo, x.Prg_Family.FloorNo, x.Prg_Family.FlatNo, x.Prg_Family.AddressNotes),


                          });

            return query1.ToList();

        }



        public DataTable PeopleWithNoServant(List<string> Ids, List<string> cols, string orderby)
        {
            PersonManagement obj = new PersonManagement();
            //Get All people in the DB
            var query1 = (from x in db.Prg_Persons
                          where  x.Active == true
                          select new
                          {
                              x.PersonId,
                              x.PersonName,
                              BirthDate = ConvertToDate(x.BirthDate),
                              x.BloodType,
                              x.Email,
                              x.FaceBook,
                              x.FatherName,
                              x.Job,
                              x.MaritalStatus,
                              x.MobileNo1,
                              x.NationalID,
                              x.PersonCode,
                              x.Prg_Family.HomePhone,
                              x.Relationship,
                              x.Skype,
                              CreatedByName = x.Admin.UserName,
                              Studious = x.Studious == 0 ? "لا يحضر" :
                                x.Studious == 1 ? "يحضر بالكنيسة" : "يحضر بالقاعة",
                              MarriageDate = ConvertToDate((x.Relationship == "الزوج" || x.Relationship == "الزوجة") ? x.Prg_Family.MarriageDate : null),
                              CreatedOn = ConvertToDate(x.CreatedOn),
                              CityName = x.Prg_Family.Prg_Area.Prg_City.CityName,
                              AreaName = x.Prg_Family.Prg_Area.AreaName,
                              Address = obj.ConvertToFullAddress(string.Empty, string.Empty, x.Prg_Family.StreetName, x.Prg_Family.BuildingNextTo, x.Prg_Family.BuildingNo, x.Prg_Family.FloorNo, x.Prg_Family.FlatNo, x.Prg_Family.AddressNotes),



                          });

            //Get All servants
            var query2 = (from pp in db.Prg_Persons
                          join psp in db.Prg_Servants on pp.PersonId equals psp.PersonId
                          where  pp.Active == true
                          select new
                          {
                              pp.PersonId,
                              pp.PersonName,
                              BirthDate = ConvertToDate(pp.BirthDate),
                              pp.BloodType,
                              pp.Email,
                              pp.FaceBook,
                              pp.FatherName,
                              pp.Job,
                              pp.MaritalStatus,
                              pp.MobileNo1,
                              pp.NationalID,
                              pp.PersonCode,
                              pp.Prg_Family.HomePhone,
                              pp.Relationship,
                              pp.Skype,
                              CreatedByName = pp.Admin.UserName,
                              Studious = pp.Studious == 0 ? "لا يحضر" :
                                pp.Studious == 1 ? "يحضر بالكنيسة" : "يحضر بالقاعة",
                              MarriageDate = ConvertToDate((pp.Relationship == "الزوج" || pp.Relationship == "الزوجة") ? pp.Prg_Family.MarriageDate : null),
                              CreatedOn = ConvertToDate(pp.CreatedOn),
                              CityName = pp.Prg_Family.Prg_Area.Prg_City.CityName,
                              AreaName = pp.Prg_Family.Prg_Area.AreaName,
                              Address = obj.ConvertToFullAddress(string.Empty, string.Empty, pp.Prg_Family.StreetName, pp.Prg_Family.BuildingNextTo, pp.Prg_Family.BuildingNo, pp.Prg_Family.FloorNo, pp.Prg_Family.FlatNo, pp.Prg_Family.AddressNotes),


                          });
            //Get All Makhdomenn that have Servants
            var query3 = (from pp in db.Prg_Persons
                          join psp in db.Prg_ServantPersons on pp.PersonId equals psp.PersonId
                          where  pp.Active == true
                          select new
                          {
                              pp.PersonId,
                              pp.PersonName,
                              BirthDate = ConvertToDate(pp.BirthDate),
                              pp.BloodType,
                              pp.Email,
                              pp.FaceBook,
                              pp.FatherName,
                              pp.Job,
                              pp.MaritalStatus,
                              pp.MobileNo1,
                              pp.NationalID,
                              pp.PersonCode,
                              pp.Prg_Family.HomePhone,
                              pp.Relationship,
                              pp.Skype,
                              CreatedByName = pp.Admin.UserName,
                              Studious = pp.Studious == 0 ? "لا يحضر" :
                                pp.Studious == 1 ? "يحضر بالكنيسة" : "يحضر بالقاعة",
                              MarriageDate = ConvertToDate((pp.Relationship == "الزوج" || pp.Relationship == "الزوجة") ? pp.Prg_Family.MarriageDate : null),
                              CreatedOn = ConvertToDate(pp.CreatedOn),
                              CityName = pp.Prg_Family.Prg_Area.Prg_City.CityName,
                              AreaName = pp.Prg_Family.Prg_Area.AreaName,
                              Address = obj.ConvertToFullAddress(string.Empty, string.Empty, pp.Prg_Family.StreetName, pp.Prg_Family.BuildingNextTo, pp.Prg_Family.BuildingNo, pp.Prg_Family.FloorNo, pp.Prg_Family.FlatNo, pp.Prg_Family.AddressNotes),


                          });
            var Result1 = query1.Where(x => !query2.Any(y => y.PersonId == x.PersonId) && !query3.Any(z => z.PersonId == x.PersonId) && Ids.Contains(x.PersonId.ToString()));
         


            return Result1.ToDataTable(orderby, "ASC");
        }


        public DataTable FilterPeopleHaveServants(List<string> Ids, List<string> cols, string orderby)
        {
            //Ids.Contains(x.PersonId.ToString())
            PersonManagement obj = new PersonManagement();
            var query = (from pp in db.Prg_Persons
                         join psp in db.Prg_ServantPersons on pp.PersonId equals psp.PersonId
                         where pp.Active == true
                         select new
                         {
                             pp.PersonId,
                             pp.PersonName,
                             BirthDate = ConvertToDate(pp.BirthDate),
                             pp.BloodType,
                             pp.Email,
                             pp.FaceBook,
                             pp.FatherName,
                             pp.Job,
                             pp.MaritalStatus,
                             pp.MobileNo1,
                             pp.NationalID,
                             pp.PersonCode,
                             pp.Prg_Family.HomePhone,
                             pp.Relationship,
                             pp.Skype,
                             CreatedByName = pp.Admin.UserName,
                             Studious = pp.Studious == 0 ? "لا يحضر" :
                                pp.Studious == 1 ? "يحضر بالكنيسة" : "يحضر بالقاعة",
                             MarriageDate = ConvertToDate((pp.Relationship == "الزوج" || pp.Relationship == "الزوجة") ? pp.Prg_Family.MarriageDate : null),
                             CreatedOn = ConvertToDate(pp.CreatedOn),
                             CityName = pp.Prg_Family.Prg_Area.Prg_City.CityName,
                             AreaName = pp.Prg_Family.Prg_Area.AreaName,
                             Address = obj.ConvertToFullAddress(string.Empty, string.Empty, pp.Prg_Family.StreetName, pp.Prg_Family.BuildingNextTo, pp.Prg_Family.BuildingNo, pp.Prg_Family.FloorNo, pp.Prg_Family.FlatNo, pp.Prg_Family.AddressNotes),
                         });
            return query.ToDataTable(orderby, "ASC");




        }
    }
        public class PersonMainInfo
        {
            public string PersonName { set; get; }
            public Guid PersonId { set; get; }
            public string PersonCode { get; set; }
            public string MobileNo1 { get; set; }
        public string MobileNo2 { get; set; }
        public string Studious { get; set; }

       // ** New 
        public string HomePhone { get; set; }






    }
    public class PersonMainInformation
        {
            public string PersonName { set; get; }
            public Guid PersonId { set; get; }
            public string PersonCode { get; set; }
            public string MobileNo1 { get; set; }
            public string BirthDate { get; set; }
            public string BloodType { get; set; }
            public string Email { get; set; }
            public string FaceBook { get; set; }
            public string FatherName { get; set; }
            public string Job { get; set; }
            public string MaritalStatus { get; set; }
            public string NationalID { get; set; }
            public string HomePhone { get; set; }
            public string Relationship { get; set; }
            public string CreatedByName { get; set; }
            public string Skype { get; set; }
            public string Studious { get; set; }
            public string MarriageDate { get; set; }
            public string CreatedOn { get; set; }
            public string CityName { get; set; }
            public string AreaName { get; set; }
            public string Address { get; set; }
            public bool Active { get; set; }


        }




    }
