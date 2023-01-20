using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace System.Backend.manage
{
    public partial class ServantAftkadManagement : MangeBackend
    {
        #region Variables
        DAL.ServantAftkadManagement _ServantAftkadManagement;
        #endregion
        #region Property
        public string ServantId
        {
            set { ViewState["ServantId"] = value; }
            get { return ViewState["ServantId"] == null ? string.Empty : ViewState["ServantId"].ToString(); }
        }
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                ServantId = Request.QueryString["id"].ToString();
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
            Response.Redirect("ServantAftkadManagement.aspx");
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ServantAftkadList.aspx");
        }
        #endregion
        #region Methods
        void Save()
        {
            _ServantAftkadManagement = new DAL.ServantAftkadManagement();
            if (string.IsNullOrEmpty(ServantId))
            {

                #region Manage Item
                if (!_ServantAftkadManagement.IfServantExists(lstServants.SelectedValue))
                {
                    string SId = _ServantAftkadManagement.Add(lstServants.SelectedValue, txtServices.Text, radServantType.SelectedValue, Request.Cookies["UserWebsiteId"].Value);
                    if (!string.IsNullOrEmpty(SId))
                    {
                        BackendMessages(101);
                        Search();
                    }
                    else
                    {
                        BackendMessages(201);
                    }
                }
                else
                { BackendMessages(301); lblMessge.Text = "تم اضافة الخادم سابقا"; }
            }
            else
            {
                if (_ServantAftkadManagement.Edit(ServantId, lstServants.SelectedValue, txtServices.Text, radServantType.SelectedValue, Request.Cookies["UserWebsiteId"].Value))
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
        #endregion
        void Search()
        {
            _ServantAftkadManagement = new DAL.ServantAftkadManagement();
            lstServants.DataSource = _ServantAftkadManagement.SearchServant(txtSearch.Text);
            lstServants.DataTextField = "PersonName";
            lstServants.DataValueField = "PersonId";
            lstServants.DataBind();
        }
        void GetInfo()
        {
            if (!string.IsNullOrEmpty(ServantId))
            {
                _ServantAftkadManagement = new DAL.ServantAftkadManagement();
                DAL.Prg_Servant ServantEnt = _ServantAftkadManagement.LoadById(ServantId);
                if (ServantEnt != null)
                {

                    txtServices.Text = ServantEnt.Prg_Person.PersonName;
                    lstServants.Items.Clear();
                    ListItem item = new ListItem(ServantEnt.Prg_Person.PersonName, ServantEnt.Prg_Person.PersonId.ToString());
                    item.Selected = true;
                    lstServants.Items.Add(item);
                    radServantType.SelectedValue = ServantEnt.IsServantAftkad.Value.ToString();
                    txtServices.Text = ServantEnt.Services;

                }
            }

        }
        protected void btnSearch_Click(object sender, EventArgs e)
        {
            Search();
        }


    }
}