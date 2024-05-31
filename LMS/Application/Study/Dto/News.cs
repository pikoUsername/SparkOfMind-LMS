using LMS.Application.Files.Dto;

namespace LMS.Application.Study.Dto
{
    public class CreateNewsDto : InputInstitution
    {
        public string Title = null!; 
        public string Text = null!;
        public List<Guid> AllowedToSee = [];
        public Guid InstitutionId;
        public List<CreateFileDto> Attachments = []; 
    }

    public class UpdateNewsDto : InputInstitution
    {
        public Guid NewsId; 
        public string? Title;
        public string? Text;

        public List<CreateFileDto> Attachments = [];
        public List<Guid> AllowedToSee = [];
    }

    public class DeleteNewsDto : InputInstitution
    {
        public Guid NewsId; 
    }
}
