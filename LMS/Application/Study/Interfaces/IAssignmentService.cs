using LMS.Application.Study.UseCases.Assigment;

namespace LMS.Application.Study.Interfaces
{
    public interface IAssignmentService
    {
        CreateAssignment Create(); 
        DeleteAssignment Delete();
        OpenSubmissionToEdit OpenSubmissionToEdit();
        GetAssignment Get();
        GetAssignmentList GetList();
        SubmitSubmission SubmitSubmission(); 
        UpdateAssignment Update();
        UpdateSubmission UpdateSubmission();
    }
}
