using LMS.Domain.Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Data.Queries
{
    public static class ReviewQueries
    {
        public static IQueryable<ReviewEntity> IncludeStandard(this IQueryable<ReviewEntity> query)
        {
            return query
                .Include(x => x.Purchase)
                .Include(x => x.Product)
                .Include(x => x.CreatedBy)
                    .ThenInclude(x => x.Image);
        }
    }
}
