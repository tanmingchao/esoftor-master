// -----------------------------------------------------------------------
//  <copyright file="UserRoleConfiguration.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Permission.Identity.Entity;
using ESoftor.EntityFrameworkCore;
using ESoftor.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ESoftor.Core.Permission.Identity.EntityConfiguration
{
    public class UserRoleConfiguration : EntityTypeConfigurationBase<UserRole, Guid>, IEntityRegister
    {
        public override void Configure(EntityTypeBuilder<UserRole> builder)
        {
            builder.HasIndex(index => new { index.UserId, index.RoleId }).HasName("UserIdRoleIdIndex").IsUnique();
            builder.HasOne<User>().WithMany().HasForeignKey(x => x.UserId);
            builder.HasOne<Role>().WithMany().HasForeignKey(x => x.RoleId);
        }
    }
}
