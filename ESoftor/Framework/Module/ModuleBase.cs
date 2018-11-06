// -----------------------------------------------------------------------
//  <copyright file="ModuleBase.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using Microsoft.Extensions.DependencyInjection;
using System;

namespace ESoftor.Framework.Module
{
    /// <summary>
    ///     模块基类
    /// </summary>
    public abstract class ModuleBase
    {
        /// <summary>
        ///     模块排序 默认0,数值越大越 靠后加载
        /// </summary>
        public virtual ModuleLevel ModuleLevel => ModuleLevel.Infrastructure;
        /// <summary>
        ///     添加模块
        /// </summary>
        /// <param name="services">services容器</param>
        /// <returns></returns>
        public virtual IServiceCollection AddModule(IServiceCollection services)
        {
            return services;
        }

        /// <summary>
        ///     应用模块
        /// </summary>
        /// <param name="provider"></param>
        public virtual void UseModule(IServiceProvider provider) { }
    }

    /// <summary>
    ///     模块层级划分
    /// </summary>
    public enum ModuleLevel
    {
        /// <summary>
        ///     0
        /// </summary>
        Infrastructure,
        /// <summary>
        ///     1
        /// </summary>
        Framework,
        /// <summary>
        ///     2
        /// </summary>
        DbFactory,
        /// <summary>
        ///     3
        /// </summary>
        Business,
        /// <summary>
        ///     4
        /// </summary>
        Other
    }

}
