using LMS.Application.Common.UseCases;

namespace LMS.Application.Study.UseCases.Assigment
{
    public class UseCase : BaseUseCase<InputDTO, OutputDTO>
    {
        public UseCase() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
