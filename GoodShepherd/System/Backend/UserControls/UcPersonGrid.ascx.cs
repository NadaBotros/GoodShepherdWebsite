using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
namespace System.Backend.UserControls
{
    public partial class UcPersonGrid : System.Web.UI.UserControl
    {
        DAL.ServantPersonsManagement obj = new DAL.ServantPersonsManagement();
        public static DataTable d;
        //public object PageIndex { get; internal set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            

        }
        public void SetData(DataTable dt)
        {
            grdData.DataSource = dt;
            grdData.DataBind();
            d = dt;


        }
        public void SetData2(DataTable dt, Label l)
        {
            grdData.DataSource = dt;
            grdData.DataBind();
            d = dt;
            l.Text = "   اجمالي العدد: - " + grdData.Rows.Count;

        }
        public List<string> GetSelectedPersons()
        {
            List<string> Persons = new List<string>();
            foreach (GridViewRow row in grdData.Rows)
            {
                CheckBox chk = row.FindControl("chkItem") as CheckBox;
                if (chk != null)
                {
                    if (chk.Checked)
                    {
                        Label lblId = row.FindControl("lblId") as Label;
                        if (!string.IsNullOrEmpty(lblId.Text))
                            Persons.Add(lblId.Text);
                    }
                }
            }
            return Persons;
        }
        protected void GridView_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdData.PageIndex = e.NewPageIndex;            // GRIDVIEW PAGING.
            grdData.DataSource = d;
            grdData.DataBind();
        }


    }
}