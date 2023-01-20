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
using System.Data;
namespace System.Backend
{
    public partial class ServantPersonsList : MangeBackend
    {
        #region Variables
        DAL.ServantPersonsManagement obj = new DAL.ServantPersonsManagement();
        #endregion
        #region EventHanlder
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                drpServant.DataBind();
                UcPersonGrid.SetData(obj.ServantPersons(drpServant.SelectedValue));
            }
        }
        protected void btnTransfer_Click(object sender, EventArgs e)
        {
            obj.Edit(UcPersonGrid.GetSelectedPersons(), drpServantTransfer.SelectedValue);
            UcPersonGrid.SetData(obj.ServantPersons(drpServant.SelectedValue));
        }
        #endregion



        protected void drpServant_SelectedIndexChanged(object sender, EventArgs e)
        {
            UcPersonGrid.SetData(obj.ServantPersons(drpServant.SelectedValue));
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            obj.Delete(UcPersonGrid.GetSelectedPersons());
            UcPersonGrid.SetData(obj.ServantPersons(drpServant.SelectedValue));
        }
     

    }
}

