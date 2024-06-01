using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Files.Interfaces;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Files.Entities;
using LMS.Domain.Study.Entities;
using LMS.Domain.User.Enums;
using LMS.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Assigment
{
    public class CompleteAssignment : BaseUseCase<CompleteAssignmentDto, SubmissionEntity>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IFileService _fileService { get; }

        public CompleteAssignment(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IFileService fileService)
        {
            _fileService = fileService; 
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<SubmissionEntity> Execute(CompleteAssignmentDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);

            var assigment = await _context.Assigments.FirstOrDefaultAsync(x => x.Id == dto.AssignmentId); 
            var student = await _context.Students
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.InstitutionMember.Id == member.Id);

            Guard.Against.Null(assigment, message: "Assigment does not exists"); 
            Guard.Against.Null(student, message: "You are not a student");

            var group = await _context.CourseGroups.FirstOrDefaultAsync(
                x => x.InstitutionId == dto.AssignmentId && 
                     x.Students.Any(x => x.Id == student.Id));
            Guard.Against.Null(group, message: "Group not found, CRITICAL");

            ICollection<FileEntity> files = []; 
            if (dto.Files != null)
            {
                files = await _fileService.UploadFiles().Execute(dto.Files); 
            }
            var submission = SubmissionEntity.Create(assigment, dto.Text, student.Id, files);

            student.User.Permissions.AddPermissionWithCode(
                submission, 
                PermissionEnum.read);
            _context.Submissions.Add(submission);
            await _context.SaveChangesAsync(); 

            return submission;
        }
    }
}
