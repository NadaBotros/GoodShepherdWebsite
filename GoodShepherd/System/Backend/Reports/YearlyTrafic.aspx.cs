using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Backend.Reports
{
    public partial class YearlyTrafic : MangeBackend
    {
        DAL.VisitorManagement _obj = new DAL.VisitorManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                for (int x = _obj.MaxVisitYear(); x >= _obj.MinVisitYear(); x--)
                {
                    drpYear.Items.Add(x.ToString());
                }
                drpYear.DataBind();
                drpYear.Items.Insert(0, new ListItem("اختر سنة الزوار", ""));
                lblNuberinMonth.Text = _obj.visitorMounth(drpYear.SelectedValue);
            }
        }
        protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblNuberinMonth.Text = _obj.visitorMounth(drpYear.SelectedValue);
        }
    }
}