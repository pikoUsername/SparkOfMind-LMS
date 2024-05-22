using LMS.Domain.Payment.Entities;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Domain.Payment.Events
{
    public class PurchaseCreated(PurchaseEntity purchase) : BaseEvent
    {
        public PurchaseEntity Purchase { get; set; } = purchase;
    }

    public class PurchaseConfirmed(PurchaseEntity purchase) : DomainEvent {
        public PurchaseEntity Purchase { get; } = purchase;
    }
}
