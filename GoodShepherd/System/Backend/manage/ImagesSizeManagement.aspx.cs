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
    public partial class ImagesSizeManagement : MangeBackend
    {
        #region Variables
        DAL.ImagesSizesManagement ImageSizesManagementObj;
        #endregion
        #region Property
        public string SizeId
        {
            get { return ViewState["SizeId"] != null ? ViewState["SizeId"].ToString() : string.Empty; }
            set { ViewState["SizeId"] = value; }
        }
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    SizeId = Request.QueryString["id"].ToString();
                    GetInfo();
                }
            }

        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Save(false))
                BackendMessages(101);
            else
                BackendMessages(201);
        }

        protected void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            if (Save(true))
                BackendMessages(101);
            else
                BackendMessages(201);
            Response.Redirect("ImagesSizeManagement.aspx");
        }

        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ImagesSizeList.aspx");
        }

        #endregion
        #region Method
        public bool Save(bool newPage)
        {
            //try
            {
                txtFolderName.Text = "S" + txtWidth.Text + "_" + txtHeight.Text;
                if (string.IsNullOrEmpty(SizeId))
                {  //Add New
                    ImageSizesManagementObj = new DAL.ImagesSizesManagement();
                    if (!string.IsNullOrEmpty(txtHeight.Text) && !string.IsNullOrEmpty(txtWidth.Text))
                    {
                        string result = ImageSizesManagementObj.Add(txtWidth.Text, txtHeight.Text, txtFolderName.Text, txtSection.Text, txtDescription.Text, radCurved.SelectedValue, txtCurveWidth.Text, radResizeHeight.SelectedValue, radResizeWidth.SelectedValue, radGrayScale.SelectedValue, radCrop.SelectedValue, Request.Cookies["UserWebsiteId"].Value);
                        if (!newPage)
                            GetInfo();
                    }
                    else { return false; }
                }
                else
                {   //Update
                    if (!string.IsNullOrEmpty(txtHeight.Text) && !string.IsNullOrEmpty(txtWidth.Text))
                    {
                        ImageSizesManagementObj = new DAL.ImagesSizesManagement();
                        ImageSizesManagementObj.Edit(txtWidth.Text, txtHeight.Text, txtFolderName.Text, txtSection.Text, txtDescription.Text, radCurved.SelectedValue, txtCurveWidth.Text, radResizeHeight.SelectedValue, radResizeWidth.SelectedValue, radGrayScale.SelectedValue, radCrop.SelectedValue, Request.Cookies["UserWebsiteId"].Value, SizeId);
                        if (!newPage)
                            GetInfo();
                    }
                    else { return false; }
                }
                string[] AcualImages = Directory.GetFiles(Server.MapPath("~/Images/ActualSize"));
                ImagesFact ImagesFactObj = new ImagesFact();
                foreach (string fn in AcualImages)
                {
                    try
                    {
                        string ImageName = Path.GetFileName(fn);
                        if (ImageName.StartsWith(txtSection.Text))
                        {
                            ImagesFact.ResizeWithCropResizeImage("", ImageName, txtSection.Text);
                        }
                    }
                    catch { }
                }
                GetInfo();
                return true;
            }
            //catch { return false; }
        }
        public void GetInfo()
        {
            if (!string.IsNullOrEmpty(SizeId))
            {
                ImageSizesManagementObj = new DAL.ImagesSizesManagement();
                DAL.ImagesSize ImageSizeObj = ImageSizesManagementObj.LoadById(SizeId);
                if (ImageSizeObj != null)
                {
                    try
                    {
                        txtDescription.Text = ImageSizeObj.Description;
                        txtFolderName.Text = ImageSizeObj.FolderName;
                        txtHeight.Text = ImageSizeObj.Height.ToString();
                        txtWidth.Text = ImageSizeObj.Width.ToString();
                        txtCurveWidth.Text = ImageSizeObj.CornerRadius.ToString();
                        radCrop.SelectedValue = ImageSizeObj.AllowCrop.ToString();
                        radCurved.SelectedValue = ImageSizeObj.CurvedCorners.ToString();
                        radGrayScale.SelectedValue = ImageSizeObj.ConvertToGrayScale.ToString();
                        radResizeHeight.SelectedValue = ImageSizeObj.ResizeHeight.ToString();
                        radResizeWidth.SelectedValue = ImageSizeObj.ResizeWidth.ToString();
                        txtSection.Text = ImageSizeObj.Section;
                    }
                    catch { }
                }
            }

        }
        public void Clear()
        {
            txtCurveWidth.Text = "";
            radCrop.SelectedValue = "False";
            radCurved.SelectedValue = "False";
            radGrayScale.SelectedValue = "False";
            radResizeHeight.SelectedValue = "True";
            radResizeWidth.SelectedValue = "True";
            txtWidth.Text = "";
            txtHeight.Text = "";
            txtFolderName.Text = "";
            txtDescription.Text = "";
        }
        #endregion
    }
}