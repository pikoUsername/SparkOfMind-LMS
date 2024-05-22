using KarmaMarketplace.Application.Common.Interactors;

namespace LMS.Application.Study.UseCases.Institution
{
    public class UpdateIstitution : BaseUseCase<InputDTO, OutputDTO>
    {
        public UpdateIstitution() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
