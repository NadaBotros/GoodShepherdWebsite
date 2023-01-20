using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace System.Backend.manage
{

    public partial class MeetingManagement : MangeBackend
    {
        #region Variables
        DAL.MeetingManage _MeetingManage;
        #endregion
        #region Property
        public string MeetingId
        {
            set { ViewState["MeetingId"] = value; }
            get { return ViewState["MeetingId"] == null ? string.Empty : ViewState["MeetingId"].ToString(); }
        }
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(Request.QueryString["id"]))
            {
                MeetingId = Request.QueryString["id"].ToString();
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
            Response.Redirect("MeetingManagement.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MeetingList.aspx");
        }
        #endregion
        #region Methods
        public bool Save()
        {
            #region Audio File
            string[] mediaExtensions = {   
    ".WAV", ".ASF", ".WM", ".WMA", ".MP3", ".MP2", ".AVI", //etc
    ".WMV", ".MP4", ".MPA"};
            string FileName = txtFileName.Text;
            //if (fupldSoundFile.PostedFile.FileName != "")
            //{
            //    if (mediaExtensions.Contains(System.IO.Path.GetExtension(fupldSoundFile.PostedFile.FileName).ToUpper()))
            //    {
            //        Guid id = Guid.NewGuid();
            //        FileName = id.ToString().Replace("-", "") + System.IO.Path.GetExtension(fupldSoundFile.PostedFile.FileName);
            //        fupldSoundFile.PostedFile.SaveAs(Server.MapPath("~/files/audio/" + FileName));
            //    }
            //    else
            //    {
            //        BackendMessages(201);
            //        lblMessge.Text = "لابد من رفع ملف الصوت بطريقة صحيحية";
            //        return false;
            //    }
            //}
            #endregion
            #region Manage Item
            _MeetingManage = new MeetingManage();
            if (string.IsNullOrEmpty(MeetingId))
            {
                MeetingId = _MeetingManage.Add(drpSpeaker.SelectedValue, txtMeetingTitle.Text, txtDate.Text, FileName, txtYoutubelink.Text, Request.Cookies["UserWebsiteId"].Value);
                if (!string.IsNullOrEmpty(MeetingId))
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
            else
            {
                if (_MeetingManage.Edit(MeetingId, drpSpeaker.SelectedValue, txtMeetingTitle.Text, txtDate.Text, FileName, txtYoutubelink.Text, Request.Cookies["UserWebsiteId"].Value))
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
        void GetInfo()
        {
            if (!string.IsNullOrEmpty(MeetingId))
            {
                _MeetingManage = new MeetingManage();
                Meeting MeetingEnt = _MeetingManage.LoadById(MeetingId);
                if (MeetingEnt != null)
                {
                    if (MeetingEnt.MeetingDate != null)
                        txtDate.Text = MeetingEnt.MeetingDate.Value.ToString("d/M/yyyy");
                    else
                        txtDate.Text = string.Empty;
                    txtMeetingTitle.Text = MeetingEnt.MeetingTitle;
                    txtFileName.Text = MeetingEnt.SoundFile;
                    txtYoutubelink.Text = MeetingEnt.VideoUrl;
                    ltlVideo.Text = GeneralMethods.GetYoutube(MeetingEnt.VideoUrl);
                    ltrAudio.Text = GeneralMethods.GetAudio("../../../files/audio/" + MeetingEnt.SoundFile);
                    if (MeetingEnt.SpeakerId != null)
                    {
                        try
                        {
                            drpSpeaker.SelectedValue = MeetingEnt.SpeakerId.ToString();
                        }
                        catch { }
                    }

                }
            }

        }
        void Clear()
        {
            txtDate.Text = txtMeetingTitle.Text = txtYoutubelink.Text = ltrAudio.Text = ltlVideo.Text = string.Empty;
        }
        #endregion
        protected void drpSpeaker_DataBound(object sender, EventArgs e)
        {
            drpSpeaker.Items.Insert(0, new ListItem("اختر الواعظ", string.Empty));
        }

    }
}