using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

public partial class Login : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }

    protected void btnLogin_Click(object sender, EventArgs e)
    {
        PersonManagement obj = new PersonManagement();
        string UserId = obj.CheckUserLogin(txtCode.Text, txtpassword.Text);
        if (string.IsNullOrEmpty(UserId))
            lblMSG.Text = "يوجد خطا فى البيانات المدخله , اعد المحاولة";
        else
        {
            HttpCookie PersonId = new HttpCookie("PersonId");
            PersonId.Value = UserId;
            PersonId.Expires = DateTime.Now.AddDays(2);
            Response.Cookies.Add(PersonId);
            Response.Redirect("UserWelcome.aspx");
        }
    }

    protected void btnForgetPassword_OnClick(object sender, EventArgs e)
    {
        Response.Redirect("~/ForgetPassword.aspx");
    }
}
