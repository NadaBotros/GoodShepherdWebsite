using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.IO;
namespace system.backend
{
    public partial class ImagesGallary : System.Web.UI.Page
    {
        #region Variables
        DAL.AlbumImageManagement AlbumImageManagementObj;
        #endregion
        #region Properties
        public string ImageId
        {
            set { ViewState["ImageId"] = value; }
            get { return ViewState["ImageId"] == null ? string.Empty : ViewState["ImageId"].ToString(); }
        }
        public string AlbumId
        {
            set { ViewState["AlbumId"] = value; }
            get { return ViewState["AlbumId"] == null ? string.Empty : ViewState["AlbumId"].ToString(); }
        }       
        #endregion
        #region EventHandler
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["AlbumId"]))
                    AlbumId = Request.QueryString["AlbumId"];
                BindingListView();
            }
        }
        protected void AjaxFileUpload1_UploadComplete(object sender, AjaxControlToolkit.AjaxFileUploadEventArgs e)
        {
            string FileName = "Album" + Guid.NewGuid().ToString().Replace("-", "") + Path.GetExtension(e.FileName);
            string PathUrl = Server.MapPath("~/Images/ActualSize/") + FileName;
            AjaxFileUpload1.SaveAs(PathUrl);
            DAL.ImagesFact.ResizeWithCropResizeImage("", FileName, "Album");
            string x = "";

            if (!Save(FileName, out x))
            {
                lblMessge.Text = x;
            }
            else
                lblMessge.Text = x;
            BindingListView();
        }
        protected void lstview_ItemCommand(object sender, ListViewCommandEventArgs e)
        {
            AlbumImageManagementObj = new AlbumImageManagement();
            switch (e.CommandName)
            {
                case "restoreitem":
                    if (AlbumImageManagementObj.Restore(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { lstview.DataBind(); }
                    break;
                case "deleteitem":
                    if (AlbumImageManagementObj.Delete(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { lstview.DataBind(); }
                    break;
            }
        }
        #endregion
        #region Methods
        bool Save(string FileName, out string MSG)
        {
            AlbumImageManagementObj = new AlbumImageManagement();
            if (!string.IsNullOrEmpty(FileName))
            {
                ImageId = AlbumImageManagementObj.Add(AlbumId, FileName, Request.Cookies["UserWebsiteId"].Value);
                if (!string.IsNullOrEmpty(ImageId))
                {
                    MSG = "done";
                    return true;
                }
                else
                {
                    MSG = "Select Images to upload";
                    return false;
                }
            }
            else
            {
                MSG = "Select Images to be uploaded";
                return false;
            }
        }
       
        public void BindingListView()
        {
            objImages.SelectParameters["AlbumId"].DefaultValue = AlbumId;
            lstview.DataBind();
            ScriptManager.GetCurrent(this.Page).RegisterPostBackControl(lstview);
        }

        #endregion
        
        protected void updatePanelAttachments_PreRender(object sender, EventArgs e)
        {
            BindingListView();
        }
      

    }
}