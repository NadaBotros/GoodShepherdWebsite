using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Backend
{
    public partial class ServantPersonsManagement : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {

            UcPersonGrid.SetData(UcSearch.GetDataTable("ServantPersons"));
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            DAL.ServantPersonsManagement obj = new DAL.ServantPersonsManagement();
            obj.Add(drpServant.SelectedValue, UcPersonGrid.GetSelectedPersons());
            UcPersonGrid.SetData(UcSearch.GetDataTable("ServantPersons"));
        }
        

    }
}