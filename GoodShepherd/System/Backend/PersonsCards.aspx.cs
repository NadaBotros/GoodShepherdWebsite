using System;
using System.Backend.UserControls;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Backend
{
    public partial class PersonsCards : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnPrint_OnClick(object sender, EventArgs e)
        {
            List<string> lstIds = UcAdvancedSearch.GetData();
            Session["lstFields"] = lstIds;
            string strUrl = "Report/ReportViewer/PersonsCardsView.aspx";
            ScriptManager.RegisterStartupScript(this, GetType(), "popup",
                                                "window.open('" + strUrl + "');", true);

        }
    }
}