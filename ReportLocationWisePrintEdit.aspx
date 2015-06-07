<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportLocationWisePrintEdit.aspx.cs" Inherits="LocationWiseReport" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Koromstar</title>
    <style type="text/css">
        body{font-family:Arial;}
        .style1 {
            width: 100%;
        }
        .colorbackground
        {
            background-color:#3399FF;
            /*background-image:url(App_Themes/Default/images/coloredbackground.png);*/
            background-repeat:repeat;
            font-weight:bold;
            font-size:21px;
            text-align:center;
            }
            .ar{text-align:right;}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div align="center">
        <asp:Label ID="lblPrint" runat="server" Text=""></asp:Label>
        
        <asp:GridView ID="gvTras" runat="server" AutoGenerateColumns="false" CssClass="gridCss">
            <Columns>
                <asp:TemplateField HeaderText="Date">
                    <ItemTemplate>
                        <asp:Label ID="Date" runat="server" Text='<%#Eval("TRANSDT","{0:MM/dd/yyyy}") %>'>
                        </asp:Label>
                        <asp:HiddenField ID="hfTRANSID" runat="server" Value='<%#Eval("TRANSID") %>'/>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="CODE">
                    <ItemTemplate>
                        <asp:TextBox ID="txtRefCode" runat="server" Text='<%#Eval("REFCODE") %>'></asp:TextBox>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Sender">
                    <ItemTemplate>
                        <asp:Label ID="SenderName" runat="server" Text='<%#Eval("SenderName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ReceiverName">
                    <ItemTemplate>
                        <asp:Label ID="ReceiverName" runat="server" Text='<%#Eval("ReceiverName") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Location">
                    <ItemTemplate>
                        <asp:Label ID="BRANCH" runat="server" Text='<%#Eval("BRANCH") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="AMOUNT">
                    <ItemTemplate>
                        <asp:Label ID="TRANSAMOUNT" runat="server" Text='<%#Eval("TRANSAMOUNT","{0:0.00}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Fee">
                    <ItemTemplate>
                        <asp:Label ID="TRANSFEES" runat="server" Text='<%#Eval("TRANSFEES","{0:0.00}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Total">
                    <ItemTemplate>
                        <asp:Label ID="TRANSTOTALAMOUNT" runat="server" Text='<%#Eval("TRANSTOTALAMOUNT","{0:0.00}") %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
        <asp:Button ID="btnSave" runat="server" Text="Save" onclick="btnSave_Click" />
    </div>
    </form>
</body>
</html>
