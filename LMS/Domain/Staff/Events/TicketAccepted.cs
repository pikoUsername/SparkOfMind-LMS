using LMS.Domain.Staff.Entities;
using LMS.Domain.User.Entities;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Domain.Staff.Events
{
    public class TicketAccepted(UserEntity byUser, TicketEntity ticket) : BaseEvent
    {
        public UserEntity ByUser { get; } = byUser;
        public TicketEntity Ticket { get; } = ticket;
    }
}
