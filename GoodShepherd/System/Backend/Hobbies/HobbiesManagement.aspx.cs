using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Web.UI.WebControls;
using DAL;
namespace System.Backend.manage
{
    public partial class HobbiesManagement : MangeBackend
    {
        #region Variables
        DAL.HobbiesManagement _HobbiesManagement;
        #endregion
        #region Property
        public string HobbyId
        {
            set { ViewState["HobbyId"] = value; }
            get { return ViewState["HobbyId"] == null ? string.Empty : ViewState["HobbyId"].ToString(); }
        }
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                HobbyId = Request.QueryString["id"].ToString();
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
            Response.Redirect("HobbiesManagement.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("HobbiesList.aspx");
        }
        #endregion
        #region Methods
        void Save()
        {
            string ImageFile = string.Empty;
            _HobbiesManagement = new DAL.HobbiesManagement();
            #region Manage Item
            if (!_HobbiesManagement.CheckIfExists(HobbyId, txtName.Text))
            {
                if (string.IsNullOrEmpty(HobbyId))
                {
                    HobbyId = _HobbiesManagement.Add(txtName.Text, Request.Cookies["UserWebsiteId"].Value);
                    if (!string.IsNullOrEmpty(HobbyId))
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
                    if (_HobbiesManagement.Edit(HobbyId, txtName.Text, Request.Cookies["UserWebsiteId"].Value))
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
            if (!string.IsNullOrEmpty(HobbyId))
            {
                _HobbiesManagement = new DAL.HobbiesManagement();
                DAL.Prg_Hobby HobbyEnt = _HobbiesManagement.LoadById(HobbyId);
                if (HobbyEnt != null)
                {
                    txtName.Text = HobbyEnt.HobbyName;
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