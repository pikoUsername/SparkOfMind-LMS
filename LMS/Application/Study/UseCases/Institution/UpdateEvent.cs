using KarmaMarketplace.Application.Common.Interactors;

namespace LMS.Application.Study.UseCases.Institution
{
    public class UpdateEvent : BaseUseCase<InputDTO, OutputDTO>
    {
        public UpdateEvent() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
