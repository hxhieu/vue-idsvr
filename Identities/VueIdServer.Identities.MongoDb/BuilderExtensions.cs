using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using VueIdServer.Data.Common;

namespace VueIdServer.Identities.MongoDb
{
    public static class BuilderExtensions
    {
        public static IServiceCollection AddIdentityUsingMongoDb(this IServiceCollection services, Action<DatabaseOptions> configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var options = new DatabaseOptions();
            configuration.Invoke(options);

            services.AddIdentity<ApplicationUser, ApplicationRole>()
                .AddMongoDbStores<ApplicationUser, ApplicationRole, Guid>
                (
                    options.ConnectionString,
                    options.Database
                )
                .AddDefaultTokenProviders();
            return services;
        }
    }
}