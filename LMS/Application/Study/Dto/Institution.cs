using System.ComponentModel.DataAnnotations;

namespace LMS.Application.Study.Dto
{
    public class InputInstitution
    {
        // helper class to remove need to write InstitutionId every time 
        [Required]
        public Guid InstitutionId { get; set; }
    }
}
