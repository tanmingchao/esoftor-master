// -----------------------------------------------------------------------
//  <copyright file="DefaultDbContext.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ESoftor.EntityFrameworkCore.Default
{
    public class DefaultDbContext : DbContextBase
    {
        public DefaultDbContext(DbContextOptions options, IEntityConfigFinder entityFinder) : base(options, entityFinder)
        {
        }
    }
}
