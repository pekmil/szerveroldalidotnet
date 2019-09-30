using System;
using System.Threading.Tasks;
using EventApp.Models;
using EventApp.Repository;
using Microsoft.EntityFrameworkCore;

namespace EventApp.UnitOfWork
{
     public interface IUnitOfWork : IDisposable
    {
        IRepository<TEntity> GetRepository<TEntity>() where TEntity : AbstractEntity;

        DbSet<TEntity> GetDbSet<TEntity>() where TEntity : AbstractEntity;

        int SaveChanges();

        Task<int> SaveChangesAsync();

        DbContext Context();
    }
}

