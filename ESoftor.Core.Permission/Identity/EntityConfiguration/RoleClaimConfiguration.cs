// -----------------------------------------------------------------------
//  <copyright file="RoleClaimConfiguration.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.Core.Permission.Identity.Entity;
using ESoftor.EntityFrameworkCore;
using ESoftor.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;

namespace ESoftor.Core.Permission.Identity.EntityConfiguration
{
    public class RoleClaimConfiguration : EntityTypeConfigurationBase<RoleClaim, Guid>, IEntityRegister
    {
        public override void Configure(EntityTypeBuilder<RoleClaim> builder)
        {

        }
    }
}
