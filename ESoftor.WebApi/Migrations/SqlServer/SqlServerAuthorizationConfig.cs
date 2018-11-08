// -----------------------------------------------------------------------
//  <copyright file="SqlServerAuthorizationConfig.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.EntityFrameworkCore.Authorization;
using ESoftor.Framework;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Reflection;

namespace ESoftor.WebApi.Migrations.SqlServer
{
    public class SqlServerAuthorizationConfig : AuthorizationBaseModule
    {
        public SqlServerAuthorizationConfig()
        {

        }

        public override DbContextOptionsBuilder BuilderSqlExpression(DbContextOptionsBuilder builder)
        {
            var connString = AppSettingManager.Get("ESoftor:DbContexts:Default:ConnectString");
            builder.UseSqlServer(connString, sql => sql.MigrationsAssembly(typeof(Startup).GetTypeInfo().Assembly.GetName().Name));
            return builder;
        }
    }
}
