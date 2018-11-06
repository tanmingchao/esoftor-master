// -----------------------------------------------------------------------
//  <copyright file="ServiceExtensions.cs" company="eSoft">
//      Copyright (c) 2014-2017 eSoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>$date$</last-date>
// -----------------------------------------------------------------------

using ESoftor.Framework.Options;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;

namespace ESoftor.Framework
{
    public static class ServiceExtensions
    {
        /// <summary>
        ///     获取框架配置对象
        /// </summary>
        /// <param name="provider"></param>
        /// <returns></returns>
        public static ESoftorOption ESoftorOption(this IServiceProvider provider)
        {
            var options = provider.GetService<IOptions<ESoftorOption>>()?.Value;
            return options;
        }

        public static ILogger GetLogger(this IServiceProvider provider, Type type)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger(type);
        }

        public static ILogger GetLogger(this IServiceProvider provider, string categoryName)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger(categoryName);
        }

        public static ILogger<T> GetLogger<T>(this IServiceProvider provider)
        {
            ILoggerFactory factory = provider.GetService<ILoggerFactory>();
            return factory.CreateLogger<T>();
        }

    }
}
