using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Courses
{
    public class CloseCourse : BaseUseCase<CloseCourseDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IAccessPolicy _accessPolicy { get; }


        public CloseCourse(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }
        public async Task<bool> Execute(CloseCourseDto dto)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == dto.CourseId);

            Guard.Against.NotFound(dto.CourseId, course);

            await _accessPolicy.EnforceIsAllowed(Domain.User.Enums.PermissionEnum.delete, course);

            course.Close();

            await _context.SaveChangesAsync(); 

            return true;
        }
    }
}
