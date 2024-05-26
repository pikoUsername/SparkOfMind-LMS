using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Assigment
{
    public class OpenSubmissionToEdit : BaseUseCase<OpenSubmissionDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }

        public OpenSubmissionToEdit(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy)
        {
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<bool> Execute(OpenSubmissionDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            await _institutionPolicy.EnforcePermission(
                Domain.User.Enums.PermissionEnum.edit, typeof(AssignmentEntity), member);
            var submission = await _context.Submissions
                .Where(x => x.Id == dto.SubmissionId && x.Assignment.InstitutionId == dto.InstitutionId)
                .FirstOrDefaultAsync();

            Guard.Against.Null(submission, message: "Submission not found"); 

            submission.AllowedToEditByStudent = true;

            return true; 
        }
    }
}
