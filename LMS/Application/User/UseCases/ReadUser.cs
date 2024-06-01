using Microsoft.EntityFrameworkCore;
using LMS.Domain.User.Enums;
using LMS.Infrastructure.Data.Queries;
using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Domain.User.Entities;
using LMS.Application.User.Dto;

namespace LMS.Application.User.UseCases
{
    public class GetUsersList : BaseUseCase<GetListUserDto, ICollection<UserEntity>>
    {
        private IApplicationDbContext _context;
        private IAccessPolicy _accessPolicy;

        public GetUsersList(IApplicationDbContext dbContext, IAccessPolicy accessPolicy)
        {
            _context = dbContext;
            _accessPolicy = accessPolicy;
        }

        public async Task<ICollection<UserEntity>> Execute(GetListUserDto dto)
        {
            var query = _context.Users
                .IncludeStandard()
                .AsQueryable();
            if (!string.IsNullOrEmpty(dto.Fullname))
            {
                query = query.Where(x => x.Fullname == dto.Fullname);
            }
            if (dto.Role != null)
            {
                query = query.Where(x => x.Roles.Any(x => x.Role == dto.Role));
            }
            var result = await query.ToListAsync();

            return result;
        }
    }

    public class GetUser : BaseUseCase<GetUserDto, UserEntity>
    {
        private IApplicationDbContext _context;

        public GetUser(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<UserEntity> Execute(GetUserDto dto)
        {
            var query = _context.Users
                .IncludeStandard()
                .AsQueryable();

            if (dto.UserId != null)
            {
                query = query.Where(x => x.Id == dto.UserId);
            }
            if (dto.Email != null)
            {
                query = query.Where(x => x.Email == dto.Email);
            }

            var result = await query.FirstOrDefaultAsync();

            Guard.Against.Null(result, message: "User does not exists");

            return result;
        }
    }
}
