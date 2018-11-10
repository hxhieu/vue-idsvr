using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.Extensions.DependencyInjection;
using VueIdServer.Data.Common;
using VueIdServer.Data.MongoDb;

namespace VueIdServer.Stores.MongoDb
{
    public static class IdentityServerBuilderExtensions
    {
        public static IIdentityServerBuilder AddMongoDbStores(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IRepository, GenericRepository>();
            builder.Services.AddTransient<IClientStore, ClientStore>();
            builder.Services.AddTransient<IResourceStore, ResourceStore>();
            builder.Services.AddSingleton<IPersistedGrantStore, PersistedGrantStore>();

            return builder;
        }
    }
}