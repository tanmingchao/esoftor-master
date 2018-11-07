// -----------------------------------------------------------------------
//  <copyright file="NovelDbContext.cs" company="ESoft">
//      Copyright (c) 2014-2018 ESoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>2018/11/7 15:15:30</last-date>
// -----------------------------------------------------------------------

using ESoftor.EntityFrameworkCore;
using ESoftor.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace ESoftor.WebApi.Migrations
{
    public class NovelDbContext : DbContextBase
    {
        public NovelDbContext(DbContextOptions options, IEntityConfigFinder entityFinder) : base(options, entityFinder)
        {
        }
    }
}
