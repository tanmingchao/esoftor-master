// -----------------------------------------------------------------------
//  <copyright file="Module.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using System;
using System.Linq;
using ESoftor.Framework.Module;
using IdentityServer4.EntityFramework.DbContexts;
using IdentityServer4.EntityFramework.Mappers;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace ESoftor.EntityFrameworkCore.Authorization
{
    public abstract class AuthorizationBaseModule : ModuleBase
    {
        public override ModuleLevel ModuleLevel => ModuleLevel.DbFactory;
        public override IServiceCollection AddModule(IServiceCollection services)
        {
            // configure identity server with in-memory stores, keys, clients and scopes
            services
                .AddIdentityServer()
                .AddDeveloperSigningCredential()
                .AddTestUsers(Config.GetUsers())
                // this adds the config data from DB (clients, resources)
                .AddConfigurationStore(options =>
                {
                    options.ConfigureDbContext = builder => BuilderSqlExpression(builder);
                })
                // this adds the operational data from DB (codes, tokens, consents)
                .AddOperationalStore(options =>
                {
                    options.ConfigureDbContext = builder => BuilderSqlExpression(builder);

                    // this enables automatic token cleanup. this is optional.
                    options.EnableTokenCleanup = true;
                    options.TokenCleanupInterval = 30;
                });
            return services;
        }

        /// <summary>
        ///     有客户端定义 builder表达式,来确定数据库类型
        ///         eg:builder.UseSqlServer(connectionString, sql => sql.MigrationsAssembly(migrationsAssembly))
        /// </summary>
        /// <param name="builder"></param>
        /// <returns></returns>
        public abstract DbContextOptionsBuilder BuilderSqlExpression(DbContextOptionsBuilder builder);

        public override void UseModule(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                scope.ServiceProvider.GetRequiredService<PersistedGrantDbContext>().Database.Migrate();
                var context = scope.ServiceProvider.GetRequiredService<ConfigurationDbContext>();
                context.Database.Migrate();
                if (!context.Clients.Any())
                {
                    foreach (var client in Config.GetClients())
                    {
                        context.Clients.Add(client.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.IdentityResources.Any())
                {
                    foreach (var resource in Config.GetIdentityResources())
                    {
                        context.IdentityResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }

                if (!context.ApiResources.Any())
                {
                    foreach (var resource in Config.GetApiResources())
                    {
                        context.ApiResources.Add(resource.ToEntity());
                    }
                    context.SaveChanges();
                }
            }
            
        }

    }
}
