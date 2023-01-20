using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using DAL;
namespace System.Backend
{
    public partial class PersonAttendList : MangeBackend
    {
        #region Variables
        DAL.ServantVisitsManagement ServantVisitsManagementObj;
        #endregion
        #region EventHanlder
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                drpDates.DataBind();
                fillAttendCount();
            }
        }
        void fillAttendCount()
        {
            grdData.DataBind();
            lblAttendNo.Text = "عدد حاضرين الاجتماع : - " + grdData.Rows.Count;
        }
        protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ServantVisitsManagementObj = new ServantVisitsManagement();
            switch (e.CommandName)
            {
                case "deleteitem":
                    if (ServantVisitsManagementObj.Delete(e.CommandArgument.ToString()))
                    { grdData.DataBind(); }
                    break;
            }
        }
        protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == 0)
            {
                grdData.UseAccessibleHeader = true;
                grdData.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string RowId = DataBinder.Eval(e.Row.DataItem, "ServantVisitId").ToString();
                string Location = ResolveUrl("ServantVisitsManagement.aspx") + "?id=" + RowId;
                e.Row.Attributes["onClick"] = string.Format("javascript:window.location='{0}';", Location);
                e.Row.Style["cursor"] = "pointer";
            }
        }
        protected void drpExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExportFile(grdData, drpExport.SelectedValue, true);
        }       
        #endregion

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            List<string> AttendIds = new List<string>();
            foreach (GridViewRow row in grdData.Rows)
            {
                CheckBox chk = row.FindControl("chkItem") as CheckBox;
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        Label lblId = row.FindControl("lblId") as Label;
                        if (!string.IsNullOrEmpty(lblId.Text))
                            AttendIds.Add(lblId.Text);
                    }
                }
            }
            DAL.PersonAttendManagement obj = new PersonAttendManagement();
            obj.Delete(AttendIds);
            grdData.DataBind();
        }

        protected void drpDates_SelectedIndexChanged(object sender, EventArgs e)
        {
            fillAttendCount();
        }
    }
}