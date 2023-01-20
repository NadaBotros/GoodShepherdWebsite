using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Backend
{
    public partial class ImagesSizeList : MangeBackend
    {
        #region EventHanlder
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            DAL.ImagesSizesManagement ImageSizesManagementObj;
            switch (e.CommandName)
            {
                case "restoreitem":
                    ImageSizesManagementObj = new DAL.ImagesSizesManagement();
                    ImageSizesManagementObj.Restore(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value);
                    grdData.DataBind();
                    break;
                case "deleteitem":
                    ImageSizesManagementObj = new DAL.ImagesSizesManagement();
                    ImageSizesManagementObj.Delete(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value);
                    grdData.DataBind();
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