// -----------------------------------------------------------------------
//  <copyright file="EntityConfigFinder.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using ESoftor.EntityFrameworkCore.Infrastructure;
using ESoftor.Framework.Infrastructure;
using ESoftor.Reflection;
using System;
using System.Linq;

namespace ESoftor.EntityFrameworkCore
{
    public class EntityConfigFinder : IEntityConfigFinder
    {
        public EntityConfigFinder(IAppAssemblyFinder assemblyFinder)
        {
            _assemblyFinder = assemblyFinder;
        }

        private readonly IAppAssemblyFinder _assemblyFinder;

        public IEntityRegister[] EntityRegisters()
        {
            var baseType = typeof(IEntityRegister);
            var types = _assemblyFinder.FindTypes<IEntityRegister>(type => type.IsDeriveClassFrom(baseType)).ToList();
            var entityRegisters = types?.Select(t => Activator.CreateInstance(t) as IEntityRegister).ToArray();
            return entityRegisters;
        }
    }
}
