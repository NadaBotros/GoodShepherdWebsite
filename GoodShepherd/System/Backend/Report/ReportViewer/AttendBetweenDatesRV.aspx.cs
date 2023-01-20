using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;

using iTextSharp.text.pdf;
using System.Diagnostics;
using System.IO;

namespace System.Backend
{
    public partial class AttendBetweenDatesRV : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (Request.QueryString["id"] != null && Request.QueryString["DateFrom"] != null && Request.QueryString["DateTo"] != null)
                {
                    #region Datatable
                    ReportManagement obj = new ReportManagement();
                    List<PersonMainInfo> Persons = obj.ServantPersons(Request.QueryString["id"].ToString());

                    List<Prg_PersonAttendance> AttendList = obj.PersonsAttendanceBetweenDates(Persons.Select(x => x.PersonId.ToString()).ToList(), Request.QueryString["DateFrom"].ToString(), Request.QueryString["DateTo"].ToString());
                    List<DateTime> MeetingDates = AttendList.OrderBy(y => y.AttendDate).Select(x => x.AttendDate).Distinct().ToList();


                    DataTable dt = new DataTable();
                    dt.Columns.Add(new DataColumn("PersonId"));
                    dt.Columns.Add(new DataColumn("PersonName"));
                    dt.Columns.Add(new DataColumn("PersonCode"));
                    dt.Columns.Add(new DataColumn("MobileNo1"));
                    dt.Columns.Add(new DataColumn("MobileNo2"));
                    // OLD 
                    //dt.Columns.Add(new DataColumn("Studious"));

                    //New 
                    dt.Columns.Add(new DataColumn("HomePhone"));



                    foreach (DateTime date in MeetingDates)
                    {
                        dt.Columns.Add(new DataColumn(date.Day + "-" + date.Month));
                        BoundField customField1 = new BoundField();
                        customField1.ShowHeader = true;
                        customField1.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                        customField1.HeaderText = date.Month + "-" + date.Day;
                        customField1.DataField = date.Day + "-" + date.Month;
                        grdData.Columns.Add(customField1);
                    }
                    BoundField customField2 = new BoundField();
                    dt.Columns.Add(new DataColumn("Total"));
                    customField2.ShowHeader = true;
                    customField2.HeaderText = "م";
                    customField2.DataField = "Total";
                    customField2.ItemStyle.HorizontalAlign = HorizontalAlign.Center;
                    grdData.Columns.Add(customField2);
                    foreach (PersonMainInfo x in Persons)
                    {
                        DataRow dr = dt.NewRow();
                        dr["PersonId"] = x.PersonId.ToString();
                        dr["PersonName"] = x.PersonName;
                        dr["PersonCode"] = x.PersonCode;
                        dr["MobileNo1"] = x.MobileNo1;
                        dr["MobileNo2"] = x.MobileNo2;
                        dr["HomePhone"] = x.HomePhone;


                        foreach (DateTime date in MeetingDates)
                        {
                            dr[date.Day + "-" + date.Month] = GetPersonAttend(AttendList, x.PersonId.ToString(), date);
                        }
                        dr["Total"] = AttendList.Where(y => y.PersonId == x.PersonId).Count();
                        dt.Rows.Add(dr);
                    }
                    grdData.DataSource = dt;
                    grdData.DataBind();
                    #endregion
                    lblPageTitle.Text = "كشف حضور الاجتماع من الفترة " + DateTime.Parse(Request.QueryString["DateFrom"].ToString()).ToShortDateString() + " الي " + DateTime.Parse(Request.QueryString["DateTo"].ToString()).ToShortDateString();
                    fillAttendCount();
                }

            }
            catch { return; }
        }
        public string GetPersonAttend(List<Prg_PersonAttendance> PersonAttendance, string PersonId, DateTime dt)
        {
            if (PersonAttendance.Any(x => x.AttendDate == dt && x.PersonId == new Guid(PersonId)))
                return "√";
            else
                return "×";
        }
        void fillAttendCount()
        {
            grdData.DataBind();
            lblAttendAllNo.Text = "   اجمالي العدد: - " + grdData.Rows.Count;
        }


    }
}