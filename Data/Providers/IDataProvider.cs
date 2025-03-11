using BusinessEntities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Infrastructure.DataProviders
{
    public interface IDataProvider<T> where T : IdObject
    {
        IQueryable<T> Query();
        T GetById(Guid id);
        void Save(T entity);
        void Delete(Guid id);
        void Delete(T entity);
        IEnumerable<T> GetAll();
    }
}
