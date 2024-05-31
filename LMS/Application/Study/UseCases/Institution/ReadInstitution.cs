using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;
using LMS.Application.Study.Interfaces;
using LMS.Domain.Study.Entities;
using LMS.Infrastructure.Data.Extensions;
using LMS.Infrastructure.Data.Queries;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.Study.UseCases.Institution
{
    public class GetInstitution : BaseUseCase<GetInstitutionDto, InstitutionEntity?>
    {
        private IApplicationDbContext _context { get; }

        public GetInstitution(
            IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<InstitutionEntity?> Execute(GetInstitutionDto dto)
        {
            return await _context.Institutions.IncludeStandard().Where(x => x.Id == dto.InstitutionId).FirstOrDefaultAsync();
        }
    }

    public class GetInstitutionsList : BaseUseCase<GetInstitutionsListDto, ICollection<InstitutionEntity>>
    {
        private IApplicationDbContext _context { get; }

        public GetInstitutionsList(
            IApplicationDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<ICollection<InstitutionEntity>> Execute(GetInstitutionsListDto dto)
        {
            var query = _context.Institutions
                .IncludeStandard()
                .AsQueryable(); 
            if (!string.IsNullOrEmpty(dto.Name))
            {
                query = query.Where(x => EF.Functions
                    .ToTsVector(x.Name)
                    .Matches(EF.Functions
                        .ToTsQuery(dto.Name))
                    );
            }
            if (!string.IsNullOrEmpty(dto.Phone))
            {
                query = query.Where(x => x.Phone == dto.Phone); 
            }
            if (!string.IsNullOrEmpty(dto.Address))
            {
                query = query.Where(x => x.Address == dto.Address); 
            }

            query = query.Paginate(dto.Start, dto.Ends); 

            var result = await query.ToListAsync();

            return result;
        }
    }
}
