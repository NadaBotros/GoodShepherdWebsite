using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace System.Backend
{
    public partial class ChangeInfo : MangeBackend
    {
        static string Old;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetInfo();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            string ImageFile = string.Empty;
            #region Save Image
            if (fupldImage.PostedFile.FileName != "")
            {
                string[] y = fupldImage.PostedFile.ContentType.Split('/');
                if (y[0] == "image")
                {
                    Guid id = Guid.NewGuid();
                    ImageFile = "User" + id.ToString().Replace("-", "") + System.IO.Path.GetExtension(fupldImage.PostedFile.FileName);
                    fupldImage.PostedFile.SaveAs(Server.MapPath("~/Images/ActualSize/" + ImageFile));
                    DAL.ImagesFact.ResizeWithCropResizeImage("", ImageFile, "User");
                }
            }
            #endregion
            DAL.AdminManagement obj = new DAL.AdminManagement();
            if (!string.IsNullOrEmpty(txtNewPassword.Text) && string.IsNullOrEmpty(txtoldPassword.Text))
            {
                lblMessge.Text = "You  Must Enter Old Password !";
                msg.Attributes["class"] = "msg-error";
            }
            else if (!string.IsNullOrEmpty(txtoldPassword.Text) && !string.IsNullOrEmpty(txtNewPassword.Text) && string.IsNullOrEmpty(txtconfirm.Text))
            {
                lblMessge.Text = "Your must enter Same password in Confirm Password !";
                msg.Attributes["class"] = "msg-error";
            }
            else if (!string.IsNullOrEmpty(txtoldPassword.Text) && Old != txtoldPassword.Text)
            {
                lblMessge.Text = "You  Must Enter correct Old Password !";
                msg.Attributes["class"] = "msg-error";
            }
            else if (obj.Edit(Request.Cookies["UserWebsiteId"].Value, txtUserName.Text, txtLogInName.Text, txtNewPassword.Text, txtEmail.Text, txtMobile.Text, txtJob.Text, ImageFile, Request.Cookies["UserWebsiteId"].Value))
            {
                BackendMessages(101);
                GetInfo();
            }
            else
                BackendMessages(201);

        }
        void GetInfo()
        {
            string UserId = Request.Cookies["UserWebsiteId"].Value;
            if (!string.IsNullOrEmpty(UserId))
            {
                AdminManagement _adminManagement = new AdminManagement();
                DAL.Admin AdminEnt = _adminManagement.LoadById(UserId);
                if (AdminEnt != null)
                {
                    txtLogInName.Text = AdminEnt.LoginName;
                    txtUserName.Text = AdminEnt.UserName;
                    txtEmail.Text = AdminEnt.Email;
                    txtJob.Text = AdminEnt.Job;
                    txtMobile.Text = AdminEnt.Mobile;
                    Old = AdminEnt.LoginPassword.ToString();
                    if (!string.IsNullOrEmpty(AdminEnt.Image))
                        imgUser.ImageUrl = "~/images/S150_150/" + AdminEnt.Image;

                }
            }

        }

    }
}