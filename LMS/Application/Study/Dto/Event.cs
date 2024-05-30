namespace LMS.Application.Study.Dto
{
    public class CreateEventDto : InputInstitution
    {
        public string Title = null!; 
        public string Text = null!;
        public DateTime StartsAt; 
        public DateTime EndsAt; 
    }

    public class UpdateEventDto : InputInstitution
    {
        public Guid EventId; 
        public string? Title;
        public string? Text;
        public DateTime? StartsAt;
        public DateTime? EndsAt; 
    }
}
