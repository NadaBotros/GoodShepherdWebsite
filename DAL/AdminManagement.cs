using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;

namespace DAL
{
    public class AdminManagement
    {
        #region Variable
        dbDataContext db;
        Admin _obj;
        #endregion
        public AdminManagement()
        { db = new dbDataContext(); }
        public string Add(string UserName, string LoginName, string LoginPassword, string Email, string Mobile, string Job, string Image, string IsAdministrator, string CreatedBy)
        {
            try
            {
                _obj = new Admin();
                _obj.UserId = Guid.NewGuid();
                _obj.UserName = UserName;
                _obj.LoginName = LoginName;
                _obj.LoginPassword = LoginPassword;
                _obj.Email = Email;
                _obj.Mobile = Mobile;
                _obj.Job = Job;
                _obj.IsAdministrator = bool.Parse(IsAdministrator);
                _obj.Image = Image;
                _obj.Active = true;
                _obj.CreatedBy = new Guid(CreatedBy);
                _obj.CreatedOn = DateTime.Now;
                db.Admins.InsertOnSubmit(_obj);
                db.SubmitChanges();
                return _obj.UserId.ToString();
            }
            catch
            {
                return string.Empty;
            }
        }
        public bool Edit(string Id, string UserName, string LoginName, string LoginPassword, string Email, string Mobile, string Job, string Image, string ModifiedBy)
        {
            try
            {
                _obj = db.Admins.FirstOrDefault(ad => ad.UserId == new Guid(Id));
                if (_obj != null)
                {
                    if (!string.IsNullOrEmpty(UserName))
                    {
                        _obj.UserName = UserName;
                    }
                    _obj.LoginName = LoginName;
                    _obj.Email = Email;
                    if (!string.IsNullOrEmpty(LoginPassword))
                    {
                        _obj.LoginPassword = LoginPassword;
                    }
                    if (!string.IsNullOrEmpty(Image))
                    {
                        _obj.Image = Image;
                    }
                    
                    _obj.Mobile = Mobile;
                    _obj.Job = Job;
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
        public bool Edit(string Id, string UserName, string LoginName, string LoginPassword, string Email, string Mobile, string Job, string Image, List<string> PagesIds, string IsAdministrator, string ModifiedBy)
        {
            try
            {
                _obj = db.Admins.FirstOrDefault(ad => ad.UserId == new Guid(Id));
                if (_obj != null)
                {
                    if (!string.IsNullOrEmpty(UserName))
                    {
                        _obj.UserName = UserName;
                    }
                    _obj.LoginName = LoginName;
                    _obj.Email = Email;
                    if (!string.IsNullOrEmpty(LoginPassword))
                    {
                        _obj.LoginPassword = LoginPassword;
                    }
                    if (!string.IsNullOrEmpty(Image))
                    {
                        _obj.Image = Image;
                    }
                    _obj.IsAdministrator = bool.Parse(IsAdministrator);
                    _obj.Mobile = Mobile;
                    _obj.Job = Job;
                    _obj.ModifiedBy = new Guid(ModifiedBy);
                    _obj.ModifiedOn = DateTime.Now;
                    db.SubmitChanges();UpdateAdminPages(PagesIds, Id);
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
                _obj = db.Admins.FirstOrDefault(ad => ad.UserId == new Guid(Id));
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
                _obj = db.Admins.FirstOrDefault(ad => ad.UserId == new Guid(Id));
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
        public string CheckAdmin(string LoginName, string LoginPassword, out string Msg)
        {
            try
            {
                _obj = db.Admins.FirstOrDefault(ad => ad.LoginName == LoginName && ad.LoginPassword == LoginPassword && ad.Active == true);
                if (_obj != null)
                {
                    Msg = string.Empty;
                    return _obj.UserId.ToString();
                }
                else
                {
                    Msg = "Error, invalid username or incorrect password";
                    return string.Empty;
                }
            }
            catch
            {
                Msg = string.Empty;
                return string.Empty;
            }
        }
        public Admin LoadById(string AdminId)
        {
            try
            {
                if (!string.IsNullOrEmpty(AdminId))
                {
                    _obj = db.Admins.FirstOrDefault(ad => ad.UserId == new Guid(AdminId));
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public Admin LoadByEmail(string Email)
        {
            try
            {
                if (!string.IsNullOrEmpty(Email))
                {
                    _obj = db.Admins.FirstOrDefault(ad => ad.Email == Email);
                    return _obj;
                }
                return null;
            }
            catch
            {
                return null;
            }
        }
        public string LoadAdminName(string UserId)
        {
            try
            {
                if (!string.IsNullOrEmpty(UserId))
                {
                    return db.Admins.FirstOrDefault(ad => ad.UserId == new Guid(UserId)).UserName;
                }
                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
        public DataTable LoadByDeleteState(string Active)
        {
            try
            {
                var query = (from admin in db.Admins
                             where admin.Active == bool.Parse(Active)
                             select admin).Distinct();
                return query.ToDataTable();
            }
            catch
            {
                return null;
            }
        }
        public bool ChangePassword(string UserId, string OldPassword, string NewPassword)
        {
            _obj = db.Admins.SingleOrDefault(ad => ad.UserId == new Guid(UserId) && ad.LoginPassword == OldPassword);
            if (_obj != null)
            {
                _obj.LoginPassword = NewPassword;
                db.SubmitChanges();
                return true;
            }
            return false;
        }
        public string ForgetPassword(string Email)
        {
            _obj = LoadByEmail(Email);
            if (_obj != null)
            {
                if (!_obj.Active.Value)
                    return "your account has been deleted";
                else
                {
                    StringBuilder _emailBody = new StringBuilder();
                    _emailBody.Append("Hi " + _obj.UserName + " , <br>");
                    _emailBody.Append("Your current user name is: <b>" + _obj.LoginName + "</b><br>");
                    _emailBody.Append("Your current password is: <b>" + _obj.LoginPassword + "</b><br>");
                    _emailBody.Append("If you have any questions, please contact us at agamal@bebrand.tv.<br>");
                    _emailBody.Append("Thanks,");
                    GeneralMethods.SendMessage(Email, "Forget Password", "", "Forget Password", _emailBody.ToString(), "");
                    return "your user name and password have been sent to your email";
                }
            }
            else
            {
                return "This E-mail not Correct, Try again later.";
            }
        }
        public bool UpdateAdminPages(List<string> PagesIds, string AdminId)
        {
            List<AdminPage> pages = db.AdminPages.Where(x => x.AdminId == new Guid(AdminId)).ToList();
            foreach (AdminPage p in pages)
            {
                db.AdminPages.DeleteOnSubmit(p);
                db.SubmitChanges();
            }
            AdminPage obj;
            foreach (string PageId in PagesIds)
            {
                obj = new AdminPage();
                obj.UserPageId = Guid.NewGuid();
                obj.PageId = new Guid(PageId);
                obj.AdminId = new Guid(AdminId);
                db.AdminPages.InsertOnSubmit(obj);
                db.SubmitChanges();
            }
            return true;
        }
        public List<AdminPage> LoadUserPages(string UserId)
        {
           return db.AdminPages.Where(x => x.AdminId == new Guid(UserId)).ToList();
        }
    }
}
