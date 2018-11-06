// -----------------------------------------------------------------------
//  <copyright file="IRepository.cs" company="eSoft">
//      Copyright (c) 2014-2017 eSoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>$date$</last-date>
// -----------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace ESoftor.Framework.Infrastructure
{
    public interface IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        #region Query
        TEntity GetByKey(TKey key);
        Task<TEntity> GetByKeyAsync(TKey key);
        IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression);

        #endregion

        #region Insert
        void Insert(TEntity entity);
        void Insert(IEnumerable<TEntity> entities);
        Task InsertAsync(TEntity entity);
        Task InsertAsync(IEnumerable<TEntity> entities);
        #endregion

        #region Update
        void Update(TEntity entity);
        void Update(IEnumerable<TEntity> entities);

        #endregion

        #region Remove
        void Remove(TEntity entity);
        void Remove(Expression<Func<TEntity, bool>> expression);
        
        #endregion

    }
}
