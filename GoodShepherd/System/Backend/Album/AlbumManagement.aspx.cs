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
    public partial class AlbumManagement : MangeBackend
    {
        #region Variables
        DAL.AlbumManagement _AlbumManagementObj;
        #endregion
        #region Property
        public string AlbumId
        {
            set { ViewState["AlbumId"] = value; }
            get { return ViewState["AlbumId"] == null ? string.Empty : ViewState["AlbumId"].ToString(); }
        }
        #endregion
        #region Album
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    AlbumId = Request.QueryString["id"].ToString();
                }
                GetInfo();
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("AlbumsList.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            if (Save())
                Response.Redirect("AlbumManagement.aspx");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            GetInfo();
        }
        public void Clear()
        {
            txtDesc.Text = txtAlbumDate.Text = txtTitle.Text = string.Empty;
        }
        public void GetInfo()
        {
            if (string.IsNullOrEmpty(AlbumId))
                tabImages.Visible = false;
            else
            {
                tabImages.Visible = true;
                _AlbumManagementObj = new DAL.AlbumManagement();
                Album obj = _AlbumManagementObj.LoadByAlbumId(AlbumId);
                if (obj != null)
                {
                    frmImages.Text = "<iframe src='../manage/ImagesGallary.aspx?AlbumId=" + AlbumId + "' frameborder='0' width='100%' height='600px'></iframe>";
                    txtTitle.Text = obj.AlbumName;
                    txtDesc.Text = obj.AlbumDescription;
                    radForAll.SelectedValue = obj.AlbumType == null ? "0" : obj.AlbumType.Value.ToString();
                    if (!string.IsNullOrEmpty(obj.PamfletFile))
                        lnkPamflet.NavigateUrl = "~/files/Pamflet/" + obj.PamfletFile;
                    if (obj.AlbumDate != null)
                        txtAlbumDate.Text = obj.AlbumDate.Value.ToString("d/M/yyyy");
                    else
                        txtAlbumDate.Text = string.Empty;
                    if (!string.IsNullOrEmpty(obj.AlbumCover))
                    {
                        imgMain.Visible = true;
                        imgMain.ImageUrl = "~/images/S150_150/" + obj.AlbumCover;
                    }
                    else
                        imgMain.Visible = false;
                }
            }
        }
        public bool Save()
        {
            _AlbumManagementObj = new DAL.AlbumManagement();
            string ImageFile = string.Empty;
            string Pamflet = string.Empty;
            if (fpldImage.PostedFile.FileName != "")
            {
                string[] y = fpldImage.PostedFile.ContentType.Split('/');
                if (y[0] == "image")
                {
                    Guid id = Guid.NewGuid();
                    ImageFile = "Album" + id.ToString().Replace("-", "") + System.IO.Path.GetExtension(fpldImage.PostedFile.FileName);
                    fpldImage.PostedFile.SaveAs(Server.MapPath("~/Images/ActualSize/" + ImageFile));
                    ImagesFact.ResizeWithCropResizeImage("", ImageFile, "Album");
                }
                else
                {
                    BackendMessages(201);
                    lblMessge.Text = "لابد من رفع صورة لغلاف الالبوم بطريقة صحيحية";
                    return false;
                }
            }
            if (fpldPamflet.PostedFile.FileName != "")
            {
                Guid id = Guid.NewGuid();
                Pamflet = "Pamflet" + id.ToString().Replace("-", "") + System.IO.Path.GetExtension(fpldPamflet.PostedFile.FileName);
                fpldPamflet.PostedFile.SaveAs(Server.MapPath("~/files/Pamflet/" + Pamflet));
            }
            if (string.IsNullOrEmpty(AlbumId))
            {
                if (!string.IsNullOrEmpty(ImageFile))
                {
                    AlbumId = _AlbumManagementObj.Add(txtTitle.Text,radForAll.SelectedValue, txtAlbumDate.Text, ImageFile, Pamflet, txtDesc.Text, Request.Cookies["UserWebsiteId"].Value);
                    if (!string.IsNullOrEmpty(AlbumId))
                    { BackendMessages(101); return true; }
                    else
                    { BackendMessages(201); return false; }
                }
                else
                {
                    BackendMessages(201); lblMessge.Text = "لابد من رفع صورة غلاف الالبوم"; return false;
                }
            }
            else
            {
                if (_AlbumManagementObj.Edit(AlbumId, radForAll.SelectedValue, txtTitle.Text, Pamflet, txtAlbumDate.Text, ImageFile, txtDesc.Text, Request.Cookies["UserWebsiteId"].Value))
                { BackendMessages(101); return true; }
                else
                { BackendMessages(201); return false; }
            }
        }
        #endregion


    }
}