using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using EventApp.Models;
using EventApp.Repository;
using Microsoft.EntityFrameworkCore;

namespace EventApp.UnitOfWork
{
    public class UnitOfWork<TContext> : IUnitOfWork where TContext : DbContext
    {
        private readonly TContext _context;
        private bool _disposed = false;
        private Dictionary<Type, object> _repositories;

        public TContext DbContext => _context;

        public UnitOfWork(TContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : AbstractEntity
        {
            if (_repositories == null)
            {
                _repositories = new Dictionary<Type, object>();
            }

            var type = typeof(TEntity);
            if (!_repositories.ContainsKey(type))
            {
                _repositories[type] = new Repository<TEntity>(_context);
            }

            return (IRepository<TEntity>)_repositories[type];
        }        

        public int SaveChanges()
        {
            try
            {
                int count = _context.SaveChanges();
                return count;
            }
            catch (Exception e)
            {
                throw e;
            }
        }        

        public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : AbstractEntity
        {
            return _context.Set<TEntity>();
        }

        public DbContext Context()
        {
            return _context;
        }

        public void Dispose()
        {
            Dispose(true);

            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (!_disposed)
            {
                if (disposing)
                {
                    // clear repositories
                    if (_repositories != null)
                    {
                        _repositories.Clear();
                    }

                    // dispose the db context.
                    _context.Dispose();
                }
            }

            _disposed = true;
        }

        public Task<int> SaveChangesAsync()
        {
            try
            {
                var count = _context.SaveChangesAsync();
                return count;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}