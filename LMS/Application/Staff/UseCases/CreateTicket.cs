using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Files.Interfaces;
using LMS.Application.Staff.Dto;
using LMS.Domain.Files.Entities;
using LMS.Domain.Staff.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Staff.UseCases
{
    public class CreateTicket : BaseUseCase<CreateTicketDto, TicketEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy;
        private readonly IFileService _fileService;

        public CreateTicket(IApplicationDbContext dbContext, IAccessPolicy accessPolicy, IFileService fileService)
        {
            _context = dbContext;
            _fileService = fileService;
            _accessPolicy = accessPolicy;
        }

        public async Task<TicketEntity> Execute(CreateTicketDto dto)
        {
            var byUser = await _accessPolicy.GetCurrentUser();

            var newFiles = await _fileService.UploadFiles().Execute(dto.Files);
            var subject = await _context.TicketSubjects.FirstOrDefaultAsync(x => x.Id == dto.SubjectId);

            Guard.Against.Null(subject, message: "Subject does not exists"); 

            var ticket = TicketEntity.Create(dto.Text, subject, byUser, (List<FileEntity>)newFiles);

            await _context.Tickets.AddAsync(ticket);
            await _context.SaveChangesAsync();

            return ticket;
        }
    }
}
