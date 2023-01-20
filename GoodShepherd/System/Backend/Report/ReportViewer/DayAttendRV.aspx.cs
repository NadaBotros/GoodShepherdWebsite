using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace System.Backend
{
    public partial class DayAttendRV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // try
            {
                if (Request.QueryString["Date"] != null && Request.QueryString["Attend"] != null)
                {
                    ServantAftkadManagement obj = new ServantAftkadManagement();
                    string title = "كشف ";
                    if (Request.QueryString["Attend"] == "True")
                        title += "حضور ";
                    else
                        title += "غياب ";
                    title += "اجتماع " + Request.QueryString["Date"].ToString();
                    if (Request.QueryString["id"] != null && !string.IsNullOrEmpty(Request.QueryString["id"]))
                        title += " للخادم " + obj.LoadById(Request.QueryString["id"].ToString()).Prg_Person.PersonName;
                    lblTitle.Text = title;
                    if (Request.QueryString["Attend"] == "True")
                    {
                        grdData.DataBind();
                        lblMsg.Text = "اجمالي عدد الحاضرين : " + grdData.Rows.Count;
                    }
                    else
                    {
                        grdData.DataBind();
                        lblMsg.Text = "اجمالي عدد الغياب : " + grdData.Rows.Count;
                    }
                }
            }
            //catch { return; }
        }
    }
}