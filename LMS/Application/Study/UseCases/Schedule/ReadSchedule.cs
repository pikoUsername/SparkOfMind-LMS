using LMS.Application.Common.UseCases;

namespace LMS.Application.Study.UseCases.Schedule
{
    public class GetSchedule : BaseUseCase<GetSchedule, ScheduleEntity>
    {
        public GetSchedule() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
