using System;
using System.Collections.Generic;
using System.Linq;

namespace AS.Oil.DAL.Interfaces
{
    public interface IRepository<T> where T : class
    {
        IQueryable<T> GetAll();

        T Get(int id);

        IEnumerable<T> Find(Func<T, Boolean> predicate);

        void Insert(T item);

        void Update(T item);

        void Delete(T item);
    }
}
