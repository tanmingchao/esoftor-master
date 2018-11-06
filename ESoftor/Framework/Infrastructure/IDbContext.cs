// -----------------------------------------------------------------------
//  <copyright file="IDbContext.cs" company="com.esoftor">
//      Copyright (c) 2014-2018 ESoftor. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>%time%</last-date>
// -----------------------------------------------------------------------

using System.Threading;
using System.Threading.Tasks;

namespace ESoftor.Framework.Infrastructure
{
    /// <summary>
    ///     上下文接口对象
    /// </summary>
    public interface IDbContext
    {
        int SaveChanges();
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default(CancellationToken));
    }
}
