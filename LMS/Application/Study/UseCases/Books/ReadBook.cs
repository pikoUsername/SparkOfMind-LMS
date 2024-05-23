using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using LMS.Infrastructure.Data.Extensions;
using LMS.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Books
{
    public class GetBook : BaseUseCase<GetBookDto, BookEntity>
    {
        private IApplicationDbContext _context;
        private IInstitutionAccessPolicy _institutionPolicy; 

        public GetBook(IApplicationDbContext dbContext, IInstitutionAccessPolicy institutionPolicy) {
            _context = dbContext;
            _institutionPolicy = institutionPolicy; 
        }

        public async Task<BookEntity> Execute(GetBookDto dto)
        {
            // deez nuts
            await _institutionPolicy.EnforceMembership(dto.InstitutionId); 

            var book = await _context.Books
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == dto.BookId || dto.Link == x.Link);

            Guard.Against.Null(book, message: "Book does not exits"); 

            return book;
        }
    }

    public class GetBooksList : BaseUseCase<GetBooksListDto, ICollection<BookEntity>> 
    {
        private IApplicationDbContext _context;
        private IInstitutionAccessPolicy _institutionPolicy;

        public GetBooksList(IApplicationDbContext dbContext, IInstitutionAccessPolicy institutionPolicy)
        {
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<ICollection<BookEntity>> Execute(GetBooksListDto dto)
        {
            // deez nuts 2 
            await _institutionPolicy.EnforceMembership(dto.InstitutionId);

            var query = _context.Books
                .IncludeStandard()
                .AsQueryable(); 

            if (dto.IsOnline != null)
            {
                query = query.Where(x => x.IsOnline == dto.IsOnline); 
            }
            if (dto.CourseId != null)
            {
                query = query.Where(x => x.CourseId == dto.CourseId); 
            }
            if (!string.IsNullOrEmpty(dto.Name))
            {
                query = query.Where(x => 
                    EF.Functions
                        .ToTsVector(x.Name)
                        .Matches(EF.Functions.ToTsQuery(dto.Name))
                    );
            }   

            query = query.Paginate(dto.Start, dto.Ends); 

            return await query.ToListAsync();        
        }
    }
}
