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
    public partial class CountriesTrafic : MangeBackend
    {
        #region Variables
        AdminManagement AdminManagementObj;
        #endregion
        #region Methods
       
        #endregion
        #region EventHanlder
        protected void Page_Load(object sender, EventArgs e)
        {
           
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            Response.Redirect("UserManagement.aspx");
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
            ExportFile(grdData, drpExport.SelectedValue,false);
        }     
        #endregion      
    }
}