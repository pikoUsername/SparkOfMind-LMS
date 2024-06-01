using LMS.Application.Common.Interfaces;
using LMS.Application.Files;
using LMS.Application.Payment;
using LMS.Application.Staff;
using LMS.Application.User;
using LMS.Infrastructure.EventDispatcher;
using LMS.Infrastructure;
using System.Reflection;
using LMS.Application.Study.EventHandlers;

namespace LMS.Application.Study
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddStudyApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<BookExpiredHandler>();
            services.AddScoped<InstitutionBlockedHandler>();
            services.AddScoped<InvitationCreatedHandler>();
            services.AddScoped<MarkDeletedHandler>();
            services.AddScoped<StudentCreatedHandler>();

            return services;
        }
    }
}
