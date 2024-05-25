using LMS.Application.Common.UseCases;
using LMS.Domain.Study.Entities;

namespace LMS.Application.Study.UseCases.Assigment
{
    public class UpdateSubmission : BaseUseCase<UpdateSubmissionDto, SubmissionEntity>
    {
        public UpdateSubmission() { }

        public async Task<OutputDTO> Execute(InputDTO dto)
        {
            return;
        }
    }
}
