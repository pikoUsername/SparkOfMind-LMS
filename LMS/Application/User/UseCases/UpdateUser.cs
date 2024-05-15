using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Files.Interfaces;
using LMS.Application.User.Dto;
using LMS.Domain.User.Entities;
using LMS.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace LMS.Application.User.UseCases
{
    public class UpdateUser : BaseUseCase<UpdateUserDto, UserEntity>
    {
        private IApplicationDbContext Context;
        private IAccessPolicy AccessPolicy;
        private PasswordService PasswordService;
        private IUser User;
        private IFileService _fileService;

        public UpdateUser(
            IApplicationDbContext context,
            IAccessPolicy accessPolicy,
            PasswordService passwordService,
            IUser user,
            IFileService fileService)
        {
            _fileService = fileService;
            User = user;
            PasswordService = passwordService;
            Context = context;
            AccessPolicy = accessPolicy;
        }

        public async Task<UserEntity> Execute(UpdateUserDto dto)
        {
            var user = await Context.Users.FirstOrDefaultAsync(x => x.Id == dto.UserId);

            Guard.Against.Null(user, message: "User does not exists");

            var byUser = await Context.Users.FirstOrDefaultAsync(x => x.Id == user.Id);

            Guard.Against.Null(byUser, message: "User does not exists");

            if (byUser.Id != user.Id || !await AccessPolicy.IsAllowed("edit", user, byUser))
            {
                throw new AccessDenied("User access denied"); 
            }
            if (dto.Email != null)
            {
                user.Email = dto.Email;
            }
            if (dto.Name != null)
            {
                user.Name = dto.Name;
            }
            if (dto.Surname != null)
            {
                user.Surname = dto.Surname; 
            }
            if (dto.TelegramId != null)
            {
                user.TelegramId = dto.TelegramId;
            }
            if (dto.NewPassword != null && dto.OldPassword != null)
            {
                user.UpdatePassword(
                    oldPassword: dto.OldPassword,
                    newPassword: dto.NewPassword,
                    passwordService: PasswordService);
            }
            if (dto.Role != null)
            {
                if (await AccessPolicy.IsAllowed("edit", user, byUser))
                {
                    var role = await Context.Roles.FirstOrDefaultAsync(x => x.Role == dto.Role);

                    Guard.Against.Null(role, message: "Role does not exists"); 

                    user.AddRole(role); 
                }
                else
                {
                    throw new AccessDenied(null);
                }
            }
            if (dto.Avatar != null)
            {
                user.Image = await _fileService.UploadFile().Execute(dto.Avatar);
            }

            Context.Users.Update(user);
            await Context.SaveChangesAsync();

            return user;
        }
    }
}
