using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

    public partial class ucPageContent : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void GetInfo(string pageTitle,string PageId)
        {
            odsSection.SelectParameters["PageId"].DefaultValue = PageId;
            rptSections.DataBind();
            lblPageTitle.Text = pageTitle;
            this.Page.Title = "اجتماع الراعي الصالح | " + pageTitle;
        }
    }
