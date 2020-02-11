using AS.Oil.DAL.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AS.Oil.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly ApplicationDbContext _dbContext;

        private readonly DbSet<T> _dbSet;

        public Repository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;

            _dbSet = dbContext.Set<T>();
        }

        public void Delete(T item)
        {
            if (_dbContext.Entry(item).State == EntityState.Detached)
            {
                _dbSet.Attach(item);
            }

            _dbSet.Remove(item);
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IQueryable<T> GetAll()
        {
            return _dbSet.AsQueryable();
        }

        public void Insert(T item)
        {
            _dbSet.Add(item);
        }

        public void Update(T item)
        {
            if (item == null)
            {
                throw new ArgumentException("Cannot update a null entity.");
            }

            _dbContext.Entry(item).State = EntityState.Modified;
        }
    }
}
