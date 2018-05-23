<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tickets.aspx.cs" Inherits="TTClient.Tickets" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">

        #title_input {
            width: 300px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <div>
            Ticket Title:<br />
            <asp:TextBox ID="title_input" runat="server" Width="300px"></asp:TextBox>
            <br />
            <br />
            Ticket Description:<br />
            <asp:TextBox ID="description_input" runat="server" Height="150px" TextMode="MultiLine" Width="300px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="send_button" runat="server" Text="Send" OnClick="send_button_Click" />
            <br />
            <asp:Label ID="status_display" runat="server"></asp:Label>
            <br />
            <br />
            My Tickets:<asp:Table ID="tickets_table" runat="server" BorderStyle="Solid" BorderWidth="1px" CellPadding="5" CellSpacing="5" GridLines="Both">
            </asp:Table>
            <br />
            <asp:Button ID="logout_button" runat="server" OnClick="logout_button_Click" Text="Logout" />
            <br />
        </div>
        </div>
    </form>
</body>
</html>
