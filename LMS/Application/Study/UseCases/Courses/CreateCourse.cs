using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Courses
{
    public class CreateCourse : BaseUseCase<CreateCourseDto, CourseEntity>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IAccessPolicy _accessPolicy { get; }


        public CreateCourse(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<CourseEntity> Execute(CreateCourseDto dto)
        {
            if (!await _institutionPolicy.IsOwner(dto.InstitutionId))
            {
                throw new AccessDenied("You are not owner"); 
            }

            var institution = await _context.Institutions.FirstOrDefaultAsync(x => x.Id == dto.InstitutionId);
            var category = await _context.Categories.FirstOrDefaultAsync(x => x.Id == dto.CategoryId);

            Guard.Against.NotFound(dto.CategoryId, category); 
            Guard.Against.NotFound(dto.InstitutionId, institution);

            var user = await _accessPolicy.GetCurrentUser(); 
            var course = CourseEntity.Create(dto.Name, dto.Description, dto.Hours, institution, category, dto.BasePrice);

            _context.Courses.Add(course);
            user.Permissions.AddPermissionWithCode(course, Domain.User.Enums.PermissionEnum.all);

            await _context.SaveChangesAsync(); 

            return course;
        }
    }
}
