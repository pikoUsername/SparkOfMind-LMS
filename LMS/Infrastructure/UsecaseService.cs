using LMS.Application.Common.UseCases;
using System.Reflection;

namespace LMS.Infrastructure
{
    public static class UseCaseServiceRegistration
    {
        //public static void AddUseCasesFromAssembly(this IServiceCollection services, Assembly assembly, params Type[] ignoredTypes)
        //{
        //    var useCaseTypes = assembly.GetTypes()
        //        .Where(type => !type.IsAbstract && !type.IsInterface && IsBaseUseCase(type) && !ignoredTypes.Contains(type))
        //        .ToList();

        //    foreach (var useCaseType in useCaseTypes)
        //    {
        //        var interfaceType = useCaseType.GetInterfaces()
        //            .FirstOrDefault(i => i.IsGenericType && i.GetGenericTypeDefinition() == typeof(BaseUseCase<,>));

        //        if (interfaceType != null)
        //        {
        //            services.AddScoped(interfaceType, useCaseType);
        //        }
        //    }
        //}

        public static void AddUseCasesFromAssembly(this IServiceCollection services, Assembly assembly, params Type[] ignoredTypes)
        {
            using ILoggerFactory factory = LoggerFactory.Create(builder => builder.AddConsole());
            ILogger logger = factory.CreateLogger("UseCaseRegister");

            var useCaseTypes = assembly.GetTypes()
                .Where(type => !type.IsAbstract && !type.IsInterface && ImplementsBaseUseCaseInterface(type) && !ignoredTypes.Contains(type))
                .ToList();

            foreach (var useCaseType in useCaseTypes)
            {
                services.AddScoped(useCaseType);

                logger.LogInformation($"Added use case service: {useCaseType.FullName}");
            }
        }

        private static bool ImplementsBaseUseCaseInterface(Type type)
        {
            var baseUseCaseInterface = typeof(BaseUseCase<,>);
            return type.GetInterfaces()
                       .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == baseUseCaseInterface);
        }
    }
}
