using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Books
{
    public class PassBook : BaseUseCase<PassBookDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IAccessPolicy _accessPolicy { get; }

        public PassBook(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IAccessPolicy accessPolicy)
        {
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
            _accessPolicy = accessPolicy;
        }

        public async Task<bool> Execute(PassBookDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            var book = await _context.Books.FirstOrDefaultAsync(x => x.Id == dto.BookId); 
            var rentBook = await _context.BookRents.FirstOrDefaultAsync(x => x.BookId == dto.BookId);

            Guard.Against.NotFound(dto.BookId, book);
            Guard.Against.NotFound(dto.BookId, rentBook);
            await _institutionPolicy.EnforcePermission(
                Domain.User.Enums.PermissionEnum.write, typeof(BookRentEntity), member, rentBook.Id);

            rentBook.PassBook(book, dto.StudentId);

            await _context.SaveChangesAsync(); 

            return true; 
        }
    }
}
