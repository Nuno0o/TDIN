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
            <asp:TextBox ID="name_input" runat="server"></asp:TextBox>
            <br />
            Email:<br />
            <asp:TextBox ID="email_input" runat="server"></asp:TextBox>
            <br />
            Password:<br />
            <input id="password_input" type="password" /><br />
            Repeat Password:<br />
            <input id="repeat_password_input" type="password" /><br />
            Department:<br />
            <asp:DropDownList ID="department_selector" runat="server" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
                <asp:ListItem Value="1">Information Technology</asp:ListItem>
                <asp:ListItem Value="2">Human Resources</asp:ListItem>
                <asp:ListItem Value="3">Sales and Marketing</asp:ListItem>
                <asp:ListItem Value="4">Financial and Accounting</asp:ListItem>
                <asp:ListItem Value="5">Research and Development</asp:ListItem>
                <asp:ListItem Value="6">Production and SCM</asp:ListItem>
            </asp:DropDownList>
            <br />
            <br />
            <asp:Button ID="register_button" runat="server" Text="Register" />
            <br />
            <asp:Button ID="back_button" runat="server" Text="Back" Width="70px" />
        </div>
    </form>
</body>
</html>
