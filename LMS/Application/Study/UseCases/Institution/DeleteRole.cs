using KarmaMarketplace.Application.Common.Interactors;

namespace LMS.Application.Study.UseCases.Institution
{
    public class DeleteRole : BaseUseCase<InputDTO, OutputDTO>
    {
        public DeleteRole() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
