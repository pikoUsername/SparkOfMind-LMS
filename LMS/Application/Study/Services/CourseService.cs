using LMS.Application.Study.Interfaces;
using LMS.Application.Study.UseCases.Courses;

namespace LMS.Application.Study.Services
{
    public class CourseService : ICourseService
    {
        private readonly IServiceProvider _serviceProvider;

        public CourseService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public GetCourse Get()
        {
            return _serviceProvider.GetRequiredService<GetCourse>();
        }

        public CloseCourse Close()
        {
            return _serviceProvider.GetRequiredService<CloseCourse>();
        }

        public GetCoursesList GetList()
        {
            return _serviceProvider.GetRequiredService<GetCoursesList>();
        }

        public UpdateCourse Update()
        {
            return _serviceProvider.GetRequiredService<UpdateCourse>();
        }
    }

}
