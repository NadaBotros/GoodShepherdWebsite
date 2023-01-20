using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using DAL;
using System.Data;
namespace System.Backend.manage
{
    public partial class LibraryManagement : MangeBackend
    {
        #region Variables
        LibraryManage _LibraryManage;
        #endregion
        #region Property
        public string LibraryId
        {
            set { ViewState["LibraryId"] = value; }
            get { return ViewState["LibraryId"] == null ? string.Empty : ViewState["LibraryId"].ToString(); }
        }
        public string fileId
        {
            set { ViewState["fileId"] = value; }
            get { return ViewState["fileId"] == null ? string.Empty : ViewState["fileId"].ToString(); }
        }
        public string ParentLibraryId
        {
            set { ViewState["ParentLibraryId"] = value; }
            get { return ViewState["ParentLibraryId"] == null ? string.Empty : ViewState["ParentLibraryId"].ToString(); }
        }
        #endregion
        #region Events
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (!string.IsNullOrEmpty(Request.QueryString["id"]))
                {
                    LibraryId = Request.QueryString["id"];
                }
                GetInfo();
                switch (Request.QueryString["type"].ToString())
                {
                    case "1":
                        lblPageMainTitle.Text = "اقسام المكتبة الصوتية";
                        trFile.Visible = trFileNotes.Visible = true;
                        trYoutube.Visible = false;
                        break;
                    case "2":
                        lblPageMainTitle.Text = "اقسام المكتبة المرئية";
                        trFile.Visible = trFileNotes.Visible = false;
                        trYoutube.Visible = true;
                        break;
                    case "3":
                        lblPageMainTitle.Text = "اقسام المكتبة الكتابية";
                        trFile.Visible = trFileNotes.Visible = true;
                        trYoutube.Visible = false;
                        break;
                }
            }
        }
        protected void btnSave_Click(object sender, EventArgs e)
        {
            Save();
            GetInfo();
        }
        protected void btnSaveAndNew_Click(object sender, EventArgs e)
        {
            Save();
            Response.Redirect("LibraryManagement.aspx?type=" + Request.QueryString["type"].ToString());
        }
        protected void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }
        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("LibraryList.aspx?type=" + Request.QueryString["type"].ToString());
        }
        void filltree()
        {
            TreeView.Nodes.Clear();
            _LibraryManage = new LibraryManage();
            DataTable dt = _LibraryManage.LoadByDeleteState("True", string.Empty, Request.QueryString["type"].ToString());
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode root = new TreeNode(dt.Rows[i]["ItemTitle"].ToString(), dt.Rows[i]["LibraryItemId"].ToString());
                root.SelectAction = TreeNodeSelectAction.Select;
                CreateNode(root);
                TreeView.Nodes.Add(root);
            }
            TreeView.DataBind();
        }
        void CreateNode(TreeNode node)
        {
            _LibraryManage = new LibraryManage();
            DataTable dt = _LibraryManage.LoadByDeleteState("True", node.Value, Request.QueryString["type"].ToString());
            if (dt.Rows.Count == 0)
            {
                return;
            }
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                TreeNode tnode = new TreeNode(dt.Rows[i]["ItemTitle"].ToString(), dt.Rows[i]["LibraryItemId"].ToString());
                tnode.SelectAction = TreeNodeSelectAction.Select;
                node.ChildNodes.Add(tnode);
                CreateNode(tnode);
            }
        }

        protected void TreeView_SelectedNodeChanged(object sender, EventArgs e)
        {
            ParentLibraryId = TreeView.SelectedNode.Value;
            GetCategoryPath(TreeView.SelectedNode.Value);
        }
        public void GetCategoryPath(string CatId)
        {
            string Catpath = "";
            _LibraryManage = new LibraryManage();
        step1: ;
            Library LibraryEnt = _LibraryManage.LoadById(CatId);
            if (LibraryEnt != null)
            {
                Catpath = LibraryEnt.ItemTitle + ">>" + Catpath;
                CatId = LibraryEnt.ParentItemId.ToString();
                if (!string.IsNullOrEmpty(LibraryEnt.ParentItemId.ToString()))
                { goto step1; }
            }
            if (Catpath.Length > 2)
                lblCategoryPath.Text = Catpath.Remove(Catpath.Length - 2);
            else
                lblCategoryPath.Text = "";
        }
        #endregion
        #region Methods
        void Save()
        {
            _LibraryManage = new LibraryManage();
            #region Manage Item
            if (string.IsNullOrEmpty(LibraryId))
            {
                LibraryId = _LibraryManage.Add(ParentLibraryId, Request.QueryString["type"], txtName.Text, txtLink.Text, Request.Cookies["UserWebsiteId"].Value);
                if (!string.IsNullOrEmpty(LibraryId))
                {
                    BackendMessages(101);
                }
                else
                {
                    BackendMessages(201);
                }
            }
            else
            {
                if (_LibraryManage.Edit(LibraryId, ParentLibraryId, txtName.Text, txtLink.Text, Request.Cookies["UserWebsiteId"].Value))
                {
                    BackendMessages(101);
                }
                else
                {
                    BackendMessages(201);
                }
            }
            #endregion
        }
        void GetInfo()
        {
            if (!string.IsNullOrEmpty(LibraryId))
            {
                _LibraryManage = new LibraryManage();
                Library LibraryEnt = _LibraryManage.LoadById(LibraryId);
                if (LibraryEnt != null)
                {
                    txtName.Text = LibraryEnt.ItemTitle;
                    txtLink.Text = LibraryEnt.ItemLink;
                    if (LibraryEnt.ParentItemId != null)
                        ParentLibraryId = LibraryEnt.ParentItemId.ToString();
                    else
                        ParentLibraryId = string.Empty;
                    filltree();
                    odsData.SelectParameters["LibraryItemId"].DefaultValue = LibraryId;
                    odsData.DataBind();
                    grdData.DataBind();
                    tabFiles.Enabled = true;
                    GetCategoryPath(ParentLibraryId);
                }
            }
            else
            {
                tabFiles.Enabled = false;
                filltree();
            }
        }
        void Clear()
        {
            txtName.Text = lblCategoryPath.Text = txtLink.Text = string.Empty;
            ParentLibraryId = string.Empty;
        }
        #endregion
        protected void drpLibraryType_SelectedIndexChanged(object sender, EventArgs e)
        {
            filltree();
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
            var libraryFileManage = new LibraryFilesManage();
            switch (e.CommandName)
            {

                case "deleteitem":
                    if (libraryFileManage.Delete(e.CommandArgument.ToString(), Request.Cookies["UserWebsiteId"].Value))
                    { grdData.DataBind(); }
                    break;
                case "viewitem":
                    fileId = e.CommandArgument.ToString();
                    FileGetInfo();
                    mpeFileInfo.Show();
                    break;
            }
        }
        protected void btnAddNew_Click(object sender, EventArgs e)
        {
            fileId = string.Empty;
            FilesClear();
            mpeFileInfo.Show();
        }

        protected void btnFileClear_OnClick(object sender, EventArgs e)
        {
            FilesClear();
        }

        protected void btnFileSaveAndNew_OnClick(object sender, EventArgs e)
        {
            FilesSave();
            FilesClear();
            mpeFileInfo.Show();
            grdData.DataBind();
        }

        protected void btnFileSave_OnClick(object sender, EventArgs e)
        {
            FilesSave();
            mpeFileInfo.Show();
            grdData.DataBind();
        }
        #region Files
        public void FilesClear()
        {
            txtFileDate.Text = fileId =
                   txtFileName.Text =
                   txtFileNotes.Text =
                   txtFileNotes.Text = txtFileOwner.Text = txtFileOwner.Text = txtFileYoutube.Text = string.Empty;
        }
        public bool FilesSave()
        {
            var libraryFilesMange = new LibraryFilesManage();
            if (Request.QueryString["type"] == "2" && string.IsNullOrEmpty(txtFileYoutube.Text))
            {
                tdMsg2.Attributes["class"] = "msg-error";
                lblMsg2.Text = "لابد من ادخال لينك اليوتيوب";
                return false;
            }
            else if (Request.QueryString["type"] != "2" && string.IsNullOrEmpty(txtFileDbName.Text))
            {
                tdMsg2.Attributes["class"] = "msg-error";
                lblMsg2.Text = "لابد من ادخال اسم الملف الموجود فى ال اف تي بي";
                return false;
            }
            else
            {
                if (string.IsNullOrEmpty(fileId))
                {
                    string newFileId = libraryFilesMange.Add(LibraryId, txtFileName.Text, txtFileOwner.Text,
                                                             txtFileNotes.Text, "",
                                                             txtFileDate.Text, txtFileDbName.Text, txtFileYoutube.Text,
                                                             Request.Cookies["UserWebsiteId"].Value);
                    if (string.IsNullOrEmpty(newFileId))
                    {
                        tdMsg2.Attributes["class"] = "msg-error";
                        lblMsg2.Text = "يوجد خطا اعد المحاولة";
                        return false;
                    }
                    else
                    {
                        tdMsg2.Attributes["class"] = "msg-success";
                        lblMsg2.Text = "تم حفظ البيانات بنجاح .";
                        return true;
                    }
                }
                else
                {
                    bool result = libraryFilesMange.Edit(fileId, txtFileName.Text, txtFileOwner.Text, txtFileNotes.Text,
                                                         "",
                                                         txtFileDate.Text, txtFileDbName.Text, txtFileYoutube.Text,
                                                         Request.Cookies["UserWebsiteId"].Value);
                    if (!result)
                    {
                        tdMsg2.Attributes["class"] = "msg-error";
                        lblMsg2.Text = "يوجد خطا اعد المحاولة";
                        return false;
                    }
                    else
                    {
                        tdMsg2.Attributes["class"] = "msg-success";
                        lblMsg2.Text = "تم حفظ البيانات بنجاح .";
                        return true;
                    }
                }
            }

        }
        public void FileGetInfo()
        {
            if (!string.IsNullOrEmpty(fileId))
            {
                var obj = new LibraryFilesManage();
                var ent = obj.LoadById(fileId);
                if (ent != null)
                {
                    txtFileNotes.Text = ent.FileDesc;
                    txtFileOwner.Text = ent.FileOwner;
                    txtFileName.Text = ent.FileTitle;
                    txtFileDbName.Text = ent.FileName;
                    txtFileYoutube.Text = ent.YoutubeLink;
                    txtFileDate.Text = ent.FileDate != null ? ent.FileDate.Value.ToShortDateString() : string.Empty;
                }
            }
        }
        #endregion

    }
}