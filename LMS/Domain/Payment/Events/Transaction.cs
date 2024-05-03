using LMS.Domain.Payment.Entities;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Domain.Payment.Events
{
    public class TransactionCreated(TransactionEntity transaction) : BaseEvent
    {
        public TransactionEntity Transaction { get; set; } = transaction;
    }

    public class TransactionConfirmed(TransactionEntity transaction) : BaseEvent
    {
        public TransactionEntity Transaction { get; set; } = transaction;
    }
}
