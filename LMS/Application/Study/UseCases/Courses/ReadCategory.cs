using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Domain.Study.Entities;
using LMS.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Courses
{
    public class GetCategory : BaseUseCase<GetCategoryDto, CategoryEntity?>
    {
        private IApplicationDbContext _context { get; }


        public GetCategory(
            IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<CategoryEntity?> Execute(GetCategoryDto dto)
        {
            var category = await _context.Categories
                .Include(x => x.Attributes)
                .FirstOrDefaultAsync(x => x.Id == dto.CategoryId); 

            return category;
        }
    }

    public class GetCategoriesList : BaseUseCase<GetCategoriesListDto, ICollection<CategoryEntity>>
    {
        private IApplicationDbContext _context { get; }


        public GetCategoriesList(
            IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<ICollection<CategoryEntity>> Execute(GetCategoriesListDto dto)
        {
            var query = _context.Categories
                .Include(x => x.Attributes)
                .AsQueryable(); 

            if (!string.IsNullOrEmpty(dto.Name))
            {
                query = query.Where(x => EF.Functions
                    .ToTsVector(x.Name)
                    .Matches(EF.Functions
                        .ToTsQuery(dto.Name))
                    );
            }

            query = query.Paginate(dto.Start, dto.Ends); 

            return await query.ToListAsync();
        }
    }
}
