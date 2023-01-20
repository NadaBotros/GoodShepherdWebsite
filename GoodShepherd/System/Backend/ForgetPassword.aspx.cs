using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace System.Backend
{
    public partial class ForgetPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
                TryNew();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            try
            {
                if (IsValid)
                {
                    AdminManagement _AdminManagement = new AdminManagement();
                    lblMsg.Text = _AdminManagement.ForgetPassword(txtEmail.Text);
                }
                else
                { lblMsg.Text = "Please enter the correct image characters"; }
            }
            catch { lblMsg.Text = "Error, try again error."; }
        }
        #region Capcha
        public void TryNew()
        {
            char[] Valichars = { '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            string Captcha = "";
            int LetterCount = MaxLetterCount > 5 ? MaxLetterCount : 5;
            for (int i = 0; i < LetterCount; i++)
            {
                char newChar = (char)0;
                do
                {
                    newChar = Char.ToUpper(Valichars[new Random(DateTime.Now.Millisecond).Next(Valichars.Count() - 1)]);
                }
                while (Captcha.Contains(newChar));
                Captcha += newChar;
            }
            GeneratedText = Captcha;

            imgCapatcha.ImageUrl = "lib/Capcha/GetImgText.ashx?CaptchaText=" +
                Server.UrlEncode(Convert.ToBase64String(SecurityHelper.EncryptString(Captcha)));
        }
        public int MaxLetterCount { get; set; }
        private string GeneratedText
        {
            get
            {
                return ViewState[this.ClientID + "text"] != null ?
                    ViewState[this.ClientID + "text"].ToString() : null;
            }
            set
            {
                // Encrypt the value before storing it in viewstate.
                ViewState[this.ClientID + "text"] = value;
            }
        }
        public bool IsValid
        {
            get
            {
                bool result = GeneratedText.ToUpper() == txtCapatcha.Text.Trim().ToUpper();
                if (!result)
                    TryNew();
                return result;
            }
        }
        protected void btnTryNewWords_Click(object sender, EventArgs e)
        {
            TryNew();
        }
        protected void imgChange_Click(object sender, ImageClickEventArgs e)
        {
            TryNew();
        }
        #endregion

    }
}