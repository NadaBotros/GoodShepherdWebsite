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
    public partial class QuizManagement : MangeBackend
    {
        #region Variables
        QuizManage _QuizManageObj;
        #endregion
        #region Property
        public string QuizId
        {
            set { ViewState["QuizId"] = value; }
            get { return ViewState["QuizId"] == null ? string.Empty : ViewState["QuizId"].ToString(); }
        }
        public string QuizWinnerId
        {
            set { ViewState["QuizWinnerId"] = value; }
            get { return ViewState["QuizWinnerId"] == null ? string.Empty : ViewState["QuizWinnerId"].ToString(); }
        }
        #endregion
        #region Quiz
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    QuizId = Request.QueryString["id"].ToString();
                }
                for (int i = 1; i <= 20; i++)
                    drpOrder.Items.Add(i.ToString());
                GetInfo();

            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("QuizList.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            if (Save())
                Response.Redirect("QuizManagement.aspx");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            GetInfo();
        }
        public void Clear()
        {
            txtDelivery.Text = txtQuizDate.Text = txtWinnerTitle.Text = txtTitle.Text = string.Empty;
            lnkDownload.Visible = false;
            imgCover.Visible = false;

        }
        public void GetInfo()
        {
            _QuizManageObj = new QuizManage();
            if (string.IsNullOrEmpty(QuizId))
            {
                tabWinners.Visible = lnkDownload.Visible = imgCover.Visible = false;
            }
            else
            {
                tabWinners.Visible = true;
                odsData.SelectParameters["QuizId"].DefaultValue = QuizId;
                grdData.DataBind();
                Quiz obj = _QuizManageObj.LoadById(QuizId);
                if (obj != null)
                {
                    txtTitle.Text = obj.QuizTitle;
                    if (obj.QuizDeliveryDate != null)
                        txtDelivery.Text = obj.QuizDeliveryDate.Value.ToString("d/M/yyyy");
                    else
                        txtDelivery.Text = string.Empty;

                    if (obj.QuizDate != null)
                        txtQuizDate.Text = obj.QuizDate.Value.ToString("d/M/yyyy");
                    else
                        txtQuizDate.Text = string.Empty;

                    lnkDownload.Visible = true;
                    lnkDownload.NavigateUrl = "~/files/quiz/" + obj.QuizPDF;
                    imgCover.Visible = true;
                    imgCover.ImageUrl = "~/images/s250_250/" + obj.QuizCover;
                }
            }
        }
        public bool Save()
        {
            string QuizCover = string.Empty, QuizFile = string.Empty;
            //magazine Cover
            #region Save Image
            if (fupldCover.PostedFile.FileName != "")
            {
                string[] y = fupldCover.PostedFile.ContentType.Split('/');
                if (y[0] == "image")
                {
                    Guid id = Guid.NewGuid();
                    QuizCover = "Quiz" + id.ToString().Replace("-", "") + System.IO.Path.GetExtension(fupldCover.PostedFile.FileName);
                    fupldCover.PostedFile.SaveAs(Server.MapPath("~/Images/ActualSize/" + QuizCover));
                    ImagesFact.ResizeWithCropResizeImage("", QuizCover, "Quiz");
                }
                else
                {
                    BackendMessages(201);
                    lblMessge.Text = "لابد من رفع صورة لغلاف المسابقة بطريقة صحيحية";
                    return false;
                }
            }
            #endregion
            #region Save Quiz File
            if (fupldQuiz.PostedFile.FileName != "")
            {
                string ext = System.IO.Path.GetExtension(fupldQuiz.PostedFile.FileName);
                if (ext.ToLower().Contains("pdf"))
                {
                    Guid id = Guid.NewGuid();
                    QuizFile = id.ToString().Replace("-", "") + ext;
                    fupldQuiz.PostedFile.SaveAs(Server.MapPath("~/files/quiz/" + QuizFile));
                }
                else
                {
                    BackendMessages(201);
                    lblMessge.Text = "لابد من رفع المسابقة بصيغة PDF";
                    return false;
                }
            }
            #endregion
            _QuizManageObj = new QuizManage();
            if (!string.IsNullOrEmpty(QuizId))
            {
                #region Edit Quiz
                if (_QuizManageObj.Edit(QuizId, txtTitle.Text, txtQuizDate.Text, txtDelivery.Text, QuizFile, QuizCover, Request.Cookies["UserWebsiteId"].Value))
                { BackendMessages(101); return true; }
                else
                { BackendMessages(201); return false; }
                #endregion
            }
            else
            {
                #region Add Quiz
                if (string.IsNullOrEmpty(QuizCover))
                {
                    BackendMessages(201); lblMessge.Text = "لابد من رفع صورة غلاف المسابقة";
                    return false;
                }
                else if (string.IsNullOrEmpty(QuizFile))
                {
                    BackendMessages(201); lblMessge.Text = "لابد من رفع ملف المسابقة";
                    return false;
                }
                else
                {
                    QuizId = _QuizManageObj.Add(txtTitle.Text, txtQuizDate.Text, txtDelivery.Text, QuizFile, QuizCover, Request.Cookies["UserWebsiteId"].Value);
                    if (!string.IsNullOrEmpty(QuizId))
                    {
                        BackendMessages(101);
                        return true;
                    }
                    else
                    {
                        BackendMessages(201);
                        return false;
                    }
                }
                #endregion
            }
        }
        #endregion
        #region Quiz Winners
        public string SaveStory()
        {
            if (!string.IsNullOrEmpty(QuizId) && !string.IsNullOrEmpty(ucSmallSearch1.GetSelectedId()))
            {
                QuizWinnersManage _story = new QuizWinnersManage();
                if (string.IsNullOrEmpty(QuizWinnerId))
                {
                    QuizWinnerId = _story.Add(QuizId, ucSmallSearch1.GetSelectedId(), drpOrder.SelectedValue, txtWinnerTitle.Text, Request.Cookies["UserWebsiteId"].Value);
                    if (string.IsNullOrEmpty(QuizId))
                    {
                        msg2.Attributes["class"] = "msg-error";
                        lblMsg2.Text = "خطا فى اضافة البيانات اعد المحاولة";
                    }
                    else
                    {
                        msg2.Attributes["class"] = "msg-success";
                        lblMsg2.Text = "تم حفظ البيانات بنجاح";
                    }
                }
                else
                {
                    if (_story.Edit(QuizWinnerId, ucSmallSearch1.GetSelectedId(), drpOrder.SelectedValue, txtWinnerTitle.Text, Request.Cookies["UserWebsiteId"].Value))
                    {
                        msg2.Attributes["class"] = "msg-success";
                        lblMsg2.Text = "تم حفظ البيانات بنجاح";
                    }
                    else
                    {
                        msg2.Attributes["class"] = "msg-error";
                        lblMsg2.Text = "خطا فى اضافة البيانات اعد المحاولة";
                    }
                }
            }
            else
            {
                msg2.Attributes["class"] = "msg-error";
                lblMsg2.Text = "لابد من اختيار اسم الشخص";
            }
            return QuizWinnerId;
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
            QuizWinnersManage _QuizWinnersManage = new QuizWinnersManage();
            switch (e.CommandName)
            {

                case "deleteitem":
                    if (_QuizWinnersManage.Delete(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { grdData.DataBind(); }
                    break;
                case "restoreitem":
                    if (_QuizWinnersManage.Restore(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { grdData.DataBind(); }
                    break;
                case "viewitem":
                    QuizWinnerId = e.CommandArgument.ToString();
                    GetInfoStory();
                    MPEPersonInfo.Show();
                    break;
            }
        }
        public void GetInfoStory()
        {
            QuizWinnersManage obj = new QuizWinnersManage();
            QuizWinner ent = obj.LoadById(QuizWinnerId);
            if (ent != null)
            {
                txtWinnerTitle.Text = ent.WinnerTitle;
                drpOrder.SelectedValue = ent.WinnerNo.ToString();
                ucSmallSearch1.AddPersonToList(ent.PersonId.ToString(), ent.Prg_Person.PersonName);
            }
        }
        public void ClearStory()
        {
            txtWinnerTitle.Text =  string.Empty;
            drpOrder.SelectedIndex = -1;
            ucSmallSearch1.Clear();
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            QuizWinnerId = string.Empty;
            ClearStory();
            MPEPersonInfo.Show();
        }
        protected void btnMagazineSave_Click(object sender, EventArgs e)
        {
            SaveStory();
            odsData.SelectParameters["QuizId"].DefaultValue = QuizId;
            grdData.DataBind();
            GetInfoStory();
            MPEPersonInfo.Show();
        }

        protected void btnMagazineSaveAndNew_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SaveStory()))
            {
                odsData.SelectParameters["QuizId"].DefaultValue = QuizId;
                grdData.DataBind();
                QuizWinnerId = string.Empty;
                ClearStory();
            }
            MPEPersonInfo.Show();
        }

        protected void btnMagazineClear_Click(object sender, EventArgs e)
        {
            ClearStory();
        }
        #endregion

    }
}