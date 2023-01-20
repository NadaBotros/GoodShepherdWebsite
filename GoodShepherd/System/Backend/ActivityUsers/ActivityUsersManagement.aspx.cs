using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Web.UI.WebControls;
using DAL;
namespace System.Backend.manage
{
    public partial class ActivityUsersManagement : MangeBackend
    {
        #region Variables
        DAL.ActivityUsersManagement activityUsersManagement;
        #endregion
        #region Property
        public string ActivityUserId
        {
            set { ViewState["ActivityUserId"] = value; }
            get { return ViewState["ActivityUserId"] == null ? string.Empty : ViewState["ActivityUserId"].ToString(); }
        }
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                ActivityUserId = Request.QueryString["id"].ToString();
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
            Response.Redirect("ActivityUsersManagement.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ActivityUsersList.aspx");
        }
        #endregion
        #region Methods
        void Save()
        {
            string ImageFile = string.Empty;
            activityUsersManagement = new DAL.ActivityUsersManagement();
            #region Manage Item
            if (!activityUsersManagement.CheckIfCodeExists(ActivityUserId,drpActivity.SelectedValue, txtCode.Text))
            {
                if (!activityUsersManagement.CheckIfNameExists(ActivityUserId, drpActivity.SelectedValue, txtName.Text))
                {
                    if (string.IsNullOrEmpty(ActivityUserId))
                    {
                        ActivityUserId = activityUsersManagement.Add(drpActivity.SelectedValue, txtName.Text, txtMobile.Text, txtMobile2.Text, txtRoomNo.Text, txtCode.Text, txtNotes.Text,
                                                                     Request.Cookies["UserWebsiteId"].Value);
                        if (!string.IsNullOrEmpty(ActivityUserId))
                        {
                            BackendMessages(101);
                        }
                        else
                        {
                            BackendMessages(201);
                        }
                    }
                    else
                    {
                        if (activityUsersManagement.Edit(ActivityUserId, drpActivity.SelectedValue, txtName.Text, txtMobile.Text, txtMobile2.Text, txtRoomNo.Text, txtCode.Text, txtNotes.Text,
                                                         Request.Cookies["UserWebsiteId"].Value))
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
                    BackendMessages(301);
                    lblMessge.Text = "تم تسجيل هذا الشخص قبل ذلك";
                }

            }
            else
            {
                BackendMessages(301);
                lblMessge.Text = "تم تسجيل هذا الكود قبل ذلك";
            }

            #endregion
        }
        void GetInfo()
        {
            if (!string.IsNullOrEmpty(ActivityUserId))
            {
                activityUsersManagement = new DAL.ActivityUsersManagement();
                DAL.ActivityUser activityUser = activityUsersManagement.LoadById(ActivityUserId);
                if (activityUser != null)
                {
                    drpActivity.DataBind();
                    drpActivity.SelectedValue = activityUser.ActivityId.ToString();
                    txtCode.Text = activityUser.Code;
                    txtMobile.Text = activityUser.Mobile;
                    txtMobile2.Text = activityUser.Mobile2;
                    txtName.Text = activityUser.FullName;
                    txtRoomNo.Text = activityUser.RoomNo;
                    txtNotes.Text = activityUser.Notes;
                }
            }

        }
        void Clear()
        {
            txtName.Text = txtCode.Text = txtMobile.Text = txtMobile2.Text = txtNotes.Text = txtRoomNo.Text;
        }
        #endregion

        protected void drpActivity_DataBound(object sender, EventArgs e)
        {
            drpActivity.Items.Insert(0, new ListItem("اختر النشاط", string.Empty));
        }

    }
}