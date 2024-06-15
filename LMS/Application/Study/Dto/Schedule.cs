namespace LMS.Application.Study.Dto
{
    public class CreateScheduleDto : InputInstitution
    {
        public List<CreateDayDto> Days { get; set; } = []; 
        public int RepeatAfter { get; set; }
        public Guid GroupId { get; set; }
        public bool IsRecurring { get; set; } = true; 
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Name { get; set; } = null!;
    }

    public class CreateDayDto {
        public DayOfWeek Day { get; set; }
        public string Name { get; set; } = null!; 
        public List<CreateDayOfLessonDto> Lessons { get; set; } = []; 
    }

    public class CreateDayOfLessonDto
    {
        public string Name { get; set; } = null!; 
        public Guid CourseId { get; set; }
        public Guid TeacherId { get; set; }
        public TimeOnly StartDate { get; set; }
        public TimeOnly EndDate { get; set; }
        public string Room { get; set; } = null!;
    }

    public class DeleteScheduleDto : InputInstitution
    {
        public Guid ScheduleId { get; set; }
        public Guid? GroupId { get; set; }
    }
}
