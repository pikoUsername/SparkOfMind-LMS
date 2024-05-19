using KarmaMarketplace.Application.Common.Interactors;

namespace LMS.Application.Study.UseCases.Assigment
{
    public class CreateAssigment : BaseUseCase<InputDTO, OutputDTO>
    {
        public CreateAssigment() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
