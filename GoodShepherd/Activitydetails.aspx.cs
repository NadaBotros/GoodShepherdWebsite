using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Web.UI.HtmlControls;

    public partial class Activitydetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                ActivitiesManage obj = new ActivitiesManage();
                Activity ent = obj.LoadById(Request.QueryString["id"].ToString());
                if (ent != null)
                {
                    lblFaceBookComment.Text = " <div id='fb-root'></div><script src='http://connect.facebook.net/en_US/all.js#xfbml=1'></script><fb:comments href='http://shepherdmeeting.com/shepherdmeeting.com/Activitydetails.aspx?id=" + ent.ActivityId + "' num_posts='50' width='670px'></fb:comments>";
                    Page.Title = "اجتماع الراعي الصالح |  " + ent.ActivityTitle;
                    lblTitle.Text = "الرحلات والمؤتمرات - " + ent.ActivityTitle;
                    if (ent.ActivityDate != null)
                        lblDate.Text = ent.ActivityDate.Value.ToString("yyyy/M/d");
                    if (ent.LastRequestDate != null)
                        lblLastTime.Text = ent.LastRequestDate.Value.ToString("yyyy/M/d");
                    img.ImageUrl = "images/S300_300/" + ent.ActivityImage;
                    lblDesc.Text = ent.ActivityDesc.Replace("\n", "<br>");
                    lblMobile.Text = ent.ServantMobile;
                    lblPeriod.Text = ent.DaysNo;
                    lblPlace.Text = ent.ActivityPlace;
                    lblPrice.Text = ent.ActivityPrice.Replace("\n", "<br>"); ;
                    lblRefuse.Text = ent.RefuseReasons.Replace("\n", "<br>"); ;
                    lblServant.Text = ent.ServantName;
                    if (!string.IsNullOrEmpty(ent.VideoUrl))
                        ltrVideo.Text = GeneralMethods.GetYoutubeBig(ent.VideoUrl);
                }
            }
        }
    }
