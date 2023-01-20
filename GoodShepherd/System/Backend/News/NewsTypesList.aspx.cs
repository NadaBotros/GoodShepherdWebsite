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
    public partial class NewsTypesList : MangeBackend
    {
        #region Variables
        DAL.NewsTypesManagement NewsTypesManagementObj;
        #endregion
        #region EventHanlder
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            NewsTypesManagementObj = new NewsTypesManagement();
            switch (e.CommandName)
            {
                case "restoreitem":
                    if (NewsTypesManagementObj.Restore(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { grdData.DataBind(); }
                    break;
                case "deleteitem":
                    if (NewsTypesManagementObj.Delete(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { grdData.DataBind(); }
                    break;
                case "ArrowDown":
                    NewsTypesManagementObj.ReOrder(e.CommandArgument.ToString(), false, Request.Cookies["UserWebsiteId"].Value, bool.Parse(drpViews.SelectedValue));
                    grdData.DataBind();
                    break;
                case "ArrowUp":
                    NewsTypesManagementObj.ReOrder(e.CommandArgument.ToString(), true, Request.Cookies["UserWebsiteId"].Value, bool.Parse(drpViews.SelectedValue));
                    grdData.DataBind();
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
                string RowId = DataBinder.Eval(e.Row.DataItem, "NewsTypeId").ToString();
                string Location = ResolveUrl("NewsTypesManagement.aspx") + "?id=" + RowId;
                e.Row.Attributes["onClick"] = string.Format("javascript:window.location='{0}';", Location);
                e.Row.Style["cursor"] = "pointer";
            }
        }
        protected void drpExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExportFile(grdData, drpExport.SelectedValue, true);
        }
        public string ShowArrow(object Recorder, string Type)
        {
            NewsTypesManagementObj = new NewsTypesManagement();
            int RowsCount = NewsTypesManagementObj.LoadItemsCount(bool.Parse(drpViews.SelectedValue));
            return MangeShowArrow(Recorder, Type, RowsCount);
        }
        #endregion
    }
}