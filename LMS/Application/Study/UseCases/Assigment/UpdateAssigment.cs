using LMS.Application.Common.UseCases;

namespace LMS.Application.Study.UseCases.Assigment
{
    public class UpdateAssigment : BaseUseCase<InputDTO, OutputDTO>
    {
        public UpdateAssigment() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
