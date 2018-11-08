// -----------------------------------------------------------------------
//  <copyright file="DbContextBase.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.EntityFrameworkCore.Infrastructure;
using ESoftor.Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using System;

namespace ESoftor.EntityFrameworkCore
{
    public class DbContextBase : DbContext, IDbContext
    {
        public DbContextBase(DbContextOptions options, IEntityConfigFinder entityFinder)
            : base(options)
        {
            _entityConfigFinder = entityFinder;
        }

        private readonly IEntityConfigFinder _entityConfigFinder;

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            Console.WriteLine($"注册实体映射");
            var dbContextType = GetType();
            IEntityRegister[] entityRegisters = _entityConfigFinder.EntityRegisters();
            foreach (var entityConfig in entityRegisters)
            {
                entityConfig.RegistTo(modelBuilder);
                Console.WriteLine($"\t注册实体:{entityConfig.EntityType}");
            }
            Console.WriteLine($"\t成功注册实体:{entityRegisters.Length}个\r");
        }
    }
}
