using LMS.Application.Common.Interfaces;
using LMS.Application.Common.UseCases;
using LMS.Application.Files.Interfaces;
using LMS.Application.User.Dto;
using LMS.Domain.User.Entities;
using LMS.Domain.User.Enums;
using LMS.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System.Data;

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

            if (dto.Email != null)
            {
                user.Email = dto.Email;
            }
            if (dto.Name != null)
            {
                user.UserName = dto.Name;
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
                if (byUser.Role == UserRoles.Owner)
                {
                    user.UpdateRole(
                        (UserRoles)dto.Role);
                }
                else
                {
                    throw new AccessDenied(null);
                }
            }
            if (!string.IsNullOrEmpty(dto.Description))
            {
                user.Description = dto.Description;
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
