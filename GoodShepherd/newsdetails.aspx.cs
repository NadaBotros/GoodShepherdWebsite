using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

    public partial class newsdetails : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                NewsManagement obj=new NewsManagement();
                DAL.New ent = obj.LoadById(Request.QueryString["id"].ToString());
                if (ent != null)
                {
                    lblTitle.Text = ent.NewsTitle;
                    imgNews.ImageUrl = "images/ActualSize/" + ent.NewsImage;
                    lblDate.Text = " بتاريخ " + ent.CreatedOn.Value.ToString("yyyy/M/d");
                    Page.Title = "اجتماع الراعي الصالح | " + ent.NewsTitle;
                    lblNewsContent.Text = ent.NewsContent.Replace("&lt;","<").Replace("&gt;",">");
                }
            }

        }
    }
