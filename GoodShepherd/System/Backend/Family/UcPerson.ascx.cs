using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace System.Backend
{
    public partial class UcPerson : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        public void GetInfo(string PersonId)
        {
            var obj = new PersonManagement();
            var personHobbiesManagement = new PersonHobbiesManagement();
            var ent = obj.LoadById(PersonId);
            if (ent != null)
            {
                if (!string.IsNullOrEmpty(ent.UserImage))
                    lnkImage.ImageUrl = "~/images/S150_150/" + ent.UserImage;
                else if (ent.Sex)
                    lnkImage.ImageUrl = "~/themes/Default/img/avatarmale.jpg";
                else
                    lnkImage.ImageUrl = "~/themes/Default/img/avatarFemale.jpg";
                lnkImage.NavigateUrl = "~/images/actualsize/" + ent.UserImage;

                txtBirthDate.Text = ent.BirthDate != null ? ent.BirthDate.Value.ToString("d/M/yyyy") : string.Empty;

                drpRelationship.SelectedValue = ent.Relationship;
                drpRelationship.Enabled = false;
                txtCode.Text = ent.PersonCode;
                txtEmail.Text = ent.Email;
                txtFacebook.Text = ent.FaceBook;
                txtFatherName.Text = ent.FatherName;
                txtFullName.Text = ent.PersonName;
                txtJob.Text = ent.Job;
                radWhatsApp.SelectedValue = ent.WhatsUp.ToString();
                drpStudious.SelectedValue = ent.Studious.ToString();
                txtJobPlace.Text = ent.JobPlace;
                txtMobile.Text = ent.MobileNo1;
                txtMobile2.Text = ent.MobileNo2;
                txtNationalId.Text = ent.NationalID;
                txtSkype.Text = ent.Skype;
                drpBooldType.SelectedValue = ent.BloodType;
                drpMaritalStatus.SelectedValue = ent.MaritalStatus;
                var hobbiesId = personHobbiesManagement.LoadPersonIds(PersonId);
                chkHobbies.DataBind();
                foreach (string hid in hobbiesId)
                {
                    ListItem item = chkHobbies.Items.FindByValue(hid.ToLower());
                    if (item != null)
                        item.Selected = true;

                }
            }
            else
                drpRelationship.Enabled = true;
        }
        public void Clear()
        {
            drpRelationship.Enabled = true;
            txtBirthDate.Text = txtCode.Text = txtEmail.Text = txtFacebook.Text = txtFatherName.Text = txtFullName.Text = txtJob.Text = txtJobPlace.Text =
            txtMobile.Text = txtMobile2.Text = txtNationalId.Text = txtSkype.Text = string.Empty;
            drpBooldType.SelectedIndex = drpMaritalStatus.SelectedIndex = drpRelationship.SelectedIndex = -1;
            chkHobbies.DataBind();
            foreach (ListItem item in chkHobbies.Items)
                item.Selected = false;
            msg.Attributes["class"] = "";
        }
        public string Save(string PersonId, string FamilyId)
        {
            if (!string.IsNullOrEmpty(FamilyId))
            {
                var obj = new PersonManagement();
                if (obj.CheckIfExists(PersonId, txtNationalId.Text, txtFullName.Text))
                {
                    msg.Attributes["class"] = "msg-error";
                    lblMessge.Text = "تم اضافة البيانات سابقا";
                    return PersonId;

                }
                #region user image

                var imageFile=string.Empty;
                if (upldImage.PostedFile.FileName != "")
                {
                    var y = upldImage.PostedFile.ContentType.Split('/');
                    if (y[0] == "image")
                    {
                        var id = Guid.NewGuid();
                        imageFile = "Person" + id.ToString().Replace("-", "") +
                                           IO.Path.GetExtension(upldImage.PostedFile.FileName);
                        upldImage.PostedFile.SaveAs(Server.MapPath("~/Images/ActualSize/" + imageFile));
                        ImagesFact.ResizeWithCropResizeImage("", imageFile, "Person");
                    }
                }
                #endregion
                var hobbiesId = (from ListItem item in chkHobbies.Items where item.Selected select item.Value).ToList();
                if (string.IsNullOrEmpty(PersonId))
                {
                    #region Add Person
                    string message;
                    PersonId = obj.Add(FamilyId, txtFullName.Text, drpRelationship.SelectedValue, txtBirthDate.Text, drpBooldType.SelectedValue, txtEmail.Text,
                                       txtFacebook.Text, txtFatherName.Text, txtJob.Text, txtJobPlace.Text, drpMaritalStatus.SelectedValue,
                                       txtMobile.Text, txtMobile2.Text, txtNationalId.Text, txtSkype.Text, drpStudious.SelectedValue, radWhatsApp.SelectedValue, hobbiesId,
                                       Request.Cookies["UserWebsiteId"].Value,imageFile, out message);
                    if (string.IsNullOrEmpty(PersonId))
                    {
                        msg.Attributes["class"] = "msg-error";
                        if (string.IsNullOrEmpty(message))
                            lblMessge.Text = "يوجد خطا اعد المحاولة";
                        else
                            lblMessge.Text = message;
                        return string.Empty;
                    }
                    msg.Attributes["class"] = "msg-success";
                    lblMessge.Text = "تم حفظ البيانات بنجاح .";
                    return PersonId;

                    #endregion
                }
                #region Edit Person
                bool result = obj.Edit(PersonId, txtFullName.Text, drpRelationship.SelectedValue, txtBirthDate.Text, drpBooldType.SelectedValue, txtEmail.Text,
                                       txtFacebook.Text, txtFatherName.Text, txtJob.Text, txtJobPlace.Text, drpMaritalStatus.SelectedValue,
                                       txtMobile.Text, txtMobile2.Text, txtNationalId.Text, txtSkype.Text, drpStudious.SelectedValue, radWhatsApp.SelectedValue,
                                       hobbiesId,imageFile,Request.Cookies["UserWebsiteId"].Value);
                if (result)
                {
                    msg.Attributes["class"] = "msg-success";
                    lblMessge.Text = "تم حفظ البيانات بنجاح .";
                    return PersonId;
                }
                msg.Attributes["class"] = "msg-error";
                lblMessge.Text = "يوجد خطا اعد المحاولة";
                return string.Empty;

                #endregion
            }
            msg.Attributes["class"] = "msg - error";
            lblMessge.Text = "يوجد خطا اعد المحاولة";
            return string.Empty;
        }
       
    }
}