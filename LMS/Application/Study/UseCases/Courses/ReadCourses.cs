using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Domain.Study.Entities;
using LMS.Infrastructure.Data.Extensions;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Courses
{
    public class GetCourse : BaseUseCase<GetCourseDto, CourseEntity?>
    {
        private IApplicationDbContext _context { get; }
        private IAccessPolicy _accessPolicy { get; }


        public GetCourse(
            IApplicationDbContext dbContext,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
        }

        public async Task<CourseEntity?> Execute(GetCourseDto dto)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == dto.CourseId);

            if (course == null)
                return null; 

            if (course.Closed 
                && !await _accessPolicy.IsAllowed(
                        Domain.User.Enums.PermissionEnum.read, course))
                return null; 

            return course;
        }
    }

    public class GetCoursesList : BaseUseCase<GetCoursesListDto, ICollection<CourseEntity>>
    {
        private IApplicationDbContext _context { get; }
        private IAccessPolicy _accessPolicy { get; }


        public GetCoursesList(
            IApplicationDbContext dbContext,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
        }

        public async Task<ICollection<CourseEntity>> Execute(GetCoursesListDto dto)
        {
            var query = _context.Courses.Include(x => x.Category).AsQueryable();

            if (dto.InstitutionId != null)
                query = query.Where(x => x.Institution.Id == dto.InstitutionId);
            if (dto.EndingPrice != null)
                query = query.Where(x => x.CurrentPrice.Amount < dto.EndingPrice); 
            if (dto.StartingPrice != null)
                query = query.Where(x => x.CurrentPrice.Amount > dto.StartingPrice);
            if (!await _accessPolicy.IsAllowed(Domain.User.Enums.PermissionEnum.read, typeof(CourseEntity)))
                query = query.Where(x => x.Closed == false); 

            query = query.Paginate(dto.Start, dto.Ends); 

            return await query.ToListAsync();
        }
    }
}
