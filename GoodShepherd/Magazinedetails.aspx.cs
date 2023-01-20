using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Web.UI.HtmlControls;


    public partial class Magazinedetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                MagazineManage obj = new MagazineManage();
                Magazine ent = obj.LoadById(Request.QueryString["id"].ToString());
                if (ent != null)
                {
                    Page.Title = "اجتماع الراعي الصالح | مجلة الاجتماع - " + ent.MagazineTitle;
                    lblTitle.Text = "مجلة الاجتماع | " + ent.MagazineTitle;
                    frmMagazine.Text = string.Format("<iframe  width='100%' height='950' style='border: none;' src='https://docs.google.com/viewer?embedded=true&url=http://shepherdmeeting.com/shepherdmeeting.com/files/magazines/{0}'></iframe>", ent.MagazinePDF);
                    imgMagazine.ImageUrl = "images/S300_300/" + ent.MagazineCover;
                    lblMagDate.Text = "تاريخ اصدار المجلة : " + obj.GetDate(ent.MagazineMonth, ent.MagazineYear);
                    lnkDownlaod.NavigateUrl = "~/files/magazines/" + ent.MagazinePDF;

                    HtmlHead head = (HtmlHead)Page.Header;
                    var hm = new HtmlMeta();
                    hm.Name = "og:image";
                    hm.Content = "images/S300_300/" + ent.MagazineCover;
                    head.Controls.Add(hm);
                }
            }
        }
    }
