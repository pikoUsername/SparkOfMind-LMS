using LMS.Domain.Files.Entities;
using LMS.Domain.User.Enums;
using LMS.Domain.User.Events;
using LMS.Domain.User.Interfaces;
using LMS.Infrastructure;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace LMS.Domain.User.Entities
{
    public class UserEntity : BaseAuditableEntity, IAccessUser
    {
        [Required]
        public string UserName { get; set; } = null!;
        [Required]
        public string HashedPassword { get; set; } = null!;
        [Required]
        [EmailAddress(ErrorMessage = "Email address is not correct")]
        public string Email { get; set; } = null!;
        [Required]
        public ICollection<RoleEntity> Roles { get; set; } = [];
        [Column(name: "TelegramId")]
        private string? _telegramId; // Закрытое поле для хранения значения
        public FileEntity? Image { get; set; }
        public string? Description { get; set; }
        [Required]
        public bool IsOnline { get; set; } = false;
        [JsonIgnore]
        public ICollection<WarningEntity> Warnings { get; set; } = [];
        public bool IsSuperadmin { get; set; } = false;
        public ICollection<PermissionEntity> Permissions { get; set; } = [];
        public ICollection<GroupEntity> Groups { get; set; } = []; 

        [NotMapped]
        public string? TelegramId
        {
            get => _telegramId; // Возвращаем значение из закрытого поля
            set
            {
                if (Blocked)
                {
                    throw new AccessDenied("blocked");
                }
                _telegramId = value; // Устанавливаем значение в закрытое поле

                AddDomainEvent(
                    new UserUpdated(
                        user: this,
                        fieldName: "TelegramId"
                    )
                );
            }
        }
        public bool Blocked { get; set; } = false;

        public static UserEntity Create(
            string UserName,
            string email,
            string password,
            PasswordService passwordService)
        {
            var user = new UserEntity()
            {
                Email = email,
                UserName = UserName,
            };
            var hashedPassword = passwordService.HashPassword(user, password);

            user.HashedPassword = hashedPassword;

            user.AddDomainEvent(new UserCreated(User: user));

            return user;
        }

        public void Block()
        {
            Blocked = true;
        }

        public void UpdatePassword(string oldPassword, string newPassword, PasswordService passwordService)
        {
            var result = passwordService.VerifyHashedPassword(
                this, HashedPassword, oldPassword);
            if (result != PasswordVerificationResult.Success)
            {
                throw new AccessDenied("Wrong password");
            }

            HashedPassword = passwordService.HashPassword(this, newPassword);
        }

        public void UpdatePassword(string newPassword, PasswordService passwordService)
        {
            HashedPassword = passwordService.HashPassword(this, newPassword);
        }

        public void Warn(string reason, UserEntity byUser)
        {
            if (Blocked)
            {
                return;
            }
            var warn = new WarningEntity() { Reason = reason, ByUserId = byUser.Id };

            Warnings.Add(warn);
            if (Warnings.Count > 3)
            {
                Block();
                return;
            }
            AddDomainEvent(new UserWarned(this, reason));
        }

        public void AddRole(RoleEntity role)
        {
            if (Roles.Select(x => x.Role).Any(x => x == role.Role) 
                && Roles.Select(x => x.Name).Any(x => x == role.Name))
            {
                return; 
            }
            Roles.Add(role);

            AddDomainEvent(
                new UserUpdated(
                    user: this,
                    fieldName: "Role"
                )
             );
        }

        // Repeating code! 
        public void AddPermission(PermissionEntity permission)
        {
            Permissions.Add(permission);

            AddDomainEvent(new PermissionAdded("USER", permission)); 
        }

        public void AddPermissionWithCode(string subjectName, string subjectId, params string[] actions)
        {
            foreach (var action in actions)
            {
                AddPermission(PermissionEntity.Create(subjectName, subjectId, action)); 
            }
        }

        public void AddGroup(GroupEntity group)
        {
            Groups.Add(group);

            AddDomainEvent(new GroupUserAdded(group, this));
        }

        public ICollection<IPermissionEntity> GetPermissions()
        {
            // is it cachable? intersting...
            var permissions = new List<IPermissionEntity>();

            permissions.AddRange(Permissions);
            foreach (var group in Groups) {
                permissions.AddRange(group.Permissions); 
            }
            foreach (var role in Roles)
            {
                permissions.AddRange(role.Permissions);
            }

            return permissions;
        }
    }
}
