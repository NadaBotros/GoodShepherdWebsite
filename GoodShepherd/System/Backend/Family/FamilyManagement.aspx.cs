using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.html.simpleparser;
using iTextSharp.text.pdf;
using System.Text;
using DAL;
namespace System.Backend
{
    public partial class FamilyManagement : MangeBackend
    {
        #region Variables
        DAL.FamilyManagement _FamilyManagementObj;
        #endregion
        #region Property
        public string FamilyId
        {
            set { ViewState["FamilyId"] = value; }
            get { return ViewState["FamilyId"] == null ? string.Empty : ViewState["FamilyId"].ToString(); }
        }
        public string PersonId
        {
            set { ViewState["PersonId"] = value; }
            get { return ViewState["PersonId"] == null ? string.Empty : ViewState["PersonId"].ToString(); }
        }
        #endregion
        #region Family
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    FamilyId = Request.QueryString["id"].ToString();
                }
                GetInfo();

            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("FamilyList.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            if (Save())
                Response.Redirect("familymanagement.aspx");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            GetInfo();
        }
        public void Clear()
        {
            txtBuildingNo.Text = txtFamilyCode.Text = txtFlatNo.Text = txtFloorNo.Text = txtMarriageDate.Text = txtNextTo.Text =
                txtNotes.Text = txtPhone.Text = txtStreet.Text = string.Empty;
            drpArea.SelectedIndex = drpCity.SelectedIndex = drpResbonsable.SelectedIndex = -1;
        }
        public void GetInfo()
        {
            if (string.IsNullOrEmpty(FamilyId))
                tabFamilyInfo.Visible = false;
            else
            {
                tabFamilyInfo.Visible = true;
                odsPersons.SelectParameters["FamilyId"].DefaultValue = FamilyId;
                odsData.SelectParameters["FamilyId"].DefaultValue = FamilyId;
                odsPersons.DataBind();
                grdData.DataBind();
                drpResbonsable.DataBind();
                _FamilyManagementObj = new DAL.FamilyManagement();
                Prg_Family obj = _FamilyManagementObj.LoadById(FamilyId);
                if (obj != null)
                {
                    txtBuildingNo.Text = obj.BuildingNo;
                    txtFamilyCode.Text = obj.FamilyCode;
                    txtFlatNo.Text = obj.FlatNo;
                    txtFloorNo.Text = obj.FloorNo;
                    if (obj.MarriageDate != null)
                        txtMarriageDate.Text = obj.MarriageDate.Value.ToString("d/M/yyyy");
                    else
                        txtMarriageDate.Text = string.Empty;
                    txtNextTo.Text = obj.BuildingNextTo;
                    txtNotes.Text = obj.AddressNotes;
                    txtPhone.Text = obj.HomePhone;
                    txtStreet.Text = obj.StreetName;
                    drpCity.DataBind();
                    if (obj.AreaId != null)
                    {
                        if (drpCity.Items.FindByValue(obj.Prg_Area.CityId.ToString()) != null)
                        {
                            drpCity.SelectedValue = obj.Prg_Area.CityId.ToString();
                            drpArea.DataBind();
                            if (drpArea.Items.FindByValue(obj.AreaId.ToString()) != null)
                            {
                                drpArea.SelectedValue = obj.AreaId.ToString();
                            }
                        }
                    }
                }
            }
        }
        public bool Save()
        {
            _FamilyManagementObj = new DAL.FamilyManagement();
            if (_FamilyManagementObj.Edit(FamilyId, drpResbonsable.SelectedValue, txtNotes.Text, drpArea.SelectedValue, txtNextTo.Text,
                 txtBuildingNo.Text, txtFlatNo.Text, txtFloorNo.Text, txtPhone.Text, txtStreet.Text,txtMarriageDate.Text , Request.Cookies["UserWebsiteId"].Value))
            { BackendMessages(101); return true; }
            else
            { BackendMessages(201); return false; }
        }
        #endregion
        #region Person
        public string SavePerson()
        {
            if (string.IsNullOrEmpty(FamilyId))
            {
                _FamilyManagementObj = new DAL.FamilyManagement();
                FamilyId = _FamilyManagementObj.Add(Request.Cookies["UserWebsiteId"].Value);
                PersonId = UcPerson.Save(PersonId, FamilyId);
                _FamilyManagementObj.ChangeResponsable(FamilyId, PersonId, Request.Cookies["UserWebsiteId"].Value);
            }
            else
                PersonId = UcPerson.Save(PersonId, FamilyId);
            
            GetInfo();
            return PersonId;
        }
        protected void grdData_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowIndex == 0)
            {
                grdData.UseAccessibleHeader = true;
                grdData.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }
        protected void grdData_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            _FamilyManagementObj = new DAL.FamilyManagement();
            switch (e.CommandName)
            {

                case "deleteitem":
                    if (_FamilyManagementObj.Delete(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { grdData.DataBind(); }
                    break;
                case "viewitem":
                    PersonId = e.CommandArgument.ToString();
                    UcPerson.GetInfo(PersonId);
                    MPEPersonInfo.Show();
                    break;
            }
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            PersonId = string.Empty;
            UcPerson.Clear();
            MPEPersonInfo.Show();
        }
        protected void btnPersonClear_Click(object sender, EventArgs e)
        {
            UcPerson.Clear();
        }
        protected void btnPersonSaveAndNew_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SavePerson()))
            {
                PersonId = string.Empty;
                UcPerson.Clear();
            }
            MPEPersonInfo.Show();
        }
        protected void btnPesonSaver_Click(object sender, EventArgs e)
        {
            SavePerson();
            UcPerson.GetInfo(PersonId);
            MPEPersonInfo.Show();
        }
        #endregion
        [System.Web.Services.WebMethod]
        public static string[] GetStreetsList(string prefixText)
        {
            DAL.FamilyManagement obj = new DAL.FamilyManagement();
            return obj.StreetsName(prefixText);
        }
        [System.Web.Services.WebMethod]
        public static string[] GetFatherNameList(string prefixText)
        {
            PersonManagement _PersonManagement = new PersonManagement();
            return _PersonManagement.GetFatherNameList(prefixText);
        }
        [System.Web.Services.WebMethod]
        public static string[] GetJobList(string prefixText)
        {
            PersonManagement _PersonManagement = new PersonManagement();
            return _PersonManagement.GetJobList(prefixText);
        }
    }
}