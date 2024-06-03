using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Courses
{
    public class DeleteCategory : BaseUseCase<DeleteCategoryDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IAccessPolicy _accessPolicy { get; }


        public DeleteCategory(
            IApplicationDbContext dbContext,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
        }

        public async Task<bool> Execute(DeleteCategoryDto dto)
        {
            await _accessPolicy.EnforceIsAllowed(Domain.User.Enums.PermissionEnum.delete, typeof(CategoryEntity)); 

            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == dto.CategoryId);
            
            Guard.Against.NotFound(dto.CategoryId, category);

            var coursesCount = _context.Courses.Where(x => x.Category.Id == category.Id).Count();
            if (coursesCount > 1)
            {
                return false; 
            }

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync(); 

            return true;
        }
    }
}
