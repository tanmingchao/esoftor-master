// -----------------------------------------------------------------------
//  <copyright file="ModuleFinder.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Framework.Infrastructure;
using ESoftor.Framework.Module;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESoftor.Framework
{
    /// <summary>
    ///     模块查找器
    /// </summary>
    public class ModuleFinder : IModuleFinder
    {
        public ModuleFinder(
            IAppAssemblyFinder finder,
            IServiceCollection services,
            IServiceProvider provider)
        {
            _finder = finder;
            _services = services;
            _provider = provider;
        }

        private readonly IAppAssemblyFinder _finder;
        private readonly IServiceCollection _services;
        private readonly IServiceProvider _provider;



    }
}
