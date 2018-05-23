using System.ServiceModel;

namespace TTService
{
    [ServiceContract]
    public interface ITTServ
    {
        [OperationContract]
        string HelloWorld(string name);

        [OperationContract]
        string InitDb(bool overwrite);

        [OperationContract]
        string SendEmail(int ticketid);

        #region Ticket

        /// <summary>
        /// Adds a new ticket to the database.
        /// </summary>
        /// <param name="title">Title of the ticket.</param>
        /// <param name="description">Description of the ticket.</param>
        /// <param name="token">Token of the user who posted this ticket.</param>
        /// <param name="parent">Id of the parent (ticket) of this ticket - can be null.</param>
        /// <returns>Success or error message.</returns>        
        [OperationContract]
        string AddTicket(string title, string description, string token, int? parent);

        [OperationContract]
        string AddTicketDepartment(string description, int author, int department);

        /// <summary>
        /// Assigns a ticket to a user.
        /// </summary>
        /// <param name="id">Id of the ticket to assign.</param>
        /// <param name="assignee">Id of the user to assign.</param>
        /// <returns>Success or error message.</returns>
        [OperationContract]
        string AssignTicket(int id, int assignee);
        
        /// <summary>
        /// Answers a ticket.
        /// </summary>
        /// <param name="id">Ticket to answer.</param>
        /// <param name="answer">Answer to add.</param>
        /// <returns>Success or error message.</returns>
        [OperationContract]
        string AnswerTicket(int id, string answer);

        [OperationContract]
        string AnswerTicketDepartment(int id, string answer);

        /// <summary>
        /// Gets a ticket's information.
        /// </summary>
        /// <param name="id">Id of ticket to retrieve information of.</param>
        /// <returns>Ticket information.</returns>
        [OperationContract]
        string GetTicket(int id);

        [OperationContract]
        string GetTicketDepartment(int id);

        /// <summary>
        /// Gets the ids of all the children of a ticket.
        /// </summary>
        /// <param name="id">Id of the parent ticket.</param>
        /// <returns>List of all the ids of the ticket's children.</returns>
        [OperationContract]
        string GetTicketChildren(int id);

        /// <summary>
        /// Gets the ids of all the tickets a user posted.
        /// </summary>
        /// <param name="token">Token of the user.</param>
        /// <param name="status">Filter tickets by status - null to get all tickets.</param>
        /// <returns>List of all the ids of the tickets posted by the user.</returns>
        [OperationContract]
        string GetAuthorTickets(string token, string status);

        [OperationContract]
        string GetAuthorTicketsDepartment(int id);

        /// <summary>
        /// Gets the ids of all the tickets a user is assigned to.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <param name="status">Filter tickets by status - null to get all tickets.</param>
        /// <returns>List of all the ids of the tickets assigned to the user.</returns>
        [OperationContract]
        string GetSolverTickets(int id, string status);

        /// <summary>
        /// Gets the ids of all the tickets that are unassigned.
        /// </summary>
        /// <returns>List of all ids of all the  unassigned tickets.</returns>
        [OperationContract]
        string GetUnassignedTickets();

        [OperationContract]
        string GetUserByEmail(string email);

        [OperationContract]
        string GetUserById(int id);

        [OperationContract]
        string GetDepartments();

        #endregion

        #region Department

        [OperationContract]
        string AddDepartment(string name);

        #endregion
    }

    public interface ITTServ2
    {

    }
}
