using LMS.Application.Common.UseCases;
using LMS.Application.Files.Dto;
using LMS.Application.Study.Dto;

namespace LMS.Application.Study.UseCases.Institution
{
    public class DeleteRole : BaseUseCase<DeleteRoleDto, bool>
    {
        public DeleteRole() { }

        public async Task<bool> Execute(DeleteRoleDto dto)
        {
            return;
        }
    }

    public class DeleteRoleFromMember : BaseUseCase<DeleteRoleDto, bool>
    {
        public DeleteRoleFromMember() { }

        public async Task<bool> Execute(DeleteRoleDto dto)
        {
            return;
        }
    }
}
