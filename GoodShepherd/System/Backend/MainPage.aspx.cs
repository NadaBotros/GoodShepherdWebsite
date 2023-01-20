using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAL;
namespace System.Backend
{
    public partial class Admin : System.Web.UI.Page
    {
        SiteTreeManage treeObj = new SiteTreeManage();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["UserWebsiteId"] != null)
            {
                AdminManagement _adminManagement = new AdminManagement();
                DAL.Admin _admin = _adminManagement.LoadById(Request.Cookies["UserWebsiteId"].Value);
                if (_admin != null)
                {
                    lblUserName.Text = _admin.UserName;
                    lblEmail.Text = _admin.Email;
                    if (!string.IsNullOrEmpty(_admin.Image))
                    {
                        imgUser.ImageUrl = "~/Images/S55_55/" + _admin.Image;
                    }
                }
            }

        }
        protected void rptMenu_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            RepeaterItem item = e.Item;
            if ((item.ItemType == ListItemType.Item) ||
                (item.ItemType == ListItemType.AlternatingItem))
            {
                Label lblId = e.Item.FindControl("lblId") as Label;
                Literal ltrEmpty = e.Item.FindControl("ltrEmpty") as Literal;
                Repeater rptImtems = e.Item.FindControl("rptSubMenu") as Repeater;
                DataTable dt = treeObj.LoadByDeleteState("True", lblId.Text, Request.Cookies["UserWebsiteId"].Value);
                rptImtems.DataSource = dt;
                rptImtems.DataBind();
                if (dt.Rows.Count == 0)
                    ltrEmpty.Text = "<li>لا يوجد صفحات مسموحه لك فى هذا القسم</li>";
            }

        }
    }
}