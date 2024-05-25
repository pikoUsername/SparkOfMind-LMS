using LMS.Application.Common.UseCases;
using LMS.Domain.Study.Entities;

namespace LMS.Application.Study.UseCases.Institution
{
    public class CreateInstitution : BaseUseCase<CreateInstitutionDto, InstitutionEntity>
    {
        public CreateInstitution() { }

        public async Task<InstitutionEntity> Execute(CreateInstitutionDto dto)
        {
            return;
        }
    }
}
