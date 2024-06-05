using LMS.Application.Study.Interfaces;
using LMS.Application.Study.UseCases.Courses;

namespace LMS.Application.Study.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IServiceProvider _serviceProvider;

        public CategoryService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public CreateCategory Create()
        {
            return _serviceProvider.GetRequiredService<CreateCategory>();
        }

        public UpdateCategory Update()
        {
            return _serviceProvider.GetRequiredService<UpdateCategory>();
        }

        public DeleteCategory Delete()
        {
            return _serviceProvider.GetRequiredService<DeleteCategory>();
        }

        public GetCategory Get()
        {
            return _serviceProvider.GetRequiredService<GetCategory>();
        }

        public GetCategoriesList GetList()
        {
            return _serviceProvider.GetRequiredService<GetCategoriesList>();
        }
    }

}
