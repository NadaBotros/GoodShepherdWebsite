using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

using System.Data.Linq.SqlClient;
using System.Globalization;
namespace DAL
{
    public class PersonManagement
    {
        #region Variable
        dbDataContext db;
        Prg_Person _obj;
        #endregion
        #region Method
        public PersonManagement()
        { db = new dbDataContext(); }
        public string Add(string FamilyId, string PersonName, string Relationship, string BirthDate, string BloodType, string Email, string FaceBook,
            string FatherName, string Job, string JobPlace, string MaritalStatus, string MobileNo1, string MobileNo2, string NationalID, string Skype,
            string Studious, string WhatsUp, List<string> HobbiesIds, string CreatedBy, string userImage, out string ErrorMessage)
        {
            try
            {
                string error = string.Empty;
                string PersonCode = GetPersonCode(FamilyId, Relationship, out error);
                if (!string.IsNullOrEmpty(error))
                {
                    ErrorMessage = error;
                    return string.Empty;
                }
                else
                {
                    _obj = new Prg_Person();
                    _obj.PersonId = Guid.NewGuid();
                    _obj.FamilyId = new Guid(FamilyId);
                    _obj.PersonCode = PersonCode;
                    _obj.PersonName = PersonName;
                    DateTime BD = new DateTime();
                    if (DateTime.TryParseExact(BirthDate, "d/M/yyyy", CultureInfo.InvariantCulture,
            DateTimeStyles.None, out BD))
                        _obj.BirthDate = BD;
                    else
                        _obj.BirthDate = null;
                    _obj.UserImage = userImage;
                    _obj.BloodType = BloodType;
                    _obj.Email = Email;
                    _obj.Relationship = Relationship;
                    _obj.FaceBook = FaceBook;
                    _obj.FatherName = FatherName;
                    _obj.Job = Job;
                    _obj.JobPlace = JobPlace;
                    _obj.MaritalStatus = MaritalStatus;
                    _obj.MobileNo1 = MobileNo1;
                    _obj.MobileNo2 = MobileNo2;
                    _obj.NationalID = NationalID;
                    _obj.PersonName = PersonName;
                    _obj.Skype = Skype;
                    _obj.Studious = int.Parse(Studious);
                    _obj.WhatsUp = bool.Parse(WhatsUp);
                    _obj.Sex = PersonCode[5] == '1' ? true : false;
                    _obj.Active = true;
                    _obj.CreatedBy = new Guid(CreatedBy);
                    _obj.CreatedOn = DateTime.Now;
                    db.Prg_Persons.InsertOnSubmit(_obj);
                    db.SubmitChanges();
                    PersonHobbiesManagement _PersonHobbiesManagement = new PersonHobbiesManagement();
                    _PersonHobbiesManagement.Add(_obj.PersonId.ToString(), HobbiesIds, CreatedBy);
                    ErrorMessage = string.Empty;
                    return _obj.PersonId.ToString();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                return string.Empty;
            }
        }
        public bool Edit(string PersonId, string PersonName, string Relationship, string BirthDate, string BloodType, string Email, string FaceBook, string FatherName,
            string Job, string JobPlace, string MaritalStatus, string MobileNo1, string MobileNo2, string NationalID, string Skype, string Studious,
            string WhatsUp, List<string> HobbiesIds, string userIamge, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_Persons.FirstOrDefault(lb => lb.PersonId == new Guid(PersonId));
                if (_obj != null)
                {
                    _obj.PersonName = PersonName;
                    DateTime BD = new DateTime();
                    if (DateTime.TryParseExact(BirthDate, "d/M/yyyy", CultureInfo.InvariantCulture,
        DateTimeStyles.None, out BD))
                        _obj.BirthDate = BD;
                    else
                        _obj.BirthDate = null;
                    _obj.BloodType = BloodType;
                    _obj.Email = Email;
                    _obj.FaceBook = FaceBook;
                    _obj.FatherName = FatherName;
                    _obj.Job = Job;
                    _obj.JobPlace = JobPlace;
                    _obj.MaritalStatus = MaritalStatus;
                    _obj.MobileNo1 = MobileNo1;
                    _obj.MobileNo2 = MobileNo2;
                    _obj.NationalID = NationalID;
                    _obj.PersonName = PersonName;
                    _obj.Skype = Skype;
                    _obj.Studious = int.Parse(Studious);
                    _obj.WhatsUp = bool.Parse(WhatsUp);
                    _obj.ModifiedBy = new Guid(ModifiedBy);
                    _obj.ModifiedOn = DateTime.Now;
                    if (!string.IsNullOrEmpty(userIamge))
                        _obj.UserImage = userIamge;
                    db.SubmitChanges();
                    var _PersonHobbiesManagement = new PersonHobbiesManagement();
                    _PersonHobbiesManagement.Delete(PersonId, ModifiedBy);
                    _PersonHobbiesManagement.Add(_obj.PersonId.ToString(), HobbiesIds, ModifiedBy);
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool Edit(string PersonId, string BloodType, string Email, string FaceBook, string FatherName, string Job, string JobPlace, string MaritalStatus, string MobileNo1, string MobileNo2, string Skype, string WhatsUp, List<string> HobbiesIds)
        {
            try
            {
                _obj = db.Prg_Persons.FirstOrDefault(lb => lb.PersonId == new Guid(PersonId));
                if (_obj != null)
                {
                    _obj.BloodType = BloodType;
                    _obj.Email = Email;
                    _obj.FaceBook = FaceBook;
                    _obj.FatherName = FatherName;
                    _obj.Job = Job;
                    _obj.JobPlace = JobPlace;
                    _obj.MaritalStatus = MaritalStatus;
                    _obj.MobileNo1 = MobileNo1;
                    _obj.MobileNo2 = MobileNo2;
                    _obj.Skype = Skype;
                    _obj.WhatsUp = bool.Parse(WhatsUp);

                    _obj.ModifiedOn = DateTime.Now;
                    db.SubmitChanges();
                    PersonHobbiesManagement _PersonHobbiesManagement = new PersonHobbiesManagement();
                    _PersonHobbiesManagement.Delete(PersonId, "");
                    _PersonHobbiesManagement.Add(_obj.PersonId.ToString(), HobbiesIds, "");
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public bool ChangePassword(string PersonId, string Password)
        {
            try
            {
                _obj = db.Prg_Persons.FirstOrDefault(lb => lb.PersonId == new Guid(PersonId));
                if (_obj != null)
                {
                    _obj.UserPassword = Password;
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
        public bool ChangeUserImage(string PersonId, string Image)
        {
            //try
            {
                _obj = db.Prg_Persons.FirstOrDefault(lb => lb.PersonId == new Guid(PersonId));
                if (_obj != null)
                {
                    _obj.UserImage = Image;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            //catch
            //{
            //    return false;
            //}
        }
        public bool ChangeUserImageByCode(string PersonCode, string Image)
        {
            //try
            {
                _obj = db.Prg_Persons.FirstOrDefault(lb => lb.PersonCode == PersonCode);
                if (_obj != null)
                {
                    _obj.UserImage = Image;
                    db.SubmitChanges();
                    return true;
                }
                return false;
            }
            //catch
            //{
            //    return false;
            //}
        }
        public string GetPersonCode(string FamilyId, string Relationship, out string MsgError)
        {
            FamilyManagement FamilyManagementObj = new FamilyManagement();
            string FamilyCode = FamilyManagementObj.LoadById(FamilyId).FamilyCode;
            MsgError = string.Empty;
            if (Relationship == "الزوج")
            {
                if (IfPersonTypeExists(FamilyId, Relationship))
                {
                    MsgError = "لا يمكن من اضافة بيانات الزوج اكثر من مرة";
                    return string.Empty;
                }
                else
                {
                    return FamilyCode + "10";
                }
            }
            else if (Relationship == "الزوجة")
            {
                if (IfPersonTypeExists(FamilyId, Relationship))
                {
                    MsgError = "لا يمكن من اضافة بيانات الزوجة اكثر من مرة";
                    return string.Empty;
                }
                else
                {
                    return FamilyCode + "20";
                }
            }
            else if (Relationship == "ابن" || Relationship == "اخ" || Relationship == "والد الزوج" || Relationship == "والد الزوجة")
                return FamilyCode + "1" + (FamilyPersonsCount(FamilyId, true) + 1);//number of male at family;
            else
                return FamilyCode + "2" + (FamilyPersonsCount(FamilyId, false) + 1);//number of male at family;

        }
        public bool IfPersonTypeExists(string FamilyId, string Relation)
        {
            return db.Prg_Persons.Any(x => x.FamilyId == new Guid(FamilyId) && x.Relationship == Relation && x.Active == true);
        }
        public int FamilyPersonsCount(string FamilyId, bool Sex)
        {
            return db.Prg_Persons.Count(x => x.FamilyId == new Guid(FamilyId) && x.Sex == Sex && x.Relationship != "الزوج" && x.Relationship != "الزوجة");
        }
        public bool Delete(string PersonId, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_Persons.FirstOrDefault(ad => ad.PersonId == new Guid(PersonId));
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
        public string CheckUserLogin(string UserCode, string UserPassword)
        {
            Prg_Person ent = db.Prg_Persons.FirstOrDefault(x => x.Active == true && x.UserPassword == UserPassword && x.PersonCode == UserCode);
            if (ent != null)
                return ent.PersonId.ToString();
            else
                return string.Empty;
        }
        public Prg_Person CheckUserId(string UserCode, string NationalityId)
        {
            Prg_Person ent = db.Prg_Persons.FirstOrDefault(x => x.Active == true && x.NationalID == NationalityId && x.PersonCode == UserCode);
            if (ent != null)
                return ent;
            else
                return null;
        }
        public bool Restore(string PersonId, string ModifiedBy)
        {
            try
            {
                _obj = db.Prg_Persons.FirstOrDefault(ad => ad.PersonId == new Guid(PersonId));
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
        public DataTable LoadByDeleteState(string Active, string FamilyId)
        {
            try
            {
                var query = (from p in db.Prg_Persons
                             where p.Active == bool.Parse(Active) && p.FamilyId == new Guid(FamilyId)
                             select new
                             {
                                 p.PersonCode,
                                 p.PersonName,
                                 p.Active,
                                 p.CreatedBy,
                                 p.CreatedOn,
                                 p.ModifiedBy,
                                 p.ModifiedOn,
                                 p.PersonId,
                                 p.Relationship,
                                 Studious = p.Studious == 0 ? "لا يحضر" :
                                p.Studious == 1 ? "يحضر بالكنيسة" : "يحضر بالقاعة"
                             }).Distinct();
                return query.ToDataTable("CreatedOn", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public List<string> GetListofFatherName()
        {
            try
            {
                return (from p in db.Prg_Persons
                        where p.Active == true
                        orderby p.FatherName
                        select p.FatherName).Distinct().ToList();

            }
            catch
            {
                return null;
            }
        }
        public DataTable LoadAllAbaKhna()
        {
            try
            {
                var query = (from p in db.Prg_Persons
                             where p.Active == true && p.FatherName!=null
                             select new { p.FatherName }).Distinct();

                return query.ToDataTable("FatherName", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public DataTable LoadAllJobs()
        {
            try
            {
                var query = (from p in db.Prg_Persons
                             where p.Active == true
                             select new { p.Job }).Distinct();
                return query.ToDataTable("Job", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public List<Prg_Person> GetByIds(List<string> personsIds)
        {
            var query = (from x in db.Prg_Persons where x.ModifiedOn > new DateTime(2000, 1, 1) && personsIds.Contains(x.PersonId.ToString()) select x).ToList();
            return query;
        }

        public bool ChangePassword(string PersonId, string OldPassword, string NewPassword)
        {
            _obj = db.Prg_Persons.SingleOrDefault(ad => ad.PersonId == new Guid(PersonId) && ad.UserPassword == OldPassword);
            if (_obj != null)
            {
                _obj.UserPassword = NewPassword;
                db.SubmitChanges();
                return true;
            }
            return false;
        }
        public Prg_Person LoadById(string PersonId)
        {
            try
            {
                if (!string.IsNullOrEmpty(PersonId))
                {
                    _obj = db.Prg_Persons.FirstOrDefault(lb => lb.PersonId == new Guid(PersonId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public bool CheckIfExists(string PersonId, string NationalID, string PersonName)
        {
            //try
            //{
            PersonName = GeneralMethods.ConvertToSearchWord(PersonName);
            PersonName = PersonName.Replace("%", "") + "%";
            if (!string.IsNullOrEmpty(PersonId))
            {
                _obj = db.Prg_Persons.FirstOrDefault(lb => lb.Prg_Family.Active == true && lb.PersonId != new Guid(PersonId) && ((lb.NationalID == NationalID) || SqlMethods.Like(lb.PersonName, PersonName)) && lb.Active == true);
                if (_obj != null)
                    return true;
            }
            else
            {
                _obj = db.Prg_Persons.FirstOrDefault(lb => lb.Prg_Family.Active == true && ((lb.NationalID == NationalID) || SqlMethods.Like(lb.PersonName, PersonName)) && lb.Active == true);
                if (_obj != null)
                    return true;
            }
            return false;
            //}
            //catch
            //{
            //    return false;
            //}
        }
        public DataTable PersonSearch(List<string> Ab3traf, List<string> Area, List<string> BooldType, List<string> Job, List<string> MaritalStatus, List<string> Relationship, List<string> Studious, string SearchWord)
        {
            string SearchAr = GeneralMethods.ConvertToSearchWord(SearchWord);
            var query = (from x in db.Prg_Persons
                         where (Ab3traf.Count == 0 || Ab3traf.Contains(x.FatherName))
                         && (Area.Count == 0 || Area.Contains(x.Prg_Family.Prg_Area.AreaName))
                         && (BooldType.Count == 0 || BooldType.Contains(x.BloodType))
                         && (Job.Count == 0 || Job.Contains(x.Job))
                         && (MaritalStatus.Count == 0 || MaritalStatus.Contains(x.MaritalStatus))
                         && (Relationship.Count == 0 || Relationship.Contains(x.Relationship))
                         && (Studious.Count == 0 || Studious.Contains(x.Studious.ToString()))
                         && (string.IsNullOrEmpty(SearchWord) || SqlMethods.Like(x.PersonName, SearchAr) || SqlMethods.Like(x.NationalID, SearchAr) || SqlMethods.Like(x.MobileNo1, SearchAr) || SqlMethods.Like(x.MobileNo2, SearchAr) || x.Email.Contains(SearchWord))
                         select new
                         {
                             x.Prg_Family.Prg_Area.AreaName,
                             x.PersonCode,
                             x.PersonName,
                             x.PersonId,
                             x.Relationship,
                             Studious = x.Studious == 0 ? "لا يحضر" :
                                x.Studious == 1 ? "يحضر بالكنيسة" : "يحضر بالقاعة"
                         }).Distinct();
            return query.ToDataTable("PersonName", "ASC");
        }


        public List<string> PersonAdvancedSearch(List<string> Ab3traf, List<string> Area, List<string> BooldType, List<string> Job, List<string> MaritalStatus, List<string> Relationship, List<string> Studious, List<string> Servants, string SearchWord, string BDMonth, string BDDay)
        {
            string SearchAr = GeneralMethods.ConvertToSearchWord(SearchWord);
            var studentsCodes = GeneralMethods.ConvertToPersonodes(SearchWord);
            var query = (from x in db.Prg_Persons
                         where x.Active == true && x.Prg_Family.Active == true &&
                         (Ab3traf.Count == 0 || Ab3traf.Contains(x.FatherName))
                         && (Area.Count == 0 || Area.Contains(x.Prg_Family.AreaId.ToString()))
                         && (BooldType.Count == 0 || BooldType.Contains(x.BloodType))
                         && (BDDay == "0" || x.BirthDate.Value.Day == int.Parse(BDDay))
                         && (BDMonth == "0" || x.BirthDate.Value.Month == int.Parse(BDMonth))
                         && (Job.Count == 0 || Job.Contains(x.Job))
                         && (Servants.Count == 0 || x.Prg_ServantPersons.Any(y => y.PersonId == x.PersonId
                         && Servants.Contains(y.ServantId.ToString())))
                         && (MaritalStatus.Count == 0 || MaritalStatus.Contains(x.MaritalStatus))
                         && (Relationship.Count == 0 || Relationship.Contains(x.Relationship))
                         && (Studious.Count == 0 || Studious.Contains(x.Studious.ToString()))
                         && (string.IsNullOrEmpty(SearchWord) || SqlMethods.Like(x.PersonName, SearchAr)
                         || SqlMethods.Like(x.NationalID, SearchAr) || SqlMethods.Like(x.MobileNo1, SearchAr)
                         || SqlMethods.Like(x.MobileNo2, SearchAr) || x.Email.Contains(SearchWord) || studentsCodes.Contains(x.PersonCode))



                         select
                             x.PersonId.ToString()).Distinct().ToList();
            return query;
        }



        public DataTable PersonSearchNotHasServant(List<string> Ab3traf, List<string> Area, List<string> BooldType, List<string> Job, List<string> MaritalStatus, List<string> Relationship, List<string> Studious, string SearchWord)
        {

            string SearchAr = GeneralMethods.ConvertToSearchWord(SearchWord);
            var query = (from x in db.Prg_Persons
                         where x.Active == true && x.Studious > 0 && x.Prg_Family.Active == true &&

                         !x.Prg_ServantPersons.Any(y => y.PersonId == x.PersonId) &&
                          (Ab3traf.Count == 0 || Ab3traf.Contains(x.FatherName))
                         && (Area.Count == 0 || Area.Contains(x.Prg_Family.AreaId.ToString()))
                         && (BooldType.Count == 0 || BooldType.Contains(x.BloodType))
                         && (Job.Count == 0 || Job.Contains(x.Job))
                         && (MaritalStatus.Count == 0 || MaritalStatus.Contains(x.MaritalStatus))
                         && (Relationship.Count == 0 || Relationship.Contains(x.Relationship))
                         && (Studious.Count == 0 || Studious.Contains(x.Studious.ToString()))
                         && (string.IsNullOrEmpty(SearchWord) || SqlMethods.Like(x.PersonName, SearchAr)
                         || SqlMethods.Like(x.NationalID, SearchAr) || SqlMethods.Like(x.MobileNo1, SearchAr)
                         || SqlMethods.Like(x.MobileNo2, SearchAr) || x.Email.Contains(SearchWord))
                         select new
                         {
                             x.Prg_Family.Prg_Area.AreaName,
                             Mobile = x.MobileNo1,
                             x.PersonCode,
                             x.PersonName,
                             x.PersonId,
                             x.Relationship,
                             Studious = x.Studious == 0 ? "لا يحضر" :
                                x.Studious == 1 ? "يحضر بالكنيسة" : "يحضر بالقاعة"
                         }).Distinct();
            return query.ToDataTable("PersonName", "ASC");
        }



        public List<string> Filter(List<string> Ab3traf, List<string> Area, List<string> BooldType, List<string> Job, List<string> MaritalStatus, List<string> Relationship, List<string> Studious, List<string> Servants, string SearchWord, string BDMonth, string BDDay)
        {
            string SearchAr = GeneralMethods.ConvertToSearchWord(SearchWord);

            var studentsCodes = GeneralMethods.ConvertToPersonodes(SearchWord);

            var query = (from x in db.Prg_Persons
                         where x.Active == true && x.Prg_Family.Active == true &&
                         (Ab3traf.Count == 0 || Ab3traf.Contains(x.FatherName))
                         && (Area.Count == 0 || Area.Contains(x.Prg_Family.AreaId.ToString()))
                         && (BooldType.Count == 0 || BooldType.Contains(x.BloodType))
                         && (BDDay == "0" || x.BirthDate.Value.Day == int.Parse(BDDay))
                         && (BDMonth == "0" || x.BirthDate.Value.Month == int.Parse(BDMonth))
                         && (Job.Count == 0 || Job.Contains(x.Job))
                         && (Servants.Count == 0 || x.Prg_ServantPersons.Any(y => y.PersonId == x.PersonId
                         && Servants.Contains(y.ServantId.ToString())))
                         && (MaritalStatus.Count == 0 || MaritalStatus.Contains(x.MaritalStatus))
                         && (Relationship.Count == 0 || Relationship.Contains(x.Relationship))
                         && (Studious.Count == 0 || Studious.Contains(x.Studious.ToString()))
                         && (string.IsNullOrEmpty(SearchWord) || SqlMethods.Like(x.PersonName, SearchAr)
                         || SqlMethods.Like(x.NationalID, SearchAr) || SqlMethods.Like(x.MobileNo1, SearchAr)
                         || SqlMethods.Like(x.MobileNo2, SearchAr) || x.Email.Contains(SearchWord) || studentsCodes.Contains(x.PersonCode))




                         select x.PersonId.ToString()).Distinct().ToList();


            return query.ToList();
        }



        public List<string> PeopleEHaveNoServantsSearch(List<string> Ab3traf, List<string> Area, List<string> BooldType, List<string> Job, List<string> MaritalStatus, List<string> Relationship, List<string> Studious ,  string SearchWord, string BDMonth, string BDDay) {

            string SearchAr = GeneralMethods.ConvertToSearchWord(SearchWord);

            var studentsCodes = GeneralMethods.ConvertToPersonodes(SearchWord);

            var query = (from x in db.Prg_Persons
                         where x.Active == true  && x.Prg_Family.Active == true &&
                         (Ab3traf.Count==0 || Ab3traf.Contains(x.FatherName))
                         && (Area.Count == 0 || Area.Contains(x.Prg_Family.AreaId.ToString()))
                         && (BooldType.Count == 0 || BooldType.Contains(x.BloodType))
                         && (BDDay == "0" || x.BirthDate.Value.Day == int.Parse(BDDay))
                         && (BDMonth == "0" || x.BirthDate.Value.Month == int.Parse(BDMonth))
                         && (Job.Count == 0 || Job.Contains(x.Job))
                         && (MaritalStatus.Count == 0 || MaritalStatus.Contains(x.MaritalStatus))
                         && (Relationship.Count == 0 || Relationship.Contains(x.Relationship))
                         && (Studious.Count == 0 || Studious.Contains(x.Studious.ToString()))
                         && (string.IsNullOrEmpty(SearchWord) || SqlMethods.Like(x.PersonName, SearchAr)
                         || SqlMethods.Like(x.NationalID, SearchAr) || SqlMethods.Like(x.MobileNo1, SearchAr)
                         || SqlMethods.Like(x.MobileNo2, SearchAr) || x.Email.Contains(SearchWord) || studentsCodes.Contains(x.PersonCode))




                         select x.PersonId.ToString()).Distinct().ToList(); 


            return query.ToList();
        }


        public string[] GetFatherNameList(string SearchWord)
        {
            try
            {
                SearchWord = GeneralMethods.ConvertToSearchWord(SearchWord);
                var query = (from P in db.Prg_Persons
                             where P.Active == true && SqlMethods.Like(P.FatherName, SearchWord)
                             select P.FatherName).Take(10).Distinct();
                return query.ToArray();
            }
            catch
            {
                return null;
            }
        }
        public string[] GetJobList(string SearchWord)
        {
            try
            {
                SearchWord = GeneralMethods.ConvertToSearchWord(SearchWord);
                var query = (from P in db.Prg_Persons
                             where P.Active == true && SqlMethods.Like(P.Job, SearchWord)
                             select P.Job).Take(10).Distinct();
                return query.ToArray();
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
                             where p.Active == true && p.Prg_Family.Active == true && codesList.Contains(p.PersonCode)
                             select new { p.PersonCode, p.PersonName, p.PersonId, Mobile = p.MobileNo1, p.Prg_Family.Prg_Area.AreaName }).Distinct();
                return query.ToDataTable("CreatedOn", "ASC");
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
                             where p.Active == true && p.Active == true && (SqlMethods.Like(p.PersonName, SearchWord) || SqlMethods.Like(p.MobileNo1, SearchWord) || SqlMethods.Like(p.PersonCode, SearchWord))
                             select new { p.PersonCode, p.PersonName, p.PersonId, Mobile = p.MobileNo1, p.Prg_Family.Prg_Area.AreaName }).Distinct();
                return query.ToDataTable("CreatedOn", "ASC");
            }
            catch
            {
                return null;
            }
        }
        public string ConvertToFullAddress(string CityName, string AreaName, string StreetName, string BuildingNextTo, string BuildingNo, string FloorNo, string FlatNo, string AddressNotes)
        {
            StringBuilder strAddress = new StringBuilder();
            if (!string.IsNullOrEmpty(BuildingNo))
                strAddress.Append(BuildingNo + " ");
            if (!string.IsNullOrEmpty(StreetName))
                strAddress.Append("شارع " + StreetName);
            if (!string.IsNullOrEmpty(BuildingNextTo))
                strAddress.Append(" , " + BuildingNextTo);
            if (!string.IsNullOrEmpty(AreaName))
                strAddress.Append(" , " + AreaName);
            if (!string.IsNullOrEmpty(CityName))
                strAddress.Append(" , " + CityName);
            if (!string.IsNullOrEmpty(FlatNo))
                strAddress.Append(" , شقة " + FlatNo);
            if (!string.IsNullOrEmpty(FloorNo))
                strAddress.Append(" , الدور " + FloorNo);
            if (!string.IsNullOrEmpty(AddressNotes))
                strAddress.Append(" , " + AddressNotes);
            return strAddress.ToString();
        }
        #endregion
       
    }
}
