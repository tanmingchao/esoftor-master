// -----------------------------------------------------------------------
//  <copyright file="Module.cs" company="ESoft">
//      Copyright (c) 2014-2018 ESoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>2018/11/7 10:23:56</last-date>
// -----------------------------------------------------------------------
using ESoftor.EntityFrameworkCore.Infrastructure;
using ESoftor.Framework.Module;
using Microsoft.Extensions.DependencyInjection;

namespace ESoftor.EntityFrameworkCore.SqlServer
{
    public class Module : ModuleBase
    {
        public override ModuleLevel ModuleLevel => ModuleLevel.DbFactory;
        public override IServiceCollection AddModule(IServiceCollection services)
        {
            services.AddSingleton<IDbContextOptionsBuilderCreator, DbContextOptionsBuilderCreator>();
            return services;
        }
    }
}
