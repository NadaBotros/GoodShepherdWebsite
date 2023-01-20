using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

using System.Web.UI.WebControls;
using DAL;
namespace System.Backend.manage
{
    public partial class PersonAttendManagement : MangeBackend
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
            DAL.PersonAttendManagement obj = new DAL.PersonAttendManagement();
            obj.Add(Persons, txtDate.Text, Request.Cookies["UserWebsiteId"].Value);
            BackendMessages(101);
            grdData.DataSource = null;
            grdData.DataBind();
        }

        #endregion


        protected void btnCodesSearch_Click(object sender, EventArgs e)
        {
            DAL.PersonAttendManagement obj = new DAL.PersonAttendManagement();
            grdData.DataSource = obj.SearchNotAddedByCodes(txtCodes.Text, txtDate.Text);
            grdData.DataBind();
        }

        protected void btnGeneralSearch_Click(object sender, EventArgs e)
        {
            DAL.PersonAttendManagement obj = new DAL.PersonAttendManagement();
            grdData.DataSource = obj.SearchGeneralNotAdded(txtSearch.Text, txtDate.Text);
            grdData.DataBind();
        }




    }
}