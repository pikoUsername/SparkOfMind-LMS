using LMS.Domain.Market.Entities;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Domain.Market.Events
{
    public class ReviewCreated(ReviewEntity review) : BaseEvent
    {
        public ReviewEntity Review { get; set; } = review;
    }
}
