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
    public partial class PageTreeList : MangeBackend
    {
        #region Variables
        SiteTreeManage SiteTreeManageObj;
        #endregion
        #region EventHanlder
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    SiteTreeManageObj = new SiteTreeManage();
                    SiteTree ent = SiteTreeManageObj.LoadById(Request.QueryString["id"].ToString());
                    if (ent != null)
                    {
                        if (!string.IsNullOrEmpty(ent.ParentSiteTreeId.ToString()))
                        {
                            lnknUp.NavigateUrl = "PageTreeList.aspx?id=" + ent.ParentSiteTreeId;
                            lnknUp.Visible = true;
                        }
                        else
                        {
                            lnknUp.NavigateUrl = "PageTreeList.aspx";
                            lnknUp.Visible = true;
                        }
                    }
                    else
                        lnknUp.Visible = false;
                }
            }
        }
        protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            SiteTreeManageObj = new SiteTreeManage();
            switch (e.CommandName)
            {
                case "restoreitem":
                    if (SiteTreeManageObj.Restore(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { grdData.DataBind(); }
                    break;
                case "deleteitem":
                    if (SiteTreeManageObj.Delete(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { grdData.DataBind(); }
                    break;
                case "Edititem":
                    Response.Redirect("PageTreeManagement.aspx?id=" + e.CommandArgument.ToString());
                    break;
                    
                case "ArrowDown":
                    SiteTreeManageObj.ReOrder(e.CommandArgument.ToString(), false, Request.Cookies["UserWebsiteId"].Value, bool.Parse(drpViews.SelectedValue));
                    grdData.DataBind();
                    break;
                case "ArrowUp":
                    SiteTreeManageObj.ReOrder(e.CommandArgument.ToString(), true, Request.Cookies["UserWebsiteId"].Value, bool.Parse(drpViews.SelectedValue));
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
                string RowId = DataBinder.Eval(e.Row.DataItem, "SiteTreeId").ToString();
                string Location = ResolveUrl("PageTreeList.aspx") + "?id=" + RowId;
                e.Row.Attributes["onClick"] = string.Format("javascript:window.location='{0}';", Location);
                e.Row.Style["cursor"] = "pointer";
            }
        }
        protected void drpExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExportFile(grdData, drpExport.SelectedValue, true);
        }
        #endregion

      
        public string ShowArrow(object Recorder, string Type)
        {
            string ParentLibraryId = string.Empty;
            if (Request.QueryString["id"] != null)
                ParentLibraryId = Request.QueryString["id"].ToString();
            SiteTreeManageObj = new SiteTreeManage();
            int RowsCount = SiteTreeManageObj.LoadItemsCount(bool.Parse(drpViews.SelectedValue),  ParentLibraryId);
            return MangeShowArrow(Recorder, Type, RowsCount);
        }
    }
}