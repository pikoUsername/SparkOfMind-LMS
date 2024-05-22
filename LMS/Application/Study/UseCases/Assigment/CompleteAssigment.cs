using KarmaMarketplace.Application.Common.Interactors;

namespace LMS.Application.Study.UseCases.Assigment
{
    public class CompleteAssigment : BaseUseCase<InputDTO, OutputDTO>
    {
        public CompleteAssigment() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
