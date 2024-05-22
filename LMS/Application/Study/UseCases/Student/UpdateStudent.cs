
using LMS.Application.Common.UseCases;

namespace LMS.Application.Study.UseCases.Student
{
    public class UpdateStudent : BaseUseCase<InputDTO, OutputDTO>
    {
        public UpdateStudent() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
