using System.IO;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


    public partial class documentsLibrary : System.Web.UI.Page
    {
        public string LibraryId
        {
            set { ViewState["LibraryId"] = value; }
            get { return ViewState["LibraryId"] == null ? string.Empty : ViewState["LibraryId"].ToString(); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            GetInfo("المكتبة الكتابية", "3");
        }
        public void GetInfo(string PageTitle, string LId)
        {
            odsData.SelectParameters["LibraryType"].DefaultValue = LId;
            grdCategories.DataBind();
            if (grdCategories.Rows.Count > 0)
                lblMsg.Text = "";
            else
                lblMsg.Text = "لا يوجد بيانات في هذا القسم";
            LibraryId = LId;
            if (Request.QueryString["id"] != null && !string.IsNullOrEmpty(Request.QueryString["id"].ToString()))
            {
                LibraryManage obj = new LibraryManage();
                Library ent = obj.LoadById(Request.QueryString["id"].ToString());
                if (ent != null)
                {
                    lblTitle.Text = PageTitle + " - " + ent.ItemTitle;
                    Page.Title = "اجتماع الراعي الصالح | " + PageTitle + " - " + ent.ItemTitle;
                }
            }
            else
            {
                lblTitle.Text = PageTitle;
                Page.Title = "اجتماع الراعي الصالح | " + PageTitle;
            }
        }
        protected void rptLibrary_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Label lblId = e.Item.FindControl("lblId") as Label;
            ObjectDataSource ods = e.Item.FindControl("odsCatParent") as ObjectDataSource;
            ods.SelectParameters["LibraryType"].DefaultValue = LibraryId;
            ods.SelectParameters["ParentItemId"].DefaultValue = lblId.Text;
            ods.DataBind();
            GridView grdChildCats = e.Item.FindControl("grdChildCats") as GridView;
            grdChildCats.DataBind();
        }
        protected void grdCategories_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                string RowId = DataBinder.Eval(e.Row.DataItem, "LibraryItemId").ToString();
                string lnkPath = DataBinder.Eval(e.Row.DataItem, "ItemLink").ToString();
                string Location = "documentsLibrary.aspx?id=" + RowId;
                HyperLink lnk = e.Row.FindControl("lnkTitle") as HyperLink;
                if (!string.IsNullOrEmpty(lnkPath) && lnkPath.Length > 10)
                {
                    lnk.NavigateUrl = lnkPath;
                    lnk.Target = "_blank";
                }
                else
                {
                    lnk.NavigateUrl = Location;
                }
            }
        }
        public string FileSize(string FileName)
        {
            if (File.Exists(Server.MapPath("~/files/audio/" + FileName)))
            {
                FileInfo fnotes = new FileInfo(Server.MapPath("~/files/audio/" + FileName));
                long size = fnotes.Length;
                double newsize = 0;
                if (size < 1024)
                    return size + " بايت";
                else if (size >= 1024 && size < (1024 * 1024 - 1))
                {
                    newsize = size / (1024.0);
                    return Math.Round(newsize, 1) + " ك بايت";
                }
                else if (size >= 1024 * 1024 && size < (1024 * 1024 * 1024 - 1))
                {
                    newsize = size / (1024.0 * 1024.0);
                    return Math.Round(newsize, 1) + " ميجا بايت";
                }
                else if (size >= 1024 * 1024 * 1024)
                {
                    newsize = size / (1024.0 * 1024.0 * 1024.0);
                    return Math.Round(newsize, 1) + " جيجا بايت";
                }
                else
                    return string.Empty;
            }
            else
                return string.Empty;
        }
    }

