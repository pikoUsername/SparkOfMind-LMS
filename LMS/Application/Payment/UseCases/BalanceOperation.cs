using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Payment.Dto;
using LMS.Domain.User.Enums;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Payment.UseCases
{
    public class BalanceOperation : BaseUseCase<BalanceOperationDto, bool>
    {
        private IApplicationDbContext _context;
        private IAccessPolicy _accessPolicy;

        public BalanceOperation(IApplicationDbContext dbContext, IAccessPolicy accessPolicy)
        {
            _context = dbContext;
            _accessPolicy = accessPolicy;
        }

        public async Task<bool> Execute(BalanceOperationDto dto)
        {
            await _accessPolicy.EnforceRole(UserRoles.Admin);

            var wallet = await _context.Wallets.FirstOrDefaultAsync(x => x.Id == dto.WalletId);

            Guard.Against.Null(wallet, message: "Wallet does not exists");

            wallet.AddBalance(dto.Balance);

            return true;
        }
    }
}
