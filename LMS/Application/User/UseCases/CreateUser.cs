using Microsoft.EntityFrameworkCore;
using LMS.Infrastructure;
using LMS.Domain.User.Enums;
using LMS.Application.Common.UseCases;
using LMS.Application.Common.Interfaces;
using LMS.Domain.User.Entities;
using LMS.Application.User.Dto;
using LMS.Application.Common.Exceptions;

namespace LMS.Application.User.UseCases
{
    public class CreateUser : BaseUseCase<CreateUserDto, UserEntity>
    {
        private IApplicationDbContext _context;
        private PasswordService passwordService;

        public CreateUser(
            IApplicationDbContext dbContext,
            PasswordService pswdService)
        {
            passwordService = pswdService;
            _context = dbContext;
        }

        public async Task<UserEntity> Execute(CreateUserDto dto)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Email == dto.EmailAddress);
            if (user != null)
            {
                throw new EntityAlreadyExists(nameof(UserEntity), dto.EmailAddress, "EmailAddress");
            }

            var newUser = UserEntity.Create(
                fullname: dto.UserName,
                email: dto.EmailAddress,
                password: dto.Password,
                passwordService: passwordService
            );

            _context.Users.Add(newUser);
            newUser.AddPermissionWithCode(nameof(UserEntity), newUser.Id.ToString(), "*"); 
            await _context.SaveChangesAsync();

            return newUser;
        }
    }
}
