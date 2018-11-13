// -----------------------------------------------------------------------
//  <copyright file="RoleConfiguration.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Permission.Identity.Entity;
using ESoftor.Data;
using ESoftor.EntityFrameworkCore;
using ESoftor.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ESoftor.Core.Permission.Identity.EntityConfiguration
{
    public class RoleConfiguration : EntityTypeConfigurationBase<Role, Guid>, IEntityRegister
    {
        public override void Configure(EntityTypeBuilder<Role> builder)
        {
            builder.HasIndex(x => x.Name).HasName("RoleNameIndex").IsUnique();
            builder.HasMany<RoleClaim>().WithOne().HasForeignKey(x => x.RoleId).IsRequired();
            builder.HasMany<UserRole>().WithOne().HasForeignKey(x => x.RoleId).IsRequired();
            builder.HasData(new Role() { ID = CombGuid.NewGuid(), Name = "系统管理员", Remark = "系统最高权限管理角色", IsAdmin = true, IsDefault = false, IsLocked = false });
        }
    }
}
