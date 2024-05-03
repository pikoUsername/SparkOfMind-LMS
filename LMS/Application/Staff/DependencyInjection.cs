using LMS.Application.Staff.Interfaces;
using LMS.Application.Staff.UseCases;

namespace LMS.Application.Staff
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStaffApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IStaffService, StaffService>();

            return services;
        }
    }
}
