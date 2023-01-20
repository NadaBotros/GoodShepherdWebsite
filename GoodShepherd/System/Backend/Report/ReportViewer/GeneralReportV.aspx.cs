using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace System.Backend
{
    public partial class GeneralReportV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["lstFields"] != null && Session["lstIds"] != null && Request.QueryString["orderby"] != null)
            {
                ListBox lstFields = Session["lstFields"] as ListBox;
                List<string> cols = new List<string>();
                foreach (ListItem item in lstFields.Items)
                {
                    grdData.Columns.Add(new BoundField() { HeaderText = item.Text, DataField = item.Value });
                    cols.Add(item.Value);
                }
                DAL.ReportManagement obj = new ReportManagement();
                List<string> Ids = Session["lstIds"] as List<string>;
                grdData.DataSource = obj.GetByPersonsIds(Ids, cols, Request.QueryString["orderby"].ToString());
                grdData.DataBind();
            }
            fillAttendCount();
        }

        protected void grdData_DataBound(object sender, EventArgs e)
        {

        }
        void fillAttendCount()
        {
            grdData.DataBind();
            lblAttendAllNo.Text = "   اجمالي العدد: - " + grdData.Rows.Count;
        }
    }
}