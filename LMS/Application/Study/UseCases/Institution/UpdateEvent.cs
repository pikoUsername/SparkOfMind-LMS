using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;

namespace LMS.Application.Study.UseCases.Institution
{
    public class UpdateEvent : BaseUseCase<UpdateEventDto, bool>
    {
        public UpdateEvent() { }

        public async Task<bool> Execute(UpdateEventDto dto)
        {
            return true;
        }
    }
}
