// -----------------------------------------------------------------------
//  <copyright file="SqlServerDesignTimeDefaultDbContextFactory.cs" company="ESoft">
//      Copyright (c) 2014-2018 ESoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>2018/11/7 15:16:59</last-date>
// -----------------------------------------------------------------------

using ESoftor.EntityFrameworkCore.Migrations;
using ESoftor.Framework;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ESoftor.WebApi.Migrations.SqlServer
{
    public class SqlServerDesignTimeDefaultDbContextFactory : DesignTimeDbContextFactoryBase<NovelDbContext>
    {
        public SqlServerDesignTimeDefaultDbContextFactory(IServiceProvider provider)
        {
            _provider = provider;
        }

        private readonly IServiceProvider _provider;

        public override string DbConnection()
        {
            if (_provider == null)
            {
                string connString = AppSettingManager.Get("ESoftor:DbContexts:Default:ConnectString");
                return connString;
            }
            var options = _provider.ESoftorOption();
            return options.ESoftorDbOption.ConnectString;
        }

        public override DbContextOptionsBuilder UseSql(DbContextOptionsBuilder optionsBuilder, string connectString)
        {
            return optionsBuilder.UseSqlServer(connectString, options =>
            {
                options.UseRowNumberForPaging();
                options.MigrationsAssembly("ESoftor.WebApi");
            });
        }
    }
}
