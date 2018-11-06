// -----------------------------------------------------------------------
//  <copyright file="Module.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Framework;
using ESoftor.Framework.Infrastructure;
using ESoftor.Framework.Module;
using ESoftor.Framework.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;

namespace ESoftor
{
    public class Module : ModuleBase
    {
        public override IServiceCollection AddModule(IServiceCollection services)
        {
            services.AddSingleton<IAppAssemblyFinder, AppAssemblyFinder>();
            services.AddSingleton<IConfigureOptions<ESoftorOption>, ESoftorOptionSetup>();
            return services;
        }
    }
}
