namespace LMS.Application.Study.Dto
{
    public class CancelLessonDto : InputInstitution
    {
        public Guid LessonId { get; set; }
    }
}
