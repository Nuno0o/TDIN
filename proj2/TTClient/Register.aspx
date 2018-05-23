<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Register.aspx.cs" Inherits="TTClient.Register" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Name:<br />
            <asp:TextBox ID="name_input" runat="server" Width="200px"></asp:TextBox>
            <br />
            Email:<br />
            <asp:TextBox ID="email_input" runat="server" TextMode="Email" Width="200px"></asp:TextBox>
            <br />
            Password:<br />
            <asp:TextBox ID="password_input" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
            <br />
            Repeat Password:<br />
            <asp:TextBox ID="repeat_password_input" runat="server" TextMode="Password" Width="200px"></asp:TextBox>
            <br />
            Department:<br />
            <asp:DropDownList ID="department_drop_down" runat="server" Width="200px">
                <asp:ListItem Value="1">Information Technology</asp:ListItem>
                <asp:ListItem Value="2">Human Resources</asp:ListItem>
                <asp:ListItem Value="3">Sales and Marketing</asp:ListItem>
                <asp:ListItem Value="4">Financial and Accounting</asp:ListItem>
                <asp:ListItem Value="5">Research and Development</asp:ListItem>
                <asp:ListItem Value="6">Production and SCM</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="register_button" runat="server" Text="Register" OnClick="register_button_Click" Width="100px" />
            <br />
            <asp:Button ID="back_button" runat="server" Text="Back" Width="100px" OnClick="back_button_Click" />
            <br />
            <br />
            <asp:Label ID="status_display" runat="server"></asp:Label>
        </div>
    </form>
</body>
</html>
