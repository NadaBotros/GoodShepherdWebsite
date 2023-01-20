using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Backend.Pages
{
    public partial class PageSectionList : MangeBackend
    {
        #region Variables
        DAL.PageSectionManagement PageSectionManagementObj;
        #endregion
        #region EventHanlder
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        #endregion
        protected void rptSections_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            PageSectionManagementObj = new DAL.PageSectionManagement();
            switch (e.CommandName)
            {
                case "restoreitem":
                    if (PageSectionManagementObj.Restore(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { rptSections.DataBind(); }
                    break;
                case "deleteitem":
                    if (PageSectionManagementObj.Delete(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { rptSections.DataBind(); }
                    break;
                case "ArrowDown":
                    PageSectionManagementObj.ReOrder(e.CommandArgument.ToString(), false, Request.Cookies["UserWebsiteId"].Value, bool.Parse(drpViews.SelectedValue));
                    rptSections.DataBind();
                    break;
                case "ArrowUp":
                    PageSectionManagementObj.ReOrder(e.CommandArgument.ToString(), true, Request.Cookies["UserWebsiteId"].Value, bool.Parse(drpViews.SelectedValue));
                    rptSections.DataBind();
                    break;
            }
        }
        public string ShowArrow(object Recorder, string Type)
        {
            PageSectionManagementObj = new DAL.PageSectionManagement();
            int RowsCount = PageSectionManagementObj.LoadItemsCount(bool.Parse(drpViews.SelectedValue),drpPages.SelectedValue);
            return MangeShowArrow(Recorder, Type, RowsCount);
        }
    }
}