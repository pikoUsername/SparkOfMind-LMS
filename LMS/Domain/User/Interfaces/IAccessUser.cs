namespace LMS.Domain.User.Interfaces
{
    public interface IAccessUser
    {
        Guid Id { get; }
        bool IsSuperadmin { get; }
        public ICollection<IPermissionEntity> GetPermissions();
        public ICollection<IRoleEntity> GetRoles(); 
    }
}
