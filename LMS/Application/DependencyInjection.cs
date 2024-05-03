using LMS.Application.Common.Interfaces;
using LMS.Application.Files;
using LMS.Application.Payment;
using LMS.Application.Staff;
using LMS.Application.User;
using LMS.Infrastructure;
using LMS.Infrastructure.EventDispatcher;
using System.Reflection;

namespace LMS.Application
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddSingleton<IEventDispatcher, EventDispatcher>();
            services.AddScoped<IAccessPolicy, AccessPolicy>();
            services.AddUserApplicationServices();
            services.AddPaymentApplicationServices();
            services.AddFilesApplicationServices();
            services.AddStaffApplicationServices();

            services.AddUseCasesFromAssembly(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
