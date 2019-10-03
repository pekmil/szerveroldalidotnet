using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EventApp.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;

namespace EventApp.Repository
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : AbstractEntity
    {
        protected readonly DbContext Context;
        protected readonly DbSet<TEntity> DbSet;

        public Repository(DbContext ctx)
        {
            Context = ctx;
            DbSet = ctx.Set<TEntity>();
        }

        public IQueryable<TEntity> GetAll()
        {
            return DbSet.AsNoTracking();
        }

        public Task<TEntity> GetById(int id)
        {
            return DbSet.AsNoTracking().FirstOrDefaultAsync(e => e.Id == id);
        }

        public async Task Create(TEntity entity)
        {
            await DbSet.AddAsync(entity);
        }

        public async Task Update(int id, TEntity entity)
        {
            DbSet.Update(entity);
        }

        public async Task Delete(int id)
        {
            var entity = await GetById(id);
            DbSet.Remove(entity);
        }

        public IQueryable<TEntity> GetAsQueryable(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool disableTracking = true)
        {
            IQueryable<TEntity> query = DbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            if (predicate != null)
            {
                query = query.Where(predicate);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            return query;
        }

        public TEntity GetByIdWithInclude(int id, Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, bool disableTracking = false)
        {
            IQueryable<TEntity> query = DbSet;

            if (disableTracking)
            {
                query = query.AsNoTracking();
            }

            if (include != null)
            {
                query = include(query);
            }

            return query.FirstOrDefault(e => e.Id == id);
        }

        public IQueryable<TEntity> GetWhereAsQueryable(Expression<Func<TEntity, bool>> predicate)
        {
            return DbSet.Where(predicate);
        }

        public void InsertRange(IEnumerable<TEntity> entities)
        {
            DbSet.AddRange(entities);
        }

        public async void UpdateRange(IEnumerable<TEntity> entities)
        {
            foreach(var entity in entities)
            {
                await Update(entity.Id, entity);
            }
        }

        public void DeleteRange(IEnumerable<TEntity> entities)
        {
            DbSet.RemoveRange(entities);
        }

        public bool Exists(Func<TEntity, bool> predicate)
        {
            return DbSet.AsNoTracking().Any(predicate);
        }

        public int DeleteAll()
        {
            var mapping = Context.Model.FindEntityType(typeof(TEntity)).Relational();
            var tableName = mapping.TableName;

            return Context.Database.ExecuteSqlCommand("DELETE FROM " + tableName);
        }

        public int Truncate()
        {
            var mapping = Context.Model.FindEntityType(typeof(TEntity)).Relational();
            var tableName = mapping.TableName;

            return Context.Database.ExecuteSqlCommand("TRUNCATE TABLE " + tableName);
        }
    }
}