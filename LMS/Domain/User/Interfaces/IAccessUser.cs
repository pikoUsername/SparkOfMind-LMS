namespace LMS.Domain.User.Interfaces
{
    public interface IAccessUser
    {
        Guid Id { get; }
        public ICollection<IPermissionEntity> GetPermissions();
        public ICollection<IRoleEntity> GetRoles(); 
    }
}
