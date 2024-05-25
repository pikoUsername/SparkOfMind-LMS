using LMS.Application.Common.UseCases;

namespace LMS.Application.Study.UseCases.CourseGroup
{
    public class ExtendGroup : BaseUseCase<ExtendGroupDto, OutputDTO>
    {
        public ExtendGroup() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
