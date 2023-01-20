using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Web.UI.HtmlControls;

    public partial class Quizdetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                QuizManage obj = new QuizManage();
                Quiz ent = obj.LoadById(Request.QueryString["id"].ToString());
                if (ent != null)
                {
                    Page.Title = "اجتماع الراعي الصالح | مسابقة الاجتماع - " + ent.QuizTitle;
                    lblTitle.Text = "مسابقة الاجتماع | " + ent.QuizTitle;
                    frmQuiz.Text = string.Format("<iframe  width='100%' height='950' style='border: none;' src='https://docs.google.com/viewer?embedded=true&url=http://shepherdmeeting.com/shepherdmeeting.com/files/quiz/{0}'></iframe>", ent.QuizPDF);
                    imgMagazine.ImageUrl = "images/S300_300/" + ent.QuizCover;
                    lblMagDate.Text = "تاريخ اصدار مسابقة : " + ent.QuizDate.Value.ToString("yyyy/M/d");
                    lblExpireDate.Text = "تاريخ تسليم المسابقة : " + ent.QuizDeliveryDate.Value.ToString("yyyy/M/d");
                    lnkDownlaod.NavigateUrl = "~/files/quiz/" + ent.QuizPDF;

                    HtmlHead head = (HtmlHead)Page.Header;
                    var hm = new HtmlMeta();
                    hm.Name = "og:image";
                    hm.Content = "images/S300_300/" + ent.QuizCover;
                    head.Controls.Add(hm);
                }
            }
        }
    }
