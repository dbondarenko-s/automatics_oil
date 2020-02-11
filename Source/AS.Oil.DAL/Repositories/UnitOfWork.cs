using AS.Oil.DAL.Entities;
using AS.Oil.DAL.Interfaces;
using System;
using System.Threading.Tasks;

namespace AS.Oil.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _dbContext;

        public UnitOfWork(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        private IRepository<Category> _Categories;

        private IRepository<Storage> _Storages;

        public IRepository<Category> Categories => _Categories ?? (_Categories = new Repository<Category>(_dbContext));

        public IRepository<Storage> Storages => _Storages ?? (_Storages = new Repository<Storage>(_dbContext));

        public void SaveChanges()
        {
            _dbContext.SaveChanges();
        }

        public Task SaveChangesAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
