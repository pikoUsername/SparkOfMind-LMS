using LMS.Application.Files.UseCases;

namespace LMS.Application.Files.Interfaces
{
    public interface IFileService
    {
        UploadFile UploadFile();
        UploadFiles UploadFiles();
        DeleteFile DeleteFile();
    }
}
