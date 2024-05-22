using KarmaMarketplace.Application.Common.Interactors;

namespace LMS.Application.Study.UseCases.Institution
{
    public class CreateIstitution : BaseUseCase<InputDTO, OutputDTO>
    {
        public CreateIstitution() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
