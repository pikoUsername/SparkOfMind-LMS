using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Institution
{
    public class UpdateInstitution : BaseUseCase<UpdateInstitutionDto, bool>
    {
        private IApplicationDbContext _context { get; }
        private IInstitutionAccessPolicy _institutionPolicy { get; }
        private IAccessPolicy _accessPolicy { get; }

        public UpdateInstitution(
            IApplicationDbContext dbContext,
            IInstitutionAccessPolicy institutionPolicy,
            IAccessPolicy accessPolicy)
        {
            _accessPolicy = accessPolicy;
            _context = dbContext;
            _institutionPolicy = institutionPolicy;
        }

        public async Task<bool> Execute(UpdateInstitutionDto dto)
        {
            if (!await _institutionPolicy.IsOwner(dto.InstitutionId))
            {
                throw new AccessDenied("You are not owner"); 
            }

            var institution = await _context.Institutions.FirstOrDefaultAsync(x => x.Id == dto.InstitutionId);
            
            Guard.Against.NotFound(dto.InstitutionId, institution); 

            if (!string.IsNullOrEmpty(dto.Name))
            {
                institution.Name = dto.Name; 
            } 
            if (!string.IsNullOrEmpty(dto.Address))
            {
                institution.Address = dto.Address;
            }
            if (!string.IsNullOrEmpty(dto.Email))
            {
                institution.ChangeEmail(dto.Email); 
            }
            if (!string.IsNullOrEmpty(dto.Description))
            {
                institution.Description = dto.Description;
            }
            if (!string.IsNullOrEmpty(dto.Phone))
            {
                institution.Phone = dto.Phone;
            }

            await _context.SaveChangesAsync(); 

            return true;
        }
    }
}
