using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using DAL;
namespace System.Backend.manage
{
    public partial class NewsManagement : MangeBackend
    {
        #region Variables
        DAL.NewsManagement _newsManagement;
        #endregion
        #region Property
        public string NewsId
        {
            set { ViewState["NewsId"] = value; }
            get { return ViewState["NewsId"] == null ? string.Empty : ViewState["NewsId"].ToString(); }
        }
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                NewsId = Request.QueryString["id"].ToString();
            }
            if (!IsPostBack)
                GetInfo();

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            GetInfo();
        }
        protected void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            Save();
            Response.Redirect("NewsManagement.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("NewsList.aspx");
        }
        #endregion
        #region Methods
        void Save()
        {
            string ImageFile = string.Empty;
            _newsManagement = new DAL.NewsManagement();
            #region Save Image
            if (fupldImage.PostedFile.FileName != "")
            {
                string[] y = fupldImage.PostedFile.ContentType.Split('/');
                if (y[0] == "image")
                {
                    Guid id = Guid.NewGuid();
                    ImageFile = "News" + id.ToString().Replace("-", "") + System.IO.Path.GetExtension(fupldImage.PostedFile.FileName);
                    fupldImage.PostedFile.SaveAs(Server.MapPath("~/Images/ActualSize/" + ImageFile));
                    ImagesFact.ResizeWithCropResizeImage("", ImageFile, "News");
                }
            }
            #endregion
            #region Manage Item
            if (string.IsNullOrEmpty(NewsId))
            {
                if (string.IsNullOrEmpty(ImageFile))
                {
                    BackendMessages(201);
                    lblMessge.Text = "لابد من تحميل صورة الخبر";
                }
                else
                {
                    NewsId = _newsManagement.Add(txtTitle.Text, ImageFile, EditorNews.Content, drpNewsType.SelectedValue, radShowInGallery.SelectedValue, radShowInNewsBar.SelectedValue, Request.Cookies["UserWebsiteId"].Value);
                    if (!string.IsNullOrEmpty(NewsId))
                    {
                        BackendMessages(101);
                    }
                    else
                    {
                        BackendMessages(201);
                    }
                }
            }
            else
            {
                if (_newsManagement.Edit(NewsId, txtTitle.Text, ImageFile, EditorNews.Content, drpNewsType.SelectedValue, radShowInGallery.SelectedValue, radShowInNewsBar.SelectedValue, Request.Cookies["UserWebsiteId"].Value))
                {
                    BackendMessages(101);
                }
                else
                {
                    BackendMessages(201);
                }
            }
            #endregion
        }
        void GetInfo()
        {
            if (!string.IsNullOrEmpty(NewsId))
            {
                _newsManagement = new DAL.NewsManagement();
                DAL.New NewsEnt = _newsManagement.LoadById(NewsId);
                if (NewsEnt != null)
                {
                    drpNewsType.DataBind();
                    txtTitle.Text = NewsEnt.NewsTitle;
                    EditorNews.Content = NewsEnt.NewsContent;

                    if (drpNewsType.Items.FindByValue(NewsEnt.NewsTypeId.ToString()) != null)
                        drpNewsType.SelectedValue = NewsEnt.NewsTypeId.ToString();
                    else
                        drpNewsType.SelectedIndex = -1;

                    radShowInGallery.SelectedValue = NewsEnt.ShowInGallery.ToString();
                    radShowInNewsBar.SelectedValue = NewsEnt.ShowInNewsBar.ToString();
                    if (!string.IsNullOrEmpty(NewsEnt.NewsImage))
                        imgNews.ImageUrl = "~/images/S250_250/" + NewsEnt.NewsImage;

                }
            }

        }
        void Clear()
        {
            txtTitle.Text = EditorNews.Content = string.Empty;
            drpNewsType.SelectedIndex = radShowInNewsBar.SelectedIndex = radShowInGallery.SelectedIndex = 0;
        }
        #endregion

        //protected void HTMLEditorExtender_ImageUploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        //{
        //    string ImageName = "Page" + Guid.NewGuid().ToString() + Path.GetExtension(e.FileName);
        //    string fullpath = Server.MapPath("~/images/ActualSize/") + ImageName;
        //    HTMLEditorExtender.AjaxFileUpload.SaveAs(fullpath);
        //    ImagesFact.ResizeWithCropResizeImage("", ImageName, "Page");
        //    e.PostedUrl = Page.ResolveUrl("~/images/S500_500/" + ImageName);
        //}
    }
}