using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Backend.Reports
{
    public partial class Last7DayReport : MangeBackend
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            DAL.VisitorManagement _obj = new DAL.VisitorManagement();
            if (!IsPostBack)
            {
                lblDays.Text = loadsevenDays();
                lblVisitDaialy.Text = _obj.visitorsLast7Days();
            }
        }
        public string loadsevenDays()
        {
            string DateNow = string.Empty;
            DateTime now = DateTime.Today;
            string format = "MMM d ddd";
            for (int i = 0; i < 7; i++)
            {


                DateNow += "'" + now.ToString(format) + "'" + ",";
                now = now.AddDays(-1);
                // nowDigit=nowDigit.AddDays(-1)
            }
            return DateNow.Remove(DateNow.Length - 1);
        }
    
    }
}