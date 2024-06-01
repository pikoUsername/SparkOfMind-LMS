using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Payment.Dto;
using LMS.Domain.Payment.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Payment.UseCases
{
    public class GetTransactionProviders
        : BaseUseCase<GetTransactionProvidersDto, ICollection<TransactionProviderEntity>>
    {
        private IApplicationDbContext _context;

        public GetTransactionProviders(IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<ICollection<TransactionProviderEntity>> Execute(
            GetTransactionProvidersDto dto)
        {
            return await _context.TransactionProviders
                .Include(x => x.Systems)
                .Include(x => x.Logo)
                .ToListAsync();
        }
    }
}
