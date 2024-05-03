using LMS.Application.Files.Interfaces;
using LMS.Application.Files.UseCases;

namespace LMS.Application.Files
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddFilesApplicationServices(this IServiceCollection services)
        {

            services.AddScoped<IFileService, FileService>();

            return services;
        }
    }
}
