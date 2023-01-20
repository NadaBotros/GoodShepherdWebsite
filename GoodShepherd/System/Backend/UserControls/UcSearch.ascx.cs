using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using DAL;
namespace System.Backend.UserControls
{
    public partial class UcSearch : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void drpAbA3traf_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem item in drpAbA3traf.Items)
                item.Selected = true;
        }
        protected void drpJob_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem item in drpJob.Items)
                item.Selected = true;
        }
        public DataTable GetDataTable(string Fillter)
        {
            #region Ab3traf
            List<string> AbA3traf = new List<string>();
            foreach (ListItem item in drpAbA3traf.Items)
            {
                if (item.Selected)
                    AbA3traf.Add(item.Value);
            }
            #endregion
            #region Area
            List<string> Area = new List<string>();
            foreach (ListItem item in drpArea.Items)
            {
                if (item.Selected)
                    Area.Add(item.Value);
            }
            #endregion
            #region BooldType
            List<string> BooldType = new List<string>();
            foreach (ListItem item in drpBooldType.Items)
            {
                if (item.Selected)
                    BooldType.Add(item.Value);
            }
            #endregion
            #region Job
            List<string> Job = new List<string>();
            foreach (ListItem item in drpJob.Items)
            {
                if (item.Selected)
                    Job.Add(item.Value);
            }
            #endregion
            #region MaritalStatus
            List<string> MaritalStatus = new List<string>();
            foreach (ListItem item in drpMaritalStatus.Items)
            {
                if (item.Selected)
                    MaritalStatus.Add(item.Value);
            }
            #endregion
            #region Relationship
            List<string> Relationship = new List<string>();
            foreach (ListItem item in drpRelationship.Items)
            {
                if (item.Selected)
                    Relationship.Add(item.Value);
            }
            #endregion
            #region Studious
            List<string> Studious = new List<string>();
            foreach (ListItem item in drpStudious.Items)
            {
                if (item.Selected)
                    Studious.Add(item.Value);
            }
            #endregion          

            PersonManagement _PersonManagement = new PersonManagement();
            if (Fillter == "ServantPersons")
            {
                return _PersonManagement.PersonSearchNotHasServant(AbA3traf, Area, BooldType, Job, MaritalStatus, Relationship, Studious,  txtSearch.Text);
            }
            else
               return _PersonManagement.PersonSearch(AbA3traf, Area, BooldType, Job, MaritalStatus, Relationship, Studious,  txtSearch.Text);

        }

        protected void drpCity_SelectedIndexChanged(object sender, EventArgs e)
        {
            List<string> Cities = new List<string>();
            foreach (ListItem item in drpCity.Items)
                if (item.Selected)
                    Cities.Add(item.Value);
            AreaManagement obj = new AreaManagement();
            drpArea.DataSource = obj.LoadByDeleteState(Cities);
            drpArea.DataBind();
            foreach (ListItem item in drpArea.Items)
                item.Selected = true;
        }

        protected void drpCity_DataBound(object sender, EventArgs e)
        {
            foreach (ListItem item in drpCity.Items)
                item.Selected = true;
            drpCity_SelectedIndexChanged(sender, e);
        }

    }
}