using LMS.Application.Common.UseCases;

namespace LMS.Application.Study.UseCases.Assigment
{
    public class CreateSubmission : BaseUseCase<InputDTO, OutputDTO>
    {
        public CreateSubmission() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
