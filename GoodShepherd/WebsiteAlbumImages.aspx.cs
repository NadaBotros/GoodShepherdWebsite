using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

namespace GoodShepherd
{
    public partial class WebsiteAlbumImages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    AlbumManagement obj = new AlbumManagement();
                    Album ent = obj.LoadByAlbumId(Request.QueryString["id"].ToString());
                    if (ent != null)
                    {
                        Page.Title = "اجتماع الراعي الصالح | " + ent.AlbumName;
                        Page.MetaDescription = ent.AlbumDescription;
                        lblDate.Text = "بتاريخ : " + ent.AlbumDate.Value.ToString("yyyy/MM/dd");
                        lblTitle.Text = "البوم الذكريات | " + ent.AlbumName;
                        lblDESC.Text = ent.AlbumDescription;
                        
                    }
                }
            }
        }
    }
}