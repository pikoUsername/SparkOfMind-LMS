using KarmaMarketplace.Application.Common.Interactors;

namespace LMS.Application.Study.UseCases.Institution
{
    public class InviteMember : BaseUseCase<InputDTO, OutputDTO>
    {
        public InviteMember() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
