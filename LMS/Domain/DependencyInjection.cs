using LMS.Domain.Market.Services;

namespace LMS.Domain
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddDomainServices(this IServiceCollection services)
        {
            services.AddScoped<ProductDomainService>();

            return services;
        }
    }
}
