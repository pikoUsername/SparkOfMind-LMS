using LMS.Application.Study.Interfaces;
using LMS.Application.Study.UseCases.Assigment;

namespace LMS.Application.Study.Services
{
    public class AssignmentService : IAssignmentService
    {
        private readonly IServiceProvider _serviceProvider;

        public AssignmentService(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public CreateAssignment Create()
        {
            return _serviceProvider.GetRequiredService<CreateAssignment>();
        }

        public DeleteAssignment Delete()
        {
            return _serviceProvider.GetRequiredService<DeleteAssignment>();
        }

        public OpenSubmissionToEdit OpenSubmissionToEdit()
        {
            return _serviceProvider.GetRequiredService<OpenSubmissionToEdit>();
        }

        public GetAssignment Get()
        {
            return _serviceProvider.GetRequiredService<GetAssignment>();
        }

        public GetAssignmentList GetList()
        {
            return _serviceProvider.GetRequiredService<GetAssignmentList>();
        }

        public SubmitSubmission SubmitSubmission()
        {
            return _serviceProvider.GetRequiredService<SubmitSubmission>();
        }

        public UpdateAssignment Update()
        {
            return _serviceProvider.GetRequiredService<UpdateAssignment>();
        }

        public UpdateSubmission UpdateSubmission()
        {
            return _serviceProvider.GetRequiredService<UpdateSubmission>();
        }
    }
}
