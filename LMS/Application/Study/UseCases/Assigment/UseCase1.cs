using KarmaMarketplace.Application.Common.Interactors;

namespace LMS.Application.Study.UseCases.Assigment
{
    public class UseCase1 : BaseUseCase<InputDTO, OutputDTO>
    {
        public UseCase1() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
