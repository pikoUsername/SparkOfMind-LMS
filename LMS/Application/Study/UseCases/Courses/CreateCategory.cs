using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Newtonsoft.Json;

namespace LMS.Application.Study.UseCases.Courses
{
    public class CreateCategory : BaseUseCase<CreateCategoryDto, CategoryEntity>
    {
        private IApplicationDbContext _context { get; }
        private IAccessPolicy _accessPolicy { get; }


        public CreateCategory(
            IApplicationDbContext dbContext,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
        }

        public async Task<CategoryEntity> Execute(CreateCategoryDto dto)
        {
            await _accessPolicy.EnforceIsAllowed(Domain.User.Enums.PermissionEnum.write, typeof(CategoryEntity));

            List<AttributeEntity> attributes = []; 

            foreach (var attr in dto.Attributes)
            {
                attributes.Add(AttributeEntity.Create(attr.Name, attr.Value, attr.Label)); 
            }

            var tags = JsonConvert.SerializeObject(dto.Tags); 

            var category = CategoryEntity.Create(dto.Name, tags, dto.Description, attributes);

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return category;
        }
    }
}
