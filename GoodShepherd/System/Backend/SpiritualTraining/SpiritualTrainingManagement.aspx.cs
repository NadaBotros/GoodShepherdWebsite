using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace System.Backend.manage
{
    public partial class SpiritualTrainingManagement : MangeBackend
    {
        #region Variables
        SpiritualTrainingManage _SpiritualTrainingManage;
        #endregion
        #region Property
        public string SpiritualTrainingId
        {
            set { ViewState["SpiritualTrainingId"] = value; }
            get { return ViewState["SpiritualTrainingId"] == null ? string.Empty : ViewState["SpiritualTrainingId"].ToString(); }
        }
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                SpiritualTrainingId = Request.QueryString["id"].ToString();
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
            Response.Redirect("SpiritualTrainingManagement.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SpiritualTrainingList.aspx");
        }
        #endregion
        #region Methods
        void Save()
        {
            string ImageFile = string.Empty;
            _SpiritualTrainingManage = new SpiritualTrainingManage();
            #region Manage Item
            if (string.IsNullOrEmpty(SpiritualTrainingId))
            {
                SpiritualTrainingId = _SpiritualTrainingManage.Add(txtName.Text, txtDesc.Text, Request.Cookies["UserWebsiteId"].Value);
                if (!string.IsNullOrEmpty(SpiritualTrainingId))
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
                if (_SpiritualTrainingManage.Edit(SpiritualTrainingId, txtName.Text, txtDesc.Text, Request.Cookies["UserWebsiteId"].Value))
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
            if (!string.IsNullOrEmpty(SpiritualTrainingId))
            {
                _SpiritualTrainingManage = new SpiritualTrainingManage();
                SpiritualTraining SpiritualTrainingEnt = _SpiritualTrainingManage.LoadById(SpiritualTrainingId);
                if (SpiritualTrainingEnt != null)
                {
                    txtName.Text = SpiritualTrainingEnt.SpiritualTrainingTitle;
                    txtDesc.Text = SpiritualTrainingEnt.SpiritualTrainingDesc;
                }
            }
        }
        void Clear()
        {
            txtName.Text = txtDesc.Text = string.Empty;
        }
        #endregion

    }
}