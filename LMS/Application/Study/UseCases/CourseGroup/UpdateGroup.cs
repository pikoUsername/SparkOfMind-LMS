using LMS.Application.Common.UseCases;
using LMS.Application.Study.Dto;

namespace LMS.Application.Study.UseCases.CourseGroup
{
    public class UpdateGroup : BaseUseCase<UpdateGroupDto, bool>
    {
        public UpdateGroup() { }

        public async Task<bool> Execute(UpdateGroupDto dto)
        {
            return true;
        }
    }
}
