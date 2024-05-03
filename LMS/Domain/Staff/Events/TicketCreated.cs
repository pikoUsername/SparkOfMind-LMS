using LMS.Domain.Staff.Entities;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Domain.Staff.Events
{
    public class TicketCreated(TicketEntity ticket) : BaseEvent
    {
        public TicketEntity Ticket { get; set; } = ticket;
    }
}
