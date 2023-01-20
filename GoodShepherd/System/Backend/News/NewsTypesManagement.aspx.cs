using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Web.UI.WebControls;
using DAL;
namespace System.Backend.manage
{
    public partial class NewsTypesManagement : MangeBackend
    {
        #region Variables
        DAL.NewsTypesManagement _NewsTypesManagementObj;
        #endregion
        #region Property
        public string NewsTypeId
        {
            set { ViewState["NewsTypeId"] = value; }
            get { return ViewState["NewsTypeId"] == null ? string.Empty : ViewState["NewsTypeId"].ToString(); }
        }
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                NewsTypeId = Request.QueryString["id"].ToString();
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
            Response.Redirect("NewsTypesManagement.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewsTypesList.aspx");
        }
        #endregion
        #region Methods
        void Save()
        {
            string ImageFile = string.Empty;
            _NewsTypesManagementObj = new DAL.NewsTypesManagement();
            #region Manage Item
            if (!_NewsTypesManagementObj.CheckIfExists(NewsTypeId, txtName.Text))
            {
                if (string.IsNullOrEmpty(NewsTypeId))
                {
                    NewsTypeId = _NewsTypesManagementObj.Add(txtName.Text, Request.Cookies["UserWebsiteId"].Value);
                    if (!string.IsNullOrEmpty(NewsTypeId))
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
                    if (_NewsTypesManagementObj.Edit(NewsTypeId, txtName.Text, Request.Cookies["UserWebsiteId"].Value))
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
            { BackendMessages(301); }

            #endregion
        }
        void GetInfo()
        {
            if (!string.IsNullOrEmpty(NewsTypeId))
            {
                _NewsTypesManagementObj = new DAL.NewsTypesManagement();
                DAL.NewsType NewsTypeEnt = _NewsTypesManagementObj.LoadById(NewsTypeId);
                if (NewsTypeEnt != null)
                {
                    txtName.Text = NewsTypeEnt.NewsTypeName;
                }
            }
        }
        void Clear()
        {
            txtName.Text = string.Empty;
        }
        #endregion

    }
}