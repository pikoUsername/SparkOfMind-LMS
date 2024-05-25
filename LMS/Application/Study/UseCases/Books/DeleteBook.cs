using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using LMS.Domain.User.Enums;
using LMS.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Books
{
    public class DeleteBook : BaseUseCase<DeleteBookDto, bool>
    {
        private IApplicationDbContext _context;
        private IInstitutionAccessPolicy _institutionPolicy;

        public DeleteBook(IApplicationDbContext dbContext, IInstitutionAccessPolicy institutionPolicy)
        {
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<bool> Execute(DeleteBookDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId); 
            await _institutionPolicy.EnforcePermission(PermissionEnum.delete, typeof(BookEntity), member, dto.BookId);

            var book = await _context.Books
                .FirstOrDefaultAsync(x => x.Id == dto.BookId);

            Guard.Against.Null(book, message: "Book does not exists");

            _context.Books.Remove(book); 
            await _context.SaveChangesAsync(); 

            return true;
        }
    }
}
