using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Events
{
    public class ScheduleCreated(ScheduleEntity schedule) : DomainEvent
    {
        public ScheduleEntity Schedule { get; set; } = schedule;
    }
}
