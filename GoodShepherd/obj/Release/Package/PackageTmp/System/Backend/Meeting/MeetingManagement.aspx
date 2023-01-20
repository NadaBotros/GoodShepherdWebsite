<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true"
    CodeBehind="MeetingManagement.aspx.cs" Inherits="System.Backend.manage.MeetingManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="برنامج الاجتماع" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img src="../lib/icons/32/Window.png" class="imgIcon" />
                <asp:Label runat="server" Text="ادارة برنامج الاجتماع" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="msg" runat="server">
                <asp:Label ID="lblMessge" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td>
                <table cellpadding="2" cellspacing="2">
                    <tr>
                        <td>تاريخ الاجتماع <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtDate"></asp:TextBox>
                            <asp:CalendarExtender Format="d/M/yyyy" ID="txtDate_CalendarExtender" runat="server" Enabled="True" TargetControlID="txtDate">
                            </asp:CalendarExtender>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1"
                                ValidationGroup="1" runat="server" ControlToValidate="txtDate"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>اسم المتكلم <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:DropDownList Style="float: right" runat="server" SkinID="drpwList" DataTextField="SpeakerName" DataValueField="SpeakerId" DataSourceID="odsSpeakers" OnDataBound="drpSpeaker_DataBound" ID="drpSpeaker"></asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator2"
                                ValidationGroup="1" runat="server" ControlToValidate="drpSpeaker"></asp:RequiredFieldValidator>
                            <asp:ObjectDataSource ID="odsSpeakers" runat="server" SelectMethod="LoadByDeleteState"
                                TypeName="DAL.SpeakersManage">
                                <SelectParameters>
                                    <asp:Parameter Name="Active" Type="string" DefaultValue="true" />
                                </SelectParameters>
                            </asp:ObjectDataSource>
                        </td>
                    </tr>
                    <tr>
                        <td>عنوان العظة <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtMeetingTitle"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3"
                                ValidationGroup="1" runat="server" ControlToValidate="txtMeetingTitle"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Literal runat="server" ID="ltrAudio"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>يرجي رفع الملف بواسطه برنامج الاف تي بي ثم انسخ اسم الملف هنا او قم بتعديله اذا كان لا يعمل
                        </td>
                    </tr>
                    <tr>
                        <td>ملف صوت العظة <span class="reqstar">*</span>
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtFileName"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:Literal runat="server" ID="ltlVideo"></asp:Literal>
                        </td>
                    </tr>
                    <tr>
                        <td>لينك يوتيوب لفيديو العظة</td>
                        <td>
                            <asp:TextBox runat="server" ID="txtYoutubelink"></asp:TextBox>
                        </td>
                    </tr>
                </table>
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
</asp:Content>
