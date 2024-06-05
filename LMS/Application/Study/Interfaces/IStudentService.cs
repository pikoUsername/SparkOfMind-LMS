using LMS.Application.Study.UseCases.Student;

namespace LMS.Application.Study.Interfaces
{
    public interface IStudentService
    {
        CreateStudent Create();
        GetStudent Get();
        GetStudentsList GetList();
        UpdateStudent Update(); 
    }
}
