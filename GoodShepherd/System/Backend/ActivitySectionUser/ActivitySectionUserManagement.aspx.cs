using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Web.UI.WebControls;
using DAL;
namespace System.Backend.manage
{
    public partial class ActivitySectionUserManagement : MangeBackend
    {
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {


        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            List<string> Persons = new List<string>();
            foreach (GridViewRow row in grdData.Rows)
            {
                CheckBox chk = row.FindControl("chkItem") as CheckBox;
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        Label lblId = row.FindControl("lblId") as Label;
                        if (!string.IsNullOrEmpty(lblId.Text))
                            Persons.Add(lblId.Text);
                    }
                }
            }
            DAL.ActivitySectionUserManagement obj = new DAL.ActivitySectionUserManagement();
            string msg = obj.Add(Persons, drpActivitySection.SelectedValue, txtNotes.Text, Request.Cookies["UserWebsiteId"].Value);
            if (string.IsNullOrEmpty(msg))
            { BackendMessages(101); }
            else
            {
                BackendMessages(301);
                lblMessge.Text = "تم اضافة الاشخاص قبل ذلك في هذة الفقرة   :- <br>" + msg;
            }
            grdData.DataSource = null;
            grdData.DataBind();
        }

        #endregion
        protected void btnCodesSearch_Click(object sender, EventArgs e)
        {
            DAL.ActivityUsersManagement obj = new DAL.ActivityUsersManagement();
            grdData.DataSource = obj.SearchByCodes(txtCodes.Text,drpActivity.SelectedValue);
            grdData.DataBind();
        }

        protected void btnGeneralSearch_Click(object sender, EventArgs e)
        {
            DAL.ActivityUsersManagement obj = new DAL.ActivityUsersManagement();
            grdData.DataSource = obj.SearchGeneral(txtSearch.Text,drpActivity.SelectedValue);
            grdData.DataBind();
        }




    }
}