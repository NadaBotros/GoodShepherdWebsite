using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


    public partial class viewmessage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["type"] != null)
            {
                switch (Request.QueryString["type"].ToString())
                {
                    case "1":
                        lblTitle.Text = "تم تسجيل بريدك الاليكتروني بنجاح";
                        break;
                    case "2":
                        lblTitle.Text = "تم التسجيل سابقا بهذا البريد الاليكتروني";
                        break;
                }
            }
        }
    }
