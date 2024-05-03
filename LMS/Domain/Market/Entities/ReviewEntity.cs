using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using LMS.Domain.Common;
using LMS.Domain.Payment.Entities;
using LMS.Domain.Payment.Enums;
using LMS.Domain.User.Entities;
using LMS.Domain.Payment.Exceptions;
using LMS.Domain.Market.Events;

namespace LMS.Domain.Market.Entities
{
    public class ReviewEntity : BaseAuditableEntity
    {
        [Required]
        public PurchaseEntity Purchase { get; set; } = null!;

        [Range(1, 5, ErrorMessage = "Рейтинг может быть только 1 и до 5"), Required]
        public int Rating { get; set; }

        [MaxLength(200)]
        public string Text { get; set; } = string.Empty;
        [Required]
        public UserEntity CreatedBy { get; set; } = null!;
        [Required]
        public ProductEntity Product { get; set; } = null!;

        public static ReviewEntity Create(
            string text,
            int rating,
            UserEntity createdBy,
            PurchaseEntity purchase,
            ProductEntity product)
        {
            if (purchase.Status == PurchaseStatus.Success)
            {
                throw new PurchaseIsAlreadyCompleted(purchase.Id);
            }

            var review = new ReviewEntity()
            {
                CreatedBy = createdBy,
                Text = text,
                Rating = rating,
                Product = product,
                Purchase = purchase
            };

            review.AddDomainEvent(new ReviewCreated(review));

            return review;
        }
    }
}
