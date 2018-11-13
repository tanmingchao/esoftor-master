// -----------------------------------------------------------------------
//  <copyright file="UserEntityConfiguration.cs" company="com.esoftor">
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
    public class UserConfiguration : EntityTypeConfigurationBase<User, Guid>, IEntityRegister
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            builder.HasIndex(x => x.UserName).HasName("UserNameIndex").IsUnique();
            builder.HasIndex(x => x.Email).HasName("EmailIndex");
            builder.HasMany<UserLogin>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
            builder.HasMany<UserClaim>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
            builder.HasMany<UserRole>().WithOne().HasForeignKey(x => x.UserId).IsRequired();
        }
    }
}
