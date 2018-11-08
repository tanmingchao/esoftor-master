// -----------------------------------------------------------------------
//  <copyright file="DbMigrationBase.cs" company="ESoft">
//      Copyright (c) 2014-2018 ESoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>2018/11/7 14:12:06</last-date>
// -----------------------------------------------------------------------
using ESoftor.Framework.Module;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;

namespace ESoftor.EntityFrameworkCore.Migrations
{

    public abstract class DbMigrationBase<TDbContext> : ModuleBase
        where TDbContext : DbContext
    {
        public override ModuleLevel ModuleLevel => ModuleLevel.DbFactory;
        public override void UseModule(IServiceProvider provider)
        {
            using (var scope = provider.CreateScope())
            {
                TDbContext dbContext = CreateDbContext(scope.ServiceProvider);
                var migrations = dbContext.Database.GetPendingMigrations().ToArray();
                if (migrations.Length > 0)
                {
                    dbContext.Database.Migrate();
                    Console.WriteLine($"已挂起 {migrations.Length} 条迁移文件已执行,数据库迁移完成!");
                    //AuthorizationSeedData();
                }
            }
        }

        public abstract TDbContext CreateDbContext(IServiceProvider serviceProvider);
    }
}
