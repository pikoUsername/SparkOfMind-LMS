using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Staff.Dto;
using LMS.Domain.Staff.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Staff.UseCases
{
    public class GetCommentsList : BaseUseCase<GetCommentsDto, List<TicketCommentEntity>>
    {
        private readonly IApplicationDbContext _context;

        public GetCommentsList(IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<List<TicketCommentEntity>> Execute(GetCommentsDto dto)
        {
            List<TicketCommentEntity> comments = [];

            if (dto.UserId != null)
            {
                comments = await _context.TicketComments
                    .Where(x => x.CreatedBy.Id == dto.UserId)
                    .Include(x => x.Files)
                    .ToListAsync();
            }
            else if (dto.TicketId != null)
            {
                comments = await _context.TicketComments
                    .Where(x => x.TicketId == dto.TicketId)
                    .Include(x => x.Files)
                    .ToListAsync();
            }
            else
            {
                throw new Exception("Ticket id and userId are none");
            }

            return comments;
        }
    }
}
