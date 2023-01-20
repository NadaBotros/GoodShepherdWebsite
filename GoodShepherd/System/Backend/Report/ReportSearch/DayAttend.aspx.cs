using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Backend
{
    public partial class DayAttend : System.Web.UI.Page
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
            string strUrl = "../ReportViewer/DayAttendRV.aspx?id=" + drpServant.SelectedValue + "&Date=" + drpDates.SelectedValue + "&Attend=" + radAttendType.SelectedValue;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + strUrl + "','_blank')", true);
        }

        protected void btnGmailExport_Click(object sender, EventArgs e)
        {
            var reportManagement = new ReportManagement();
            var personAttendManagement = new PersonAttendManagement();
            var ids = personAttendManagement.PersonsAttendReportAsList(drpServant.SelectedValue, drpDates.SelectedValue, radAttendType.SelectedValue);
            CSVExporter.WriteToCSV(reportManagement.GetByPersonsIds(ids));
        }
    }
}