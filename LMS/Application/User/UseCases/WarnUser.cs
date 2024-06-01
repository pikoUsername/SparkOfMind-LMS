using LMS.Application.Common.Exceptions;
using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.User.Dto;
using LMS.Domain.User.Entities;
using LMS.Domain.User.Enums;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.User.UseCases
{
    public class WarnUser : BaseUseCase<WarnUserDto, UserEntity>
    {
        private IApplicationDbContext Context { get; set; }
        private IAccessPolicy AccessPolicy { get; set; }

        public WarnUser(IApplicationDbContext dbContext,
            IAccessPolicy accessPolicy)
        {
            Context = dbContext;
            AccessPolicy = accessPolicy;
        }

        public async Task<UserEntity> Execute(WarnUserDto dto)
        {
            var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);
            if (user == null)
                throw new EntityDoesNotExists(nameof(UserEntity), "");

            user.Warn(dto.Reason, await AccessPolicy.GetCurrentUser());
            await Context.SaveChangesAsync();

            return user;
        }
    }
}
