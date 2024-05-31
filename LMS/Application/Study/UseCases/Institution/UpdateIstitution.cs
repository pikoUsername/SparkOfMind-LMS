using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;

namespace LMS.Application.Study.UseCases.Institution
{
    public class UpdateInstitution : BaseUseCase<UpdateInstitutionDto, bool>
    {
        public UpdateInstitution() { }

        public async Task<bool> Execute(UpdateInstitutionDto dto)
        {
            return true;
        }
    }
}
