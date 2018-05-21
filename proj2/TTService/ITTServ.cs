using System.ServiceModel;

namespace TTService
{
    [ServiceContract]
    public interface ITTServ
    {
        [OperationContract]
        string HelloWorld(string name);

        #region Ticket

        /// <summary>
        /// Adds a new ticket to the database.
        /// </summary>
        /// <param name="title">Title of the ticket.</param>
        /// <param name="description">Description of the ticket.</param>
        /// <param name="author">Id of the user who posted this ticket.</param>
        /// <param name="parent">Id of the parent (ticket) of this ticket - can be null.</param>
        /// <returns>Greater than 0 if success, less than or equal to 0 otherwise.</returns>
        [OperationContract]
        dynamic AddTicket(string title, string description, int author, int? parent);
        
        /// <summary>
        /// Assigns a ticket to a user.
        /// </summary>
        /// <param name="id">Id of the ticket to assign.</param>
        /// <param name="assignee">Id of the user to assign.</param>
        /// <returns>Greater than 0 if success, less than or equal to 0 otherwise.</returns>
        [OperationContract]
        dynamic AssignTicket(int id, int assignee);
        
        /// <summary>
        /// Answers a ticket.
        /// </summary>
        /// <param name="id">Ticket to answer.</param>
        /// <param name="answer">Answer to add.</param>
        /// <returns>Greater than 0 if success, less than or equal to 0 otherwise.</returns>
        [OperationContract]
        dynamic AnswerTicket(int id, string answer);
        
        /// <summary>
        /// Gets a ticket's information.
        /// </summary>
        /// <param name="id">Id of ticket to retrieve information of.</param>
        /// <returns>Ticket information.</returns>
        [OperationContract]
        dynamic GetTicket(int id);

        /// <summary>
        /// Gets the ids of all the children of a ticket.
        /// </summary>
        /// <param name="id">Id of the parent ticket.</param>
        /// <returns>List of all the ids of the ticket's children.</returns>
        [OperationContract]
        dynamic GetTicketChildren(int id);

        /// <summary>
        /// Gets the ids of all the tickets a user posted.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <param name="status">Filter tickets by status - null to get all tickets.</param>
        /// <returns>List of all the ids of the tickets posted by the user.</returns>
        [OperationContract]
        dynamic GetAuthorTickets(int id, string status);

        /// <summary>
        /// Gets the ids of all the tickets a user is assigned to.
        /// </summary>
        /// <param name="id">User id.</param>
        /// <param name="status">Filter tickets by status - null to get all tickets.</param>
        /// <returns>List of all the ids of the tickets assigned to the user.</returns>
        [OperationContract]
        dynamic GetSolverTickets(int id, string status);

        #endregion

        #region Department

        [OperationContract]
        dynamic AddDepartment(string name);

        #endregion
    }

    public interface ITTServ2
    {

    }
}
