using System;
using System.Collections.Generic;
using System.Web.UI.WebControls;

namespace TTClient
{
    public partial class Ticket : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
            if(Request.QueryString["id"] == null)
                Response.Redirect("Tickets.aspx", true);

            if (Session["token"] == null)
                Response.Redirect("Login.aspx", true);

            int ticket_id = Convert.ToInt32(Request.QueryString["id"]);
            dynamic ticket = Operations.GetTicket(ticket_id);

            if(ticket == null)
                Response.Redirect("Tickets.aspx", true);

            id_text.Text = ticket.id;
            date_text.Text = ticket.date;
            title_text.Text = ticket.title;
            description_text.Text = ticket.description;
            answer_text.Text = ticket.answer;

            TableRow row = new TableRow();
            TableCell question = new TableCell();
            TableCell answer = new TableCell();
            question.Text = "Question";
            answer.Text = "Answer";
            row.Cells.Add(question);
            row.Cells.Add(answer);
            questions_table.Rows.Add(row);

            List<dynamic> questions = Operations.GetQuestions(ticket_id);
            if (questions == null)
            {
                status_display.Text = "Error getting questions!";
                return;
            }
            foreach (dynamic q in questions)
            {
                row = new TableRow();
                question = new TableCell();
                answer = new TableCell();
                question.Text = q.question;
                answer.Text = q.answer;
                row.Cells.Add(question);
                row.Cells.Add(answer);
                questions_table.Rows.Add(row);
            }

        }

        protected void send_button_Click(object sender, EventArgs e)
        {
            string question = question_input.Text;

            if (Operations.SendQuestion(question))
            {
                Response.Redirect(Request.Url.PathAndQuery, true);
            }
            else
            {
                status_display.Text = "Error sending the ticket!";
            }
        }

        protected void back_button_Click(object sender, EventArgs e)
        {
            Response.Redirect("Tickets.aspx", true);
        }
    }
}