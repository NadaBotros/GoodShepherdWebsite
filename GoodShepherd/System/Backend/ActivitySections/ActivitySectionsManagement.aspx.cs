using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Web.UI.WebControls;
using DAL;
namespace System.Backend.manage
{
    public partial class ActivitySectionsManagement : MangeBackend
    {
        #region Variables
        DAL.ActivitySectionsManagement activitySectionsManagement;
        #endregion
        #region Property
        public string ActivitySectionId
        {
            set { ViewState["ActivitySectionId"] = value; }
            get { return ViewState["ActivitySectionId"] == null ? string.Empty : ViewState["ActivitySectionId"].ToString(); }
        }
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                ActivitySectionId = Request.QueryString["id"].ToString();
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
            Response.Redirect("ActivitySectionsManagement.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ActivitySectionsList.aspx");
        }
        #endregion
        #region Methods
        void Save()
        {
            string ImageFile = string.Empty;
            activitySectionsManagement = new DAL.ActivitySectionsManagement();
            #region Manage Item

            if (string.IsNullOrEmpty(ActivitySectionId))
            {
                ActivitySectionId = activitySectionsManagement.Add(drpActivity.SelectedValue, txtName.Text, txtNotes.Text,txtDate.Text,
                                                             Request.Cookies["UserWebsiteId"].Value);
                if (!string.IsNullOrEmpty(ActivitySectionId))
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
                if (activitySectionsManagement.Edit(ActivitySectionId, drpActivity.SelectedValue, txtName.Text, txtNotes.Text,txtDate.Text,
                                                 Request.Cookies["UserWebsiteId"].Value))
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
            if (!string.IsNullOrEmpty(ActivitySectionId))
            {
                activitySectionsManagement = new DAL.ActivitySectionsManagement();
                DAL.ActivitySection activitySection = activitySectionsManagement.LoadById(ActivitySectionId);
                if (activitySection != null)
                {
                    drpActivity.DataBind();
                    drpActivity.SelectedValue = activitySection.ActivityId.ToString();
                    txtName.Text = activitySection.SectionTitle;
                    txtNotes.Text = activitySection.SectionDesc;
                    if (activitySection.SectionDate != null)
                        txtDate.Text = activitySection.SectionDate.Value.ToString("d/M/yyyy");
                    else
                    {
                        txtDate.Text = string.Empty;
                    }
                }
            }

        }
        void Clear()
        {
            txtName.Text = txtDate.Text = txtNotes.Text;
        }
        #endregion

        protected void drpActivity_DataBound(object sender, EventArgs e)
        {
            drpActivity.Items.Insert(0, new ListItem("اختر النشاط", string.Empty));
        }

    }
}