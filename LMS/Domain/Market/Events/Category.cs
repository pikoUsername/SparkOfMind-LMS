using LMS.Domain.Market.Entities;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Domain.Market.Events
{
    public class CategoryCreated(CategoryEntity category) : BaseEvent
    {
        public CategoryEntity Category { get; set; } = category;
    }

    public class CategoryDeleted(CategoryEntity category) : BaseEvent
    {
        public CategoryEntity Category { get; set; } = category;
    }

    public class CategoryUpdated(CategoryEntity category, CategoryEntity oldCategory) : BaseEvent
    {
        public CategoryEntity Category { get; set; } = category;
        public CategoryEntity OldCategory { get; set; } = oldCategory;
    }
}
