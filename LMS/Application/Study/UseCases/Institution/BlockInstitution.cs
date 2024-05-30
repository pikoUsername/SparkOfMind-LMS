using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Institution
{
    public class BlockInstitution : BaseUseCase<BlockInstitutionDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IAccessPolicy _accessPolicy { get; }


        public BlockInstitution(
            IApplicationDbContext dbContext,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
        }

        public async Task<bool> Execute(BlockInstitutionDto dto)
        {
            await _accessPolicy.EnforceRole(Domain.User.Enums.UserRoles.Admin);
            var institution = await _context.Institutions.FirstOrDefaultAsync(x => x.Id == dto.InstitutionId);

            Guard.Against.NotFound(dto.InstitutionId, institution);

            if (institution.Blocked)
            {
                return false; 
            }
            institution.Block(dto.Reason, await _accessPolicy.GetCurrentUser());

            await _context.SaveChangesAsync(); 
            
            return true;
        }
    }
}
