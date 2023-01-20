using System.Web.UI.HtmlControls;
using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;

    public partial class MeetingDetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["mid"] != null)
            {
                try
                {
                    MeetingManage obj = new MeetingManage();
                    Meeting ent = obj.LoadById(Request.QueryString["mid"].ToString());
                    if (ent != null)
                    {
                        lblFaceBookComment.Text =
                            " <div id='fb-root'></div><script src='http://connect.facebook.net/en_US/all.js#xfbml=1'></script><fb:comments href='http://shepherdmeeting.com/shepherdmeeting.com/MeetingDetails.aspx?mid=" +
                            ent.MeetingId + "' num_posts='50' width='670px'></fb:comments>";
                        
                        Page.Title = "اجتماع الراعي الصالح | " + ent.Speaker.SpeakerName + " | " + ent.MeetingTitle;
                        Page.MetaDescription =ent.Speaker.ChurchName+" - "+ "تاريخ العظة : " + ent.MeetingDate.Value.ToString("dd-MM-yyyy");

                        HtmlHead head = (HtmlHead)Page.Header;
                        
                        var hm = new HtmlMeta();
                        hm.Name = "og:image";
                        hm.Content = "images/ActualSize/" + ent.Speaker.SpeakerImage;
                        head.Controls.Add(hm);

                        lnkSPeaker.NavigateUrl = "sound.aspx?speakerId=" + ent.SpeakerId;
                        lnkSPeaker.Text = ent.Speaker.SpeakerName;
                        lblTitle.Text = " | " + ent.MeetingTitle;
                        imgSpeaker.ImageUrl = "images/S100_100/" + ent.Speaker.SpeakerImage;
                        lblDate.Text = "تاريخ العظة : " + ent.MeetingDate.Value.ToString("dd-MM-yyyy");
                        lblChurch.Text = ent.Speaker.ChurchName;
                        lblSpeaker.Text = ent.Speaker.SpeakerName;
                        lblYoutube.Text = GeneralMethods.GetYoutubeBig(ent.VideoUrl);
                        if (!string.IsNullOrEmpty(ent.SoundFile) &&
                            File.Exists(Server.MapPath("~/files/audio/" + ent.SoundFile)))
                        {
                            lnkDownlaod.NavigateUrl = "~/files/audio/" + ent.SoundFile;
                            lnkDownlaod.Visible = true;
                            lblMediaPlayer.Text = GeneralMethods.MediaPlayer("files/audio/" + ent.SoundFile, 300, 65);
                        }
                        else
                        {
                            lnkDownlaod.NavigateUrl = string.Empty;
                            lnkDownlaod.Visible = false;
                            lblMediaPlayer.Text = "ملف العظة غير متوافر الان";
                        }
                    }
                }
                catch
                {
                }
            }
        }
    }
