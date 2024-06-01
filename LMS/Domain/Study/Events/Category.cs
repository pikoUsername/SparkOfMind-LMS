using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Events
{
    public class CategoryCreated(CategoryEntity category) : DomainEvent
    {
        public CategoryEntity Category { get; set; } = category; 
    }
}
