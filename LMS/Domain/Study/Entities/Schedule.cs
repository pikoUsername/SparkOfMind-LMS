using LMS.Domain.Study.Events;
using System.ComponentModel.DataAnnotations.Schema;

namespace LMS.Domain.Study.Entities
{
    public class ScheduleEntity : BaseAuditableEntity
    {
        public string Name { get; set; } = null!; 
        public int RepeatAfter { get; set; }
        public ICollection<DayScheduleEntity> DaySchedules { get; set; } = []; 
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        [ForeignKey(nameof(CourseGroupEntity))]
        public Guid GroupId { get; set; }
        public bool IsRecurring { get; set; } = true; 

        public static ScheduleEntity Create(
            string name, 
            int repeatAfter, 
            ICollection<DayScheduleEntity> days, 
            bool isRecurring, 
            DateTime startDate, 
            DateTime? endDate)
        {
            ScheduleEntity entity = new ScheduleEntity()
            {
                Name = name, 
                RepeatAfter = repeatAfter,
                IsRecurring = isRecurring, 
                StartDate = startDate,
                EndDate = endDate,
                DaySchedules = days,
            };

            entity.AddDomainEvent(new ScheduleCreated(entity)); 

            return entity; 
        }
    }
}
