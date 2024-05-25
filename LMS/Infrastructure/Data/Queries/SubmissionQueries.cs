using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Infrastructure.Data.Queries
{
    public static class SubmissionQueries 
    {
        public static IQueryable<SubmissionEntity> IncludeStandard(this IQueryable<SubmissionEntity> query)
        {
            return query
                .Include(x => x.Attachments); 
        }
    }
}
