using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using LMS.Domain.User.Enums;
using LMS.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Institution
{
    public class CreateInstitution : BaseUseCase<CreateInstitutionDto, InstitutionEntity>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IAccessPolicy _accessPolicy { get; }

        public CreateInstitution(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<InstitutionEntity> Execute(CreateInstitutionDto dto)
        {
            await _accessPolicy.EnforceRole(UserRoles.Owner);

            var user = await _context.Users
                .Include(x => x.Permissions)
                .FirstOrDefaultAsync(x => x.Id == dto.OwnerId);

            Guard.Against.NotFound(dto.OwnerId, user); 

            var institution = InstitutionEntity.Create(dto.Name, dto.Description, dto.Email, dto.Phone, dto.Address, dto.OwnerId);

            user.Permissions.AddPermissionWithCode(institution, PermissionEnum.all); 

            _context.Institutions.Add(institution); 
            await _context.SaveChangesAsync();

            return institution;
        }
    }
}
