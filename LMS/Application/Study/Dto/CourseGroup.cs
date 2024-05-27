using System.ComponentModel.DataAnnotations;

namespace LMS.Application.Study.Dto
{
    public class CreateGroupDto : InputInstitution
    {
        [Required]
        public string Name = null!;
        [Required]
        public Guid GradeTypeId; 
        public Guid? CourseId = null!; 
    }
    
    public class ExtendGroupDto : InputInstitution
    {
        [Required]
        public Guid StudentId { get; set; }
        [Required]
        public Guid GroupId { get; set; }
    }

    public class GetGroupDto : InputInstitution
    {
        public Guid GroupId; 
    }

    public class UpdateGroupDto : InputInstitution { }
    public class SendGroupNotificationDto : InputInstitution
    {
    }

    public class GetGroupsListDto : InputInstitution
    {
        public int Start = 0;
        public int Ends = 10; 
        public Guid? CourseId; 
    }
}
