<%@ Page Title="" Language="C#" MasterPageFile="~/System/Backend/admin.Master" AutoEventWireup="true" CodeBehind="FamilyManagement.aspx.cs" Inherits="System.Backend.FamilyManagement" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register Src="~/System/Backend/Family/UcPerson.ascx" TagPrefix="uc1" TagName="UcPerson" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="CPHPageHeader" runat="server">
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
                        <asp:TabPanel HeaderText="افراد الاسرة" ID="tabPersons" runat="server">
                            <ContentTemplate>
                                <table width="100%">
                                    <tr>
                                        <td class="tdHeader">
                                            <img class="imgIcon2" src="../lib/icons/24/Dashboard.png" />
                                            <asp:Label runat="server" Text="اسماء افراد الاسرة" CssClass="tdPageTitle" ID="lblPageSubTitle"></asp:Label>
                                            <asp:LinkButton runat="server" class="addnew" ID="btnAddNew" OnClick="btnAddNew_Click"
                                                Text="اضافة جديد ...."></asp:LinkButton>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:GridView ID="grdData" Width="100%" runat="server" CssClass="grd" DataSourceID="odsData"
                                                AutoGenerateColumns="False" OnRowCommand="grdData_RowCommand" OnRowDataBound="grdData_RowDataBound">
                                                <Columns>
                                                    <asp:BoundField DataField="PersonCode" HeaderText="الكود" SortExpression="PersonCode">
                                                        <HeaderStyle Width="20%" />
                                                        <ItemStyle Width="20%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="PersonName" HeaderText="الاسم" SortExpression="PersonName">
                                                        <HeaderStyle Width="20%" />
                                                        <ItemStyle Width="20%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Studious" HeaderText="الحضور" SortExpression="Studious">
                                                        <HeaderStyle Width="20%" />
                                                        <ItemStyle Width="20%" />
                                                    </asp:BoundField>
                                                    <asp:BoundField DataField="Relationship" HeaderText="القرابة" SortExpression="Relationship">
                                                        <HeaderStyle Width="20%" />
                                                        <ItemStyle Width="20%" />
                                                    </asp:BoundField>
                                                    <asp:TemplateField>
                                                        <ItemTemplate>
                                                            <asp:ImageButton ID="imgEdit" runat="server" CssClass="imgIcon3 tip_right_top" CommandName="viewitem" CommandArgument='<%# Eval("PersonId") %>' ImageUrl="~/System/Backend/lib/img/Edit.png" title="Edit"></asp:ImageButton>
                                                            <asp:ImageButton ID="imgDelete" runat="server" CssClass="imgIcon3 tip_right_top" ImageUrl="../lib/img/Delete.png"
                                                                CommandName="deleteitem" CommandArgument='<%# Eval("PersonId") %>' OnClientClick="if(!confirm('Are you sure you want delete this?')) return false;"
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
                                                TypeName="DAL.PersonManagement">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="true" Name="Active" Type="String" />
                                                    <asp:Parameter Name="FamilyId" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                </table>

                            </ContentTemplate>
                        </asp:TabPanel>
                        <asp:TabPanel HeaderText="بيانات الأسرة" ID="tabFamilyInfo" runat="server">
                            <ContentTemplate>
                                <table cellpadding="5">
                                    <tr>
                                        <td>كود الاسرة
                                        </td>
                                        <td>
                                            <asp:TextBox ReadOnly="True" runat="server" ID="txtFamilyCode"></asp:TextBox>
                                        </td>
                                        <td>تليفون المنزل
                                        </td>
                                        <td>
                                            <asp:TextBox runat="server" CssClass="phone" ID="txtPhone"></asp:TextBox>
                                            <asp:FilteredTextBoxExtender TargetControlID="txtPhone" FilterMode="InvalidChars"  ID="FilteredTextBoxExtender1" runat="server" Enabled="True" FilterType="Numbers"></asp:FilteredTextBoxExtender>
                                            <asp:RegularExpressionValidator ValidationGroup="1" ControlToValidate="txtPhone" ID="RegularExpressionValidator1" ValidationExpression="[0-9]{7,11}" runat="server" ErrorMessage="رقم التليفون من 7 الي 11 رقم"></asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>العنوان - المدينة <span class="reqstar">*</span></td>
                                        <td>
                                            <asp:DropDownList SkinID="drpwList" runat="server" ID="drpCity" AutoPostBack="True" DataTextField="CityName" DataValueField="CityId" DataSourceID="odsCity"></asp:DropDownList>
                                            <asp:ObjectDataSource ID="odsCity" runat="server" SelectMethod="LoadByDeleteState" TypeName="DAL.CityManagement">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </td>
                                        <td>العنوان - المنطقة <span class="reqstar">*</span></td>
                                        <td>
                                            <asp:DropDownList SkinID="drpwList" runat="server" ID="drpArea" DataTextField="AreaName" DataValueField="AreaId" DataSourceID="odsArea"></asp:DropDownList>
                                            <asp:ObjectDataSource ID="odsArea" runat="server" SelectMethod="LoadByDeleteState" TypeName="DAL.AreaManagement">
                                                <SelectParameters>
                                                    <asp:ControlParameter ControlID="drpCity" Name="CityId" PropertyName="SelectedValue" Type="String" />
                                                    <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>العنوان - الشارع <span class="reqstar">*</span></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtStreet"></asp:TextBox>                                           
                                            <asp:AutoCompleteExtender ID="txtStreet_AutoCompleteExtender" runat="server" ServiceMethod="GetStreetsList" MinimumPrefixLength="1" TargetControlID="txtStreet" DelimiterCharacters="" Enabled="True" ServicePath="">
                                            </asp:AutoCompleteExtender>
                                             <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2"
                                                ValidationGroup="1" runat="server" ControlToValidate="txtStreet"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>من / بجوار</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtNextTo"></asp:TextBox></td>
                                    </tr>
                                    <tr>
                                        <td>رقم العمارة <span class="reqstar">*</span></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtBuildingNo"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1"
                                                ValidationGroup="1" runat="server" ControlToValidate="txtBuildingNo"></asp:RequiredFieldValidator>
                                        </td>
                                        <td>الدور <span class="reqstar">*</span></td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtFloorNo"></asp:TextBox>
                                            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3"
                                                ValidationGroup="1" runat="server" ControlToValidate="txtFloorNo"></asp:RequiredFieldValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>رقم الشقة</td>
                                        <td>
                                            <asp:TextBox runat="server" ID="txtFlatNo"></asp:TextBox></td>
                                        <td rowspan="2">ملاحظات</td>
                                        <td rowspan="2">
                                            <asp:TextBox runat="server" SkinID="txtmult" ID="txtNotes" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>تاريخ الزواج</td>
                                        <td>
                                            <asp:CalendarExtender Format="d/M/yyyy" ID="ceMarriageDate" TargetControlID="txtMarriageDate" runat="server" Enabled="True"></asp:CalendarExtender>
                                            <asp:TextBox runat="server" ID="txtMarriageDate"></asp:TextBox></td>

                                    </tr>
                                    <tr>
                                        <td>عائل الاسرة</td>
                                        <td>
                                            <asp:DropDownList runat="server" SkinID="drpwList" ID="drpResbonsable" DataTextField="PersonName" DataValueField="PersonId" DataSourceID="odsPersons"></asp:DropDownList>
                                            <asp:ObjectDataSource ID="odsPersons" runat="server" SelectMethod="LoadByDeleteState" TypeName="DAL.PersonManagement">
                                                <SelectParameters>
                                                    <asp:Parameter DefaultValue="True" Name="Active" Type="String" />
                                                    <asp:Parameter Name="FamilyId" Type="String" />
                                                </SelectParameters>
                                            </asp:ObjectDataSource>
                                        </td>
                                        <td></td>
                                        <td></td>
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
                                    <asp:Label runat="server" Text="بيانات افراد الاسرة" CssClass="tdPageTitle" ID="Label1"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <uc1:UcPerson runat="server" ID="UcPerson" />
                                </td>
                            </tr>
                            <tr>
                                <td class="tdbtns">
                                    <asp:Button ID="btnPersonClear" runat="server" Text="تفريغ الخانات" OnClick="btnPersonClear_Click" />
                                    <asp:Button ID="btnPersonSaveAndNew" ValidationGroup="11" runat="server" Text="حفظ البيانات واضافة جديد"
                                        OnClick="btnPersonSaveAndNew_Click" />
                                    <asp:Button ID="btnPesonSaver" ValidationGroup="11" runat="server" Text="حفظ البيانات" OnClick="btnPesonSaver_Click" />
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
