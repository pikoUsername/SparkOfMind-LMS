using LMS.Application.Common.UseCases;

namespace LMS.Application.Study.UseCases.Institution
{
    public class UpdateEvent : BaseUseCase<UpdateEvent, bool>
    {
        public UpdateEvent() { }

        public async Task<bool> Execute(UpdateEvent dto)
        {
            return;
        }
    }
}
