using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Web.UI.WebControls;
using DAL;
namespace System.Backend.manage
{
    public partial class ServantVisitsManagement : MangeBackend
    {
        #region Variables
        DAL.ServantVisitsManagement _ServantVisitsManagement;
        #endregion
        #region Property
        public string ServantVisitId
        {
            set { ViewState["ServantVisitId"] = value; }
            get { return ViewState["ServantVisitId"] == null ? string.Empty : ViewState["ServantVisitId"].ToString(); }
        }
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                ServantVisitId = Request.QueryString["id"].ToString();
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
            Response.Redirect("ServantVisitsManagement.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ServantVisitList.aspx");
        }
        #endregion
        #region Methods
        void Save()
        {            
            _ServantVisitsManagement = new DAL.ServantVisitsManagement();
            #region Manage Item

            if (string.IsNullOrEmpty(ServantVisitId))
            {
                ServantVisitId = _ServantVisitsManagement.Add(drpServant.SelectedValue, drpPerson.SelectedValue, txtVisitDate.Text, txtNotes.Text, radNotesType.SelectedValue, txtReminder.Text, Request.Cookies["UserWebsiteId"].Value);
                if (!string.IsNullOrEmpty(ServantVisitId))
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
                if (_ServantVisitsManagement.Edit(ServantVisitId, drpServant.SelectedValue, drpPerson.SelectedValue, txtVisitDate.Text, txtNotes.Text, radNotesType.SelectedValue, txtReminder.Text, Request.Cookies["UserWebsiteId"].Value))
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
            if (!string.IsNullOrEmpty(ServantVisitId))
            {
                _ServantVisitsManagement = new DAL.ServantVisitsManagement();
                DAL.Prg_ServantVisit ent = _ServantVisitsManagement.LoadById(ServantVisitId);
                if (ent != null)
                {
                    drpServant.DataBind();
                    drpServant.SelectedValue = ent.ServantId.ToString();
                    drpPerson.DataBind();
                    drpPerson.SelectedValue = ent.PersonId.ToString();
                    txtNotes.Text = ent.VisitNotes;
                    radNotesType.SelectedValue = ent.ImpotantNotes.Value.ToString();                    
                    if (ent.ReminderDate != null)
                        txtReminder.Text = ent.ReminderDate.Value.ToShortDateString();
                    else
                        txtReminder.Text = string.Empty;
                    if (ent.VisitDate != null)
                        txtVisitDate.Text = ent.VisitDate.ToShortDateString();
                    else
                        txtVisitDate.Text = string.Empty;
                }
            }
        }
        void Clear()
        {
            txtNotes.Text = txtReminder.Text = txtVisitDate.Text = string.Empty;
            drpServant.SelectedIndex = drpPerson.SelectedIndex = -1;
        }
        #endregion

        protected void drpServant_DataBound(object sender, EventArgs e)
        {
            drpServant.Items.Insert(0,new ListItem("اختر اسم الخادم",string.Empty));
        }

        protected void drpPerson_DataBound(object sender, EventArgs e)
        {
            drpPerson.Items.Insert(0, new ListItem("اختر اسم المخدوم", string.Empty));
        }

    }
}