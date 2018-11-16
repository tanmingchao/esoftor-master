// -----------------------------------------------------------------------
//  <copyright file="Repository.cs" company="eSoft">
//      Copyright (c) 2014-2017 eSoft. All rights reserved.
//  </copyright>
//  <last-editor>谭明超</last-editor>
//  <last-date>$date$</last-date>
// -----------------------------------------------------------------------

using ESoftor.Framework.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
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
        #region ctor
        public Repository(IUnitOfWork unitOfWork)
        {
            _dbContext = unitOfWork.GetDbContext as DbContext;
            _dbSet = _dbContext.Set<TEntity>();
        }
        #endregion

        #region fields
        private readonly DbSet<TEntity> _dbSet;
        private readonly DbContext _dbContext;
        #endregion

        #region query
        public TEntity GetByKey(TKey key)
        {
            return _dbSet.Find(key);
        }

        public async Task<TEntity> GetByKeyAsync(TKey key)
        {
            return await _dbSet.FindAsync(key);
        }

        public IQueryable<TEntity> Query(Expression<Func<TEntity, bool>> expression = null)
        {
            return expression != null ? _dbSet.Where(expression) : _dbSet.AsQueryable();
        }

        #endregion

        #region insert
        public void Insert(TEntity entity)
        {
            _dbSet.Add(entity);
        }

        public void Insert(IEnumerable<TEntity> entities)
        {
            _dbSet.AddRange(entities);
        }

        public async Task InsertAsync(TEntity entity)
        {
            await _dbSet.AddAsync(entity);
        }

        public async Task InsertAsync(IEnumerable<TEntity> entities)
        {
            await _dbSet.AddRangeAsync(entities);
        }

        #endregion

        #region update
        public void Remove(TEntity entity)
        {
            _dbSet.Remove(entity);
        }

        public void Remove(Expression<Func<TEntity, bool>> expression)
        {
            var entities = _dbSet.AsNoTracking().Where(expression).ToList();
            _dbSet.RemoveRange(entities);
        }

        #endregion

        #region remove
        public void Update(TEntity entity)
        {
            try
            {
                EntityEntry<TEntity> entry = _dbContext.Entry<TEntity>(entity);
                if (entry.State == EntityState.Detached)
                {
                    _dbSet.Attach(entity);
                    entry.State = EntityState.Modified;
                }
            }
            catch (InvalidOperationException)
            {
                TEntity oldEntity = _dbSet.Find(entity.ID);
                _dbContext.Entry(oldEntity).CurrentValues.SetValues(entity);
            }
            //_dbSet.Update(entity);
        }

        public void Update(IEnumerable<TEntity> entities)
        {
            //_dbSet.UpdateRange(entities);
            foreach (var entity in entities)
            {
                Update(entity);
            }
        }

        #endregion
    }
}
