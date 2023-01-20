using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Backend.UserControls
{
    public partial class ucSmallSearch : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void btnCodesSearch_Click(object sender, EventArgs e)
        {
            DAL.PersonAttendManagement obj = new DAL.PersonAttendManagement();
            lstResult.DataSource = obj.SearchByCodes(txtCodes.Text);
            lstResult.DataBind();
        }

        protected void btnGeneralSearch_Click(object sender, EventArgs e)
        {
            DAL.PersonAttendManagement obj = new DAL.PersonAttendManagement();
            lstResult.DataSource = obj.SearchGeneral(txtSearch.Text);
            lstResult.DataBind();
        }
        public string GetSelectedId()
        {
            if (lstResult.SelectedIndex > -1)
            {
                return lstResult.SelectedValue;
            }
            return string.Empty;
        }
        public void Clear()
        {
            txtCodes.Text = txtSearch.Text = string.Empty;
            lstResult.Items.Clear();
        }
        public void AddPersonToList(string PersonId, string PersonName)
        {
            lstResult.Items.Clear();
            ListItem item = new ListItem(PersonName, PersonId);
            item.Selected = true;
            lstResult.Items.Add(item);
        }
    }
}