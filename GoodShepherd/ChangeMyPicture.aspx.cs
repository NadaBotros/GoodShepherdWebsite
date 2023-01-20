using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

    public partial class ChangeMyPicture : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                GetInfo();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (fpldImage.PostedFile.FileName != "")
            {
                string[] y = fpldImage.PostedFile.ContentType.Split('/');
                if (y[0] == "image")
                {
                    var id = Guid.NewGuid();
                    string imageFile = "Person" + id.ToString().Replace("-", "") + System.IO.Path.GetExtension(fpldImage.PostedFile.FileName);
                    fpldImage.PostedFile.SaveAs(Server.MapPath("~/Images/ActualSize/" + imageFile));
                    ImagesFact.ResizeWithCropResizeImage("", imageFile, "Person");
                    var obj = new PersonManagement();
                    obj.ChangeUserImage(Request.Cookies["PersonId"].Value, imageFile);
                    GetInfo();
                    lblMSG.Text = "تم تغير الصورة بنجاح";
                }
                else
                    lblMSG.Text = "لابد من رفع صورة";
            }
            else
                lblMSG.Text = "لابد من اختر الصورة الشخصية";
        }
        public void GetInfo()
        {
            var obj = new PersonManagement();
            var ent = obj.LoadById(Request.Cookies["PersonId"].Value);
            if (ent != null)
            {
                if (!string.IsNullOrEmpty(ent.UserImage))
                    imgPerson.ImageUrl = "~/images/S150_150/" + ent.UserImage;
                else if (ent.Sex)
                    imgPerson.ImageUrl = "~/themes/Default/img/avatarmale.jpg";
                else
                    imgPerson.ImageUrl = "~/themes/Default/img/avatarFemale.jpg";
            }
        }
    }
