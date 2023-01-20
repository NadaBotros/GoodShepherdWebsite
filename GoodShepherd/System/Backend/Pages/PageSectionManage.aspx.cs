using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace System.Backend.Pages
{
    public partial class PageSectionManage : MangeBackend
    {
        #region Variables
        DAL.PageSectionManagement _PageSectionManagement;
        #endregion
        #region Property
        public string PageSectionId
        {
            set { ViewState["PageSectionId"] = value; }
            get { return ViewState["PageSectionId"] == null ? string.Empty : ViewState["PageSectionId"].ToString(); }
        }
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                PageSectionId = Request.QueryString["id"].ToString();
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
            Response.Redirect("PageSectionManage.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("PageSectionList.aspx");
        }
        #endregion
        #region Methods
        void Save()
        {
            string ImageFile = string.Empty;
            _PageSectionManagement = new PageSectionManagement();
            #region Manage Item
            string FileName = string.Empty;
            if (fpld.PostedFile.FileName != "")
            {
                FileName = "Section" + Guid.NewGuid().ToString().Replace("-", "") + System.IO.Path.GetExtension(fpld.PostedFile.FileName);
                string PathUrl = Server.MapPath("~/Images/ActualSize/") + FileName;
                fpld.SaveAs(PathUrl);
                DAL.ImagesFact.ResizeWithCropResizeImage("", FileName, "Section");
            }
            if (string.IsNullOrEmpty(PageSectionId))
            {
                PageSectionId = _PageSectionManagement.Add(drpPageName.SelectedValue,txtPageTitle.Text,FileName,edContent.Content,txtYoutube.Text, Request.Cookies["UserWebsiteId"].Value);
                if (!string.IsNullOrEmpty(PageSectionId))
                {
                    BackendMessages(101);
                }
                else
                {
                    BackendMessages(201);
                }
            }
            else
            {
                if (_PageSectionManagement.Edit(PageSectionId,drpPageName.SelectedValue,txtPageTitle.Text,FileName,edContent.Content,txtYoutube.Text, Request.Cookies["UserWebsiteId"].Value))
                {
                    lblMessge.Text = "Done, changes has been saved successfully!";
                    msg.Attributes["class"] = "msg-success";
                }
                else
                {
                    lblMessge.Text = "Error, Please try again later!";
                    msg.Attributes["class"] = "msg-error";
                }
            }
            #endregion
        }
        void GetInfo()
        {
            if (!string.IsNullOrEmpty(PageSectionId))
            {
                _PageSectionManagement = new PageSectionManagement();
                DAL.PageSection PageSectionEnt = _PageSectionManagement.LoadById(PageSectionId);
                if (PageSectionEnt != null)
                {
                    txtPageTitle.Text = PageSectionEnt.SectionName;
                    txtYoutube.Text = PageSectionEnt.VideoUrl;
                    if (!string.IsNullOrEmpty(PageSectionEnt.ImageFile))
                        imgSection.ImageUrl = "~/images/S150_150/" + PageSectionEnt.ImageFile;
                    else
                        imgSection.Visible = false;
                    edContent.Content = PageSectionEnt.SectionContent;
                    try
                    {
                        drpPageName.SelectedValue = PageSectionEnt.PageId.ToString();
                    }
                    catch { drpPageName.SelectedIndex = -1; }
                }
            }

        }
        void Clear()
        {
            txtPageTitle.Text = edContent.Content=txtYoutube.Text = string.Empty;
            drpPageName.SelectedIndex = -1;
        }
        #endregion
    }
}