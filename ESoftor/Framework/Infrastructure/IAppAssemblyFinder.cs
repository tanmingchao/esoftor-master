// -----------------------------------------------------------------------
//  <copyright file="IAppAssemblyFinder.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace ESoftor.Framework.Infrastructure
{
    /// <summary>
    ///     查找应用程序中的程序集对象
    /// </summary>
    public interface IAppAssemblyFinder
    {
        /// <summary>
        ///     查询所有程序集对象
        /// </summary>
        /// <param name="filterAssembly">是否排除非业务程序集对象</param>
        /// <returns></returns>
        Assembly[] FindAllAssembly(bool filterAssembly = true);
        /// <summary>
        ///     获取指定类型的对象集合
        /// </summary>
        /// <typeparam name="ItemType">指定的类型</typeparam>
        /// <param name="expression">
        ///     过滤表达式:
        ///         查询接口(type=>typeof(ItemType).IsAssignableFrom(type));
        ///         查询实体:type => type.IsDeriveClassFrom<ItemType>()
        /// </param>
        /// <param name="fromCache">是否从缓存查询</param>
        /// <returns></returns>
        Type[] FindTypes<ItemType>(Func<Type, bool> expression, bool fromCache = true) where ItemType : class;
    }
}
