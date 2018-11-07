// -----------------------------------------------------------------------
//  <copyright file="DesignTimeDbContextFactoryBase.cs" company="ESoft">
//      Copyright (c) 2014-2018 ESoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>2018/11/7 10:51:31</last-date>
// -----------------------------------------------------------------------
using ESoftor.Framework;
using ESoftor.Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using System;

namespace ESoftor.EntityFrameworkCore.Migrations
{
    public abstract class DesignTimeDbContextFactoryBase<TDbContext> : IDesignTimeDbContextFactory<TDbContext>
        where TDbContext : DbContext, IDbContext
    {

        public TDbContext CreateDbContext(string[] args)
        {
            Console.WriteLine($"开始创建数据迁移上下文");
            string connectString = DbConnection();
            var optionsBuilder = UseSql(new DbContextOptionsBuilder<TDbContext>(), connectString);
            var entityFinder = new EntityConfigFinder(new AppAssemblyFinder());
            return (TDbContext)Activator.CreateInstance(typeof(TDbContext), optionsBuilder.Options, entityFinder);//这里的参数是由DbContextBase决定的 两个参数
        }

        /// <summary>
        ///     连接字符串
        ///         同样由客户端实现重写,客户端决定了不同数据库的连接字符串的格式
        /// </summary>
        /// <returns></returns>
        public abstract string DbConnection();

        /// <summary>
        ///     重写 optionBuilder,
        ///         比如 builder.UseSqlServer(connection,option=>{});这里的 userSqlServer或者use其他数据,是由客户端决定的,所以抽象出来,由客户端重写
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <param name="connectString"></param>
        /// <returns></returns>
        public abstract DbContextOptionsBuilder UseSql(DbContextOptionsBuilder optionsBuilder, string connectString);
    }
}
