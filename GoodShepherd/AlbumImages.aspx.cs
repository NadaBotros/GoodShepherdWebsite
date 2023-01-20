using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

    public partial class AlbumImages : System.Web.UI.Page
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
                        if (!string.IsNullOrEmpty(ent.PamfletFile))
                        {
                            lnkDownload.NavigateUrl = "~/files/Pamflet/" + ent.PamfletFile;
                            lnkDownload.Style.Add("padding", "5px");
                        }
                        else
                            lnkDownload.Visible = false;
                    }
                }
            }
        }
    }
