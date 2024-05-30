using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Files.Interfaces;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using LMS.Domain.User.Enums;

namespace LMS.Application.Study.UseCases.Institution
{
    public class CreateNews : BaseUseCase<CreateNewsDto, InstitutionNewsEntity>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IFileService _fileService; 

        public CreateNews(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IFileService fileService)
        {
            _fileService = fileService;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<InstitutionNewsEntity> Execute(CreateNewsDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            await _institutionPolicy.EnforcePermission(PermissionEnum.write, typeof(InstitutionNewsEntity), member);

            var attachments = await _fileService.UploadFiles().Execute(dto.Attachments); 

            var news = InstitutionNewsEntity.Create(title: dto.Title, "", dto.Text, dto.InstitutionId, attachments);

            _context.InstitutionNews.Add(news);
            await _context.SaveChangesAsync();

            return news;
        }
    }
}
