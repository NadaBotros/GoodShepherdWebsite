using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Backend
{
    public partial class ActivityAttend : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void drpActivity_DataBound(object sender, EventArgs e)
        {
            drpActivity.Items.Insert(0, new ListItem("كل الانشطة", string.Empty));
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            var selectedSections =
                (from ListItem item in drpSections.Items where item.Selected select item.Value).ToArray();
            Session["SectionsIds"] = string.Join(",", selectedSections);
            string strUrl = "../ReportViewer/ActivityAttendRV.aspx?id=" + drpActivity.SelectedValue;
            //Response.Redirect(strUrl);
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "popup", "window.open('" + strUrl + "','_blank')", true);
        }
    }
}