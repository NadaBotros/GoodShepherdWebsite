<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true" CodeBehind="MagazinesManagement.aspx.cs" Inherits="System.Backend.MagazinesManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/System/Backend/Family/UcPerson.ascx" TagPrefix="uc1" TagName="UcPerson" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="مجلة الاجتماع" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
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
                        <asp:TabPanel HeaderText="بيانات المجلة" ID="tabMagazines" runat="server">
                            <ContentTemplate>
                                <table cellpadding="5">
                                    <tr>
                                        <td>اسم المجلة
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server"  ID="txtTitle"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator3" ControlToValidate="txtTitle" ValidationGroup="1"></asp:RequiredFieldValidator>                                            
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>تاريخ المجلة - الشهر
                                        </td>
                                        <td>
                                            <asp:DropDownList Style="float: right" runat="server" ID="drpMagazineMonth">
                                                <asp:ListItem Value="1" Text="يناير"></asp:ListItem>
                                                <asp:ListItem Value="2" Text="فبراير"></asp:ListItem>
                                                <asp:ListItem Value="3" Text="مارس"></asp:ListItem>
                                                <asp:ListItem Value="4" Text="ابريل"></asp:ListItem>
                                                <asp:ListItem Value="5" Text="مايو"></asp:ListItem>
                                                <asp:ListItem Value="6" Text="يونيو"></asp:ListItem>
                                                <asp:ListItem Value="7" Text="يوليو"></asp:ListItem>
                                                <asp:ListItem Value="8" Text="اغسطس"></asp:ListItem>
                                                <asp:ListItem Value="9" Text="سبتمبر"></asp:ListItem>
                                                <asp:ListItem Value="10" Text="اكتوبر"></asp:ListItem>
                                                <asp:ListItem Value="11" Text="نوفمبر"></asp:ListItem>
                                                <asp:ListItem Value="12" Text="ديسمبر"></asp:ListItem>
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>تاريخ المجلة - السنة
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" SkinID="textSmall" MaxLength="4" ID="txtMagazineYear"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator2" ControlToValidate="txtMagazineYear" ValidationGroup="1"></asp:RequiredFieldValidator>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtMagazineYear" FilterMode="InvalidChars" ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                            <asp:RegularExpressionValidator ValidationGroup="1" ControlToValidate="txtMagazineYear" ID="RegularExpressionValidator1" ValidationExpression="[0-9]{4}" runat="server" ErrorMessage="السنة من 4 ارقام"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Image Style="float: right" runat="server" ID="imgCover" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>ارفع صورة غلاف المجلة <span class="reqstar">*</span></td>
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
                                        <td>ارفع ملف المجلة PDF <span class="reqstar">*</span></td>
                                        <td>
                                            <asp:FileUpload runat="server" ID="fupldMagazine" />
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel HeaderText="قصص من المجلة" ID="tabStories" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td>
                                            <asp:LinkButton runat="server" class="addnew" ID="btnAddNew" OnClick="btnAddNew_Click"
                                                Text="اضافة قصة جديدة ...."></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdData" Width="100%" runat="server" CssClass="grd" DataSourceID="odsData"
                                                AutoGenerateColumns="False" OnRowCommand="grdData_RowCommand" OnRowDataBound="grdData_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="StoryTitle" HeaderText="عنوان القصة" SortExpression="StoryTitle">
                                                        <HeaderStyle Width="85%" />
                                                        <ItemStyle Width="85%" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgEdit" runat="server" CssClass="imgIcon3 tip_right_top" CommandName="viewitem" CommandArgument='<%# Eval("MagazineStoryId") %>' ImageUrl="~/System/Backend/lib/img/Edit.png" title="Edit"></asp:ImageButton>
                                                            <asp:ImageButton ID="imgDelete" runat="server" CssClass="imgIcon3 tip_right_top" ImageUrl="../lib/img/Delete.png"
                                                                CommandName="deleteitem" CommandArgument='<%# Eval("MagazineStoryId") %>' OnClientClick="if(!confirm('Are you sure you want delete this?')) return false;"
                                                                Visible='<%# DAL.GeneralMethods.DeleteRestorVisible(Eval("Active"),"false") %>'
                                                                title="Delete Item"></asp:ImageButton>
                                                            <asp:ImageButton ID="btnRestore" runat="server" CssClass="imgIcon3 tip_right_top" ImageUrl="../lib/img/restore.png"
                                                                CommandName="restoreitem" CommandArgument='<%# Eval("MagazineStoryId") %>'
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
                                                TypeName="DAL.MagazineStoryManage">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="true" Name="Active" Type="String" />
                                                    <asp:Parameter Name="MagazineId" Type="String" />
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
                        <table>
                            <tr>
                                <td class="tdHeader">
                                    <asp:ImageButton ID="btnClosePopup" Style="float: left" ImageUrl="~/System/Backend/lib/img/close.png" runat="server" />
                                    <img src="../lib/icons/32/Window.png" class="imgIcon" />
                                    <asp:Label runat="server" Text="قصص المجلة" CssClass="tdPageTitle" ID="Label1"></asp:Label>
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
                                            <td>عنوان القصة</td>
                                            <td>
                                                <asp:TextBox runat="server" ID="txtStoryName" style="width:400px;"></asp:TextBox></td>
                                            <td>
                                                <asp:RequiredFieldValidator runat="server" ID="reqtxtName" ControlToValidate="txtStoryName" ValidationGroup="2"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>نص القصة</td>
                                            <td>
                                                <asp:TextBox runat="server" style="height:200px;width:400px;" TextMode="MultiLine" SkinID="txtmult" ID="txtStoryDesc"></asp:TextBox></td>
                                            <td>
                                                <asp:RequiredFieldValidator runat="server" ID="RequiredFieldValidator1" ControlToValidate="txtStoryDesc" ValidationGroup="2"></asp:RequiredFieldValidator>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbtns">
                                    <asp:Button ID="btnMagazineClear" runat="server" Text="تفريغ الخانات" OnClick="btnMagazineClear_Click" />
                                    <asp:Button ID="btnMagazineSaveAndNew" ValidationGroup="2" runat="server" Text="حفظ البيانات واضافة جديد"
                                        OnClick="btnMagazineSaveAndNew_Click" />
                                    <asp:Button ID="btnMagazineSave" ValidationGroup="2" runat="server" Text="حفظ البيانات" OnClick="btnMagazineSave_Click" />
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
