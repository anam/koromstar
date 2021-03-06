﻿<%@ Page Title="" Language="C#" MasterPageFile="~/MemberMaster.master" AutoEventWireup="true" CodeFile="SearchFoodSenderPage.aspx.cs" Inherits="SearchFoodSenderPage" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="Server">
    <style type="text/css">
        .modalBackground
        {
            background-color: #DCDCDC;
            filter: alpha(opacity=60);
            opacity: 0.6;
        }
        
        .modalPopup
        {
            background-color: White;
            border-width: 3px;
            border-style: solid;
            border-color: Gray;
            padding: 5px;
            width: 600px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="cpMenu" runat="Server">
    <ul>
        <li id="selected"><a href="SearchFoodSenderPage.aspx">Search Food Sender</a></li>
        <li><a href="CUSTOMERInsertUpdateFood.aspx?cUSTOMERID=0">Sender</a></li>
        <li><a href="SearchFoodReceiverPage.aspx">Search Food Receiver</a></li>
        <li><a href="ReceiverInsertUpdateFood.aspx?rECEIVERID=0">Receiver</a></li>
        <li><a href="SearchFoodLocation.aspx">Search Food Location</a></li>
                <li><a href="FoodTransactionPage.aspx">Food Transaction</a></li><li><a href="Default.aspx">Back</a></li>
    </ul>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="mainMemberContent" runat="Server">
    <script language="javascript" type="text/javascript">
        function SelectSingleRadiobutton(rdBtnID) {
            //process the radio button Being checked.
            var rduser = $(document.getElementById(rdBtnID));
            rduser.closest('TR').addClass('SelectedRowStyle');
            rduser.checked = true;
            // process all other radio buttons (excluding the the radio button Being checked).
            var list = rduser.closest('table').find("INPUT[type='radio']").not(rduser);
            list.attr('checked', false);
            list.closest('TR').removeClass('SelectedRowStyle');
        }
    </script>
    <div class="memberReg">
        <!-- Begin: memberReg -->
        <div class="nameRow">
            <table style="width: 100%; table-layout: fixed;">
                <colgroup>
                    <col width="15%" style="font-weight: bold;" />
                    <col width="85%" align="left" />
                </colgroup>
                <tr>
                    <td>
                        Member ID :
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <span class="regBoxsmall">
                                        <asp:TextBox ID="txtMemberID" runat="server" Text="" CssClass="txtRegsmall">
                                        </asp:TextBox>
                                        <ajaxToolkit:FilteredTextBoxExtender ID="ftbe" runat="server" TargetControlID="txtMemberID"
                                            FilterType="Numbers" />
                                    </span>
                                </td>
                                <td>
                                    Phone Number :
                                </td>
                                <td>
                                    <span class="regBoxsmall">
                                        <asp:TextBox ID="txtPhoneNumber" runat="server" Text="" CssClass="txtRegsmall">
                                        </asp:TextBox>
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        Driving License :
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <span class="regBoxsmall">
                                        <asp:TextBox ID="txtDrivingLicense" runat="server" Text="" CssClass="txtRegsmall">
                                        </asp:TextBox>
                                    </span>
                                </td>
                                <td>
                                    SSN :
                                </td>
                                <td>
                                    <span class="regBoxsmall">
                                        <asp:TextBox ID="txtSSN" runat="server" Text="" CssClass="txtRegsmall">
                                        </asp:TextBox>
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        Member Name :
                    </td>
                    <td>
                        <table>
                            <tr>
                                <td>
                                    <span class="regBoxsmall">
                                        <asp:TextBox ID="txtFirstName" runat="server" Text="" CssClass="txtRegsmall">
                                        </asp:TextBox>
                                    </span>
                                </td>
                                <td>
                                    <span class="regBoxsmall">
                                        <asp:TextBox ID="txtMiddleName" runat="server" Text="" CssClass="txtRegsmall">
                                        </asp:TextBox>
                                    </span>
                                </td>
                                <td>
                                    <span class="regBoxsmall">
                                        <asp:TextBox ID="txtLastName" runat="server" Text="" CssClass="txtRegsmall">
                                        </asp:TextBox>
                                    </span>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                    </td>
                    <td>
                        <asp:Label ID="lblmessage" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div class="regRow">
            <div class="regbtnrow">
                <asp:Button ID="btnBack" runat="server" Text="Back" CssClass="btnregtop" OnClick="btnBack_Click" />
                <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btnregtop" OnClick="btnSearch_Click" />
                <asp:Button ID="btnClear" runat="server" Text="Clear" CssClass="btnregtop" OnClick="btnClear_Click" />
            </div>
            <asp:Button ID="btnSelect" runat="server" Text="Select" CssClass="btnselect" OnClick="btnSelect_Click" />
        </div>
    </div>
    <!-- End: memberReg -->
    <div class="memberlist" style="overflow: scroll; height: 500px; width:580px;">
        <!-- Begin: memberlist -->
        <%-- <img src="App_Themes/Default/images/bg_memberlist.jpg" width="581" height="265" alt="0" />--%>
        <asp:GridView ID="gvCUSTOMER" runat="server" AutoGenerateColumns="false" CssClass="gridCss"
            GridLines="Both" BorderColor="#CDCDCD" PageSize="15" AllowPaging="True" OnPageIndexChanging="gvCUSTOMER_PageIndexChanging" Width="100%">
            <PagerSettings Mode="NumericFirstLast" />
            <Columns>
                <asp:TemplateField HeaderText="Select">
                    <ItemTemplate>
                        <%--                        <asp:LinkButton ID="lbSelect" runat="server" CommandArgument='<%#Eval("CUSTOMERID") %>' OnClick="lbSelect_Click">
                            Select
                        </asp:LinkButton>--%>
                        <asp:RadioButton ID="rbtSelect" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)" />
                        <asp:Label ID="lblCUSTOMERID" runat="server" Text='<%#Eval("CUSTOMERID") %>' Visible="false"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Memeber Name">
                    <ItemTemplate>
                        <table width="100%">
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblCUSTFNAME" runat="server" Text='<%#Eval("CUSTFNAME") %>'>
                        </asp:Label>&nbsp
                                </td>
                                <td valign="top">
                                    <asp:Label ID="lblCUSTMNAME" runat="server" Text='<%#Eval("CUSTMNAME") %>'>
                        </asp:Label>&nbsp
                                </td>
                                <td valign="top">
                                    <asp:Label ID="lblCUSTLNAME" runat="server" Text='<%#Eval("CUSTLNAME") %>'>
                        </asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Address" ControlStyle-Width="300px">
                    <ItemTemplate>
                        <asp:Label ID="lblCUSTADDRESS1" runat="server" Text='<%#Eval("CUSTADDRESS1") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Phone">
                    <ItemTemplate>
                        <table width="100%">
                            <tr>
                                <td valign="top">
                                    <asp:Label ID="lblCUSTHPHONE" runat="server" Text='<%#Eval("CUSTHPHONE") %>'>
                        </asp:Label>&nbsp
                                </td>
                                <td valign="top">
                                    <asp:Label ID="lblCUSTCPHONE" runat="server" Text='<%#Eval("CUSTCPHONE") %>'>
                        </asp:Label>
                                    &nbsp
                                </td>
                                <td valign="top">
                                    <asp:Label ID="lblCUSTWPHONE" runat="server" Text='<%#Eval("CUSTWPHONE") %>'>
                        </asp:Label>
                                </td>
                            </tr>
                        </table>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="City" ControlStyle-Width="150px">
                    <ItemTemplate>
                        <asp:Label ID="lblCUSTCITY" runat="server" Text='<%#Eval("CUSTCITY") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="State">
                    <ItemTemplate>
                        <asp:Label ID="lblCUSTSTATE" runat="server" Text='<%#Eval("CUSTSTATE") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ZIP">
                    <ItemTemplate>
                        <asp:Label ID="lblCUSTZIP" runat="server" Text='<%#Eval("CUSTZIP") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <EmptyDataTemplate>
                            <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Size="Larger" Text="No data found for the above search criterion"></asp:Label>
                            <asp:Label ID="Label12" runat="server" ForeColor="Green" Font-Size="Larger" Text=" Please change the search criterion.."></asp:Label>
                        </EmptyDataTemplate>
            <HeaderStyle CssClass="gvHeadercss" />
            <RowStyle CssClass="gvRowCss" />
            <AlternatingRowStyle CssClass="gvRowAltCss" />
        </asp:GridView>
    </div>
    <!-- End: memberlist -->
    <div class="memberBotBtnlist" >
        <asp:Button ID="btnSAR" runat="server" Text="SAR" CssClass="btnregbot" OnClick="btnSAR_Click" />
        <asp:Button ID="btnHistory" runat="server" Text="History" CssClass="btnregbot" OnClick="btnHistory_Click" />
        <asp:Button ID="btnDelete" runat="server" Text="Delete" CssClass="btnregbot" OnClick="btnDelete_Click" />
        <asp:Button ID="btnEditSender" runat="server" Text="Edit Sender" CssClass="btnregbot"
            OnClick="btnEditSender_Click" />
        <asp:Button ID="btnNewSender" runat="server" Text="New Sender" CssClass="btnregbot nomargin"
            OnClick="btnNewSender_Click" />
    </div>






        <div style="visibility: hidden;">
        <asp:Button ID="Button1" runat="server" Text="" />
    </div>
    <br />
    <br />
    <asp:Panel ID="panAll" runat="server" CssClass="modalPopup">
        <center>
    <div style=" height:300px; width:100%; overflow:scroll;">
            <h2>Select the Sender to which you want to switch the info..</h2>
                <asp:GridView ID="gvwSearch" runat="server" AutoGenerateColumns="false" CssClass="gridCss"
                    GridLines="Both" BorderColor="#CDCDCD" Width="100%">
                    <Columns>
                        <asp:TemplateField HeaderText="Select">
                            <ItemTemplate>
                                <asp:RadioButton ID="rbtSelect" runat="server" OnClick="javascript:SelectSingleRadiobutton(this.id)" />
                                <asp:Label ID="lblCUSTOMERID" runat="server" Text='<%#Eval("CUSTOMERID") %>' Visible="false"></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Memeber Name">
                            <ItemTemplate>
                                <asp:Label ID="lblCUSTFNAME" runat="server" Text='<%#Eval("CUSTFNAME") %>'>
                        </asp:Label>&nbsp
                                <asp:Label ID="lblCUSTMNAME" runat="server" Text='<%#Eval("CUSTMNAME") %>'>
                        </asp:Label>&nbsp
                                <asp:Label ID="lblCUSTLNAME" runat="server" Text='<%#Eval("CUSTLNAME") %>'>
                        </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Address">
                            <ItemTemplate>
                                <asp:Label ID="lblCUSTADDRESS1" runat="server" Text='<%#Eval("CUSTADDRESS1") %>'>
                        </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Phone">
                            <ItemTemplate>
                                <asp:Label ID="lblCUSTHPHONE" runat="server" Text='<%#Eval("CUSTHPHONE") %>'>
                        </asp:Label>&nbsp
                                <asp:Label ID="lblCUSTCPHONE" runat="server" Text='<%#Eval("CUSTCPHONE") %>'>
                        </asp:Label>
                                &nbsp
                                <asp:Label ID="lblCUSTWPHONE" runat="server" Text='<%#Eval("CUSTWPHONE") %>'>
                        </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <EmptyDataTemplate>
                        <asp:Label ID="Label1" runat="server" ForeColor="Red" Font-Size="Larger" Text="No data found for the above search criterion"></asp:Label>
                        <asp:Label ID="Label12" runat="server" ForeColor="Green" Font-Size="Larger" Text=" Please change the search criterion.."></asp:Label>
                    </EmptyDataTemplate>
                    <HeaderStyle CssClass="gvHeadercss" />
                    <RowStyle CssClass="gvRowCss" />
                    <AlternatingRowStyle CssClass="gvRowAltCss" />
                </asp:GridView>
            </div>



            

            <br />
            <br />
                    <table>
                    <tr>
                    <td>
                            <asp:Button ID="btnOk" runat="server" Text="Ok" CssClass="btnregbot" 
                    onclick="btnOk_Click"  />
                    </td>
                    <td>
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CssClass="btnregbot" 
                    onclick="btnCancel_Click"  />
                    </td>
                    </tr>
                    </table>
                    <br />
                    

    
            </center>
    </asp:Panel>
    <ajaxToolkit:ModalPopupExtender BackgroundCssClass="modalBackground" DropShadow="false"
        runat="server" PopupControlID="panAll" ID="ModalPopupExtender1" TargetControlID="Button1" />
</asp:Content>

