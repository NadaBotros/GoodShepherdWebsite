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
    public partial class SpeakersManagement : MangeBackend
    {
        #region Variables
        SpeakersManage _SpeakersManageObj;
        #endregion
        #region Property
        public string SpeakerId
        {
            set { ViewState["SpeakerId"] = value; }
            get { return ViewState["SpeakerId"] == null ? string.Empty : ViewState["SpeakerId"].ToString(); }
        }
        #endregion
        #region Magazine
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    SpeakerId = Request.QueryString["id"].ToString();
                }
                GetInfo();

            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("SpeakersList.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            if (Save())
                Response.Redirect("SpeakersManagement.aspx");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            GetInfo();
        }
        public void Clear()
        {
            txtChurch.Text = string.Empty;
            txtNotes.Text = txtSpeakerTitle.Text = string.Empty;
            imgSpeaker.Visible = false;
        }
        public void GetInfo()
        {
            _SpeakersManageObj = new SpeakersManage();
            if (string.IsNullOrEmpty(SpeakerId))
            {
                imgSpeaker.Visible = false;
            }
            else
            {
                imgSpeaker.Visible = true;
                Speaker obj = _SpeakersManageObj.LoadById(SpeakerId);
                if (obj != null)
                {
                    txtChurch.Text = obj.ChurchName;
                    txtNotes.Text = obj.Notes;
                    txtSpeakerTitle.Text = obj.SpeakerName;
                    imgSpeaker.ImageUrl = "~/images/s250_250/" + obj.SpeakerImage;
                }
            }
        }
        public bool Save()
        {
            string imgFile = string.Empty;
            //magazine Cover
            #region Save Image
            if (fupldImage.PostedFile.FileName != "")
            {
                string[] y = fupldImage.PostedFile.ContentType.Split('/');
                if (y[0] == "image")
                {
                    Guid id = Guid.NewGuid();
                    imgFile = "Speaker" + id.ToString().Replace("-", "") + System.IO.Path.GetExtension(fupldImage.PostedFile.FileName);
                    fupldImage.PostedFile.SaveAs(Server.MapPath("~/Images/ActualSize/" + imgFile));
                    ImagesFact.ResizeWithCropResizeImage("", imgFile, "Speaker");
                }
                else
                {
                    BackendMessages(201);
                    lblMessge.Text = "لابد من رفع صورة الواعظ بطريقة صحيحية";
                    return false;
                }
            }
            #endregion
            _SpeakersManageObj = new SpeakersManage();
            if (!string.IsNullOrEmpty(SpeakerId))
            {
                #region Edit
                if (_SpeakersManageObj.Edit(SpeakerId, txtSpeakerTitle.Text, txtChurch.Text, imgFile, txtNotes.Text, Request.Cookies["UserWebsiteId"].Value))
                { BackendMessages(101); return true; }
                else
                { BackendMessages(201); return false; }
                #endregion
            }
            else
            {
                #region Add
                if (string.IsNullOrEmpty(imgFile))
                {
                    BackendMessages(201); lblMessge.Text = "لابد من رفع صورة الواعظ";
                    return false;
                }
                else
                {
                    SpeakerId = _SpeakersManageObj.Add(txtSpeakerTitle.Text, txtChurch.Text, imgFile, txtNotes.Text, Request.Cookies["UserWebsiteId"].Value);
                    if (!string.IsNullOrEmpty(SpeakerId))
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