using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
namespace System.Backend
{
    public partial class _default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Request.Cookies["UserWebsiteId"] != null )
            {
                Response.Redirect("MainPage.aspx");
            }
            //--------------Capcha----------------
            if (!IsPostBack)
            {
                if (GeneratedText == null)
                    TryNew();
            }
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
        protected void imgChange_Click1(object sender, ImageClickEventArgs e)
        {
            TryNew();
        }
        protected void btnLogin_Click(object sender, EventArgs e)
        {
            if (IsValid)
            {
                string Msg = string.Empty;
                string UserType = string.Empty;
                string UserId = string.Empty;
                AdminManagement _AdminManagement = new AdminManagement();
                UserId = _AdminManagement.CheckAdmin(txtUserName.Text, txtPassword.Text, out Msg);
                if (string.IsNullOrEmpty(UserId))
                {
                    lblMsg.Text = Msg;
                }
                else
                {
                    HttpCookie UserWebsiteId = new HttpCookie("UserWebsiteId");
                    UserWebsiteId.Value = UserId;
                    Response.Cookies.Add(UserWebsiteId);                 

                    if (chkRemberMe.Checked)
                    {
                        UserWebsiteId.Expires = DateTime.Now.AddDays(30);
                                          }
                    else
                    {
                        UserWebsiteId.Expires = DateTime.Now.AddDays(1);
                                            }
                    if (Request.QueryString["page"] == null)
                        Response.Redirect("MainPage.aspx");
                    else
                        Response.Redirect(Request.QueryString["page"].ToString());
                }
            }
            else
            { lblMsg.Text = "Please enter the correct image characters"; }
        }
    }
}