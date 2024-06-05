using LMS.Application.Study.Interfaces;
using LMS.Application.Study.UseCases.Institution;

namespace LMS.Application.Study.Services
{
    public class InstitutionService : IInstitutionService
    {
        private readonly IServiceProvider _serviceProvider;

        public InstitutionService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public AddRoleToMember AddRoleToMember()
        {
            return _serviceProvider.GetRequiredService<AddRoleToMember>();
        }

        public BlockInstitution Block()
        {
            return _serviceProvider.GetRequiredService<BlockInstitution>();
        }

        public CreateInstitution Create()
        {
            return _serviceProvider.GetRequiredService<CreateInstitution>();
        }

        public DeleteInstitutionRole DeleteInstitutionRole()
        {
            return _serviceProvider.GetRequiredService<DeleteInstitutionRole>();
        }

        public DeleteInstitution Delete()
        {
            return _serviceProvider.GetRequiredService<DeleteInstitution>();
        }

        public InviteMember Invite()
        {
            return _serviceProvider.GetRequiredService<InviteMember>();
        }

        public GetInstitution Get()
        {
            return _serviceProvider.GetRequiredService<GetInstitution>();
        }

        public GetInstitutionsList GetList()
        {
            return _serviceProvider.GetRequiredService<GetInstitutionsList>();
        }

        public UpdateInstitution Update()
        {
            return _serviceProvider.GetRequiredService<UpdateInstitution>();
        }
    }

}
