using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Backend.Reports
{
    public partial class BrowserTrafic : MangeBackend
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DAL.VisitorManagement _obj = new DAL.VisitorManagement();
            if (!IsPostBack)
            {
                rptbrowser.DataSource = _obj.BrowserUsedWithDate();
                rptbrowser.DataBind();
            }

        }
    }
}