using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Payment.Dto;
using LMS.Domain.Payment.Entities;
using LMS.Domain.User.Enums;
using LMS.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Payment.UseCases
{
    public class GetWallet : BaseUseCase<GetWalletDto, WalletEntity>
    {
        private readonly IApplicationDbContext _context;
        private readonly IAccessPolicy _accessPolicy;

        public GetWallet(IApplicationDbContext dbContext, IAccessPolicy accessPolicy)
        {
            _context = dbContext;
            _accessPolicy = accessPolicy;
        }

        public async Task<WalletEntity> Execute(GetWalletDto dto)
        {
            var wallet = await _context.Wallets
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == dto.WalletId || x.UserId == dto.UserId);

            Guard.Against.Null(wallet, message: "Wallet does not exists");

            await _accessPolicy.EnforceRelationship(PermissionEnum.read, wallet, wallet.User.Id);

            return wallet;
        }
    }
}
