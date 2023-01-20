using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;

    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            //check if person code and nationality id
            dbDataContext db = new dbDataContext();
            PersonManagement obj = new PersonManagement();
            Prg_Person ent = obj.CheckUserId(txtCode.Text, txtNationalId.Text);
            if (ent != null)
            {
                string Password = ent.UserPassword;
                if (string.IsNullOrEmpty(ent.UserPassword))
                {
                    Password = Guid.NewGuid().ToString().Replace("-", "").Remove(7);
                    obj.ChangePassword(ent.PersonId.ToString(), Password);
                    //string msg = "مرحبا بك فى موقع اجتماع الراعي الصالح بكنيسه السيده العذراء والقديس اثناسيوس الراسولي<br>" +
                    //    "يمكنك متابعه عظات الاجتماع ومجلات الاجتماع ومسابقات الاجتماع وصور المؤتمرات واخبار الاجتماع <br>" +
                    //    "عن طريق هذا الرابط " + "http://shepherdmeeting.com/" +
                    //    "يمكنك الدخول على الموقع عن طريق البيانات الاتيه <br>" +
                    //    "الكود : " + ent.PersonCode + "<br>" +
                    //    "كلمة المرور : " + ent.UserPassword;
                    //GeneralMethods.SendMessage(txtEmail.Text, "اجتماع الراعي الصالح ", "", "كلمة المرور", msg, "");
                    //lblMSG.Text = "تم ارسال كلمة المرور علي بريدك الالكتروني";
                }
                lblMSG.Text = "كلمة المرور الخاصه بك هي : " + Password;
            }
            else
                lblMSG.Text = "يوجد خطا فى الكود او الرقم القومي";
        }
    }
