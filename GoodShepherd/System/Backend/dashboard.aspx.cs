using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;

namespace System.Backend
{
    public partial class dashboard : MangeBackend
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Application["ActiveUsers"] != null)
                    lblCurrent.Text = Application["ActiveUsers"].ToString();
                else
                    lblCurrent.Text = "0";
                VisitorManagement obj = new VisitorManagement();
                lblTotal.Text = obj.VisitorsNumber().ToString();
                lblPages.Text = obj.PageVisitorNumber().ToString();
                lbllast30days.Text = obj.VisitorsNumberLast30Days().ToString();
                SaveValuesManage saveobj = new SaveValuesManage();
                drpSound.SelectedValue = saveobj.LoadById(1);
                drpVideo.SelectedValue = saveobj.LoadById(2);
                drpTheme.SelectedValue = saveobj.LoadById(3);
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            SaveValuesManage saveobj = new SaveValuesManage();
            saveobj.Edit(1, drpSound.SelectedValue);
            saveobj.Edit(2, drpVideo.SelectedValue);
            saveobj.Edit(3, drpTheme.SelectedValue);
        }
    }
}