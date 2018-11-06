// -----------------------------------------------------------------------
//  <copyright file="IEntityRegister.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using Microsoft.EntityFrameworkCore;
using System;

namespace ESoftor.EntityFrameworkCore.Infrastructure
{
    public interface IEntityRegister
    {
        Type EntityType { get; }
        void RegistTo(ModelBuilder builder);
    }
}
