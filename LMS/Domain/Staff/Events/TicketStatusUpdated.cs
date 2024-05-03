using LMS.Domain.Staff.Entities;
using LMS.Domain.Staff.Enums;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Domain.Staff.Events
{
    public class TicketStatusUpdated(TicketEntity ticket, TicketStatus status) : BaseEvent
    {
        public TicketEntity Ticket { get; set; } = ticket;
        public TicketStatus Status { get; set; } = status;
    }
}
