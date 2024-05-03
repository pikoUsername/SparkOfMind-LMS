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

        public static void AddUseCasesFromAssembly(this IServiceCollection services, Assembly assembly, ILoggingBuilder logging, params Type[] ignoredTypes)
        {
            var useCaseTypes = assembly.GetTypes()
                .Where(type => !type.IsAbstract && !type.IsInterface && IsBaseUseCase(type) && !ignoredTypes.Contains(type))
                .ToList();

            foreach (var useCaseType in useCaseTypes)
            {
                services.AddScoped(useCaseType);
            }
        }

        private static bool IsBaseUseCase(Type type)
        {
            return type.BaseType != null && type.BaseType.IsGenericType &&
                   type.BaseType.GetGenericTypeDefinition() == typeof(BaseUseCase<,>);
        }
    }
}
