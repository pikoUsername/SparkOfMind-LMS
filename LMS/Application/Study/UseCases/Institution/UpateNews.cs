using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Files.Interfaces;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Institution
{
    public class UpateNews : BaseUseCase<UpdateNewsDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IFileService _fileService; 

        public UpateNews(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy, 
            IFileService fileService)
        {
            _fileService = fileService; 
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<bool> Execute(UpdateNewsDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            await _institutionPolicy.EnforcePermission(
                Domain.User.Enums.PermissionEnum.edit, typeof(InstitutionEventEntity), member, dto.NewsId);

            var news = await _context.InstitutionEvents.FirstOrDefaultAsync(x => x.Id == dto.NewsId);

            Guard.Against.NotFound(dto.NewsId, news);

            if (!string.IsNullOrEmpty(dto.Title))
            {
                news.Title = dto.Title;
            }
            if (!string.IsNullOrEmpty(dto.Text))
            {
                news.Text = dto.Text;
            }
            if (dto.Attachments != null)
            {
                var files = await _fileService.UploadFiles().Execute(dto.Attachments);
                news.Attachments = files; 
            }

            await _context.SaveChangesAsync();

            return true;
        }
    }
}
