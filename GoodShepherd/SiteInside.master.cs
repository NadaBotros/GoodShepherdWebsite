using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


    public partial class SiteInside : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GetInfo();
            try
            {
                if (Request.Cookies["PersonId"] != null)
                {
                    pnlMyInfo.Visible = pnlMyInfoTitle.Visible = true;
                    clpsMyInfo.Enabled = true;
                    clpsMyInfo.DataBind();
                }
                else
                {
                    pnlMyInfo.Visible = pnlMyInfoTitle.Visible = false;
                    clpsMyInfo.Enabled = false;
                    clpsMyInfo.DataBind();
                }
            }
            catch { }
        }
        public void GetInfo()
        {
            try
            {
                HomeManage obj = new HomeManage();
                SaveValuesManage saveobj = new SaveValuesManage();
                ViewDailyMeetingWithGod _ViewDailyMeetingWithGod = obj.DailyMeetingWithGods();
                #region Audio
                string AudioValue = saveobj.LoadById(1);
                if (AudioValue == "1")
                {
                    lblStreamAudio.Text = "لا يعمل";
                    lnkAudioStream.ImageUrl = "themes/Default/img/AudioNo.png";
                }
                else if (AudioValue == "2")
                {
                    lblStreamAudio.Text = "مسجل";
                    lnkAudioStream.ImageUrl = "themes/Default/img/Audio.png";
                }
                else if (AudioValue == "3")
                {
                    lblStreamAudio.Text = "بث مباشر";
                    lnkAudioStream.ImageUrl = "themes/Default/img/Audio.png";
                }
                #endregion
                #region Video
                string VideoValue = saveobj.LoadById(2);
                if (VideoValue == "1")
                {
                    lblStreamVideo.Text = "لا يعمل";
                    lnkVideoStream.ImageUrl = "themes/Default/img/VideoNo.png";
                }
                else if (VideoValue == "2")
                {
                    lblStreamVideo.Text = "مسجل";
                    lnkVideoStream.ImageUrl = "themes/Default/img/video2.png";
                }
                else if (VideoValue == "3")
                {
                    lblStreamVideo.Text = "بث مباشر";
                    lnkVideoStream.ImageUrl = "themes/Default/img/video2.png";
                }
                #endregion
                ViewAya aya = obj.Aya();
                if (aya != null)
                {
                    lblAya.Text = aya.Aya;
                }

                ViewA2wal a2wal = obj.A2wal();
                if (a2wal != null)
                {
                    lblA2walTitle.Text = a2wal.Name;
                    lblA2walDesc.Text = a2wal.Data;
                }
                ViewSpiritualTraining spiritualTrain = obj.SpiritualTrainings();
                if (spiritualTrain != null)
                {
                    lblSpiritualTrainingDesc.Text = spiritualTrain.SpiritualTrainingDesc.Replace("\n", "<br>");
                    lblSpiritualTrainingTitle.Text = spiritualTrain.SpiritualTrainingTitle;
                }
                ViewSpeaker SpeakerObj = obj.Speaker();
                if (SpeakerObj != null)
                {
                    lnkspeakerimage.ImageUrl = "images/S100_100/" + SpeakerObj.SpeakerImage;
                    lnkspeakername.Text = SpeakerObj.SpeakerName;
                    lnkspeakername.NavigateUrl = lnkspeakerimage.NavigateUrl = "sound.aspx?speakerId=" + SpeakerObj.SpeakerId;
                }

            }
            catch { }
        }
    }
