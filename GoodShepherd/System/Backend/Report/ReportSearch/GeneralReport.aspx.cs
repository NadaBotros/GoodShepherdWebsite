using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Backend
{
    public partial class GeneralReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
          
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            ListBox lstFields = new ListBox();
            foreach (ListItem item in chkViewData.Items)
            {
                if (item.Selected)
                    lstFields.Items.Add(item);
            }
            List<string> lstIds=UcAdvancedSearch.GetData();
            Session["lstFields"] = lstFields;
            Session["lstIds"] = lstIds;
           


            string strUrl = "../ReportViewer/GeneralReportV.aspx?orderby=" + radOrderBy.SelectedValue;
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "popup", "window.open('" + strUrl + "','_blank')", true);
        }
        protected void btnReport2_Click(object sender, EventArgs e)
        {   
            ListBox lstFields = new ListBox();
            foreach (ListItem item in chkViewData.Items)
            {
                if (item.Selected)
                    lstFields.Items.Add(item);
            }
            List<string> lstIds = UcAdvancedSearch.GetData2();
            Session["lstFields"] = lstFields;
            Session["lstIds"] = lstIds;

            string strUrl = "../ReportViewer/PeopleWithNoServantsRV.aspx?orderby=" + radOrderBy.SelectedValue;
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "popup", "window.open('" + strUrl + "','_blank')", true);
        }
        protected void btnReport3_Click(object sender, EventArgs e)
        {
            ListBox lstFields = new ListBox();
            foreach (ListItem item in chkViewData.Items)
            {
                if (item.Selected)
                    lstFields.Items.Add(item);
            }
          
            Session["lstFields"] = lstFields;
       
            string strUrl = "../ReportViewer/AllPeopleRV.aspx?orderby=" + radOrderBy.SelectedValue;
            ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "popup", "window.open('" + strUrl + "','_blank')", true);
        }
        //protected void btnReport4_Click(object sender, EventArgs e)
        //{
        //    ListBox lstFields = new ListBox();
        //    foreach (ListItem item in chkViewData.Items)
        //    {
        //        if (item.Selected)
        //            lstFields.Items.Add(item);
        //    }
        //    List<string> lstIds = UcAdvancedSearch.GetData2();
        //    Session["lstFields"] = lstFields;
        //    Session["lstIds"] = lstIds;

        //    string strUrl = "../ReportViewer/FilterPeopleHaveServantsRV.aspx?orderby=" + radOrderBy.SelectedValue;
        //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "popup", "window.open('" + strUrl + "','_blank')", true);
        //}
        //protected void btnReport5_Click(object sender, EventArgs e)
        //{
        //    ListBox lstFields = new ListBox();
        //    foreach (ListItem item in chkViewData.Items)
        //    {
        //        if (item.Selected)
        //            lstFields.Items.Add(item);
        //    }
        //    List<string> lstIds = UcAdvancedSearch.GetData2();
        //    Session["lstFields"] = lstFields;
        //    Session["lstIds"] = lstIds;

        //    string strUrl = "../ReportViewer/FilterPepleWithNoServantsRV.aspx?orderby=" + radOrderBy.SelectedValue;
        //    ScriptManager.RegisterClientScriptBlock(UpdatePanel1, UpdatePanel1.GetType(), "popup", "window.open('" + strUrl + "','_blank')", true);
        //}
    }
}