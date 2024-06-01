using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Institution
{
    public class DeleteNews : BaseUseCase<DeleteNewsDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }


        public DeleteNews(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy)
        {
            _institutionPolicy = institutionPolicy;
            _context = dbContext;
        }

        public async Task<bool> Execute(DeleteNewsDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            var student = await _context.Students.FirstOrDefaultAsync(x => x.InstitutionMember.Id == member.Id); 
            
            if (student != null)
            {
                throw new AccessDenied("You are student"); 
            }

            var news = await _context.InstitutionNews.FirstOrDefaultAsync(x => x.Id == dto.NewsId);

            Guard.Against.NotFound(dto.NewsId, news); 
            _context.InstitutionNews.Remove(news);

            await _context.SaveChangesAsync(); 

            return true;
        }
    }
}
