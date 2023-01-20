<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true" CodeBehind="help.aspx.cs" Inherits="help" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
    <img src="../lib/icons/32/Dashboard.png" class="imgIcon" />
    <asp:Label runat="server" Text="مساعدة" CssClass="tdMainTitle" ID="lblPageMainTitle"> </asp:Label>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="CPHContent" runat="server">
    <table width="100%" cellpadding="2" cellspacing="2">
        <tr>
            <td class="tdHeader">
                <img class="imgIcon" src="../lib/icons/24/User.png" />
                <asp:Label runat="server" Text="شرح رفع ملفات العظات" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
            </td>
        </tr>
        <tr>
            <td id="Td1" runat="server" style="padding-top: 10px; padding-bottom: 10px; font-size: 16px">نحن نستخدم هذه الطريقه لان حجم ملف العظات يكون كبير جدا والانترنت ممكن يفصل اثناء رفع الملفات بالطريقه العاديه وكمان هذه الطريقه تسمح لنا برفع اى عدد من العظات فى نفس الوقت
            </td>
        </tr>
        <tr>
            <td runat="server" style="padding-top: 10px; padding-bottom: 80px; text-align: right; padding-right: 50px; font-size: 16px">
                <ul>
                    <li>لابد من تحميل برنامج ال اف تي بي من  <a target="_blank" href="https://filezilla-project.org/download.php?type=client">هنا</a>  </li>
                    <li>قم بتثبيت البرنامج على جهازك وبعدها سوف يطلب منك البيانات الاتية
                     
                     <ul style="padding-right: 15px">
                         <li>Host :  shepherdmeeting.com</li>
                         <li>User Name : shepherdsound</li>
                         <li>Password : Ftpsh2015#</li>
                         <li>Port : 21</li>
                     </ul>
                    </li>
                    <li>ثم نضغط على quick connect</li>
                    <li>بعدها سوف نجد كل الملفات التي تم رفعها سابقا</li>
                    <li>وكل ما نفعله هو سحب ملف العظه الجديد فى البرنامج بجوار العظات القديمة وهو سوف يقوم برفع الملف
ويمكن رفع اكثر من ملف فى نفس الوقت</li>
                    <li>بعد رفع كل الملفات بالبرنامج ندخل على الموقع ونضيف بيانات العظة ونختار اسم الملف الذين قمنا برفعه</li>

                </ul>

            </td>
        </tr>
        <tr>
            <td class="tdHeader">
                <img class="imgIcon" src="../lib/icons/24/User.png" />
                <asp:Label runat="server" Text="شرح نقل الاسماء من الموقع للجي ميل" CssClass="tdPageTitle" ID="Label1"></asp:Label>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 10px; padding-bottom: 80px; text-align: right; padding-right: 50px; font-size: 16px">
                <ul>
                    <li>ندخل على التقرير العام بالموقع</li>
                    <li>نبحث عن بيانات الاشخاص التي نريد نقل بياتهم للجي ميل
                    </li>
                    <li>نضغط على  Export Contacts to Gmail</li>
                    <li>نفتح الجي ميل</li>
                    <li>ثم Contacts</li>
                    <li>ثم Import</li>
                    <li>ثم نختار الملف الذي قمنا بتحميله من الموقع</li>
                    <li>ثم يظهر لنا بيانات الشخص الاساسية مثل الاسم والبريد الالكتروني والعنوان وتليفون المنزل ورقم الموبايل الاساسي والثاني وتاريخ الميلاد</li>
                </ul>
            </td>
        </tr>
    </table>
</asp:Content>
