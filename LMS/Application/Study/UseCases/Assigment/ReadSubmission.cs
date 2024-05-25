using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using LMS.Domain.User.Enums;
using LMS.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Assigment
{
    public class GetSubmissionsList : BaseUseCase<GetSubmissionsListDto, ICollection<SubmissionEntity>>
    {
        private IApplicationDbContext _context;
        private IInstitutionAccessPolicy _institutionPolicy;
        private IAccessPolicy _accessPolicy;

        public GetSubmissionsList(IApplicationDbContext dbContext, IInstitutionAccessPolicy institutionPolicy, IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<ICollection<SubmissionEntity>> Execute(GetSubmissionsListDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            await _institutionPolicy.EnforcePermission(
                Domain.User.Enums.PermissionEnum.read, typeof(SubmissionEntity), member);

            var submissions = await _context.Submissions
                // TODO: will it work???? 
                .Where(x => x.Assignment.AssignedBy.InstitutionId == dto.InstitutionId)
                .Where(x => x.Assignment.Id == dto.AssignmentId)
                .ToListAsync();

            return submissions;
        }
    }

    public class GetSubmission : BaseUseCase<GetSubmissionDto, SubmissionEntity?>
    {
        private IApplicationDbContext _context;
        private IInstitutionAccessPolicy _institutionPolicy;
        private IAccessPolicy _accessPolicy; 

        public GetSubmission(IApplicationDbContext dbContext, IInstitutionAccessPolicy institutionPolicy, IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy; 
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<SubmissionEntity?> Execute(GetSubmissionDto dto)
        {
            var member = await _institutionPolicy.GetMemberByCurrentUser(dto.InstitutionId);
            await _institutionPolicy.EnforcePermission(
                    PermissionEnum.read, typeof(SubmissionEntity), member, dto.SubmissionId); 

            var submission = await _context.Submissions
                .IncludeStandard()
                .FirstOrDefaultAsync(x => x.Id == dto.SubmissionId);                ; 

            return submission;
        }
    }
}
