using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

    public partial class SoundLibraryDetails : System.Web.UI.Page
    {
        public string LibraryId
        {
            set { ViewState["LibraryId"] = value; }
            get { return ViewState["LibraryId"] == null ? string.Empty : ViewState["LibraryId"].ToString(); }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null && !IsPostBack)
            {
                var libaryFileManage = new LibraryFilesManage();
                var libaryFile = libaryFileManage.LoadById(Request.QueryString["id"]);
                if (libaryFile != null)
                {

                    lblDate.Text = libaryFile.FileDate == null
                                       ? string.Empty
                                       : "تاريخ الملف  : " + libaryFile.FileDate.Value.ToString("dd-MM-yyyy");
                    LibraryId = libaryFile.LibraryItemId.ToString();
                    lblNotes.Text = libaryFile.FileDesc;
                    lblTitle.Text = Page.Title = libaryFile.Library.ItemTitle + " | " + libaryFile.FileTitle;
                    lblOwner.Text = libaryFile.FileOwner;
                    odsPath.SelectParameters["id"].DefaultValue = libaryFile.LibraryItemId.ToString();
                    rptLibrary.DataBind();
                    if (!string.IsNullOrEmpty(libaryFile.FileName) && File.Exists(Server.MapPath("~/files/audio/" + libaryFile.FileName)))
                    {
                        lnkDownlaod.NavigateUrl = "~/files/audio/" + libaryFile.FileName;
                        lnkDownlaod.Visible = true;
                        lblMediaPlayer.Text = GeneralMethods.MediaPlayer("files/audio/" + libaryFile.FileName, 300, 65);
                    }
                    else
                    {
                        lnkDownlaod.NavigateUrl = string.Empty;
                        lnkDownlaod.Visible = false;
                        lblMediaPlayer.Text = "الملف غير متوافر الان";
                    }
                    lblFaceBookComment.Text = " <div id='fb-root'></div><script src='http://connect.facebook.net/en_US/all.js#xfbml=1'></script><fb:comments href='http://shepherdmeeting.com/shepherdmeeting.com/SoundLibraryDetails.aspx?id=" + libaryFile.FileId + "' num_posts='50' width='670px'></fb:comments>";
                }
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
                string Location = "SoundLibrary.aspx?id=" + RowId;
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
    }
