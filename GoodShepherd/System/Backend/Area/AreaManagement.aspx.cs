using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Web.UI.WebControls;
using DAL;
namespace System.Backend.manage
{
    public partial class AreaManagement : MangeBackend
    {
        #region Variables
        DAL.AreaManagement _AreaManagement;
        #endregion
        #region Property
        public string AreaId
        {
            set { ViewState["AreaId"] = value; }
            get { return ViewState["AreaId"] == null ? string.Empty : ViewState["AreaId"].ToString(); }
        }
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                AreaId = Request.QueryString["id"].ToString();
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
            Response.Redirect("AreaManagement.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AreaList.aspx");
        }
        #endregion
        #region Methods
        void Save()
        {
            string ImageFile = string.Empty;
            _AreaManagement = new DAL.AreaManagement();
            #region Manage Item
            if (!_AreaManagement.CheckIfExists(AreaId,drpCity.SelectedValue, txtName.Text))
            {
                if (string.IsNullOrEmpty(AreaId))
                {
                    AreaId = _AreaManagement.Add(drpCity.SelectedValue,txtName.Text, Request.Cookies["UserWebsiteId"].Value);
                    if (!string.IsNullOrEmpty(AreaId))
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
                    if (_AreaManagement.Edit(AreaId,drpCity.SelectedValue, txtName.Text, Request.Cookies["UserWebsiteId"].Value))
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
            if (!string.IsNullOrEmpty(AreaId))
            {
                _AreaManagement = new DAL.AreaManagement();
                DAL.Prg_Area Prg_AreaEnt = _AreaManagement.LoadById(AreaId);
                if (Prg_AreaEnt != null)
                {
                    drpCity.DataBind();
                    if (drpCity.Items.FindByValue(Prg_AreaEnt.CityId.ToString()) != null)
                        drpCity.SelectedValue = Prg_AreaEnt.CityId.ToString();
                    else
                        drpCity.SelectedIndex = -1;
                    txtName.Text = Prg_AreaEnt.AreaName;
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