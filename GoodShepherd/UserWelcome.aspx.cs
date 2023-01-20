using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

    public partial class UserWelcome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["PersonId"] != null)
            {
                PersonManagement obj = new PersonManagement();
                Prg_Person ent = obj.LoadById(Request.Cookies["PersonId"].Value);
                if (ent != null)
                {
                    lblWelcome.Text = "مرحبا بك : - " + ent.PersonName;
                }
            }
        }
    }
