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
    public partial class MagazinesManagement : MangeBackend
    {
        #region Variables
        DAL.MagazineManage _MagazineManageObj;
        #endregion
        #region Property
        public string MagazineId
        {
            set { ViewState["MagazineId"] = value; }
            get { return ViewState["MagazineId"] == null ? string.Empty : ViewState["MagazineId"].ToString(); }
        }
        public string StoryId
        {
            set { ViewState["StoryId"] = value; }
            get { return ViewState["StoryId"] == null ? string.Empty : ViewState["StoryId"].ToString(); }
        }
        #endregion
        #region Magazine
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (Request.QueryString["id"] != null)
                {
                    MagazineId = Request.QueryString["id"].ToString();
                }
                GetInfo();

            }
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("MagazinesList.aspx");
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            if (Save())
                Response.Redirect("MagazinesManagement.aspx");
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            GetInfo();
        }
        public void Clear()
        {
            txtMagazineYear.Text = txtTitle.Text = string.Empty;
            lnkDownload.Visible = false;
            imgCover.Visible = false;
            drpMagazineMonth.SelectedIndex = -1;
        }
        public void GetInfo()
        {
            _MagazineManageObj = new MagazineManage();
            if (string.IsNullOrEmpty(MagazineId))
            {
                tabStories.Visible = lnkDownload.Visible = imgCover.Visible = false;
            }
            else
            {
                tabStories.Visible = true;
                odsData.SelectParameters["MagazineId"].DefaultValue = MagazineId;
                grdData.DataBind();
                Magazine obj = _MagazineManageObj.LoadById(MagazineId);
                if (obj != null)
                {
                    txtTitle.Text = obj.MagazineTitle;
                    txtMagazineYear.Text = obj.MagazineYear.ToString();
                    drpMagazineMonth.SelectedValue = obj.MagazineMonth.ToString();
                    lnkDownload.Visible = true;
                    lnkDownload.NavigateUrl = "~/files/magazines/" + obj.MagazinePDF;
                    imgCover.Visible = true;
                    imgCover.ImageUrl = "~/images/s250_250/" + obj.MagazineCover;
                }
            }
        }
        public bool Save()
        {
            string MagCover = string.Empty, MagFile = string.Empty;
            //magazine Cover
            #region Save Image
            if (fupldCover.PostedFile.FileName != "")
            {
                string[] y = fupldCover.PostedFile.ContentType.Split('/');
                if (y[0] == "image")
                {
                    Guid id = Guid.NewGuid();
                    MagCover = "Magazine" + id.ToString().Replace("-", "") + System.IO.Path.GetExtension(fupldCover.PostedFile.FileName);
                    fupldCover.PostedFile.SaveAs(Server.MapPath("~/Images/ActualSize/" + MagCover));
                    ImagesFact.ResizeWithCropResizeImage("", MagCover, "Magazine");
                }
                else
                {
                    BackendMessages(201);
                    lblMessge.Text = "لابد من رفع صورة لغلاف المجلة بطريقة صحيحية";
                    return false;
                }
            }
            #endregion
            #region Save Magazine File
            if (fupldMagazine.PostedFile.FileName != "")
            {
                string ext = System.IO.Path.GetExtension(fupldMagazine.PostedFile.FileName);
                if (ext.ToLower().Contains("pdf"))
                {
                    Guid id = Guid.NewGuid();
                    MagFile = id.ToString().Replace("-", "") + ext;
                    fupldMagazine.PostedFile.SaveAs(Server.MapPath("~/files/Magazines/" + MagFile));
                }
                else
                {
                    BackendMessages(201);
                    lblMessge.Text = "لابد من رفع المجلة بصيغة PDF";
                    return false;
                }
            }
            #endregion
            _MagazineManageObj = new DAL.MagazineManage();
            if (!string.IsNullOrEmpty(MagazineId))
            {
                #region Edit Magazine
                if (_MagazineManageObj.Edit(MagazineId, txtTitle.Text, drpMagazineMonth.SelectedValue, txtMagazineYear.Text, MagFile, MagCover, Request.Cookies["UserWebsiteId"].Value))
                { BackendMessages(101); return true; }
                else
                { BackendMessages(201); return false; }
                #endregion
            }
            else
            {
                #region Add Magazine
                if (string.IsNullOrEmpty(MagCover))
                {
                    BackendMessages(201); lblMessge.Text = "لابد من رفع صورة غلاف المجلة";
                    return false;
                }
                else if (string.IsNullOrEmpty(MagFile))
                {
                    BackendMessages(201); lblMessge.Text = "لابد من رفع ملف المجلة";
                    return false;
                }
                else
                {
                    MagazineId = _MagazineManageObj.Add(txtTitle.Text, drpMagazineMonth.SelectedValue, txtMagazineYear.Text, MagFile, MagCover, Request.Cookies["UserWebsiteId"].Value);
                    if (!string.IsNullOrEmpty(MagazineId))
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
        #region Magazine Stories
        public string SaveStory()
        {
            if (!string.IsNullOrEmpty(MagazineId))
            {
                MagazineStoryManage _story = new MagazineStoryManage();
                if (string.IsNullOrEmpty(StoryId))
                {
                    StoryId = _story.Add(MagazineId, txtStoryName.Text, txtStoryDesc.Text, Request.Cookies["UserWebsiteId"].Value);
                    if (string.IsNullOrEmpty(MagazineId))
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
                    if (_story.Edit(StoryId, txtStoryName.Text, txtStoryDesc.Text, Request.Cookies["UserWebsiteId"].Value))
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
            return StoryId;
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
            MagazineStoryManage _MagazineStoryManage = new MagazineStoryManage();
            switch (e.CommandName)
            {

                case "deleteitem":
                    if (_MagazineStoryManage.Delete(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { grdData.DataBind(); }
                    break;
                case "restoreitem":
                    if (_MagazineStoryManage.Restore(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { grdData.DataBind(); }
                    break;
                case "viewitem":
                    StoryId = e.CommandArgument.ToString();
                    GetInfoStory();
                    MPEPersonInfo.Show();
                    break;
            }
        }
        public void GetInfoStory()
        {
            MagazineStoryManage obj = new MagazineStoryManage();
            MagazineStory ent = obj.LoadById(StoryId);
            if (ent != null)
            {
                txtStoryDesc.Text = ent.StoryContent;
                txtStoryName.Text = ent.StoryTitle;
            }
        }
        public void ClearStory()
        {
            txtStoryName.Text = txtStoryDesc.Text = string.Empty;
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            StoryId = string.Empty;
            ClearStory();
            MPEPersonInfo.Show();
        }
        protected void btnMagazineSave_Click(object sender, EventArgs e)
        {
            SaveStory();
            odsData.SelectParameters["MagazineId"].DefaultValue = MagazineId;
            grdData.DataBind();
            GetInfoStory();
            MPEPersonInfo.Show();
        }

        protected void btnMagazineSaveAndNew_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(SaveStory()))
            {
                odsData.SelectParameters["MagazineId"].DefaultValue = MagazineId;
                grdData.DataBind();
                StoryId = string.Empty;
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