using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Domain.Study.Entities;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Institution
{
    public class CreateRole : BaseUseCase<CreateRoleDto, InstitutionRolesEntity>
    {
        private IApplicationDbContext _context { get; }
        private IAccessPolicy _accessPolicy; 

        public CreateRole(
            IApplicationDbContext dbContext,
            IAccessPolicy accessPolicy)
        {
            _context = dbContext;
            _accessPolicy = accessPolicy;
        }

        public async Task<InstitutionRolesEntity> Execute(CreateRoleDto dto)
        {
            var institution = await _context.Institutions.FirstOrDefaultAsync(x => x.Id == dto.InstitutionId);

            Guard.Against.NotFound(dto.InstitutionId, institution);

            var user = await _accessPolicy.GetCurrentUser(); 
            
            if (institution.OwnerId == user.Id)
            {
                throw new AccessDenied("You are not owner"); 
            }

            var role = InstitutionRolesEntity.Create(dto.InstitutionId, dto.Name);

            user.Permissions.AddPermissionWithCode(role, Domain.User.Enums.PermissionEnum.all); 
            _context.InstitutionRoles.Add(role); 
            await _context.SaveChangesAsync(); 

            return role;
        }
    }
}
