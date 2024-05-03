using LMS.Domain.Market.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Data.Queries
{
    public static class CategoryQueries
    {
        public static IQueryable<CategoryEntity> IncludeStandard(this IQueryable<CategoryEntity> query)
        {
            return query
                .Include(x => x.Options);
        }
    }
}
