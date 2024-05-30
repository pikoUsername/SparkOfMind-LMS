using LMS.Domain.Study.Enums;
using System.ComponentModel.DataAnnotations;

namespace LMS.Application.Study.Dto
{
    public class InputInstitution
    {
        // helper class to remove need to write InstitutionId every time 
        [Required]
        public Guid InstitutionId { get; set; }
    }

    public class CreateInstitutionDto
    {
        public string Name = null!;
        public string Phone = null!;
        public string Address = null!; 
        public string Email = null!;
        public InstitutionTypes Type;
        public InstitutionStatus Status;
        public string Description = null!;
        public Guid OwnerId; 
        public decimal Fee; 
    }

    public class UpdateInstitutionDto 
    {
        public Guid InstitutionId; 
        public string? Name;
        public string? Phone;
        public string? Address;
        public string? Email;
        public string? Description;
        public InstitutionStatus? Status;
        public decimal? Fee;
    }

    public class DeleteInstitutionDto
    {
        public Guid InstitutionId;
        [StringLength(250, MinimumLength = 50)]
        public string Reason = null!; 
    }

    public class BlockInstitutionDto
    {
        public Guid InstitutionId;
        [StringLength(250, MinimumLength = 50)]
        public string Reason = null!;
    }

    public class InviteMemberDto : InputInstitution
    {
        public Guid UserId;
        public bool IsTeacher = false;
    }

    public class AcceptMembershipDto 
    {
        public string Token; 
    } 
}
