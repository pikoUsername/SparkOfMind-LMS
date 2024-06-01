using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Payment.Dto;
using LMS.Domain.Payment.Entities;

namespace LMS.Application.Payment.UseCases
{
    public class ConfirmPurchase : BaseUseCase<ConfirmPurchaseDto, PurchaseEntity>
    {
        private IApplicationDbContext _context;
        private IUser _user;
        private ILogger _logger;

        public ConfirmPurchase(IApplicationDbContext dbContext, IUser user, ILogger<ConfirmPurchase> logger)
        {
            _context = dbContext;
            _user = user;
            _logger = logger;
        }

        public async Task<PurchaseEntity> Execute(ConfirmPurchaseDto dto)
        {
            return new(); 
        }
    }
}
