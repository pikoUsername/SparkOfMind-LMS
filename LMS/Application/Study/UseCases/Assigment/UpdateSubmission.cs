using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Files.Interfaces;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Assigment
{
    public class UpdateSubmission : BaseUseCase<UpdateSubmissionByStudentDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IFileService _fileService { get; }

        public UpdateSubmission(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IFileService fileService)
        {
            _fileService = fileService;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<bool> Execute(UpdateSubmissionByStudentDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId); 
            var student = await _context.Students.FirstOrDefaultAsync(x => x.InstitutionMember.Id == member.Id);

            Guard.Against.Null(student, message: "Student does not exists");

            var submission = await _context.Submissions
                .Include(x => x.Attachments)
                .FirstOrDefaultAsync(x => x.Assignment.Id == dto.AssignmentId && x.StudentId == student.Id);

            Guard.Against.Null(submission, message: "Submission not found"); 

            if (dto.Attachments != null)
            {
                var files = await _fileService.UploadFiles().Execute(dto.Attachments);
                submission.Attachments = files;  
            }
            if (dto.Comment != null)
            {
                submission.Comment = dto.Comment; 
            }

            await _context.SaveChangesAsync(); 

            return true;
        }
    }
}
