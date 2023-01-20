using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Backend
{
    public partial class AttendBetweenDates : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void drpServant_DataBound(object sender, EventArgs e)
        {
            drpServant.Items.Insert(0, new ListItem("كل الاشخاص", string.Empty));
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            string strUrl = "../ReportViewer/AttendBetweenDatesRV.aspx?id=" + drpServant.SelectedValue + "&DateFrom=" + txtFrom.Text + "&DateTo=" + txtTo.Text;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + strUrl + "','_blank')", true);
        }
    }
}