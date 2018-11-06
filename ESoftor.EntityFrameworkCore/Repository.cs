// -----------------------------------------------------------------------
//  <copyright file="Repository.cs" company="eSoft">
//      Copyright (c) 2014-2017 eSoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>$date$</last-date>
// -----------------------------------------------------------------------

using ESoftor.Framework.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace ESoftor.EntityFrameworkCore
{
    public class Repository<TEntity, TKey> : IRepository<TEntity, TKey>
        where TEntity : class, IEntity<TKey>
    {
        #region query
        public TEntity GetByKey(TKey key)
        {
            throw new NotImplementedException();
        }

        public Task<TEntity> GetByKeyAsync(TKey key)
        {
            throw new NotImplementedException();
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region insert
        public void Insert(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task InsertAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region update
        public void Remove(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task RemoveAsync(Expression<Func<TEntity, bool>> expression)
        {
            throw new NotImplementedException();
        }

        #endregion

        #region remove
        public void Update(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(TEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task UpdateAsync(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
