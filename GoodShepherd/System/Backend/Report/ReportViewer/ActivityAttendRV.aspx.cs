using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace System.Backend
{
    public partial class ActivityAttendRV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // try
            {
                if (Request.QueryString["id"] != null && Session["SectionsIds"] != null)
                {
                    var obj = new ActivitiesManage();
                    Activity activity = obj.LoadById(Request.QueryString["id"].ToString());
                    if (activity != null)
                    {
                        lblTitle.Text = "كشف حضور  " + activity.ActivityTitle;
                        var activitySectionUserManagement = new ActivitySectionUserManagement();
                        List<spActivitiesAttendResult> result = activitySectionUserManagement.ActivitiesAttendReport(Session["SectionsIds"].ToString());
                        //grdData.DataSource = result;
                        //grdData.DataBind();
                        int SectionsCount = Session["SectionsIds"].ToString().Split(',').Count();
                        string stats = "اجمالي عدد الاشخاص :- " + result.Count();
                        int attendNum = (int)result.Select(x => x.ActivitiesCount).Sum();
                        stats += "<br> اجمالي عدد حضور الفقرات :- " + attendNum;
                        stats += "<br> اجمالي عدد غياب الفقرات :- " + ((SectionsCount * result.Count()) - attendNum);
                        lblMsg.Text = stats;
                    }
                }
            }
            //catch { return; }
        }

        
    }
}