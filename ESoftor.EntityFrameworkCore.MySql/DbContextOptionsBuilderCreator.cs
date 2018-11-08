// -----------------------------------------------------------------------
//  <copyright file="DbContextOptionsBuilderCreator.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.EntityFrameworkCore.Infrastructure;
using ESoftor.Framework.Options;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Text;

namespace ESoftor.EntityFrameworkCore.MySql
{
    public class DbContextOptionsBuilderCreator : IDbContextOptionsBuilderCreator
    {
        public DatabaseType DatabaseType => DatabaseType.MySql;

        public DbContextOptionsBuilder Create(string connString, DbConnection existingConnection)
        {
            return existingConnection == null
                ? new DbContextOptionsBuilder().UseMySql(connString, options => options.UseRelationalNulls())
                : new DbContextOptionsBuilder().UseMySql(existingConnection);
        }
    }
}
