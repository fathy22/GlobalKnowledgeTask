﻿
using Microsoft.EntityFrameworkCore;
using RepositoryLayer.DbContextLayer;

namespace ServiceLayer.UnitOfWorks
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly TaskDbContext _context;
        private Dictionary<Type, object> _repositories;

        public UnitOfWork(TaskDbContext context)
        {
            _context = context;
            _repositories = new Dictionary<Type, object>();
        }

        public IRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            if (_repositories.ContainsKey(typeof(TEntity)))
            {
                return (IRepository<TEntity>)_repositories[typeof(TEntity)];
            }

            var repository = new Repository<TEntity>(_context);
            _repositories.Add(typeof(TEntity), repository);
            return repository;
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
