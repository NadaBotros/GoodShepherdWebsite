using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace System.Backend.manage
{
    public partial class MeetingArchiveManagement : MangeBackend
    {
        #region Variables
        MeetingArchiveManage _MeetingArchiveManage;
        #endregion
        #region Property
        public string MeetingArchiveId
        {
            set { ViewState["MeetingArchiveId"] = value; }
            get { return ViewState["MeetingArchiveId"] == null ? string.Empty : ViewState["MeetingArchiveId"].ToString(); }
        }
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                MeetingArchiveId = Request.QueryString["id"].ToString();
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
            Response.Redirect("MeetingArchiveManagement.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MeetingArchiveList.aspx");
        }
        #endregion
        #region Methods
        void Save()
        {
            string ImageFile = string.Empty;
            _MeetingArchiveManage = new MeetingArchiveManage();
            #region Manage Item
            if (string.IsNullOrEmpty(MeetingArchiveId))
            {
                MeetingArchiveId = _MeetingArchiveManage.Add(drpType.SelectedValue, txtName.Text, txtDate.Text, txtNevative.Text, txtPositives.Text, txtSuggestion.Text, Request.Cookies["UserWebsiteId"].Value);
                if (!string.IsNullOrEmpty(MeetingArchiveId))
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
                if (_MeetingArchiveManage.Edit(MeetingArchiveId, drpType.SelectedValue, txtName.Text, txtDate.Text, txtNevative.Text, txtPositives.Text, txtSuggestion.Text, Request.Cookies["UserWebsiteId"].Value))
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
            if (!string.IsNullOrEmpty(MeetingArchiveId))
            {
                _MeetingArchiveManage = new MeetingArchiveManage();
                MeetingArchive MeetingArchiveEnt = _MeetingArchiveManage.LoadById(MeetingArchiveId);
                if (MeetingArchiveEnt != null)
                {
                    txtName.Text = MeetingArchiveEnt.Title;
                    txtDate.Text = MeetingArchiveEnt.Date.ToString("d/M/yyyy");
                    txtNevative.Text = MeetingArchiveEnt.Negatives;
                    txtPositives.Text = MeetingArchiveEnt.Positives;
                    txtSuggestion.Text = MeetingArchiveEnt.Suggestions;
                    drpType.SelectedValue = MeetingArchiveEnt.ArchiveType;
                }
            }
        }
        void Clear()
        {
            txtName.Text = txtDate.Text = txtNevative.Text = txtPositives.Text = txtSuggestion.Text = string.Empty;
        }
        #endregion

    }
}