// -----------------------------------------------------------------------
//  <copyright file="IUnitOfWork.cs" company="eSoft">
//      Copyright (c) 2014-2017 eSoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>$date$</last-date>
// -----------------------------------------------------------------------

using System;
using System.Threading.Tasks;

namespace ESoftor.Framework.Infrastructure
{
    /// <summary>
    ///     工作单元接口
    /// </summary>
    public interface IUnitOfWork : IDisposable
    {
        IDbContext GetDbContext { get; }
        IRepository<TEntity, TKey> Repository<TEntity, TKey>() where TEntity : class, IEntity<TKey>;
        void BeginTransaction();
        int Commit();
        Task<int> CommitAsync();
    }
}
