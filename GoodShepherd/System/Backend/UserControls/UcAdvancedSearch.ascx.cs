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
    public partial class UcAdvancedSearch : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                FillDrops();
        }
        void FillDrops()
        {
            for (int i = 1; i <= 31; i++)
            {
                drpBDDay.Items.Add(i.ToString());
            }
            for (int i = 1; i <= 12; i++)
            {
                drpBDMonh.Items.Add(i.ToString());
            }
            drpBDDay.DataBind();
            drpBDMonh.DataBind();
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
        public List<string> GetData()
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
            #region Servants
            List<string> Servants = new List<string>();
            foreach (ListItem item in drpServant.Items)
            {
                if (item.Selected)
                    Servants.Add(item.Value);
            }
            #endregion
            PersonManagement _PersonManagement = new PersonManagement();
            return _PersonManagement.PersonAdvancedSearch(AbA3traf, Area, BooldType, Job, MaritalStatus, Relationship, Studious, Servants, txtSearch.Text, drpBDMonh.SelectedValue, drpBDDay.SelectedValue);

        }

        public List<string> GetData2()
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
            #region Servants
            List<string> Servants = new List<string>();
            foreach (ListItem item in drpServant.Items)
            {
                if (item.Selected)
                    Servants.Add(item.Value);
            }
            #endregion



            PersonManagement _PersonManagement = new PersonManagement();
            return _PersonManagement.PeopleEHaveNoServantsSearch(AbA3traf, Area, BooldType, Job,MaritalStatus, Relationship, Studious, txtSearch.Text, drpBDMonh.SelectedValue, drpBDDay.SelectedValue);
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

        protected void drpBDDayFrom_DataBound(object sender, EventArgs e)
        {
            drpBDDay.Items.Insert(0, new ListItem("اختر", "0"));
        }



        protected void drpBDMonhFrom_DataBound(object sender, EventArgs e)
        {
            drpBDMonh.Items.Insert(0, new ListItem("اختر", "0"));
        }

        protected void drpServant_DataBound(object sender, EventArgs e)
        {
            //foreach (ListItem item in drpServant.Items)
            //    item.Selected = true;
        }

        protected void btnExport_OnClick(object sender, EventArgs e)
        {
            var reportManagement=new ReportManagement();
            CSVExporter.WriteToCSV(reportManagement.GetByPersonsIds(GetData()));
        }
    }
}