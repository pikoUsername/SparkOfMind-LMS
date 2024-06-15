namespace LMS.Domain.Study.Exceptions
{
    public class ScheduleIsAlreadyAssignedToGroup : Exception
    {
        public ScheduleIsAlreadyAssignedToGroup(Guid scheduleId) 
            : base($"Reoccuring schedule is already set for this group: {scheduleId}") { 
        }
    }
}
