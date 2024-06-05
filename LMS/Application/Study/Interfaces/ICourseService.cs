using LMS.Application.Study.UseCases.Courses;

namespace LMS.Application.Study.Interfaces
{
    public interface ICourseService
    {
        GetCourse Get();
        CloseCourse Close();
        GetCoursesList GetList();
        UpdateCourse Update(); 
    }
}
