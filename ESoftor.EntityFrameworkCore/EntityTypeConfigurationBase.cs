// -----------------------------------------------------------------------
//  <copyright file="EntityTypeConfigurationBase.cs" company="ESoft">
//      Copyright (c) 2014-2018 ESoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>2018/11/7 9:05:03</last-date>
// -----------------------------------------------------------------------
using ESoftor.EntityFrameworkCore.Infrastructure;
using ESoftor.Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;

namespace ESoftor.EntityFrameworkCore
{
    public abstract class EntityTypeConfigurationBase<TEntity, TKey> : IEntityTypeConfiguration<TEntity>, IEntityRegister
        where TEntity : class, IEntity<TKey>
    {
        public Type EntityType => typeof(TEntity);

        public abstract void Configure(EntityTypeBuilder<TEntity> builder);

        public void RegistTo(ModelBuilder builder)
        {
            builder.ApplyConfiguration(this);
        }
    }
}
