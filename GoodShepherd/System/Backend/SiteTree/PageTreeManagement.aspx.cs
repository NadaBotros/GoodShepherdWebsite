using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
namespace System.Backend.manage
{
    public partial class PageTreeManagement : MangeBackend
    {
        #region Variables
        SiteTreeManage _SiteTreeManage;
        #endregion
        #region Property
        public string PageTreeId
        {
            set { ViewState["PageTreeId"] = value; }
            get { return ViewState["PageTreeId"] == null ? string.Empty : ViewState["PageTreeId"].ToString(); }
        }

        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    PageTreeId = Request.QueryString["id"].ToString();
                }
                GetInfo();
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            GetInfo();
        }
        protected void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            Save();
            Response.Redirect("PageTreeManagement.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PageTreeList.aspx");
        }


        #endregion
        #region Methods
        void Save()
        {
            _SiteTreeManage = new SiteTreeManage();
            #region Manage Item
            if (string.IsNullOrEmpty(PageTreeId))
            {
                PageTreeId = _SiteTreeManage.Add(drpCategory.SelectedValue, txtName.Text, txtPageFile.Text, Request.Cookies["UserWebsiteId"].Value);
                if (!string.IsNullOrEmpty(PageTreeId))
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
                if (_SiteTreeManage.Edit(PageTreeId, drpCategory.SelectedValue, txtName.Text, txtPageFile.Text, Request.Cookies["UserWebsiteId"].Value))
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
            if (!string.IsNullOrEmpty(PageTreeId))
            {
                _SiteTreeManage = new SiteTreeManage();
                SiteTree SiteTreeEnt = _SiteTreeManage.LoadById(PageTreeId);
                if (SiteTreeEnt != null)
                {
                    drpCategory.DataBind();
                    txtName.Text = SiteTreeEnt.PageTitle;
                    txtPageFile.Text = SiteTreeEnt.PageFile;
                    if (SiteTreeEnt.ParentSiteTreeId != null)
                        drpCategory.SelectedValue = SiteTreeEnt.ParentSiteTreeId.ToString();
                    else
                        drpCategory.SelectedValue = "";
                }
            }
        }
        void Clear()
        {
            txtName.Text = txtPageFile.Text = string.Empty;
            drpCategory.SelectedValue = string.Empty;
        }
        #endregion



        protected void drpCategory_DataBound(object sender, EventArgs e)
        {
            drpCategory.Items.Insert(0, new ListItem(""));
        }

    }
}