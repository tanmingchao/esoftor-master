// -----------------------------------------------------------------------
//  <copyright file="Module.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.EntityFrameworkCore.Infrastructure;
using ESoftor.Framework.Module;
using Microsoft.Extensions.DependencyInjection;

namespace ESoftor.EntityFrameworkCore
{
    public class Module : ModuleBase
    {
        public override ModuleLevel ModuleLevel => ModuleLevel.Framework;
        public override IServiceCollection AddModule(IServiceCollection services)
        {
            services.AddSingleton<IEntityConfigFinder, EntityConfigFinder>();
            return services;
        }
    }
}
