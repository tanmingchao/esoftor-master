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
        private static List<ModuleBase> _modules;
        private static AppAssemblyFinder _finder = new AppAssemblyFinder();
        public static IServiceCollection AddESoftor(this IServiceCollection services)
        {
            Console.WriteLine($"初始模块注入");
            List<Type> modules = _finder.FindTypes<ModuleBase>(type => type.IsDeriveClassFrom<ModuleBase>()).ToList();
            _modules = modules?.Select(m => (ModuleBase)Activator.CreateInstance(m)).OrderBy(m => m.ModuleLevel).ToList();
            for (int i = 0; i < _modules.Count(); i++)
            {
                services = _modules[i].AddModule(services);
                Console.WriteLine($"\t注册模块 [{modules[i].FullName}]");
            }
            Console.WriteLine($"\t注册模块 [{_modules.Count()}] 个\r");

            return services;
        }

        public static void UseESoftor(this IServiceProvider provider)
        {
            Console.WriteLine($"开始Use模块");
            if (_modules == null)
            {
                List<Type> modules = _finder.FindTypes<ModuleBase>(type => type.IsDeriveClassFrom<ModuleBase>()).ToList();
                _modules = modules?.Select(m => (ModuleBase)Activator.CreateInstance(m)).OrderBy(m => m.ModuleLevel).ToList();
            }
            foreach (var module in _modules)
            {
                module.UseModule(provider);
            }
            Console.WriteLine($"\tUse模块 [{_modules.Count()}] 个");
        }
    }
}
