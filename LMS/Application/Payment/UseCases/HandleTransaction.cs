using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Payment.Dto;
using LMS.Application.Payment.Exceptions;
using LMS.Domain.Payment.Entities;
using LMS.Domain.Payment.Enums;
using LMS.Infrastructure.Adapters.Payment;
using LMS.Infrastructure.Data.Queries;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace LMS.Application.Payment.UseCases
{
    public class HandleTransaction : BaseUseCase<HandleTransactionDto, bool>
    {
        private IApplicationDbContext _context;

        public HandleTransaction(IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<bool> Execute(HandleTransactionDto dto)
        {
            var transaction = await _context.Transactions.FirstOrDefaultAsync(x => x.Id == dto.TransactionId);

            Guard.Against.Null(transaction, message: "Transaction does not exists");

            var ok = Enum.TryParse(transaction.Provider.Name, out PaymentProviders provider);
            if (!ok)
                throw new ValidationException($"Transaction: {transaction.Id} does not have provider name");

            if (transaction.Status == TransactionStatusEnum.Pending && provider != PaymentProviders.Balance)
            {
                throw new PendingGatewayResult(transaction.Id);
            }

            var userWallet = await _context.Wallets
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.User.Id == transaction.CreatedByUser.Id);

            Guard.Against.Null(userWallet, message: "User wallet");

            var purchase = await _context.Purchases.FirstOrDefaultAsync(x => x.Transaction.Id == transaction.Id);

            Guard.Against.Null(purchase, message: "Purchase is not bound to transaction");

            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == purchase.CourseId);

            Guard.Against.Null(course, message: "Product does not exists");

            var productOwnerWallet = await _context.Wallets
                .FirstOrDefaultAsync(x => x.User.Id == course.CreatedById);

            Guard.Against.Null(productOwnerWallet, message: "Product owner wallet does not exists");

            if (productOwnerWallet.UserId == userWallet.UserId)
            {
                throw new Exception("Product owner and buyer are same user");
            }

            userWallet.ConfirmTransaction(transaction, productOwnerWallet);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
