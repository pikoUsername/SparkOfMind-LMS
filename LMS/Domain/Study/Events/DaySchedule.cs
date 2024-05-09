using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Events
{
    public class DayScheduleCreated(DayScheduleEntity schedule) : DomainEvent
    {
        public DayScheduleEntity Schedule { get; set; } = schedule;
    }
}
