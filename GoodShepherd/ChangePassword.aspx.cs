using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;


    public partial class ChangePassword : ManageSite
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (Request.Cookies["PersonId"] != null)
            {
                PersonManagement obj = new PersonManagement();
                if (obj.ChangePassword(Request.Cookies["PersonId"].Value, txtoldPassword.Text, txtNewPassword.Text))
                {
                    lblMSG.Text = "تم تغير كلمة المرور بنجاح";
                    txtconfirm.Text = txtNewPassword.Text = txtoldPassword.Text = string.Empty;
                }
                else
                    lblMSG.Text = "كلمة المرور القديمة غير صحيحة";
            }
        }
    }
