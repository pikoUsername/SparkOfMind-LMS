namespace LMS.Domain.User.Interfaces
{
    public interface IAccessUser
    {
        public ICollection<IPermissionEntity> GetPermissions();  
    }
}
