<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="UserManagement.aspx.cs" Inherits="System.Backend.manage.UserManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="قائمة مستخدمين الموقع" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <div class="dvtabs">
        <table width="100%" cellpadding="2" cellspacing="2">

            <tr>
                <td id="msg" runat="server">
                    <asp:Label ID="lblMessge" runat="server"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:TabContainer ID="TabContainer" runat="server" runat="server" Width="99%"
                        CssClass="TabContainer" AutoPostBack="False" OnDemand="False" ActiveTabIndex="0">
                        <asp:TabPanel HeaderText="بيانات المستخدم" ID="tabUserInfo" runat="server">
                            <ContentTemplate>
                                <table cellpadding="2" cellspacing="2">
                                    <tr>
                                        <td width="15%">اسم المستخدم <span class="reqstar">*</span>
                                        </td>
                                        <td width="85%">
                                            <asp:TextBox runat="server" ID="txtUserName"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" ErrorMessage="You Must Enter Value In This Field"
                                                ValidationGroup="1" runat="server" ControlToValidate="txtUserName"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>اسم الدخول <span class="reqstar">*</span>
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtLogInName"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2" ErrorMessage="You Must Enter Value In This Field"
                                                ValidationGroup="1" runat="server" ControlToValidate="txtLogInName"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>البريد الالكتروني
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtEmail" runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="reqLoginEmail" ValidationGroup="1" ControlToValidate="txtEmail"
                                                runat="server" Text="Error" ErrorMessage="خطا بالبريد الالكتروني" ValidationExpression="\w+([-+.']\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>رقم الموبايل
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtMobile" runat="server"></asp:TextBox>
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ValidationGroup="1"
                                                ControlToValidate="txtMobile" runat="server" Text="Error" ErrorMessage="خطا برقم الموبايل"
                                                ValidationExpression="[0-9]+"></asp:RegularExpressionValidator>
                                            <asp:FilteredTextBoxExtender ID="txtCost_FilteredTextBoxExtender" runat="server"
                                                Enabled="True" FilterType="Numbers" TargetControlID="txtMobile">
                                            </asp:FilteredTextBoxExtender>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>الوظيفة
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtJob" runat="server"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>رفع الصورة الشخصية
                                        </td>
                                        <td>
                                            <asp:FileUpload ID="fupldImage" runat="server" />
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td></td>
                                        <td>
                                            <asp:Image runat="server" CssClass="imgborder" ImageUrl="~/System/Backend/lib/img/avatar150.png"
                                                ID="imgUser" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>كلمة المرور
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtPassWord" runat="server" TextMode="Password"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>تاكيد كلمة المرور
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtConfirmPassword" TextMode="Password"></asp:TextBox>
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="txtPassWord"
                                                ControlToValidate="txtConfirmPassword" ValidationGroup="1" ErrorMessage="كلمة السر غير متطابقة"></asp:CompareValidator>
                                        </td>
                                    </tr>
                                     <tr>
                                        <td>يسمح للمستخدم بفتح كل الصفحات
                                        </td>
                                        <td>
                                            <asp:RadioButtonList ID="radIsAdmin" RepeatDirection="Horizontal" runat="server">
                                                <asp:ListItem Value="True">نعم</asp:ListItem>
                                                <asp:ListItem Value="False">لا</asp:ListItem>
                                            </asp:RadioButtonList>
                                        </td>
                                    </tr>
                                </table>
                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel HeaderText="صلاحيات المستخدم" ID="TabPages" runat="server">
                            <ContentTemplate>
                                <asp:GridView AutoGenerateColumns="False" Width="100%" SkinID="grdnotPag" ID="grdPages" runat="server" DataSourceID="odsPages">
                                    <Columns>
                                        <asp:BoundField DataField="PageTitle" SortExpression="PageTitle" HeaderText="الصفحة">
                                            <ItemStyle Width="70%" />
                                        </asp:BoundField>
                                        <asp:TemplateField HeaderText="يسمح بدخول الصفحة">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="lblId" Visible="false" Text='<%#Eval("SiteTreeId") %>'></asp:Label>
                                                <asp:RadioButtonList BorderStyle="None" runat="server" ItemStyle-Width="30%" ID="radAllow" RepeatDirection="Horizontal">
                                                    
                                                    <asp:ListItem Value="True">يمسح بالدخول</asp:ListItem>
                                                    <asp:ListItem Value="False" Selected="True">لا يسمح بالدخول</asp:ListItem>
                                                </asp:RadioButtonList>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                                <asp:ObjectDataSource ID="odsPages" runat="server" SelectMethod="LoadAllPages" TypeName="DAL.SiteTreeManage"></asp:ObjectDataSource>
                            </ContentTemplate>
                        </asp:TabPanel>
                    </asp:TabContainer>
                </td>
            </tr>
            <tr>
                <td></td>
            </tr>
            <tr>
                <td class="tdbtns">
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
