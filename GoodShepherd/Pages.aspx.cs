using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace GoodShepherd
{
    public partial class Pages : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                var pageSectionManagement = new PageSectionManagement();
                DAL.Page obj = pageSectionManagement.LoadPageId(Request.QueryString["id"]);
                if (obj != null)
                {
                    ucPageContent.GetInfo(obj.PageName, Request.QueryString["id"]);
                }
            }
        }
    }
}