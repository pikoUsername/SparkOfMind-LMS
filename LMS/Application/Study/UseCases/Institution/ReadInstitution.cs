using LMS.Application.Common.UseCases;

namespace LMS.Application.Study.UseCases.Institution
{
    public class ReadInstitution : BaseUseCase<InputDTO, OutputDTO>
    {
        public ReadInstitution() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
