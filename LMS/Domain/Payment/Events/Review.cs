using LMS.Domain.Payment.Entities;

namespace LMS.Domain.Payment.Events
{
    public class ReviewCreated(ReviewEntity review) : DomainEvent
    {
        public ReviewEntity Review { get; set; }
    }
}
