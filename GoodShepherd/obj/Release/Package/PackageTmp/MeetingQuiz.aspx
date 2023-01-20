<%@ Page Title="اجتماع الراعي الصالح | مسابقة الاجتماع" Language="C#" MasterPageFile="~/SiteInside.master" AutoEventWireup="true" CodeBehind="MeetingQuiz.aspx.cs" Inherits="MeetingQuiz" %>

<asp:Content ID="Content1" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%">
        <tr>
            <td>
                <div class="dvMainPageTitle">
                    <div class="dvTitle">
                        مسابقة الاجتماع
                    </div>
                    <div class="dvTitleBg"></div>
                </div>
            </td>
        </tr>
        <tr>
            <td>
                <asp:ObjectDataSource ID="ods" runat="server" SelectMethod="LoadByDeleteState" TypeName="DAL.QuizManage">
                    <SelectParameters>
                        <asp:Parameter DefaultValue="true" Name="Active" Type="String" />
                    </SelectParameters>
                </asp:ObjectDataSource>
                <asp:ListView ID="lst" DataSourceID="ods" GroupItemCount="4" runat="server">
                    <LayoutTemplate>
                        <table id="Table1" runat="server" cellspacing="3" cellpadding="0">
                            <tr runat="server" id="groupPlaceholder">
                            </tr>
                        </table>
                    </LayoutTemplate>
                    <GroupTemplate>
                        <tr runat="server" id="tableRow">
                            <td runat="server" id="itemPlaceholder" />
                        </tr>
                    </GroupTemplate>
                    <ItemTemplate>
                        <td id="itemPlaceholder" class="tdView" align="center" valign="top" runat="server">
                            <table style="margin: auto; text-align: center" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td valign="top">
                                        <asp:HyperLink runat="server" CssClass="lnkImage" NavigateUrl='<%#"Quizdetails.aspx?id="+Eval("QuizId") %>' ID="lnkImage" ImageUrl='<%#"images/S140_200/"+Eval("QuizCover") %>'></asp:HyperLink>
                                    </td>
                                </tr>
                                <tr>
                                    <td><a href='<%#"Quizdetails.aspx?id="+Eval("QuizId") %>'><%#Eval("QuizTitle")+"<br/>"+Eval("QuizDate") %></a></td>
                                </tr>
                            </table>
                        </td>
                    </ItemTemplate>
                </asp:ListView>
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:DataPager PageSize="12" runat="server" ID="lstPager"
                    PagedControlID="lst">
                    <Fields>
                        <asp:NumericPagerField ButtonCount="10" NumericButtonCssClass="numeric_button" CurrentPageLabelCssClass="current_page" />
                    </Fields>
                </asp:DataPager>
            </td>
        </tr>
    </table>
</asp:Content>
