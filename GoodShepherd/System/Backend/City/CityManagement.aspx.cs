using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Web.UI.WebControls;
using DAL;
namespace System.Backend.manage
{
    public partial class CityManagement : MangeBackend
    {
        #region Variables
        DAL.CityManagement _CityManagement;
        #endregion
        #region Property
        public string CityId
        {
            set { ViewState["CityId"] = value; }
            get { return ViewState["CityId"] == null ? string.Empty : ViewState["CityId"].ToString(); }
        }
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                CityId = Request.QueryString["id"].ToString();
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
            Response.Redirect("CityManagement.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("CityList.aspx");
        }
        #endregion
        #region Methods
        void Save()
        {
            string ImageFile = string.Empty;
            _CityManagement = new DAL.CityManagement();
            #region Manage Item
            if (!_CityManagement.CheckIfExists(CityId, txtName.Text))
            {
                if (string.IsNullOrEmpty(CityId))
                {
                    CityId = _CityManagement.Add(txtName.Text, Request.Cookies["UserWebsiteId"].Value);
                    if (!string.IsNullOrEmpty(CityId))
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
                    if (_CityManagement.Edit(CityId, txtName.Text, Request.Cookies["UserWebsiteId"].Value))
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
            if (!string.IsNullOrEmpty(CityId))
            {
                _CityManagement = new DAL.CityManagement();
                DAL.Prg_City CityEnt = _CityManagement.LoadById(CityId);
                if (CityEnt != null)
                {
                    txtName.Text = CityEnt.CityName;
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