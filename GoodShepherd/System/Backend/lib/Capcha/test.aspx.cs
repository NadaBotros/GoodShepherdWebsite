using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


    public partial class test : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnCheck_Click(object sender, EventArgs e)
        {
            if (MyCaptcha1.IsValid)
                lblCheckResult.Text = "It is ok";
            else
                lblCheckResult.Text = "oops!, invalid text was entered.";
        }
    }
