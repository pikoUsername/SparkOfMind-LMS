using LMS.Application.Common.UseCases;

namespace LMS.Application.Study.UseCases.Assigment
{
    public class CreateAssigment : BaseUseCase<CreateAssignmentDto, OutputDTO>
    {
        public CreateAssigment() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
