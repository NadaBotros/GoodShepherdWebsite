using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using DAL;
namespace System.Backend
{
    public partial class ServantPersons : MangeBackend
    {
        #region EventHanlder
        protected void Page_Load(object sender, EventArgs e)
        { }
        #endregion
        protected void btnReport_Click(object sender, EventArgs e)
        {
            string strUrl = "../ReportViewer/ServantPersonsRV.aspx?id=" + drpServant.SelectedValue;
            ScriptManager.RegisterStartupScript(Page, Page.GetType(), "popup", "window.open('" + strUrl + "','_blank')", true);
        }
    }
}