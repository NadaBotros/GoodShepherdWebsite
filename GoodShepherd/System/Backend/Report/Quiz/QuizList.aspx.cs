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
    public partial class QuizList : MangeBackend
    {
        #region Variables
        QuizManage QuizManageObj;
        #endregion
        #region EventHanlder
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            QuizManageObj = new QuizManage();
            switch (e.CommandName)
            {
                case "restoreitem":
                    if (QuizManageObj.Restore(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { grdData.DataBind(); }
                    break;
                case "deleteitem":
                    if (QuizManageObj.Delete(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { grdData.DataBind(); }
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