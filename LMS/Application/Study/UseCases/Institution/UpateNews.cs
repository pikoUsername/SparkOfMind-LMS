using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Domain.Study.Entities;

namespace LMS.Application.Study.UseCases.Institution
{
    public class UpateNews : BaseUseCase<UpdateNewsDto, bool>
    {
        public UpateNews() { }

        public async Task<bool> Execute(UpdateNewsDto dto)
        {
            return;
        }
    }
}
