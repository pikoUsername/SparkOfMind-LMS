using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Domain.Staff.Entities;
using LMS.Domain.User.Enums;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Staff.UseCases
{
    public class DeleteTicket : BaseUseCase<Guid, TicketEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy;

        public DeleteTicket(IApplicationDbContext dbContext, IAccessPolicy accessPolicy)
        {
            _context = dbContext;
            _accessPolicy = accessPolicy;
        }

        public async Task<TicketEntity> Execute(Guid ticketId)
        {
            Guard.Against.Null(ticketId, nameof(ticketId));

            // Perform access check for admin role
            await _accessPolicy.EnforceIsAllowed("delete");

            var ticket = await _context.Tickets
                .Include(x => x.Comments)
                .FirstOrDefaultAsync(x => x.Id == ticketId);

            Guard.Against.Null(ticket, $"Ticket with ID {ticketId} does not exist.");

            _context.Tickets.Remove(ticket);
            await _context.SaveChangesAsync();

            return ticket; // Return the deleted ticket
        }
    }
}
