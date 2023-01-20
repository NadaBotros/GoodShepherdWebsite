using DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;


    public partial class MyInfo : ManageSite
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                GetInfo();
            }
        }
        public void GetInfo()
        {
            PersonManagement obj=new DAL.PersonManagement();
            PersonHobbiesManagement _PersonHobbiesManagement = new DAL.PersonHobbiesManagement();
            Prg_Person ent = obj.LoadById(Request.Cookies["PersonId"].Value);
            if (ent != null)
            {
                txtEmail.Text = ent.Email;
                txtFacebook.Text = ent.FaceBook;
                txtFatherName.Text = ent.FatherName;
                txtJob.Text = ent.Job;
                txtJobPlace.Text = ent.JobPlace;
                txtMobile.Text = ent.MobileNo1;
                txtMobile2.Text = ent.MobileNo2;
                txtSkype.Text = ent.Skype;
                drpMaritalStatus.SelectedValue = ent.MaritalStatus;
                drpBooldType.SelectedValue = ent.BloodType;
                List<string> HobbiesId = _PersonHobbiesManagement.LoadPersonIds(Request.Cookies["PersonId"].Value);
                chkHobbies.DataBind();
                foreach (string hid in HobbiesId)
                {
                    ListItem item = chkHobbies.Items.FindByValue(hid.ToLower());
                    if (item != null)
                        item.Selected = true;
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            PersonManagement obj = new DAL.PersonManagement();
            List<string> HobbiesId = new List<string>();
            foreach (ListItem item in chkHobbies.Items)
            {
                if (item.Selected)
                    HobbiesId.Add(item.Value);
            }
            bool result = obj.Edit(Request.Cookies["PersonId"].Value,  drpBooldType.SelectedValue, txtEmail.Text,
                                                    txtFacebook.Text, txtFatherName.Text, txtJob.Text, txtJobPlace.Text, drpMaritalStatus.SelectedValue,
                                                    txtMobile.Text, txtMobile2.Text, txtSkype.Text,  radWhatsApp.SelectedValue, HobbiesId);
            if (result)
            {
                lblMsg.Text = "تم حفظ البيانات بنجاح .";
                GetInfo();
            }
            else
            {
                lblMsg.Text = "يوجد خطا اعد المحاولة";
            }
        }
        
    }
