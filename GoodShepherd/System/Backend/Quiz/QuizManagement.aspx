<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true" CodeBehind="QuizManagement.aspx.cs" Inherits="System.Backend.QuizManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="../UserControls/ucSmallSearch.ascx" TagName="ucSmallSearch" TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="مسابقة الاجتماع" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <div class="dvtabs">
        <table width="99%">
            <tr>
                <td id="msg" runat="server">
                    <asp:Label ID="lblMessge" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <asp:TabContainer ID="TabContainer" runat="server" Width="99%"
                        CssClass="TabContainer" AutoPostBack="False" OnDemand="False" ActiveTabIndex="0">
                        <asp:TabPanel HeaderText="بيانات المسابقة" ID="tabMagazines" runat="server">
                            <ContentTemplate>
                                <table cellpadding="5">
                                    <tr>
                                        <td>عنوان المسابقة
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtTitle"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtTitle" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>تاريخ اصدار المسابقة
                                        </td>
                                        <td>
                                            <asp:CalendarExtender ID="CalendarExtender1" Format="d/M/yyyy" TargetControlID="txtQuizDate" runat="server"></asp:CalendarExtender>
                                            <asp:TextBox runat="server" ID="txtQuizDate"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtQuizDate" ValidationGroup="1"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>تاريخ تسليم المسابقة
                                        </td>
                                        <td>
                                            <asp:CalendarExtender ID="CalendarExtender2" Format="d/M/yyyy" TargetControlID="txtDelivery" runat="server"></asp:CalendarExtender>
                                            <asp:TextBox runat="server" ID="txtDelivery"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Image Style="float: right" runat="server" ID="imgCover" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ارفع صورة غلاف المسابقة <span class="reqstar">*</span></td>
                                        <td>
                                            <asp:FileUpload runat="server" ID="fupldCover" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:HyperLink Style="float: right" runat="server" Target="_blank" ID="lnkDownload">تحميل الملف</asp:HyperLink>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ارفع ملف المسابقة PDF <span class="reqstar">*</span></td>
                                        <td>
                                            <asp:FileUpload runat="server" ID="fupldQuiz" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel HeaderText="اوائل المسابقة" ID="tabWinners" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:LinkButton runat="server" class="addnew" ID="btnAddNew" OnClick="btnAddNew_Click"
                                                Text="اضافة اوائل المسابقة ...."></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdData" Width="100%" runat="server" CssClass="grd" DataSourceID="odsData"
                                                AutoGenerateColumns="False" OnRowCommand="grdData_RowCommand" OnRowDataBound="grdData_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="WinnerTitle" HeaderText="رتبة الشخص" SortExpression="WinnerTitle">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PersonCode" HeaderText="كود الشخص" SortExpression="PersonCode">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PersonName" HeaderText="اسم الشخص" SortExpression="PersonName">
                                                        <HeaderStyle Width="15%" />
                                                        <ItemStyle Width="15%" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgEdit" runat="server" CssClass="imgIcon3 tip_right_top" CommandName="viewitem" CommandArgument='<%# Eval("QuizWinnerId") %>' ImageUrl="~/System/Backend/lib/img/Edit.png" title="Edit"></asp:ImageButton>
                                                            <asp:ImageButton ID="imgDelete" runat="server" CssClass="imgIcon3 tip_right_top" ImageUrl="../lib/img/Delete.png"
                                                                CommandName="deleteitem" CommandArgument='<%# Eval("QuizWinnerId") %>' OnClientClick="if(!confirm('Are you sure you want delete this?')) return false;"
                                                                Visible='<%# DAL.GeneralMethods.DeleteRestorVisible(Eval("Active"),"false") %>'
                                                                title="Delete Item"></asp:ImageButton>
                                                            <asp:ImageButton ID="btnRestore" runat="server" CssClass="imgIcon3 tip_right_top" ImageUrl="../lib/img/restore.png"
                                                                CommandName="restoreitem" CommandArgument='<%# Eval("QuizWinnerId") %>'
                                                                Visible='<%# DAL.GeneralMethods.DeleteRestorVisible(Eval("Active"),"true") %>'
                                                                title="Restore Item"></asp:ImageButton>
                                                            <asp:Image ID="imgInfo" CssClass="imgIcon3 tip_right_top" title='<%#DAL.GeneralMethods.GetRecordInfo(Eval("CreatedOn"),Eval("CreatedBy"),Eval("ModifiedOn"),Eval("ModifiedBy")) %>'
                                                                ImageUrl="../lib/img/Info.png" runat="server" />
                                                        </ItemTemplate>
                                                        <HeaderStyle Width="20%" />
                                                        <ItemStyle HorizontalAlign="Center" Width="20%" />
                                                    </asp:TemplateField>
                                                </Columns>
                                            </asp:GridView>
                                            <asp:ObjectDataSource ID="odsData" runat="server" SelectMethod="LoadByDeleteState"
                                                TypeName="DAL.QuizWinnersManage">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="true" Name="Active" Type="String" />
                                                    <asp:Parameter Name="QuizId" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>
                    <asp:Button ID="btnPopup" runat="server" Style="display: none" />
                    <asp:ModalPopupExtender ID="MPEPersonInfo" runat="server"
                        TargetControlID="btnPopup"
                        PopupControlID="pnlPoup"
                        BackgroundCssClass="modalBackground"
                        DropShadow="True"
                        CancelControlID="btnClosePopup" DynamicServicePath="" Enabled="True">
                    </asp:ModalPopupExtender>
                    <asp:Panel runat="server" CssClass="dvPopup" ID="pnlPoup">
                        <asp:UpdatePanel runat="server" ID="update">
                            <ContentTemplate>
                                <table>
                                    <tr>
                                        <td class="tdHeader">
                                            <asp:ImageButton ID="btnClosePopup" Style="float: left" ImageUrl="~/System/Backend/lib/img/close.png" runat="server" />
                                            <img src="../lib/icons/32/Window.png" class="imgIcon" />
                                            <asp:Label runat="server" Text="اوائل المسابقة" CssClass="tdPageTitle" ID="Label1"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td id="msg2" runat="server">
                                            <asp:Label ID="lblMsg2" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <table>
                                                <tr>
                                                    <td width="120px">اسم الرتبة</td>
                                                    <td>
                                                        <asp:TextBox runat="server" ID="txtWinnerTitle"  Style="width: 200px;"></asp:TextBox></td>
                                                    <td>
                                                        <asp:RequiredFieldValidator runat="server" ID="reqtxtName" ControlToValidate="txtWinnerTitle" ValidationGroup="4"></asp:RequiredFieldValidator>
                                                        &nbsp;الاول او الاول مكرر
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>ترتيب الشخص</td>
                                                    <td>
                                                        <asp:DropDownList Style="width: 200px;float: right" runat="server" ID="drpOrder"></asp:DropDownList>

                                                    </td>
                                                    <td></td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3" align="center" cssclass="tdPageTitle">ابحث عن اسم الشخص</td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <uc1:ucSmallSearch ID="ucSmallSearch1" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td class="tdbtns">
                                            <asp:Button ID="btnMagazineClear" runat="server" Text="تفريغ الخانات" OnClick="btnMagazineClear_Click" />
                                            <asp:Button ID="btnMagazineSaveAndNew" ValidationGroup="4" runat="server" Text="حفظ البيانات واضافة جديد"
                                                OnClick="btnMagazineSaveAndNew_Click" />
                                            <asp:Button ID="btnMagazineSave" ValidationGroup="4" runat="server" Text="حفظ البيانات" OnClick="btnMagazineSave_Click" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                            <Triggers>
                                <asp:PostBackTrigger ControlID="btnMagazineSaveAndNew" />
                                   <asp:PostBackTrigger ControlID="btnMagazineSave" />
                            </Triggers>
                        </asp:UpdatePanel>

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
