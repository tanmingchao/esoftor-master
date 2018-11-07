// -----------------------------------------------------------------------
//  <copyright file="AppAssemblyFinder.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Extensions;
using ESoftor.Framework.Infrastructure;
using Microsoft.Extensions.DependencyModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace ESoftor.Framework
{
    public class AppAssemblyFinder : IAppAssemblyFinder
    {
        private List<Assembly> _assemblies = new List<Assembly>();

        public Assembly[] FindAllAssembly(bool filterAssembly = true)
        {
            var filter = new string[]{
                "System",
                "Microsoft",
                "netstandard",
                "dotnet",
                "Window",
                "mscorlib",
                "Newtonsoft",
                "Remotion.Linq",
                "Castle",
                //"UnitTest",
                //"ESoftor.WebApi"
            };
            //core中获取依赖对象的方式
            DependencyContext context = DependencyContext.Default;
            if (context != null)
            {
                List<string> names = new List<string>();
                string[] dllNames = context.CompileLibraries.SelectMany(m => m.Assemblies).Distinct().Select(m => m.Replace(".dll", "")).ToArray();
                if (dllNames.Length > 0)
                {
                    names = (from name in dllNames
                             let index = name.LastIndexOf('/') + 1
                             select name.Substring(index))
                          .Distinct()
                          .WhereIf(name => !filter.Any(name.StartsWith), filterAssembly)
                          .ToList();
                }
                else
                {
                    foreach (var library in context.CompileLibraries)
                    {
                        var name = library.Name;
                        if (!names.Contains(name) && !filter.Any(name.StartsWith))
                            names.Add(name);
                    }
                }

                return LoadFromFiles(names);
            }
            //传统方式
            string pathBase = AppDomain.CurrentDomain.BaseDirectory;
            string[] files = Directory.GetFiles(pathBase, "*.dll", SearchOption.TopDirectoryOnly)
                .Concat(Directory.GetFiles(pathBase, ".exe", SearchOption.TopDirectoryOnly))
                .ToArray();
            if (filterAssembly)
            {
                files = files.WhereIf(f => !filter.Any(n => f.StartsWith(n, StringComparison.OrdinalIgnoreCase)), filterAssembly).Distinct().ToArray();
            }
            _assemblies = files.Select(Assembly.LoadFrom).ToList();
            return _assemblies.ToArray();
        }

        /// <summary>
        /// 获取指定类型的对象集合
        /// </summary>
        /// <typeparam name="ItemType">指定的类型</typeparam>
        /// <param name="expression"> 过滤表达式: 查询接口(type=>typeof(ItemType).IsAssignableFrom(type)); 查询实体:type => type.IsDeriveClassFrom<ItemType>()</param>
        /// <param name="fromCache">是否从缓存查询</param>
        /// <returns></returns>
        public Type[] FindTypes<ItemType>(Func<Type, bool> expression, bool fromCache = true) where ItemType : class
        {
            List<Assembly> assemblies;
            if (fromCache) assemblies = _assemblies;
            if (_assemblies == null || _assemblies.Count() == 0)
                _assemblies = assemblies = this.FindAllAssembly().ToList();

            Type[] types = _assemblies.SelectMany(a => a.GetTypes())
                .Where(expression).Distinct().ToArray();

            return types;
        }
        /// <summary>
        ///    从文件加载程序集对象
        /// </summary>
        /// <param name="files">文件(名称集合)</param>
        /// <returns></returns>
        private static Assembly[] LoadFromFiles(List<string> files)
        {
            List<Assembly> assemblies = new List<Assembly>();
            files?.ToList().ForEach(f =>
            {
                AssemblyName name = new AssemblyName(f);
                try { Assembly assembly = Assembly.Load(name); assemblies.Add(assembly); } catch { }
            });
            return assemblies.ToArray();
        }

    }
}
