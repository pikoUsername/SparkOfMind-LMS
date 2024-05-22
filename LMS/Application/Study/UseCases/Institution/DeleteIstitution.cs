using LMS.Application.Common.UseCases;

namespace LMS.Application.Study.UseCases.Institution
{
    public class DeleteIstitution : BaseUseCase<InputDTO, OutputDTO>
    {
        // cannot be deleted directly, it has two steps: 
        // First, owner starts institution deletion proccess, and marks for deletion. 
        // Then, all members of institution will be notified including owner
        // In Second step, 1-2 days task will hang around, only then All institution including 
        // All members will be deleted forever. 
        public DeleteIstitution() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
