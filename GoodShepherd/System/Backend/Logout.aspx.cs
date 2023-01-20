using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Backend
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            HttpCookie UserWebsiteId = new HttpCookie("UserWebsiteId");
            UserWebsiteId.Expires = DateTime.Now.AddDays(-1);
            Response.Cookies.Add(UserWebsiteId);

            
            Response.Redirect("default.aspx");
        }
    }
}