using LMS.Domain.Files.Entities;
using LMS.Infrastructure.EventDispatcher;

namespace LMS.Application.Files.Events
{
    public class FileDeleted(FileEntity file) : BaseEvent
    {
        public FileEntity File { get; set; } = file;
    }
}
