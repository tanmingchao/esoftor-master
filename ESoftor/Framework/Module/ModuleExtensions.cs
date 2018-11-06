// -----------------------------------------------------------------------
//  <copyright file="ModuleExtensions.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Reflection;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ESoftor.Framework.Module
{
    /// <summary>
    ///     模块扩展方法,主要用于注册Module和使用Module
    /// </summary>
    public static class ModuleExtensions
    {
        private static IEnumerable<ModuleBase> _modules;
        private static AppAssemblyFinder _finder = new AppAssemblyFinder();
        public static IServiceCollection AddESoftor(this IServiceCollection services)
        {
            Type[] modules = _finder.FindTypes<ModuleBase>(type => type.IsDeriveClassFrom<ModuleBase>());
            _modules = modules?.Select(m => (ModuleBase)Activator.CreateInstance(m)).OrderBy(m => m.ModuleLevel);
            foreach (var module in _modules)
                services = module.AddModule(services);

            return services;
        }

        public static void UseESoftor(this IServiceProvider provider)
        {
            if (_modules == null)
            {
                Type[] modules = _finder.FindTypes<ModuleBase>(type => type.IsDeriveClassFrom<ModuleBase>());
                _modules = modules?.Select(m => (ModuleBase)Activator.CreateInstance(m)).OrderBy(m => m.ModuleLevel);
            }
            foreach (var module in _modules)
            {
                module.UseModule(provider);
            }
        }
    }
}
