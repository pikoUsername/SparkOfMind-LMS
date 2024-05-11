using LMS.Domain.Study.Entities;
using LMS.Domain.Study.Exceptions;
using System.ComponentModel;
using System.Text.Json;

namespace LMS.Domain.Study.Services
{
    public static class AssigmentService 
    {
        public static void VerifyMark(string mark, GradeTypeEntity gradeType)
        {
            if (gradeType.Min != null && gradeType.Max != null)
            {
                if (decimal.TryParse(mark, out var markAsDecimal))
                {
                    if (markAsDecimal > gradeType.Max || markAsDecimal < gradeType.Min) { 
                        throw new MarkIsNotValid(mark, gradeType);
                    }
                } else
                {
                    throw new MarkIsNotValid(mark, gradeType); 
                }
            }
            if (gradeType.Variants == null)
            {
                throw new Exception("Gradetype does not have variants in it"); 
            }
            var variants = JsonSerializer.Deserialize<List<string>>(gradeType.Variants);
            if (variants == null)
                throw new Exception($"CRITICAL BUG, GradeType_{gradeType.Id}.Variants is not possible to parse into List<string>"); 
            if (!variants.Contains(mark))
            {
                throw new MarkIsNotValid(mark, gradeType);
            }
        }
    }
}
