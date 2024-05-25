using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Files.Interfaces;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Assigment
{
    public class UpdateAssignment : BaseUseCase<UpdateAssignmentDto, AssignmentEntity>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IFileService _fileService { get; }

        public UpdateAssignment(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IFileService fileService)
        {
            _fileService = fileService;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<AssignmentEntity> Execute(UpdateAssignmentDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId); 

            var assignment = await _context.Assigments.FirstOrDefaultAsync(x => x.Id == dto.AssignmentId);

            Guard.Against.Null(assignment, message: "Assignment not found"); 

            await _institutionPolicy.EnforcePermission(
                Domain.User.Enums.PermissionEnum.edit, typeof(AssignmentEntity), member, assignment.Id);

            if (dto.StartDate != null)
            {
                assignment.StartDate = (DateTime)dto.StartDate; 
            }
            if (dto.EndDate != null)
            {
                assignment.EndDate = (DateTime)dto.EndDate;
            }
            if (dto.Attachments != null)
            {
                var files = await _fileService.UploadFiles().Execute(dto.Attachments);
                assignment.Attachments = files; 
            }
            if (dto.GradeType != null)
            {
                var grade = await _context.GradeTypes.FirstOrDefaultAsync(x => x.Id == dto.GradeType);

                Guard.Against.Null(grade, message: "Grade type does not exists");
                
                assignment.GradeType = grade; 
            }
            if (!string.IsNullOrEmpty(dto.Description))
            {
                assignment.Description = dto.Description; 
            }

            _context.Assigments.Update(assignment);
            await _context.SaveChangesAsync();

            return assignment;
        }
    }
}
