using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
namespace System.Backend
{
    public partial class admin : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["UserWebsiteId"] == null)
                Response.Redirect("~/System/Backend/Default.aspx?page=" + Request.RawUrl);
        }

    }
}