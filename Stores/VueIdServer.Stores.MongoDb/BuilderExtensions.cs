using System;
using IdentityServer4.Models;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using MongoDB.Bson.Serialization;
using VueIdServer.Data.Common;
using VueIdServer.Data.MongoDb;

namespace VueIdServer.Stores.MongoDb
{
    public static class BuilderExtensions
    {
        public static IIdentityServerBuilder AddMongoDbStores(this IIdentityServerBuilder builder)
        {
            builder.Services.AddTransient<IRepository, GenericRepository>();
            builder.Services.AddTransient<IClientStore, ClientStore>();
            builder.Services.AddTransient<IResourceStore, ResourceStore>();
            builder.Services.AddSingleton<IPersistedGrantStore, PersistedGrantStore>();

            return builder;
        }

        public static IApplicationBuilder UseMongoDbStores(this IApplicationBuilder app)
        {
            // Ignore _Id and irrelevant fields
            BsonClassMap.RegisterClassMap<Client>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<IdentityResource>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<ApiResource>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });
            BsonClassMap.RegisterClassMap<PersistedGrant>(cm =>
            {
                cm.AutoMap();
                cm.SetIgnoreExtraElements(true);
            });

            // Seed
            bool createdNewRepository = false;
            var repository = app.ApplicationServices.GetService<IRepository>();

            //  --Client
            if (!repository.CollectionExists<Client>())
            {
                foreach (var client in DefaultConfig.GetClients())
                {
                    repository.Add<Client>(client);
                }
                createdNewRepository = true;
            }

            //  --IdentityResource
            if (!repository.CollectionExists<IdentityResource>())
            {
                foreach (var res in DefaultConfig.GetIdentityResources())
                {
                    repository.Add<IdentityResource>(res);
                }
                createdNewRepository = true;
            }


            //  --ApiResource
            if (!repository.CollectionExists<ApiResource>())
            {
                foreach (var api in DefaultConfig.GetApiResources())
                {
                    repository.Add<ApiResource>(api);
                }
                createdNewRepository = true;
            }

            // If it's a new Repository (database), need to restart the website to configure Mongo to ignore Extra Elements.
            if (createdNewRepository)
            {
                var newRepositoryMsg = $"Mongo Repository created/populated! Please restart you website, so Mongo driver will be configured  to ignore Extra Elements.";
                throw new Exception(newRepositoryMsg);
            }

            return app;
        }
    }
}