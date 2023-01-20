<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true" CodeBehind="GeneralReport.aspx.cs" Inherits="System.Backend.GeneralReport" %>

<%@ Register Src="~/System/Backend/UserControls/UcAdvancedSearch.ascx" TagPrefix="uc1" TagName="UcAdvancedSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../../lib/icons/32/Dashboard.png" class="imgIcon" /><asp:Label runat="server"
        Text="التقارير" CssClass="tdMainTitle" ID="lblPageMainTitle"></asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon2" src="../../lib/icons/24/Dashboard.png" />
                <asp:Label runat="server" Text="تقرير عام" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>

            </td>
        </tr>
        <tr>
            <td>
                <uc1:UcAdvancedSearch runat="server" ID="UcAdvancedSearch" />
            </td>
        </tr>
        <tr>
            <td>
                <table>
                    <tr>
                        <td>البيانات المراد عرضها</td>
                        <td>
                            <asp:CheckBoxList runat="server" Width="500px" RepeatColumns="4" RepeatDirection="Horizontal" ID="chkViewData">
                                <asp:ListItem Value="PersonCode" Selected="True">كود الشخص</asp:ListItem>
                                <asp:ListItem Value="PersonName" Selected="True">الاسم بالكامل</asp:ListItem>
                                <asp:ListItem Value="Studious">الحضور</asp:ListItem>
                                <asp:ListItem Value="Relationship">القرابة</asp:ListItem>
                                <asp:ListItem Value="NationalID">الرقم القومي</asp:ListItem>
                                <asp:ListItem Value="BirthDate">تاريخ الميلاد</asp:ListItem>
                                <asp:ListItem Value="MarriageDate">تاريخ الزواج</asp:ListItem>
                                <asp:ListItem Value="BloodType">فصيله الدم</asp:ListItem>
                                <asp:ListItem Value="MaritalStatus">الحالة الاجتماعية</asp:ListItem>
                                <asp:ListItem Value="Job">الوظيفة</asp:ListItem>
                                <asp:ListItem Value="FatherName">اب الاعتراف</asp:ListItem>
                                <asp:ListItem Value="MobileNo1" Selected="True">الموبايل</asp:ListItem>
                                <asp:ListItem Value="Email">البريد الالكتروني</asp:ListItem>
                                <asp:ListItem Value="Skype">Skype</asp:ListItem>
                                <asp:ListItem Value="FaceBook">FaceBook</asp:ListItem>
                                <asp:ListItem Value="CityName">المدينة</asp:ListItem>
                                <asp:ListItem Value="AreaName">المنطقة</asp:ListItem>
                                <asp:ListItem Value="Address">العنوان</asp:ListItem>
                                <asp:ListItem Value="HomePhone">التليفون</asp:ListItem>
                                <asp:ListItem Value="CreatedByName">مدخل البيان</asp:ListItem>
                                <asp:ListItem Value="CreatedOn">تاريخ الادخال</asp:ListItem>

                            </asp:CheckBoxList>
                        </td>
                    </tr>
                    <tr>
                        <td>ترتيب البيانات بواسطة</td>
                        <td>
                            <asp:RadioButtonList runat="server" ID="radOrderBy" Style="width: 500px" RepeatDirection="Horizontal">
                                <asp:ListItem Value="PersonCode">كود الشخص</asp:ListItem>
                                <asp:ListItem Value="PersonName" Selected="True">الاسم بالكامل</asp:ListItem>
                                <asp:ListItem Value="MarriageDate">تاريخ الزواج</asp:ListItem>
                                <asp:ListItem Value="BirthDate">تاريخ الميلاد</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
        </tr>
            </td>
        <tr>
            <td class="tdbtns">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                    <ContentTemplate>
                        <asp:Button runat="server" OnClick="btnReport_Click" Text="عرض التقرير" ID="btnReport" CssClass="btn" />
                       <asp:Button runat="server" OnClick="btnReport2_Click" Text="تقرير كل المخدومين غير مربوطين بخدام" ID="Button1" CssClass="btn" />
                       <asp:Button runat="server" OnClick="btnReport3_Click" Text="تقرير كل المخدومين " ID="Button2" CssClass="btn" />
                <%--       <asp:Button runat="server" OnClick="btnReport4_Click" Text="تقرير المخدومين المربوطين بخدام " ID="Button3" CssClass="btn" />
                       <asp:Button runat="server" OnClick="btnReport5_Click" Text="تقرير المخدومين الغير مربوطين بخدام " ID="Button4" CssClass="btn" />--%>





                    </ContentTemplate>
                </asp:UpdatePanel>
            </td>
        </tr>
    </table>

</asp:Content>
