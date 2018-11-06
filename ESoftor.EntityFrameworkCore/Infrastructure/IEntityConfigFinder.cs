// -----------------------------------------------------------------------
//  <copyright file="IEntityConfigFinder.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

namespace ESoftor.EntityFrameworkCore.Infrastructure
{
    public interface IEntityConfigFinder
    {
        IEntityRegister[] EntityRegisters();
    }
}
