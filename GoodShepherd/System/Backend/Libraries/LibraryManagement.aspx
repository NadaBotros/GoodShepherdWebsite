<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="LibraryManagement.aspx.cs" Inherits="System.Backend.manage.LibraryManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="اقسام المكتبات" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <div class="dvtabs">
        <table width="100%" cellpadding="2" cellspacing="2">
            <%-- <tr>
            <td class="tdHeader">
                <img src="../lib/icons/32/Window.png" class="imgIcon" />
                <asp:Label runat="server" Text="ادارة اقسام المكتبات" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>--%>
            <tr>
                <td id="msg" runat="server">
                    <asp:Label ID="lblMessge" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TabContainer ID="tcMain" runat="server" Width="99%"
                        CssClass="TabContainer" AutoPostBack="False" OnDemand="False" ActiveTabIndex="0">
                        <asp:TabPanel HeaderText="بيانات القسم" ID="tabPersons" runat="server">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td valign="top" style="min-width: 200px;">
                                            <table style="float: right">
                                                <tr>
                                                    <td>اختر القسم الذي يضاف فيه القسم الجديد
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <asp:TreeView ID="TreeView" runat="server" OnSelectedNodeChanged="TreeView_SelectedNodeChanged"
                                                            ExpandDepth="0" ImageSet="XPFileExplorer" NodeIndent="15">
                                                            <HoverNodeStyle Font-Underline="True" ForeColor="#6666AA" />
                                                            <NodeStyle Font-Names="Tahoma" Font-Size="8pt" ForeColor="Black" HorizontalPadding="2px" NodeSpacing="0px" VerticalPadding="2px" />
                                                            <ParentNodeStyle Font-Bold="False" />
                                                            <SelectedNodeStyle BackColor="#B5B5B5" Font-Underline="False" HorizontalPadding="0px" VerticalPadding="0px" />
                                                        </asp:TreeView>
                                                    </td>
                                                </tr>
                                            </table>

                                        </td>
                                        <td valign="top">
                                            <table cellpadding="2" cellspacing="2">

                                                <tr>
                                                    <td>مسار القسم</td>
                                                    <td>
                                                        <asp:Label runat="server" ID="lblCategoryPath"></asp:Label></td>
                                                </tr>
                                                <tr>
                                                    <td>اسم القسم <span class="reqstar">*</span>
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" Style="width: 550px" ID="txtName"></asp:TextBox>
                                                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                                            ValidationGroup="1" runat="server" ControlToValidate="txtName"></asp:RequiredFieldValidator>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td></td>
                                                    <td>يمكن اضافه لينك لمدرسه الشمامسه او اى موقع اخر او يترك فارغا اذا كنا نريد اضافه الملفات بواسطتنا </td>
                                                </tr>
                                                <tr>
                                                    <td>لينك القسم
                                                    </td>
                                                    <td>
                                                        <asp:TextBox runat="server" Style="width: 550px" ID="txtLink"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel HeaderText="ملفات القسم" ID="tabFiles" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td class="tdHeader">
                                            <img class="imgIcon2" src="../lib/icons/24/Dashboard.png" />
                                            <asp:Label runat="server" Text="ملفات القسم" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
                                            <asp:LinkButton runat="server" class="addnew" ID="btnAddNew" OnClick="btnAddNew_Click"
                                                Text="اضافة جديد ...."></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdData" Width="100%" runat="server" CssClass="grd" DataSourceID="odsData"
                                                AutoGenerateColumns="False" OnRowCommand="grdData_RowCommand" OnRowDataBound="grdData_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="FileTitle" HeaderText="اسم الملف" SortExpression="FileTitle">
                                                        <HeaderStyle Width="30%" />
                                                        <ItemStyle Width="30%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="FileOwner" HeaderText="صاحب الملف" SortExpression="FileOwner">
                                                        <HeaderStyle Width="30%" />
                                                        <ItemStyle Width="30%" />
                                                    </asp:BoundField>
                                                    
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgEdit" runat="server" CssClass="imgIcon3 tip_right_top" CommandName="viewitem" CommandArgument='<%# Eval("FileId") %>' ImageUrl="~/System/Backend/lib/img/Edit.png" title="Edit"></asp:ImageButton>
                                                            <asp:ImageButton ID="imgDelete" runat="server" CssClass="imgIcon3 tip_right_top" ImageUrl="../lib/img/Delete.png"
                                                                CommandName="deleteitem" CommandArgument='<%# Eval("FileId") %>' OnClientClick="if(!confirm('Are you sure you want delete this?')) return false;"
                                                                Visible='<%# DAL.GeneralMethods.DeleteRestorVisible(Eval("Active"),"false") %>'
                                                                title="Delete Item"></asp:ImageButton>
                                                            <asp:Image ID="imgInfo" CssClass="imgIcon3 tip_right_top" title='<%#DAL.GeneralMethods.GetRecordInfo(Eval("CreatedOn"),Eval("CreatedBy"),Eval("ModifiedOn"),Eval("ModifiedBy")) %>'
                                                                ImageUrl="../lib/img/Info.png" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="20%" />
                                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="LoadByDeleteState"
                                                TypeName="DAL.LibraryFilesManage">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="true" Name="Active" Type="String" />
                                                    <asp:Parameter Name="LibraryItemId" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>
                    <asp:Button ID="btnPopup" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="mpeFileInfo" runat="server"
                        TargetControlID="btnPopup"
                        PopupControlID="pnlPoup"
                        BackgroundCssClass="modalBackground"
                        DropShadow="True"
                        CancelControlID="btnClosePopup" DynamicServicePath="" Enabled="True">
                    </asp:ModalPopupExtender>
                    <asp:Panel runat="server" CssClass="dvPopup" ID="pnlPoup">
                        <table>
                            <tr>
                                <td class="tdHeader">
                                    <asp:ImageButton ID="btnClosePopup" Style="float: left" ImageUrl="~/System/Backend/lib/img/close.png" runat="server" />
                                    <img src="../lib/icons/32/Window.png" class="imgIcon" />
                                    <asp:Label runat="server" Text="بيانات الملف" CssClass="tdPageTitle" ID="Label1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td id="tdMsg2" runat="server">
                                    <asp:Label ID="lblMsg2" Style="display: block; width: 100%" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table width="800px">
                                        <tr>
                                            <td>اسم الملف
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtFileName"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RequiredFieldValidator runat="server" ID="reqFileName" ValidationGroup="11" ControlToValidate="txtFileName"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>صاحب الملف / المتكلم
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtFileOwner"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td>تاريخ الملف
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtFileDate"></asp:TextBox>
                                                <asp:CalendarExtender TargetControlID="txtFileDate" Format="d/M/yyyy" ID="CalendarExtender1" runat="server"></asp:CalendarExtender>
                                            </td>
                                            <td></td>
                                        </tr>
                                        <tr id="trYoutubeViewer">
                                            <td></td>
                                            <td>
                                                <asp:Literal runat="server" ID="ltrFileVideo"></asp:Literal></td>
                                            <td></td>
                                        </tr>
                                        <tr id="trYoutube" runat="server">
                                            <td>لينك اليوتيوب
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtFileYoutube"></asp:TextBox>
                                            </td>
                                            <td>
                                                <asp:RegularExpressionValidator ID="revFileVideo" ControlToValidate="txtFileYoutube" runat="server" ErrorMessage="خطا فى اللينك المضاف" ValidationExpression="http(s)?://([\w-]+\.)+[\w-]+(/[\w- ./?%&amp;=]*)?" ValidationGroup="11"></asp:RegularExpressionValidator>
                                            </td>
                                        </tr>
                                        <tr id="trFileNotes" runat="server">
                                            <td></td>
                                            <td>يرجي رفع الملف بواسطه برنامج الاف تي بي ثم انسخ اسم الملف هنا او قم بتعديله اذا كان لا يعمل
                                            </td>
                                        </tr>
                                        <tr id="trFile" runat="server">
                                            <td>اسم الملف <span class="reqstar">*</span>
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtFileDbName"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>ملاحظات الملف
                                            </td>
                                            <td>
                                                <asp:TextBox runat="server" Height="50px" TextMode="MultiLine" ID="txtFileNotes"></asp:TextBox>
                                            </td>
                                            <td></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbtns">
                                    <asp:Button ID="btnFileClear" runat="server" Text="تفريغ الخانات" OnClick="btnFileClear_OnClick" />
                                    <asp:Button ID="btnFileSaveAndNew" ValidationGroup="11" runat="server" OnClick="btnFileSaveAndNew_OnClick" Text="حفظ البيانات واضافة جديد" />
                                    <asp:Button ID="btnFileSave" ValidationGroup="11" runat="server" OnClick="btnFileSave_OnClick" Text="حفظ البيانات" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td colspan="2" class="tdbtns">
                    <asp:Button ID="btnBack" runat="server" Text="رجوع للصفحة السابقة" OnClick="btnBack_Click" />
                    <asp:Button ID="btnClear" runat="server" Text="تفريغ الخانات" OnClick="btnClear_Click" />
                    <asp:Button ID="btnSaveAndNew" ValidationGroup="1" runat="server" Text="حفظ البيانات واضافة جديد"
                        OnClick="btnSaveAndNew_Click" />
                    <asp:Button ID="btnSave" ValidationGroup="1" runat="server" Text="حفظ البيانات" OnClick="btnSave_Click" />
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
