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
    public partial class ActivityManagement : MangeBackend
    {
        #region Variables
        ActivitiesManage _ActivitiesManageObj;
        #endregion
        #region Property
        public string ActivityId
        {
            set { ViewState["ActivityId"] = value; }
            get { return ViewState["ActivityId"] == null ? string.Empty : ViewState["ActivityId"].ToString(); }
        }

        #endregion
        #region Activity
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    ActivityId = Request.QueryString["id"].ToString();
                }
                GetInfo();
            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("ActivityList.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            if (Save())
                Response.Redirect("ActivityManagement.aspx");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            GetInfo();
        }
        public void Clear()
        {
            txtDate.Text=txtDaysNo.Text=txtDesc.Text=txtLastDate.Text=txtMobile.Text=txtPlace.Text=
                txtPrice.Text=txtRefuse.Text=txtServantName.Text=txtVideoPath.Text=txtTitle.Text= string.Empty;
            imgActivity.Visible = false;

        }
        public void GetInfo()
        {
            _ActivitiesManageObj = new ActivitiesManage();
            if (!string.IsNullOrEmpty(ActivityId))
            {
                Activity obj = _ActivitiesManageObj.LoadById(ActivityId);
                if (obj != null)
                {
                    if (obj.ActivityDate != null)
                        txtDate.Text = obj.ActivityDate.Value.ToString("d/M/yyyy");
                    else
                        txtDate.Text = string.Empty;
                    txtDaysNo.Text = obj.DaysNo;
                    txtDesc.Text = obj.ActivityDesc;
                    if (obj.LastRequestDate != null)
                        txtLastDate.Text = obj.LastRequestDate.Value.ToString("d/M/yyyy");
                    else
                        txtLastDate.Text = string.Empty;
                    txtMobile.Text = obj.ServantMobile;
                    txtPlace.Text = obj.ActivityPlace;
                    txtPrice.Text = obj.ActivityPrice;
                    txtVideoPath.Text = obj.VideoUrl;
                    txtRefuse.Text = obj.RefuseReasons;
                    txtServantName.Text = obj.ServantName;
                    txtTitle.Text = obj.ActivityTitle;
                    imgActivity.Visible = true;
                    imgActivity.ImageUrl = "~/images/s250_250/" + obj.ActivityImage;
                }
            }
            else
            {
                imgActivity.Visible = false;
            }
        }
        public bool Save()
        {
            string ActivityImage = string.Empty;
            #region Save Image
            if (fupldActivity.PostedFile.FileName != "")
            {
                string[] y = fupldActivity.PostedFile.ContentType.Split('/');
                if (y[0] == "image")
                {
                    Guid id = Guid.NewGuid();
                    ActivityImage = "Activity" + id.ToString().Replace("-", "") + System.IO.Path.GetExtension(fupldActivity.PostedFile.FileName);
                    fupldActivity.PostedFile.SaveAs(Server.MapPath("~/Images/ActualSize/" + ActivityImage));
                    ImagesFact.ResizeWithCropResizeImage("", ActivityImage, "Activity");
                }
                else
                {
                    BackendMessages(201);
                    lblMessge.Text = "لابد من رفع صورة النشاط بطريقة صحيحية";
                    return false;
                }
            }
            #endregion
            _ActivitiesManageObj = new ActivitiesManage();
            if (!string.IsNullOrEmpty(ActivityId))
            {
                #region Edit Quiz
                if (_ActivitiesManageObj.Edit(ActivityId, txtTitle.Text, txtDesc.Text, txtPrice.Text, txtPlace.Text, txtDaysNo.Text, txtServantName.Text, txtMobile.Text
                    , txtRefuse.Text, ActivityImage, txtLastDate.Text, txtDate.Text,txtVideoPath.Text, Request.Cookies["UserWebsiteId"].Value))
                { BackendMessages(101); return true; }
                else
                { BackendMessages(201); return false; }
                #endregion
            }
            else
            {
                #region Add Quiz
                if (string.IsNullOrEmpty(ActivityImage))
                {
                    BackendMessages(201); lblMessge.Text = "لابد من رفع صورة لمكان النشاط";
                    return false;
                }

                else
                {
                    ActivityId = _ActivitiesManageObj.Add(txtTitle.Text, txtDesc.Text, txtPrice.Text, txtPlace.Text, txtDaysNo.Text, txtServantName.Text, txtMobile.Text
                    , txtRefuse.Text, ActivityImage, txtLastDate.Text, txtDate.Text,txtVideoPath.Text, Request.Cookies["UserWebsiteId"].Value);
                    if (!string.IsNullOrEmpty(ActivityId))
                    {
                        BackendMessages(101);
                        return true;
                    }
                    else
                    {
                        BackendMessages(201);
                        return false;
                    }
                }
                #endregion
            }
        }
        #endregion
    }
}