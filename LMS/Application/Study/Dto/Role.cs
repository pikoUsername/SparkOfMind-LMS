namespace LMS.Application.Study.Dto
{
    public class CreateRoleDto : InputInstitution
    {
        public string Name = null!; 
        public ICollection<string> AccessLevels = null!; 
    }

    public class DeleteRoleDto : InputInstitution {

        public Guid RoleId; 
    }

    public class AddRoleToMemberDto : InputInstitution
    {
        public Guid RoleId;
        public Guid MemberId; 
    }
}
