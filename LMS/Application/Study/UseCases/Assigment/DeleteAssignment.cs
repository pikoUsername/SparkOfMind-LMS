using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Assigment
{
    public class DeleteAssignment : BaseUseCase<DeleteAssignmentDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }

        public DeleteAssignment(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy)
        {
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<bool> Execute(DeleteAssignmentDto dto)
        {
            // permission verfication 
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            await _institutionPolicy.EnforcePermission(
                Domain.User.Enums.PermissionEnum.delete, typeof(AssignmentEntity), member, dto.AssignmentId);

            var assignment = await _context.Assigments
                .FirstOrDefaultAsync(x => x.Id == dto.AssignmentId && x.InstitutionId == dto.InstitutionId);
            
            Guard.Against.Null(assignment, message: "Assignment not found"); 

            var submissions = await _context.Submissions.Where(x => x.Assignment.Id == assignment.Id).ToListAsync();

            _context.Submissions.RemoveRange(submissions); 
            _context.Assigments.Remove(assignment);
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
