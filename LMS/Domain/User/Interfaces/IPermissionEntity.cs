using System.ComponentModel.DataAnnotations;

namespace LMS.Domain.User.Interfaces
{
    public interface IPermissionEntity
    {
        string SubjectName { get; set; }
        List<string> SubjectActions { get; set; }
        string SubjectId { get; set; } 

        public string[] Join(); 
    }
}
