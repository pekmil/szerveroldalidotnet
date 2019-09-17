using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Query;

namespace EventApp.Repository
{
    public interface IRepository<TEntity> where TEntity : class
    {       
        IQueryable<TEntity> GetAll();
    
        Task<TEntity> GetById(int id);
    
        Task Create(TEntity entity);
    
        Task Update(int id, TEntity entity);
    
        Task Delete(int id);

        IQueryable<TEntity> GetAsQueryable(
            Expression<Func<TEntity, bool>> predicate = null,
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            bool disableTracking = true);

        TEntity GetByIdWithInclude(
            int id, 
            Func<IQueryable<TEntity>, IIncludableQueryable<TEntity, object>> include = null, 
            bool disableTracking = false);

        IQueryable<TEntity> GetWhereAsQueryable(Expression<Func<TEntity, bool>> predicate);

        void InsertRange(IEnumerable<TEntity> entities);

        void UpdateRange(IEnumerable<TEntity> entities);

        void DeleteRange(IEnumerable<TEntity> entities);

        bool Exists(Func<TEntity, bool> predicate);

        int DeleteAll();

        int Truncate();
    }
}