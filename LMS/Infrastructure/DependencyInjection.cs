﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.AspNetCore.Identity;
using LMS.Infrastructure.Data;
using LMS.Infrastructure.Adapters.FileStorage;
using LMS.Application.Common.Interfaces;
using LMS.Domain.User.Entities;
using LMS.Infrastructure.Adapters.Mailing;
using LMS.Infrastructure.Caching;
using LMS.Infrastructure.EventStore;
using LMS.Infrastructure.Data.Intercepters;
using LMS.Application.Study.Interfaces;
using EFCoreSecondLevelCacheInterceptor;

namespace LMS.Infrastructure
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration["ConnectionString"];
            var filesDirectory = configuration["FilesDirectory"];
            var storageType = configuration["StorageType"];

            Guard.Against.Null(storageType, message: "Storage type is not selected");

            if (Enum.TryParse(storageType, out StorageTypes type))
            {
                if (type == StorageTypes.Local && string.IsNullOrEmpty(filesDirectory))
                {
                    throw new Exception("Files directory is empty, while storage type is local");
                }
            }

            Guard.Against.Null(connectionString, message: "Connection string 'DefaultConnection' not found.");

            services.AddScoped<ISaveChangesInterceptor, AuditableEntityInterceptor>();

            services.AddDbContext<ApplicationDbContext>((sp, options) =>
            {
                options.EnableDetailedErrors(true);
                options.AddInterceptors(sp.GetServices<ISaveChangesInterceptor>());
                options.UseNpgsql(
                    connectionString,
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                );
            });
            services.AddDbContext<EventStoreContext>((sp, options) =>
            {
                options.EnableDetailedErrors(true);
                options.UseNpgsql(
                    connectionString,
                    o => o.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)
                );
            });

            services.AddScoped<IApplicationDbContext>(provider => provider.GetRequiredService<ApplicationDbContext>());
            services.AddScoped<IEventStoreContext>(provider => provider.GetRequiredService<EventStoreContext>());
            services.AddScoped<IEventStore, EventStore.EventStore>();
            services.AddScoped<ApplicationDbContextInitialiser>();
            services.AddScoped<EventStoreContextInitialiser>();
            services.AddScoped<IInstitutionAccessPolicy, InstitutionAccessPolicy>();

            services.AddSingleton(TimeProvider.System);
            //services.AddScoped<IEventSubscriber<BaseEvent>, LoggingHandler<BaseEvent>>(); 
            services.AddScoped<INotificationCache, InMemoryNotificationCache>();

            services.AddScoped<PasswordService, PasswordService>(
                x =>
                {
                    return new PasswordService(passwordHasher: new PasswordHasher<UserEntity>());
                }
            );

            //services.AddEFSecondLevelCache(options =>
            //    options.UseMemoryCacheProvider().ConfigureLogging(true).UseCacheKeyPrefix("EF_")
            //           // Fallback on db if the caching provider fails.
            //           .UseDbCallsIfCachingProviderIsDown(TimeSpan.FromMinutes(1))
            //);
            services.AddMailing(configuration);

            //services.AddScoped<IFileStorageAdapter, S3StorageAdapter>();
            if (type == StorageTypes.Local)
            {
                Guard.Against.Null(filesDirectory, message: "No files directory");

                services.AddScoped<IFileStorageAdapter, LocalFileStorageAdapter>(x => new LocalFileStorageAdapter(filesDirectory));
            }
            else if (type == StorageTypes.Minio)
            {
                var minioConfig = configuration.GetRequiredSection("Minio");

                services.AddScoped<IFileStorageAdapter, MinioStorage>(provider =>
                {
                    var endpoint = minioConfig["Endpoint"];
                    var accessKey = minioConfig["AccessKey"];
                    var secretKey = minioConfig["SecretKey"];
                    var bucketName = minioConfig["BucketName"];

                    if (string.IsNullOrEmpty(endpoint)
                        || string.IsNullOrEmpty(accessKey)
                        || string.IsNullOrEmpty(secretKey)
                        || string.IsNullOrEmpty(bucketName))
                    {
                        throw new ArgumentException("Minio configuration one of the parameters is empty");
                    }

                    var loggerFactory = provider.GetRequiredService<ILoggerFactory>();

                    // Create a logger
                    var logger = loggerFactory.CreateLogger<MinioStorage>();

                    logger.LogInformation("Creating minio storage");

                    return new MinioStorage(
                        endpoint: endpoint,
                        accessKey: accessKey,
                        secretKey: secretKey,
                        bucketName: bucketName);
                });
            }
            services.AddAuthorizationBuilder();

            return services;
        }
    }
}
