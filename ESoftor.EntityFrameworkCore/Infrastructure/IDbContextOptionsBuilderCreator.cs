// -----------------------------------------------------------------------
//  <copyright file="IDbContextOptionsBuilderCreator.cs" company="eSoft">
//      Copyright (c) 2014-2017 eSoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>$date$</last-date>
// -----------------------------------------------------------------------

using ESoftor.Framework.Options;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ESoftor.EntityFrameworkCore.Infrastructure
{
    /// <summary>
    ///     上下文配置项创建器接口，不同数据库 只需要实现该接口，即可
    /// </summary>
    public interface IDbContextOptionsBuilderCreator
    {
        /// <summary>
        ///     数据库类型
        /// </summary>
        DatabaseType DatabaseType { get; }
        /// <summary>
        ///     上下文配置项创建器
        /// </summary>
        /// <param name="connString"></param>
        /// <param name="existingConnection"></param>
        /// <returns></returns>
        DbContextOptionsBuilder Create(string connString, DbConnection existingConnection);
    }
}
