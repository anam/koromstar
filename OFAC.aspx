﻿<%@ Page Language="C#" AutoEventWireup="true" CodeFile="OFAC.aspx.cs" Inherits="OFAC" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:TextBox ID="txtCUSTFNAME" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtCUSTMNAME" runat="server"></asp:TextBox>
        <asp:TextBox ID="txtCUSTLNAME" runat="server"></asp:TextBox>
        <asp:Button ID="Button1" runat="server" Text="Button" onclick="Button1_Click" />
        <br />
        <asp:Label ID="Label1" runat="server" Text=""></asp:Label>
    </div>
    </form>
</body>
</html>
