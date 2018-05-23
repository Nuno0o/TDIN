<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Tickets.aspx.cs" Inherits="TTClient.Main" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        #TextArea1 {
            height: 149px;
            width: 315px;
        }
        #ticket_input {
            height: 150px;
            width: 300px;
        }
        #title_input {
            width: 300px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            Ticket Title:<br />
            <input id="title_input" type="text" /><br />
            <br />
            Ticket Body:<br />
            <textarea id="ticket_input" name="S1"></textarea><br />
            <br />
            <asp:Button ID="send_button" runat="server" Text="Send" />
            <br />
            <br />
            <br />
            My Tickets:<table id="tickets_table" style="width:100%;">
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </div>
    </form>
</body>
</html>
