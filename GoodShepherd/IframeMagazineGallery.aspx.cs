using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


public partial class IframeMagazineGallery : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {
        SaveValuesManage saveobj = new SaveValuesManage();
        ltrCurrentTheme.Text = "<link href='themes/" + saveobj.LoadById(3) + "/StyleSheet.css' rel='stylesheet' />";
    }
}
