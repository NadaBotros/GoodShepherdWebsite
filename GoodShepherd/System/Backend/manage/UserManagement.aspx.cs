using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace System.Backend.manage
{
    public partial class UserManagement : MangeBackend
    {
        #region Variables
        AdminManagement _adminManagement;
        #endregion
        #region Property
        public string UserId
        {
            set { ViewState["UserId"] = value; }
            get { return ViewState["UserId"] == null ? string.Empty : ViewState["UserId"].ToString(); }
        }
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                UserId = Request.QueryString["id"].ToString();
            }
            if (!IsPostBack)
                GetInfo();

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            GetInfo();
        }
        protected void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            Save();
            Response.Redirect("UserManagement.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsersList.aspx");
        }
        #endregion
        #region Methods
        void Save()
        {
            string ImageFile = string.Empty;
            _adminManagement = new AdminManagement();
            #region Save Image
            if (fupldImage.PostedFile.FileName != "")
            {
                string[] y = fupldImage.PostedFile.ContentType.Split('/');
                if (y[0] == "image")
                {
                    Guid id = Guid.NewGuid();
                    ImageFile = "User" + id.ToString().Replace("-", "") + System.IO.Path.GetExtension(fupldImage.PostedFile.FileName);
                    fupldImage.PostedFile.SaveAs(Server.MapPath("~/Images/ActualSize/" + ImageFile));
                    ImagesFact.ResizeWithCropResizeImage("", ImageFile, "User");
                }
            }
            #endregion
            #region Pages
            List<string> pages = new List<string>();
            if (radIsAdmin.SelectedValue == "False")
            {
               
                foreach (GridViewRow row in grdPages.Rows)
                {
                    Label lblId = row.FindControl("lblId") as Label;
                    RadioButtonList rad = row.FindControl("radAllow") as RadioButtonList;
                    if (lblId != null)
                    {
                        if (rad.SelectedValue == "True")
                            pages.Add(lblId.Text);
                    }
                }
            }
            #endregion
            #region Manage Item
            if (string.IsNullOrEmpty(UserId))
            {
                if (string.IsNullOrEmpty(txtPassWord.Text))
                {
                    BackendMessages(102);
                }
                else
                {
                    UserId = _adminManagement.Add(txtUserName.Text, txtLogInName.Text, txtPassWord.Text, txtEmail.Text, txtMobile.Text, txtJob.Text, ImageFile, radIsAdmin.SelectedValue, Request.Cookies["UserWebsiteId"].Value);
                    if (!string.IsNullOrEmpty(UserId))
                    {
                        BackendMessages(101);
                    }
                    else
                    {
                        BackendMessages(201);
                    }
                }
            }
            else
            {
                if (_adminManagement.Edit(UserId, txtUserName.Text, txtLogInName.Text, txtPassWord.Text, txtEmail.Text, txtMobile.Text, txtJob.Text, ImageFile, pages, radIsAdmin.SelectedValue, Request.Cookies["UserWebsiteId"].Value))
                {
                    BackendMessages(101);
                }
                else
                {
                    BackendMessages(201);
                }
            }
            #endregion
        }
        void GetInfo()
        {
            if (!string.IsNullOrEmpty(UserId))
            {
                _adminManagement = new AdminManagement();
                DAL.Admin AdminEnt = _adminManagement.LoadById(UserId);
                if (AdminEnt != null)
                {
                    #region User Info
                    txtLogInName.Text = AdminEnt.LoginName;
                    txtUserName.Text = AdminEnt.UserName;
                    txtEmail.Text = AdminEnt.Email;
                    txtJob.Text = AdminEnt.Job;
                    radIsAdmin.SelectedValue = AdminEnt.IsAdministrator.ToString();
                    txtMobile.Text = AdminEnt.Mobile;
                    if (!string.IsNullOrEmpty(AdminEnt.Image))
                        imgUser.ImageUrl = "~/images/S150_150/" + AdminEnt.Image;
                    #endregion
                    #region User Pages
                    if (AdminEnt.IsAdministrator.Value)
                    {
                        TabPages.Enabled = false;
                    }
                    else
                    {
                        TabPages.Enabled = true;
                        grdPages.DataBind();
                        List<AdminPage> pages = _adminManagement.LoadUserPages(UserId);
                        foreach (GridViewRow row in grdPages.Rows)
                        {
                            Label lblId = row.FindControl("lblId") as Label;
                            RadioButtonList rad = row.FindControl("radAllow") as RadioButtonList;
                            if (lblId != null)
                            {
                                if (pages.Exists(x => x.PageId == new Guid(lblId.Text)))
                                    rad.SelectedValue = "True";
                                else
                                    rad.SelectedValue = "False";
                            }
                        }
                    }
                    #endregion
                }
                else
                {
                    TabPages.Enabled = false;
                }
            }

        }
        void Clear()
        {
            txtLogInName.Text = txtUserName.Text = txtEmail.Text = txtPassWord.Text = txtConfirmPassword.Text = string.Empty;
        }
        #endregion
    }
}