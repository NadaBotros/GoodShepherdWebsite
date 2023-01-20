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
    public partial class LibraryList : MangeBackend
    {
        #region Variables
        LibraryManage LibraryManageObj;
        #endregion
        #region EventHanlder
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    LibraryManageObj = new LibraryManage();
                    Library ent = LibraryManageObj.LoadById(Request.QueryString["id"].ToString());
                    if (ent != null)
                    {
                        if (!string.IsNullOrEmpty(ent.ParentItemId.ToString()))
                        {
                            lnknUp.NavigateUrl = "LibraryList.aspx?type=" + Request.QueryString["type"].ToString() + "&id=" + ent.ParentItemId;
                            lnknUp.Visible = true;
                        }
                        else
                        {
                            lnknUp.NavigateUrl = "LibraryList.aspx?type=" + Request.QueryString["type"].ToString();
                            lnknUp.Visible = true;
                        }
                    }
                    else
                        lnknUp.Visible = false;
                }
                btnAddNew.NavigateUrl = "LibraryManagement.aspx?type=" + Request.QueryString["type"].ToString();
                switch (Request.QueryString["type"].ToString())
                {
                    case "1":
                        lblPageMainTitle.Text = "اقسام المكتبة الصوتية";
                        lblPageSubTitle.Text = "قائمة اقسام المكتبة الصوتية";
                        break;
                    case "2":
                        lblPageMainTitle.Text = "اقسام المكتبة المرئية";
                        lblPageSubTitle.Text = "قائمة اقسام المكتبة المرئية";
                        break;
                    case "3":
                        lblPageMainTitle.Text = "اقسام المكتبة الكتابية";
                        lblPageSubTitle.Text = "قائمة اقسام المكتبة الكتابية";
                        break;
                }
            }
        }
        protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LibraryManageObj = new LibraryManage();
            switch (e.CommandName)
            {
                case "restoreitem":
                    if (LibraryManageObj.Restore(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { grdData.DataBind(); }
                    break;
                case "deleteitem":
                    if (LibraryManageObj.Delete(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { grdData.DataBind(); }
                    break;
                case "Edititem":
                    Response.Redirect("LibraryManagement.aspx?id=" + e.CommandArgument.ToString() + "&type=" + Request.QueryString["type"].ToString());
                    break;
                    
                case "ArrowDown":
                    LibraryManageObj.ReOrder(e.CommandArgument.ToString(), false, Request.Cookies["UserWebsiteId"].Value, bool.Parse(drpViews.SelectedValue));
                    grdData.DataBind();
                    break;
                case "ArrowUp":
                    LibraryManageObj.ReOrder(e.CommandArgument.ToString(), true, Request.Cookies["UserWebsiteId"].Value, bool.Parse(drpViews.SelectedValue));
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
                string RowId = DataBinder.Eval(e.Row.DataItem, "LibraryItemId").ToString();
                string Location = ResolveUrl("LibraryList.aspx") + "?type=" + Request.QueryString["type"].ToString() + "&id=" + RowId;
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
            LibraryManageObj = new LibraryManage();
            int RowsCount = LibraryManageObj.LoadItemsCount(bool.Parse(drpViews.SelectedValue), ParentLibraryId, Request.QueryString["type"].ToString());
            return MangeShowArrow(Recorder, Type, RowsCount);
        }
    }
}