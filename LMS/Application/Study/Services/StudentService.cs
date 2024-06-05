using LMS.Application.Study.Interfaces;
using LMS.Application.Study.UseCases.Student;

namespace LMS.Application.Study.Services
{
    public class StudentService : IStudentService
    {
        private readonly IServiceProvider _serviceProvider;

        public StudentService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public CreateStudent Create()
        {
            return _serviceProvider.GetRequiredService<CreateStudent>();
        }

        public GetStudent Get()
        {
            return _serviceProvider.GetRequiredService<GetStudent>();
        }

        public GetStudentsList GetList()
        {
            return _serviceProvider.GetRequiredService<GetStudentsList>();
        }

        public UpdateStudent Update()
        {
            return _serviceProvider.GetRequiredService<UpdateStudent>();
        }
    }

}
