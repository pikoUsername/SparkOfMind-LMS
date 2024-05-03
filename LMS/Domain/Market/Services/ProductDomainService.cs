using LMS.Application.Common.Interfaces;
using LMS.Domain.Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Domain.Market.Services
{
    public class ProductDomainService
    {
        private readonly IApplicationDbContext _dbContext;

        public ProductDomainService(IApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public int CountViews(ProductEntity product, DateTime startDate, DateTime endDate)
        {
            return _dbContext.ProductViews
                             .Count(visit => visit.ProductId == product.Id &&
                                             visit.CreatedAt >= startDate &&
                                             visit.CreatedAt <= endDate);
        }

        public int CountViews(ProductEntity product)
        {
            return _dbContext.ProductViews
                             .Count(visit => visit.ProductId == product.Id);
        }
    }
}
