<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="TTClient.Login" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #Password1 {
            margin-bottom: 0px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Email:<br />
            <asp:TextBox ID="email_input" runat="server" Width="200px" TextMode="Email"></asp:TextBox>
            <br />
            Password:<br />
            <asp:TextBox ID="password_input" runat="server" Width="200px" TextMode="Password"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="login_button" runat="server" Text="Login" Width="100px" OnClick="login_button_Click" />
            <br />
            <asp:Button ID="register_button" runat="server" Text="Register" OnClick="register_button_Click" Width="100px" />
            <br />
            <br />
            <asp:Label ID="status_display" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
