using LMS.Application.Study.UseCases.Courses;

namespace LMS.Application.Study.Interfaces
{
    public interface ICategoryService
    {
        CreateCategory Create(); 
        UpdateCategory Update();
        DeleteCategory Delete();
        GetCategory Get(); 
        GetCategoriesList GetList();
    }
}
