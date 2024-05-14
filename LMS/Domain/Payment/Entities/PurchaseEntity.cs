using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LMS.Domain.Common;
using LMS.Domain.Payment.ValueObjects;
using LMS.Domain.Payment.Enums;
using LMS.Domain.Payment.Events;
using LMS.Domain.Payment.Exceptions;

namespace LMS.Domain.Payment.Entities
{
    public class PurchaseEntity : BaseAuditableEntity
    {
        // Assuming there's an enum for Currency that was not included in the initial schema
        [Required]
        public CurrencyEnum Currency { get; set; }

        [Required]
        public Money Amount { get; set; } = new(0);

        [Required]
        public WalletEntity Wallet { get; set; } = null!;

        [Required]
        public bool Completed { get; set; } = false;

        [Required]
        public PurchaseStatus Status { get; set; }
        //[ForeignKey(nameof(ReviewEntity))]
        //public Guid? ReviewId { get; set; }

        [Required]
        public TransactionEntity Transaction { get; set; } = null!;

        [MaxLength(256)]
        public string? StatusDescription { get; set; } = null!;

        public static PurchaseEntity Create(WalletEntity wallet, TransactionEntity transaction)
        {
            throw new NotImplementedException();

            var purchase = new PurchaseEntity
            {
                Wallet = wallet,
                //Product = product,
                Amount = transaction.Amount,
                Currency = transaction.Amount.Currency,
                Transaction = transaction,
                Status = PurchaseStatus.Processing
            };

            purchase.AddDomainEvent(new PurchaseCreated(purchase));

            return purchase;
        }

        public void Problem(string description)
        {
            Status = PurchaseStatus.Rejected;
            StatusDescription = description;
        }

        //public void Complete(ReviewEntity review)
        //{
        //    if (Status == PurchaseStatus.Success || Completed)
        //        throw new PurchaseIsAlreadyCompleted(Id);

        //    Completed = true;
        //    ReviewId = review.Id;
        //    Status = PurchaseStatus.Success;
        //}
    }
}
