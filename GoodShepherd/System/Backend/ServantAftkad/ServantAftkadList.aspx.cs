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
namespace System.Backend
{
    public partial class ServantAftkadList : MangeBackend
    {
        #region Variables
        DAL.ServantAftkadManagement ServantAftkadManagementObj;
        #endregion
        #region EventHanlder
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ServantAftkadManagementObj = new ServantAftkadManagement();
            switch (e.CommandName)
            {
                case "deleteitem":
                    if (ServantAftkadManagementObj.ServantPersonsCount(e.CommandArgument.ToString()) > 0)
                    { MPEPersonInfo.Show(); }
                    else
                    {
                        if (ServantAftkadManagementObj.Delete(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                        { grdData.DataBind(); }
                    }
                    break;
            }
        }
        protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == 0)
            {
                grdData.UseAccessibleHeader = true;
                grdData.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
         
        }
        protected void drpExport_SelectedIndexChanged(object sender, EventArgs e)
        {
            ExportFile(grdData, drpExport.SelectedValue, true);
        }
       
        #endregion
    }
}