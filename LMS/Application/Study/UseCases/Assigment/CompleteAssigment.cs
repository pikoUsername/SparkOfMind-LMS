using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Files.Interfaces;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;
using SQLitePCL;

namespace LMS.Application.Study.UseCases.Assigment
{
    public class CompleteAssignment : BaseUseCase<CompleteAssigmentDto, AssignmentEntity>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IAccessPolicy _accessPolicy { get; }
        private IFileService _fileService { get; }

        public CompleteAssignment(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IAccessPolicy accessPolicy,
            IFileService fileService)
        {
            _fileService = fileService; 
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
            _accessPolicy = accessPolicy;
        }

        public async Task<AssignmentEntity> Execute(CompleteAssigmentDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);

            var assigment = await _context.Assigments.FirstOrDefaultAsync(x => x.Id == dto.AssignmentId); 
            var student = await _context.Students.FirstOrDefaultAsync(x => x.InstitutionMember.Id == member.Id);

            Guard.Against.Null(assigment, message: "Assigment does not exists"); 
            Guard.Against.Null(student, message: "You are not a student");

            var group = await _context.CourseGroups.FirstOrDefaultAsync(
                x => x.InstitutionId == dto.AssignmentId && 
                     x.Students.Any(x => x.Id == student.Id));
            Guard.Against.Null(group, message: "Group not found, CRITICAL"); 

            if (dto.Files != null)
            {
                var files = await _fileService.UploadFiles().Execute(dto.Files); 
            }
            var submission = SubmissionEntity.Create(assigment, dto.Text); 

            return;
        }
    }
}
