// -----------------------------------------------------------------------
//  <copyright file="SqlServerDbMigration.cs" company="ESoft">
//      Copyright (c) 2014-2018 ESoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>2018/11/7 15:31:10</last-date>
// -----------------------------------------------------------------------

using ESoftor.EntityFrameworkCore.Migrations;
using System;

namespace ESoftor.WebApi.Migrations.SqlServer
{
    public class SqlServerDbMigration : DbMigrationBase<NovelDbContext>
    {
        public override NovelDbContext CreateDbContext(IServiceProvider serviceProvider)
        {
            Console.WriteLine("开始迁移NovelDbContext上下文");
            return new SqlServerDesignTimeDefaultDbContextFactory(serviceProvider).CreateDbContext(new string[0]);
        }
    }
}
