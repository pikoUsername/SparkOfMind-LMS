using LMS.Application.Study.UseCases.Institution;

namespace LMS.Application.Study.Interfaces
{
    public interface IInstitutionService
    {
        AddRoleToMember AddRoleToMember();
        BlockInstitution Block();
        CreateInstitution Create(); 
        DeleteInstitutionRole DeleteInstitutionRole();
        DeleteInstitution Delete();
        InviteMember Invite();
        GetInstitution Get();
        GetInstitutionsList GetList();
        UpdateInstitution Update(); 
    }
}
