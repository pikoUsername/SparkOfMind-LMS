using LMS.Application.User.EventHandlers;
using LMS.Application.User.Interfaces;
using LMS.Application.User.UseCases;

namespace LMS.Application.User
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddUserApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();

            services.AddScoped<UserCreatedHandler>();

            return services;
        }
    }
}
