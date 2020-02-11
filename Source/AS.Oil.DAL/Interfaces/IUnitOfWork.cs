using System;
using System.Threading.Tasks;

namespace AS.Oil.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IRepository<Entities.Category> Categories { get; }

        IRepository<Entities.Storage> Storages { get; }

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
