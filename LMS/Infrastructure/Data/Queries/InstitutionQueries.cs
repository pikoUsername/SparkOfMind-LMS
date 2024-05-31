using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Data.Queries
{
    public static class InstitutionQueries 
    {
        public static IQueryable<InstitutionEntity> IncludeStandard(this IQueryable<InstitutionEntity> query)
        {
            return query
                .Include(x => x.Images); 
        }
    }
}
