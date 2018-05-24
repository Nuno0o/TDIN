using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace TTClient
{
    public partial class Tickets : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["token"] == null)
                Response.Redirect("Login.aspx", true);

            TableRow row = new TableRow();
            TableCell id = new TableCell();
            TableCell date = new TableCell();
            TableCell title = new TableCell();
            id.Text = "Id";
            date.Text = "Date";
            title.Text = "Title";
            row.Cells.Add(id);
            row.Cells.Add(date);
            row.Cells.Add(title);
            tickets_table.Rows.Add(row);

            List<dynamic> tickets = Operations.GetTickets();
            if (tickets == null)
            {
                status_display.Text = "Error getting tickets!";
                return;
            }
            foreach (dynamic ticket in tickets)
            {
                if (ticket.title == "") continue;

                row = new TableRow();
                id = new TableCell();
                date = new TableCell();
                title = new TableCell();
                HyperLink hl = new HyperLink();                
                hl.Text = Convert.ToString((int)ticket.id);                
                hl.NavigateUrl = "Ticket.aspx?id=" + hl.Text;
                
                id.Controls.Add(hl);
                date.Text = ticket.date;
                title.Text = ticket.title;
                row.Cells.Add(id);
                row.Cells.Add(date);
                row.Cells.Add(title);
                tickets_table.Rows.Add(row);
            }

        }

        protected void send_button_Click(object sender, EventArgs e)
        {
            string title = title_input.Text;
            string description = description_input.Text;

            if (Operations.SendTicket(title, description))
            {
                Response.Redirect("Tickets.aspx", true);
            }
            else
            {
                status_display.Text = "Error sending the ticket!";
            }
        }

        protected void logout_button_Click(object sender, EventArgs e)
        {
            if (Operations.Logout())
            {
                Session["token"] = null;
                Response.Redirect("Login.aspx", true);
            }
        }
    }
}