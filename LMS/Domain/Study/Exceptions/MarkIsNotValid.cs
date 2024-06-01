using LMS.Domain.Study.Entities;

namespace LMS.Domain.Study.Exceptions
{
    public class MarkIsNotValid : Exception
    {
        public MarkIsNotValid(string mark, GradeTypeEntity gradeType) 
            : base($"{mark} is not valid, {gradeType} has min, or max, or variants is not equals to mark") { }
    }
}
