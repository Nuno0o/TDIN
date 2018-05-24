<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Ticket.aspx.cs" Inherits="TTClient.Ticket" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Ticket Id:<br />
            <asp:TextBox ID="id_text" runat="server" BackColor="#CCCCCC" ReadOnly="True" Width="300px"></asp:TextBox>
            <br />
            Ticket Date:<br />
            <asp:TextBox ID="date_text" runat="server" BackColor="#CCCCCC" ReadOnly="True" Width="300px"></asp:TextBox>
            <br />
            Ticket Title:<br />
            <asp:TextBox ID="title_text" runat="server" BackColor="#CCCCCC" ReadOnly="True" Width="300px"></asp:TextBox>
            <br />
            Ticket Description:<br />
            <asp:TextBox ID="description_text" runat="server" BackColor="#CCCCCC" Height="100px" ReadOnly="True" TextMode="MultiLine" Width="300px"></asp:TextBox>
            <br />
            Ticket Answer:<br />
            <asp:TextBox ID="answer_text" runat="server" BackColor="#CCCCCC" Height="100px" ReadOnly="True" TextMode="MultiLine" Width="300px"></asp:TextBox>
            <br />
            <br />
            Additional Question:<br />
            <asp:TextBox ID="question_input" runat="server" Height="100px" TextMode="MultiLine" Width="300px"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="send_button" runat="server" OnClick="send_button_Click" Text="Send" Width="100px" />
            <br />
            <asp:Button ID="back_button" runat="server" OnClick="back_button_Click" Text="Back" Width="100px" />
            <br />
            <asp:Label ID="status_display" runat="server"></asp:Label>
            <br />
            <br />
            My Questions:<asp:Table ID="questions_table" runat="server" BorderStyle="Solid" BorderWidth="1px" CellPadding="5" CellSpacing="5" GridLines="Both">
            </asp:Table>
        </div>
    </form>
</body>
</html>
