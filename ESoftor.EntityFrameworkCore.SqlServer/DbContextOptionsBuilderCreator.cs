// -----------------------------------------------------------------------
//  <copyright file="DbContextOptionsBuilderCreator.cs" company="ESoft">
//      Copyright (c) 2014-2018 ESoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>2018/11/7 10:17:08</last-date>
// -----------------------------------------------------------------------
using ESoftor.EntityFrameworkCore.Infrastructure;
using ESoftor.Framework.Options;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace ESoftor.EntityFrameworkCore.SqlServer
{
    /// <summary>
    /// 数据上下文创建器创建器
    /// </summary>
    public class DbContextOptionsBuilderCreator : IDbContextOptionsBuilderCreator
    {
        public DatabaseType DatabaseType => DatabaseType.SqlServer;

        public DbContextOptionsBuilder Create(string connString, DbConnection existingConnection)
        {
            return existingConnection == null
                ? new DbContextOptionsBuilder().UseSqlServer(connString, options => options.UseRowNumberForPaging())
                : new DbContextOptionsBuilder().UseSqlServer(existingConnection, options => options.UseRowNumberForPaging());
        }
    }
}
