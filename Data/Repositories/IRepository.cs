using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BusinessEntities;

namespace Data.Repositories
{
    public interface IRepository<T> where T : IdObject
    {
        T Get(Guid id);
        List<T> GetByExpression(int skip, int take, Expression<Func<T, bool>> filter);
        void Save(T entity);
        void Delete(Guid id);
        void Delete(T entity);
        IEnumerable<T> GetAll();
    }
}