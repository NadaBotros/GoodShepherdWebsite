using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


    public partial class AudioStream : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucPageContent.GetInfo("بث الصوت", "8");
            lblFaceBookComment.Text = " <div id='fb-root'></div><script src='http://connect.facebook.net/en_US/all.js#xfbml=1'></script><fb:comments href='http://shepherdmeeting.com/audiostream.aspx' num_posts='50' width='670px'></fb:comments>";
            SaveValuesManage saveobj = new SaveValuesManage();
            #region Audio
            string AudioValue = saveobj.LoadById(1);
            if (AudioValue == "1")
            {
                lblStreamAudio.Text = "نوع البث : لا يعمل";
                lnkAudioStream.ImageUrl = "themes/Default/img/AudioNo.png";
            }
            else if (AudioValue == "2")
            {
                lblStreamAudio.Text = "نوع البث : مسجل";
                lnkAudioStream.ImageUrl = "themes/Default/img/Audio.png";
            }
            else if (AudioValue == "3")
            {
                lblStreamAudio.Text = "نوع البث : بث مباشر";
                lnkAudioStream.ImageUrl = "themes/Default/img/Audio.png";
            }
            #endregion
        }
    }
