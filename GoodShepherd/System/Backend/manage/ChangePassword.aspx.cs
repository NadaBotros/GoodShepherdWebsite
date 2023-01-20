using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace System.Backend
{
    public partial class ChangePassword : MangeBackend
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }        
        protected void btnSave_Click(object sender, EventArgs e)
        {
            DAL.AdminManagement obj = new DAL.AdminManagement();
            if (obj.ChangePassword(Request.Cookies["UserWebsiteId"].Value, txtoldPassword.Text, txtNewPassword.Text))
            {
                BackendMessages(101);
            }
            else
                BackendMessages(201);
        }
    }
}