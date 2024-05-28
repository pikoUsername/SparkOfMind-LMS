using System.ComponentModel.DataAnnotations;

namespace LMS.Application.Study.Dto
{
    public class CreateAttributeDto
    {
        public string Name = null!;
        public string Value = null!;
        public string Label = null!;
    }

    public class CreateCategoryDto 
    {
        [Required]
        public string Name = null!;
        [Required]
        public List<string> Tags = [];
        [Required]
        public string Description = null!;
        [Required]
        public List<CreateAttributeDto> Attributes = []; 
    }

    public class GetCategoryDto
    {
        public Guid CategoryId;
    }

    public class GetCategoriesListDto
    {
        public string? Name = null!;
    }

    public class UpdateCategoryDto
    {
        public Guid CategoryId;
        public string? Name = null!;
        public List<string>? Tags = [];
        public string? Description = null!;
        public List<CreateAttributeDto>? Attributes = [];
    }

    public class  DeleteCategoryDto { 
        public Guid CategoryId;
    }
}
