using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

    public partial class VideoStream : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ucPageContent.GetInfo("بث الفيديو", "9");
            lblFaceBookComment.Text = " <div id='fb-root'></div><script src='http://connect.facebook.net/en_US/all.js#xfbml=1'></script><fb:comments href='http://shepherdmeeting.com/VideoStream.aspx' num_posts='50' width='670px'></fb:comments>";
            SaveValuesManage saveobj = new SaveValuesManage();
            #region Audio
            string VideoValue = saveobj.LoadById(1);
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
        }
    }
