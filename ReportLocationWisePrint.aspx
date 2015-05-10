<%@ Page Language="C#" AutoEventWireup="true" CodeFile="ReportLocationWisePrint.aspx.cs" Inherits="LocationWiseReport" %>

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
        
        <br />
        <table class="style1">
            <tr>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    </td>
            </tr>
            <tr>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    </td>
                <td>
                    </td>
            </tr>
        </table>

    </div>
    </form>
</body>
</html>
