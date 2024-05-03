using LMS.Domain.Market.Entities;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Domain.Market.Events
{
    public class ProductCreated(ProductEntity product) : BaseEvent
    {
        public ProductEntity Product { get; set; } = product;
    }

    public class ProductViewed(ProductEntity product) : BaseEvent
    {
        public ProductEntity Product { get; set; } = product;

    }

    public class ProductSold(ProductEntity product, DateTime completedAt) : BaseEvent
    {
        public ProductEntity Product { get; set; } = product;
        public DateTime CompletedAt { get; set; } = completedAt;
    }
}
