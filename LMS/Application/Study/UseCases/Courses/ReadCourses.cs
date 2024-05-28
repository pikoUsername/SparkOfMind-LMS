using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Domain.Study.Entities;

namespace LMS.Application.Study.UseCases.Courses
{
    public class GetCourse : BaseUseCase<GetCourseDto, CourseEntity>
    {
        public GetCourse() { }

        public async Task<CourseEntity> Execute(GetCourseDto dto)
        {
            return;
        }
    }

    public class GetCoursesList : BaseUseCase<GetCoursesListDto, ICollection<CourseEntity>>
    {
        public GetCoursesList() { }

        public async Task<ICollection<CourseEntity>> Execute(GetCoursesListDto dto)
        {
            return;
        }
    }
}
