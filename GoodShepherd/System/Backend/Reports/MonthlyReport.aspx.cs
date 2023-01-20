using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Backend.Reports
{
    public partial class MonthlyReport : MangeBackend
    {
        DAL.VisitorManagement _obj = new DAL.VisitorManagement();
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack)
                {

                    for (int x = _obj.MaxVisitYear(); x >= _obj.MinVisitYear(); x--)
                    {
                        drpYear.Items.Add(x.ToString());
                    }

                    for (int i = 1; i <= 12; i++)
                    {
                        drpMonth.Items.Add(i.ToString());
                    }
                    drpYear.DataBind();
                    drpMonth.Items.Insert(0, new ListItem("اختر الشهر", ""));
                    drpMonth.SelectedIndex = 0;
                    drpYear.Items.Insert(0, new ListItem("اختر السنة", ""));
                    drpYear.SelectedIndex = 0;
                    //drpMonth.SelectedValue = DateTime.Now.Month.ToString();
                    //drpYear.SelectedValue = DateTime.Now.Year.ToString();
                    lblDays.Text = loadMonthDays();
                    lblVisitMonthly.Text = _obj.visitorsLastMonth(drpYear.SelectedValue, drpMonth.SelectedValue);
                }
            }
            catch { }
        }


        protected void drpYear_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblVisitMonthly.Text = _obj.visitorsLastMonth(drpYear.SelectedValue, drpMonth.SelectedValue);
        }
        protected void drpMonth_SelectedIndexChanged(object sender, EventArgs e)
        {
            lblVisitMonthly.Text = _obj.visitorsLastMonth(drpYear.SelectedValue, drpMonth.SelectedValue);
        }
        private string loadMonthDays()
        {
            string DateNow = string.Empty;
            int Month = DateTime.DaysInMonth(DateTime.Now.Year, DateTime.Now.Month);
            for (int i = 1; i <= Month; i++)
            {
                DateNow += "'" + i + "'" + ",";
            }
            return DateNow.Remove(DateNow.Length - 1);
        }

    }
}