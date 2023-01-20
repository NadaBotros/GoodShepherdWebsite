using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace System.Backend
{
    public partial class ServantPersonsRV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.QueryString["id"] != null)
            {
                ServantAftkadManagement obj = new ServantAftkadManagement();
                Prg_Servant ent = obj.LoadById(Request.QueryString["id"].ToString());
                if (ent != null)
                {
                    lblTitle.Text = "كشف افتقاد " + ent.Prg_Person.PersonName;
                }
            }
        }
    }
}