using LMS.Application.Files.Interfaces;
using LMS.Application.Files.UseCases;

namespace LMS.Application.Files
{
    public class FileService : IFileService
    {
        private readonly IServiceProvider ServiceProvider;

        public FileService(
            IServiceProvider serviceProvider
        )
        {
            ServiceProvider = serviceProvider;
        }

        public UploadFile UploadFile()
        {
            return ServiceProvider.GetRequiredService<UploadFile>();
        }

        public UploadFiles UploadFiles()
        {
            return ServiceProvider.GetRequiredService<UploadFiles>();
        }

        public DeleteFile DeleteFile()
        {
            return ServiceProvider.GetRequiredService<DeleteFile>();
        }
    }
}
