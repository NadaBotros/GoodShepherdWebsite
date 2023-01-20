using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class Site : System.Web.UI.MasterPage
{
    protected void Page_Load(object sender, EventArgs e)
    {
        if (!IsPostBack)
            GetInfo();
        if (Session["Loginuser"] != null && !IsPostBack)
        {
            VisitorManagement obj = new VisitorManagement();
            obj.IncVisit(Session["Loginuser"].ToString());
        }
    }
    public void GetInfo()
    {
        try
        {
            SaveValuesManage saveobj = new SaveValuesManage();
            ltrCurrentTheme.Text = "<link href='themes/" + saveobj.LoadById(3) + "/StyleSheet.css' rel='stylesheet' />";
            DateToCoptic _DateToCoptic = new DateToCoptic();
            lblDate.Text = _DateToCoptic.CurrentDateMeladyAndCoptic(GeneralMethods.GetEgyptTime());
            if (Request.Cookies["PersonId"] != null)
            {
                btnLogin.Text = "خروج";
            }
            else
            {
                btnLogin.Text = "دخول المستخدم";
                btnLogin.PostBackUrl = "login.aspx";
            }
        }
        catch { }
    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        try
        {
            if (Request.Cookies["PersonId"] != null)
            {
                HttpCookie UserId = new HttpCookie("PersonId");
                UserId.Expires = DateTime.Now.AddDays(-30);
                Response.Cookies.Add(UserId);
                Response.Redirect("login.aspx");
            }

        }
        catch { }
    }

    protected void btnSaerch_Click(object sender, ImageClickEventArgs e)
    {
        Response.Redirect("sitesearch.aspx?q=" + txtSearch.Text);
    }

    protected void btnSaveContacts_Click(object sender, EventArgs e)
    {
        SubscribeManage obj = new SubscribeManage();
        bool result = obj.Add(txtAddToContacts.Text);
        if (result)
            Response.Redirect("viewmessage.aspx?type=1");
        else
            Response.Redirect("viewmessage.aspx?type=2");
    }
}
