using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.IO;
namespace System.Backend
{
    public partial class HomeAlbumList : MangeBackend
    {
        #region Variables

        DAL.HomeGalleryManagement HomeGalleryManagementObj;
        #endregion
        #region Properties
        public string SliderId
        {
            set { ViewState["ImageId"] = value; }
            get { return ViewState["ImageId"] == null ? string.Empty : ViewState["ImageId"].ToString(); }
        }
        #endregion
        #region EventHandler
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void lstview_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            HomeGalleryManagementObj = new HomeGalleryManagement();
            switch (e.CommandName)
            {
                case "restoreitem":
                    if (HomeGalleryManagementObj.Restore(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { lstview.DataBind(); }
                    break;
                case "deleteitem":
                    if (HomeGalleryManagementObj.Delete(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { lstview.DataBind(); }
                    break;
                case "edititem":
                    SliderId = e.CommandArgument.ToString();
                    GetInfo();
                    break;
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            Save();
            lstview.DataBind();
            Clear();
        }
        #endregion
        #region Methods
        bool Save()
        {
            HomeGalleryManagementObj = new HomeGalleryManagement();
            string FileName = string.Empty;
            if (fupldImage.PostedFile.FileName != "")
            {
                FileName = "Slider" + Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(fupldImage.PostedFile.FileName);
                string PathUrl = Server.MapPath("~/Images/ActualSize/") + FileName;
                fupldImage.SaveAs(PathUrl);
                DAL.ImagesFact.ResizeWithCropResizeImage("", FileName, "Slider");
            }
            if (string.IsNullOrEmpty(SliderId))
            {
                //Add
                if (!string.IsNullOrEmpty(FileName))
                {
                    SliderId = HomeGalleryManagementObj.Add(FileName, EdDesc.Content, Request.Cookies["UserWebsiteId"].Value);
                    if (!string.IsNullOrEmpty(SliderId))
                    {
                        lblMessge.Text = "Image Uploaded Successfully";
                        return true;
                    }
                }
                else
                { lblMessge.Text = "you must upload image"; return false; }
            }
            else
            {
                //Edit
                if (HomeGalleryManagementObj.Edit(SliderId, FileName, EdDesc.Content, Request.Cookies["UserWebsiteId"].Value))
                {
                    lblMessge.Text = "Update Complete.";
                    return true;
                }
                else
                {
                    lblMessge.Text = "Update Failed";
                    return false;
                }
            }
            return false;
        }
        void GetInfo()
        {
            if (!string.IsNullOrEmpty(SliderId))
            {
                HomeGalleryManagementObj = new HomeGalleryManagement();
                HomeGallery HomeGalleryEnt = HomeGalleryManagementObj.LoadByImageId(SliderId);
                if (HomeGalleryEnt != null)
                {                 
                    EdDesc.Content= HomeGalleryEnt.ImageDescription;
                    img1.ImageUrl = "~/Images/S150_150/" + HomeGalleryEnt.ImageFile;
                }
            }
        }
        void Clear() { EdDesc.Content = SliderId = string.Empty; img1.ImageUrl = string.Empty; }
        #endregion
       
    }
}